using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using ZedGraph;

namespace GraphVelocity2Motor
{
    public partial class GraphVelocity2Motor: Form
    {
        private GraphPane VelLeftPane;
        private GraphPane VelRightPane;
        private LineItem VelLeftSetPointCurve;
        private LineItem VelLeftCurrentCurve;
        private LineItem VelRightSetPointCurve;
        private LineItem VelRightCurrentCurve;
        private double vel_left, set_vel_left, vel_right, set_vel_right;
        private bool isPaused = false;  // Biến kiểm tra có đang tạm dừng không
        private bool isStopped = true; // Biến kiểm tra có đang dừng hẳn không

        private bool isSetValueFinish = false;
        private byte state_run = 0x00;

        private Stopwatch stopwatch = new Stopwatch(); // Biến toàn cục

        private BackgroundWorker bkgdWorker;

        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        private bool isClientConnected = true;
        private bool hasShownDisconnectMessage = false; // Biến toàn cục

        enum CommunicationMode { UART, TCP }
        CommunicationMode commMode = CommunicationMode.UART; // mặc định


        public GraphVelocity2Motor()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing; // Đăng ký sự kiện FormClosing
            GetAvailablePorts();
            SetupGraph();

            //setup timergraph
            TimerGraph.Enabled = false;
            TimerGraph.Tick += new EventHandler(TimerGraph_Tick);
            TimerGraph.Interval = 25;
            TimerGraph.Stop();

            //setup timergraph
            TimerSend.Enabled = false;
            TimerSend.Tick += new EventHandler(TimerSend_Tick);
            TimerSend.Interval = 25;
            TimerSend.Stop();

            LabelAnnouncement.Text = "";
            FindAndAddLocalIPs();
        }

        private void FindAndAddLocalIPs()
        {

            foreach (var ip in GetAllLocalIPAddresses())
            {
                IPComboBox.Items.Add(ip.ToString());
            }

            if (IPComboBox.Items.Count > 0)
            {
                LabelAnnouncement.ForeColor = Color.Green;
                LabelAnnouncement.Text = "Find all IPs";
            }
            else
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "No IP found";
            }

            IPComboBox.SelectedIndex = -1;
        }

        private List<IPAddress> GetAllLocalIPAddresses()
        {
            List<IPAddress> ipList = new List<IPAddress>();

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up &&
                    netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    var props = netInterface.GetIPProperties();
                    foreach (var ip in props.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(ip.Address))
                        {
                            ipList.Add(ip.Address);
                        }
                    }
                }
            }

            return ipList;
        }

        private void OpenPortButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortBox.Text == "" || BaudRateBox.Text == "" || ParityBox.Text == "")
                {
                    LabelAnnouncement.ForeColor = Color.Red;
                    LabelAnnouncement.Text = "Please select Port Settings";
                }
                else
                {
                    LabelAnnouncement.Text = "";
                    serialPort1.PortName = PortBox.Text;
                    serialPort1.BaudRate = Convert.ToInt32(BaudRateBox.Text);
                    serialPort1.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), ParityBox.SelectedItem.ToString());
                    //serialPort1.Encoding = System.Text.Encoding.GetEncoding(28591);
                    serialPort1.NewLine = "\r\n";  // Định nghĩa ký tự kết thúc dòng

                    serialPort1.Open();

                    StatusProgressBar.Value = 100;
                    OpenPortButton.Enabled = false;
                    ClosePortButton.Enabled = true;
                    SetPointGroupBox.Enabled = true;
                    CurrentValueGroupBox.Enabled = true;
                    ControlGroupBox.Enabled = true;
                    TCPGroupBox.Enabled = false;

                    commMode = CommunicationMode.UART;

                    // Khởi tạo BackgroundWorker nếu chưa có
                    if (bkgdWorker == null)
                    {
                        bkgdWorker = new BackgroundWorker();
                        bkgdWorker.WorkerSupportsCancellation = true;
                        bkgdWorker.DoWork += bkgdWorker_DoWork;
                    }

                    VelocityLeftGraph.Enabled = true;
                    VelocityRightGraph.Enabled = true;

                    //TimerGraph.Start(); // Chạy TimerGraph

                    if (!bkgdWorker.IsBusy)
                        bkgdWorker.RunWorkerAsync(); // Chạy BackgroundWorker
                }
            }
            catch (UnauthorizedAccessException)
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Unauthorized Access";
            }
        }

        void GetAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            PortBox.Items.AddRange(ports);
        }

        private void ClosePortButton_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            StatusProgressBar.Value = 0;

            // Reset ComboBox
            PortBox.SelectedIndex = -1;  // Bỏ chọn Port
            BaudRateBox.SelectedIndex = -1;  // Bỏ chọn Baud Rate
            ParityBox.SelectedIndex = -1;  // Bỏ chọn Parity

            LabelAnnouncement.Text = "";

            VelocityLeftGraph.Enabled = false;

            OpenPortButton.Enabled = true;
            ClosePortButton.Enabled = false;
            SetPointGroupBox.Enabled = false;
            CurrentValueGroupBox.Enabled = false;
            ControlGroupBox.Enabled = false;

            TimerGraph.Stop();  // ⛔ Dừng Timer cập nhật đồ thị
            TimerSend.Stop();

            if (bkgdWorker.IsBusy)
            {
                bkgdWorker.CancelAsync();  // ⛔ Yêu cầu dừng BackgroundWorker
            }

            // Xóa dữ liệu cũ
            VelLeftSetPointCurve.Clear();
            VelLeftCurrentCurve.Clear();
            VelocityLeftGraph.Invalidate();
            VelocityLeftGraph.Enabled = false;

            VelRightSetPointCurve.Clear();
            VelRightCurrentCurve.Clear();
            VelocityRightGraph.Invalidate();
            VelocityRightGraph.Enabled = false;
        }

        

        private void SetupGraph()
        {
            // Lấy GraphPane để thiết lập
            VelLeftPane = VelocityLeftGraph.GraphPane;
            VelLeftPane.Title.FontSpec.Size = 14;
            VelLeftPane.XAxis.Title.FontSpec.Size = 14;
            VelLeftPane.YAxis.Title.FontSpec.Size = 14;
            VelLeftPane.Title.Text = "Velocity - Time Graph Motor Left";
            VelLeftPane.XAxis.Title.Text = "Time (s)";
            VelLeftPane.YAxis.Title.Text = "Velocity (Rad/s)";

            //Bật lưới cho cả 2 trục X và Y
            VelLeftPane.XAxis.MajorGrid.IsVisible = true;
            VelLeftPane.YAxis.MajorGrid.IsVisible = true;

            //Điều chỉnh scale cho trục X và Y
            VelLeftPane.XAxis.Scale.Max = 60;
            VelLeftPane.XAxis.Scale.MinorStep = 1;
            VelLeftPane.XAxis.Scale.MajorStep = 5;
            VelLeftPane.YAxis.Scale.MaxAuto = true;
            VelLeftPane.YAxis.Scale.MinAuto = true;

            //Tạo list dữ liệu cho đồ thị
            RollingPointPairList VelLeftSetPointList = new RollingPointPairList(60000);
            RollingPointPairList VelLeftCurrentList = new RollingPointPairList(60000);

            // Khởi tạo danh sách dữ liệu
            VelLeftSetPointCurve = VelLeftPane.AddCurve("Velocity Set Point", VelLeftSetPointList, Color.Blue, SymbolType.None);
            VelLeftCurrentCurve = VelLeftPane.AddCurve("Current Velocity", VelLeftCurrentList, Color.Red, SymbolType.None);
            VelLeftSetPointCurve.Line.Width = 2.0f; // Độ dày 2.0
            VelLeftCurrentCurve.Line.Width = 2.0f; // Độ dày 2.0

            // Cập nhật trục đồ thị vận tốc
            VelocityLeftGraph.AxisChange();

            // Lấy GraphPane để thiết lập
            VelRightPane = VelocityRightGraph.GraphPane;
            VelRightPane.Title.FontSpec.Size = 14;
            VelRightPane.XAxis.Title.FontSpec.Size = 14;
            VelRightPane.YAxis.Title.FontSpec.Size = 14;
            VelRightPane.Title.Text = "Velocity - Time Graph Motor Right";
            VelRightPane.XAxis.Title.Text = "Time (s)";
            VelRightPane.YAxis.Title.Text = "Velocity (Rad/s)";

            //Bật lưới cho cả 2 trục X và Y
            VelRightPane.XAxis.MajorGrid.IsVisible = true;
            VelRightPane.YAxis.MajorGrid.IsVisible = true;

            //Điều chỉnh scale cho trục X và Y
            VelRightPane.XAxis.Scale.Max = 60;
            VelRightPane.XAxis.Scale.MinorStep = 1;
            VelRightPane.XAxis.Scale.MajorStep = 5;
            VelRightPane.YAxis.Scale.MaxAuto = true;
            VelRightPane.YAxis.Scale.MinAuto = true;

            //Tạo list dữ liệu cho đồ thị
            RollingPointPairList VelRightSetPointList = new RollingPointPairList(60000);
            RollingPointPairList VelRightCurrentList = new RollingPointPairList(60000);

            // Khởi tạo danh sách dữ liệu
            VelRightSetPointCurve = VelRightPane.AddCurve("Velocity Set Point", VelRightSetPointList, Color.Blue, SymbolType.None);
            VelRightCurrentCurve = VelRightPane.AddCurve("Current Velocity", VelRightCurrentList, Color.Red, SymbolType.None);
            VelRightSetPointCurve.Line.Width = 2.0f; // Độ dày 2.0
            VelRightCurrentCurve.Line.Width = 2.0f; // Độ dày 2.0

            // Cập nhật trục đồ thị vận tốc
            VelocityRightGraph.AxisChange();

            // Bắt đầu đếm thời gian khi khởi tạo đồ thị
            stopwatch.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if ((isStopped == true) && (isSetValueFinish == true)) // Nếu đã dừng hẳn trước đó
            {
                isStopped = false; // Reset lại trạng thái
                isPaused = false;

                // Xóa dữ liệu cũ
                VelLeftSetPointCurve.Clear();
                VelLeftCurrentCurve.Clear();
                VelocityLeftGraph.Invalidate();

                VelRightSetPointCurve.Clear();
                VelRightCurrentCurve.Clear();
                VelocityRightGraph.Invalidate();

                //serialPort1.Write("r");
                state_run = 0x02;

                TimerGraph.Start(); // Chạy TimerGraph
                if (isClientConnected || commMode == CommunicationMode.UART) //Chỉ bật TimerSend nếu kết nối hợp lệ
                    TimerSend.Start();

                if (!bkgdWorker.IsBusy)
                    bkgdWorker.RunWorkerAsync(); // Chạy BackgroundWorker
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            isStopped = true;   // Đánh dấu đã dừng
            isPaused = false;    // Kích hoạt trạng thái Pause để ngăn vẽ đồ thị

            //serialPort1.Write("s");
            state_run = 0x01;

            TimerGraph.Stop();  // ⛔ Dừng Timer cập nhật đồ thị
            //TimerSend.Stop();

            if (bkgdWorker.IsBusy)
            {
                bkgdWorker.CancelAsync();  // ⛔ Yêu cầu dừng BackgroundWorker
            }

            PauseButton.Text = "Pause"; // Reset lại nút Pause
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (!isStopped) // Chỉ Pause nếu chương trình đang chạy
            {
                isPaused = !isPaused; // Đảo trạng thái Pause

                if (isPaused)
                {
                    PauseButton.Text = "Resume"; // Nếu đang Pause thì đổi thành Resume
                }
                else
                {
                    PauseButton.Text = "Pause"; // Nếu Resume thì đổi lại thành Pause
                }
            }
        }

        private void ExportCSVButton_Click(object sender, EventArgs e)
        {
            if (!isPaused && !isStopped)
            {
                MessageBox.Show("Please Pause or Stop before exporting data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveFileDialog.Title = "Save CSV File";
                    saveFileDialog.FileName = "VelocityData.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToCSV(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu TextBox không rỗng
            if (!string.IsNullOrWhiteSpace(TextboxSPVelLeft.Text) && !string.IsNullOrWhiteSpace(TextboxSPVelRight.Text))
            {
                // Parse và gán vào biến toàn cục
                if (double.TryParse(TextboxSPVelLeft.Text, out set_vel_left) &&
                    double.TryParse(TextboxSPVelRight.Text, out set_vel_right))
                {
                        isSetValueFinish = true;
                }
                else
                {
                    MessageBox.Show("Invalid speed values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {

        }

        private void UpdateGraph()
        {
            // Không cập nhật nếu đang Pause hoặc Stop
            if (isPaused || isStopped) return;

            //Nếu một trong các đường cong không hợp lệ (null) => return
            if (VelLeftSetPointCurve == null || VelLeftCurrentCurve == null) return;
            if (VelRightSetPointCurve == null || VelRightCurrentCurve == null) return;

            //Mỗi đường cong chứa một danh sách các điểm(Points). Ép kiểu Points thành IPointListEdit, cho phép chỉnh sửa danh sách điểm.
            IPointListEdit VelLeftSetPointList = VelLeftSetPointCurve.Points as IPointListEdit;
            IPointListEdit VelLeftCurrentList = VelLeftCurrentCurve.Points as IPointListEdit;

            //Nếu một trong các danh sách điểm không hợp lệ(null) => return.
            if (VelLeftSetPointList == null || VelLeftCurrentList == null) return;

            double timeLeft = stopwatch.ElapsedMilliseconds / 1000.0; // Tính thời gian trôi qua (giây)

            VelLeftSetPointList.Add(timeLeft, set_vel_left);
            VelLeftCurrentList.Add(timeLeft, vel_left);

            Scale VelLeftScale = VelocityLeftGraph.GraphPane.XAxis.Scale;

            if (timeLeft > VelLeftScale.Max - VelLeftScale.MajorStep)
            {
                VelLeftScale.Max = timeLeft + VelLeftScale.MajorStep;
                VelLeftScale.Min = VelLeftScale.Max - 30.0;
            }

            //Mỗi đường cong chứa một danh sách các điểm(Points). Ép kiểu Points thành IPointListEdit, cho phép chỉnh sửa danh sách điểm.
            IPointListEdit VelRightSetPointList = VelRightSetPointCurve.Points as IPointListEdit;
            IPointListEdit VelRightCurrentList = VelRightCurrentCurve.Points as IPointListEdit;

            //Nếu một trong các danh sách điểm không hợp lệ(null) => return.
            if (VelRightSetPointList == null || VelRightCurrentList == null) return;

            double timeRight = stopwatch.ElapsedMilliseconds / 1000.0; // Tính thời gian trôi qua (giây)

            VelRightSetPointList.Add(timeRight, set_vel_right);
            VelRightCurrentList.Add(timeRight, vel_right);

            Scale VelRightScale = VelocityRightGraph.GraphPane.XAxis.Scale;

            if (timeRight > VelRightScale.Max - VelRightScale.MajorStep)
            {
                VelRightScale.Max = timeRight + VelRightScale.MajorStep;
                VelRightScale.Min = VelRightScale.Max - 30.0;
            }

            // Cập nhật đồ thị
            VelocityLeftGraph.AxisChange();
            VelocityLeftGraph.Invalidate();

            VelocityRightGraph.AxisChange();
            VelocityRightGraph.Invalidate();
        }

        public static ushort CRC16_Check_A(byte[] data, int lenght)
        {
            ushort crc = 0xFFFF;

            for (int x = 0; x < lenght; ++x)
            {
                crc ^= data[x];

                for (int i = 8; i != 0; --i)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return crc;
        }

        List<byte> receiveBuffer = new List<byte>();
        bool isReceivingFrame = false;

        private void LogToFile(string message)
        {
            // Lấy đường dẫn thư mục hiện tại
            string currentDirectory = Directory.GetCurrentDirectory();

            // Tạo đường dẫn file log trong thư mục hiện tại
            string filePath = Path.Combine(currentDirectory, "serialLog.txt");

            try
            {
                // Ghi dữ liệu vào file
                File.AppendAllText(filePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.Message);
            }
        }

        private List<byte> listRx = new List<byte>();

        private void bkgdWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            while (!bw.CancellationPending)
            {
                try
                {
                    if (commMode == CommunicationMode.UART)
                    {
                        int bytesToRead = serialPort1.BytesToRead;
                        if (bytesToRead > 0)
                        {
                            byte[] buffer = new byte[bytesToRead];
                            serialPort1.Read(buffer, 0, bytesToRead);
                            listRx.AddRange(buffer);

                            ProcessFramesFromList(listRx);
                        }
                    }
                    else if (commMode == CommunicationMode.TCP)
                    {
                        if (stream != null && stream.DataAvailable)
                        {
                            byte[] buffer = new byte[1024]; // Tùy vào lượng dữ liệu dự kiến
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead > 0)
                            {
                                listRx.AddRange(buffer.Take(bytesRead));
                                ProcessFramesFromList(listRx);
                            }
                        }
                    }

                    Thread.Sleep(5); // nghỉ một chút để không chiếm CPU

                }
                catch (Exception ex)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Lỗi đọc UART: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            }
        }

        private void ProcessFramesFromList(List<byte> list)
        {
            const int FRAME_LENGTH = 17;
            // Tìm và xử lý frame hợp lệ
            while (listRx.Count >= FRAME_LENGTH)
            {
                // Tìm vị trí bắt đầu hợp lệ
                int startIndex = listRx.FindIndex(b => b == 0xAA);

                if (startIndex == -1)
                {
                    listRx.Clear(); // không có start byte, bỏ toàn bộ
                    break;
                }
                if (listRx.Count - startIndex < FRAME_LENGTH)
                    break; // chưa đủ 13 bytes kể từ start

                // Có thể lấy frame
                byte[] frame = listRx.Skip(startIndex).Take(FRAME_LENGTH).ToArray();

                if (frame[FRAME_LENGTH - 1] == 0x55)
                {
                    // Kiểm tra CRC
                    ushort crcReceived = (ushort)((frame[15] << 8) | frame[14]);
                    ushort crcCalculated = CRC16_Check_A(frame.Skip(1).Take(13).ToArray(), 13);

                    if (crcReceived == crcCalculated && frame[1] == 0x03)
                    {
                        // CRC đúng, truyền vào SetText
                        this.BeginInvoke(new Action<byte[]>(SetText), frame);

                        // Hiển thị hex lên LabelAnnouncement
                        string hexFrame = BitConverter.ToString(frame).Replace("-", " ");
                        this.BeginInvoke(new Action(() =>
                        {
                            LabelAnnouncement.ForeColor = Color.Black;
                            LabelAnnouncement.Text = "Valid Frame: " + hexFrame;
                            SetText(frame);
                        }));
                    }
                    else
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            LabelAnnouncement.ForeColor = Color.Red;
                            LabelAnnouncement.Text = "CRC error - erase frame";
                        }));
                    }

                    listRx.RemoveRange(0, startIndex + 13); // bỏ hết phần đã xử lý
                }
                else
                {
                    // End byte không đúng, bỏ byte đầu
                    listRx.RemoveAt(0);
                }
            }
        }


        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void SetText(byte[] data)
        {
            try
            {
                if (data == null || data.Length != 17 || data[0] != 0xAA || data[16] != 0x55)
                    return;

                byte id = data[1];
                float speed1 = BitConverter.ToSingle(data, 2);
                float speed2 = BitConverter.ToSingle(data, 6);

                TextboxCurVelLeft.Text = speed1.ToString("F2", CultureInfo.InvariantCulture);
                TextboxCurVelRight.Text = speed2.ToString("F2", CultureInfo.InvariantCulture);
                // Nếu có textbox cho động cơ 2:
                // TextboxCurVel2.Text = speed2.ToString("F2", CultureInfo.InvariantCulture);

                // Có thể gán biến nếu cần:
                vel_left = speed1;
                vel_right = speed2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xử lý dữ liệu nhị phân: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextboxSPPos_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextboxSPVel_TextChanged(object sender, EventArgs e)
        {

        }

        private void GraphVelocity2Motor_Load(object sender, EventArgs e)
        {

        }

        private void StartConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IPComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid IP address.");
                    return;
                }

                IPAddress selectedIP = IPAddress.Parse(IPComboBox.SelectedItem.ToString());
                int port = int.Parse(PortTextBox.Text.Trim());

                server = new TcpListener(selectedIP, port);
                server.Start();

                LabelAnnouncement.ForeColor = Color.Green;
                LabelAnnouncement.Text = $"Server is working at {selectedIP}:{port}";

                SetPointGroupBox.Enabled = true;
                CurrentValueGroupBox.Enabled = true;
                ControlGroupBox.Enabled = true;
                SerialGroupBox.Enabled = false;

                VelocityLeftGraph.Enabled = true;
                VelocityRightGraph.Enabled = true;

                commMode = CommunicationMode.TCP;

                // Gọi hàm lắng nghe client dưới dạng async để không block giao diện
                _ = ListenForClientAsync();

                if (bkgdWorker == null)
                {
                    bkgdWorker = new BackgroundWorker();
                    bkgdWorker.WorkerSupportsCancellation = true;
                    bkgdWorker.DoWork += bkgdWorker_DoWork;
                }

                if (!bkgdWorker.IsBusy)
                    bkgdWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Error start server: " + ex.Message;
            }
        }

        private async Task ListenForClientAsync()
        {
            while (true)
            {
                try
                {
                    client = await server.AcceptTcpClientAsync();
                    stream = client.GetStream();

                    Invoke(new Action(() =>
                    {
                        LabelAnnouncement.ForeColor = Color.Green;
                        LabelAnnouncement.Text = "✅ Connected Client";
                        isClientConnected = true;
                        hasShownDisconnectMessage = false;
                    }));

                    // Tạm ngưng lắng nghe client mới cho đến khi client hiện tại disconnect
                    // Đợi client disconnect:
                    await WaitForClientDisconnectAsync();
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        LabelAnnouncement.ForeColor = Color.Red;
                        LabelAnnouncement.Text = "❌ Error connect client: " + ex.Message;
                        isClientConnected = false;
                    }));
                }
            }
        }

        private async Task WaitForClientDisconnectAsync()
        {
            try
            {
                // Đọc dữ liệu để phát hiện disconnect
                byte[] buffer = new byte[1];
                while (true)
                {
                    int read = await stream.ReadAsync(buffer, 0, 1);
                    if (read == 0) // Client đã disconnect
                        break;
                }
            }
            catch
            {
                // Nếu có lỗi đọc thì client cũng đã disconnect
            }

            Invoke(new Action(() =>
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Client disconnected";
                isClientConnected = false;

                if (!hasShownDisconnectMessage)
                {
                    hasShownDisconnectMessage = true;
                    MessageBox.Show("Client đã ngắt kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }));
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Ghi tiêu đề cho cột dữ liệu
                    writer.WriteLine("Time_Left, Velocity Left Set Point, Current Velocity Left, Time_Right, Velocity Right Set Point, Current Velocity Right");

                    // Lấy số lượng nhỏ nhất trong 4 danh sách để tránh lỗi truy cập vượt chỉ số
                    int count = Math.Min(
                        Math.Min(VelLeftSetPointCurve.Points.Count, VelLeftCurrentCurve.Points.Count),
                        Math.Min(VelRightSetPointCurve.Points.Count, VelRightCurrentCurve.Points.Count)
                    );

                    // Duyệt qua từng điểm dữ liệu và ghi vào file
                    for (int i = 0; i < VelLeftSetPointCurve.Points.Count; i++)
                    {
                        double timeLeft = VelLeftSetPointCurve.Points[i].X;
                        double setVelLeft = VelLeftSetPointCurve.Points[i].Y;
                        double currentVelLeft = VelLeftCurrentCurve.Points[i].Y;

                        double timeRight = VelRightSetPointCurve.Points[i].X;
                        double setVelRight = VelRightSetPointCurve.Points[i].Y;
                        double currentVelRight = VelRightCurrentCurve.Points[i].Y;

                        writer.WriteLine($"{timeLeft}, {setVelLeft}, {currentVelLeft}, {timeRight}, {setVelRight}, {currentVelRight}");
                    }
                }

                MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ControlGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x02;
            set_vel_left = -100;
            set_vel_right = 100;
        }

        private void UpButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void LeftButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x02;
            set_vel_left = 50;
            set_vel_right = 50;
        }

        private void LeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void DownButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x02;
            set_vel_left = 100;
            set_vel_right = -100;
        }

        private void DownButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void RightButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x02;
            set_vel_left = -50;
            set_vel_right = -50;
        }

        private void RightButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void AutoModeButton_Click(object sender, EventArgs e)
        {
            state_run = 0x04;
        }

        private void TimerSend_Tick(object sender, EventArgs e)
        {
            if (!isSetValueFinish)
                return;

            // Tạo frame 13 byte: [STX][ID][SPEED1][SPEED2][CRC][ETX]
            byte[] frame = new byte[13];
            frame[0] = 0xAA; // STX
            frame[1] = state_run; // ID

            // Convert float to 4 byte IEEE754
            byte[] speed1Bytes = BitConverter.GetBytes((float)set_vel_left);
            byte[] speed2Bytes = BitConverter.GetBytes((float)set_vel_right);

            Array.Copy(speed1Bytes, 0, frame, 2, 4); // SPEED1
            Array.Copy(speed2Bytes, 0, frame, 6, 4); // SPEED2

            // CRC16 từ byte [1] đến [9]
            byte[] crcInput = frame.Skip(1).Take(9).ToArray();
            ushort crc = CRC16_Check_A(crcInput, crcInput.Length);
            frame[10] = (byte)(crc & 0xFF);       // CRC low
            frame[11] = (byte)((crc >> 8) & 0xFF); // CRC high
            frame[12] = 0x55; // ETX

            // Gửi dữ liệu theo chế độ hiện tại
            if (commMode == CommunicationMode.UART)
            {
                if (serialPort1 != null && serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.Write(frame, 0, frame.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("UART send error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
            else if (commMode == CommunicationMode.TCP)
            {
                if (isClientConnected && stream != null && stream.CanWrite)
                {
                    try
                    {
                        stream.Write(frame, 0, frame.Length);
                        hasShownDisconnectMessage = false; // reset nếu gửi thành công
                    }
                    catch (Exception ex)
                    {
                        isClientConnected = false;

                        if (!hasShownDisconnectMessage)
                        {
                            hasShownDisconnectMessage = true;
                            MessageBox.Show("Client disconnected: " + ex.Message, "TCP Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void TimerGraph_Tick(object sender, EventArgs e)
        {
            if ((commMode == CommunicationMode.UART && serialPort1.IsOpen) ||
                (commMode == CommunicationMode.TCP && stream != null && stream.CanRead))
            {
                UpdateGraph();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TimerGraph.Stop();  //Dừng Timer cập nhật đồ thị
                TimerSend.Stop();

                if (bkgdWorker != null && bkgdWorker.IsBusy)
                {
                    bkgdWorker.CancelAsync();  //Yêu cầu dừng BackgroundWorker
                    Thread.Sleep(50); // đợi 1 chút để đảm bảo worker thoát
                }

                if (commMode == CommunicationMode.UART)
                {
                    if (serialPort1 != null && serialPort1.IsOpen)
                        serialPort1.Close();
                }
                else if (commMode == CommunicationMode.TCP)
                {
                    if (stream != null) stream.Close();
                    if (client != null) client.Close();
                    if (server != null) server.Stop();
                }

                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
