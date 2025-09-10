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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GraphVelocity2MotorTCPClient
{
    public partial class GraphVelocity2MotorTCPClient : Form
    {
        private GraphPane VelLeftPane;
        private GraphPane VelRightPane;
        private LineItem VelLeftSetPointCurve;
        private LineItem VelLeftCurrentCurve;
        private LineItem VelRightSetPointCurve;
        private LineItem VelRightCurrentCurve;
        private double vel_left, set_vel_left, vel_right, set_vel_right;
        private bool isPaused = false;
        private bool isStopped = true;

        private bool isSetValueFinish = false;
        private byte state_run = 0x00;

        private Stopwatch stopwatch = new Stopwatch();

        private BackgroundWorker bkgdWorker;

        // Đã bỏ TcpListener server;
        TcpClient client;
        NetworkStream stream;
        private bool isClientConnected = true;
        private bool hasShownDisconnectMessage = false;

        enum CommunicationMode { UART, TCP }
        CommunicationMode commMode = CommunicationMode.UART;

        public GraphVelocity2MotorTCPClient()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing;
            GetAvailablePorts();
            SetupGraph();

            TimerGraph.Enabled = false;
            TimerGraph.Tick += new EventHandler(TimerGraph_Tick);
            TimerGraph.Interval = 25;
            TimerGraph.Stop();

            TimerSend.Enabled = false;
            TimerSend.Tick += new EventHandler(TimerSend_Tick);
            TimerSend.Interval = 25;
            TimerSend.Stop();

            LabelAnnouncement.Text = "";
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
                    serialPort1.NewLine = "\r\n";

                    serialPort1.Open();

                    StatusProgressBar.Value = 100;
                    OpenPortButton.Enabled = false;
                    ClosePortButton.Enabled = true;
                    SetPointGroupBox.Enabled = true;
                    CurrentValueGroupBox.Enabled = true;
                    ControlGroupBox.Enabled = true;
                    TCPGroupBox.Enabled = false;

                    commMode = CommunicationMode.UART;

                    if (bkgdWorker == null)
                    {
                        bkgdWorker = new BackgroundWorker();
                        bkgdWorker.WorkerSupportsCancellation = true;
                        bkgdWorker.DoWork += bkgdWorker_DoWork;
                    }

                    VelocityLeftGraph.Enabled = true;
                    VelocityRightGraph.Enabled = true;

                    if (!bkgdWorker.IsBusy)
                        bkgdWorker.RunWorkerAsync();
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

            PortBox.SelectedIndex = -1;
            BaudRateBox.SelectedIndex = -1;
            ParityBox.SelectedIndex = -1;

            LabelAnnouncement.Text = "";

            VelocityLeftGraph.Enabled = false;
            VelocityRightGraph.Enabled = false;

            OpenPortButton.Enabled = true;
            ClosePortButton.Enabled = false;
            SetPointGroupBox.Enabled = false;
            CurrentValueGroupBox.Enabled = false;
            ControlGroupBox.Enabled = false;

            TimerGraph.Stop();
            TimerSend.Stop();

            if (bkgdWorker.IsBusy)
            {
                bkgdWorker.CancelAsync();
            }

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
            VelLeftPane = VelocityLeftGraph.GraphPane;
            VelLeftPane.Title.FontSpec.Size = 20;
            VelLeftPane.XAxis.Title.FontSpec.Size = 20;
            VelLeftPane.YAxis.Title.FontSpec.Size = 20;
            VelLeftPane.XAxis.Scale.FontSpec.Size = 20;
            VelLeftPane.YAxis.Scale.FontSpec.Size = 20;
            VelLeftPane.Title.Text = "Velocity - Time Graph Motor Left";
            VelLeftPane.XAxis.Title.Text = "Time (s)";
            VelLeftPane.YAxis.Title.Text = "Velocity (Rad/s)";

            VelLeftPane.XAxis.MajorGrid.IsVisible = true;
            VelLeftPane.YAxis.MajorGrid.IsVisible = true;

            VelLeftPane.XAxis.Scale.Max = 60;
            VelLeftPane.XAxis.Scale.MinorStep = 1;
            VelLeftPane.XAxis.Scale.MajorStep = 5;
            VelLeftPane.YAxis.Scale.MaxAuto = true;
            VelLeftPane.YAxis.Scale.MinAuto = true;

            RollingPointPairList VelLeftSetPointList = new RollingPointPairList(60000);
            RollingPointPairList VelLeftCurrentList = new RollingPointPairList(60000);

            VelLeftSetPointCurve = VelLeftPane.AddCurve("Velocity Set Point", VelLeftSetPointList, Color.Blue, SymbolType.None);
            VelLeftCurrentCurve = VelLeftPane.AddCurve("Current Velocity", VelLeftCurrentList, Color.Red, SymbolType.None);
            VelLeftSetPointCurve.Line.Width = 2.0f;
            VelLeftCurrentCurve.Line.Width = 2.0f;

            VelLeftPane.Legend.FontSpec.Size = 20;

            VelocityLeftGraph.AxisChange();

            VelRightPane = VelocityRightGraph.GraphPane;
            VelRightPane.Title.FontSpec.Size = 20;
            VelRightPane.XAxis.Title.FontSpec.Size = 20;
            VelRightPane.YAxis.Title.FontSpec.Size = 20;
            VelRightPane.XAxis.Scale.FontSpec.Size = 20;
            VelRightPane.YAxis.Scale.FontSpec.Size = 20;
            VelRightPane.Title.Text = "Velocity - Time Graph Motor Right";
            VelRightPane.XAxis.Title.Text = "Time (s)";
            VelRightPane.YAxis.Title.Text = "Velocity (Rad/s)";

            VelRightPane.XAxis.MajorGrid.IsVisible = true;
            VelRightPane.YAxis.MajorGrid.IsVisible = true;

            VelRightPane.XAxis.Scale.Max = 60;
            VelRightPane.XAxis.Scale.MinorStep = 1;
            VelRightPane.XAxis.Scale.MajorStep = 5;
            VelRightPane.YAxis.Scale.MaxAuto = true;
            VelRightPane.YAxis.Scale.MinAuto = true;

            RollingPointPairList VelRightSetPointList = new RollingPointPairList(60000);
            RollingPointPairList VelRightCurrentList = new RollingPointPairList(60000);

            VelRightSetPointCurve = VelRightPane.AddCurve("Velocity Set Point", VelRightSetPointList, Color.Blue, SymbolType.None);
            VelRightCurrentCurve = VelRightPane.AddCurve("Current Velocity", VelRightCurrentList, Color.Red, SymbolType.None);
            VelRightSetPointCurve.Line.Width = 2.0f;
            VelRightCurrentCurve.Line.Width = 2.0f;

            VelRightPane.Legend.FontSpec.Size = 20;

            VelocityRightGraph.AxisChange();

            stopwatch.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if ((isStopped == true) && (isSetValueFinish == true))
            {
                isStopped = false;
                isPaused = false;

                VelLeftSetPointCurve.Clear();
                VelLeftCurrentCurve.Clear();
                VelocityLeftGraph.Invalidate();

                VelRightSetPointCurve.Clear();
                VelRightCurrentCurve.Clear();
                VelocityRightGraph.Invalidate();

                state_run = 0x02;

                TimerGraph.Start();
                if (isClientConnected || commMode == CommunicationMode.UART)
                    TimerSend.Start();

                if (!bkgdWorker.IsBusy)
                    bkgdWorker.RunWorkerAsync();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            isStopped = true;
            isPaused = false;

            state_run = 0x01;
            TimerGraph.Stop();

            if (bkgdWorker.IsBusy)
            {
                bkgdWorker.CancelAsync();
            }

            PauseButton.Text = "Pause";
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (!isStopped)
            {
                isPaused = !isPaused;

                if (isPaused)
                {
                    PauseButton.Text = "Resume";
                }
                else
                {
                    PauseButton.Text = "Pause";
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
            if (!string.IsNullOrWhiteSpace(TextboxSPVelLeft.Text) && !string.IsNullOrWhiteSpace(TextboxSPVelRight.Text))
            {
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
            if (isPaused || isStopped) return;

            if (VelLeftSetPointCurve == null || VelLeftCurrentCurve == null) return;
            if (VelRightSetPointCurve == null || VelRightCurrentCurve == null) return;

            IPointListEdit VelLeftSetPointList = VelLeftSetPointCurve.Points as IPointListEdit;
            IPointListEdit VelLeftCurrentList = VelLeftCurrentCurve.Points as IPointListEdit;

            if (VelLeftSetPointList == null || VelLeftCurrentList == null) return;

            double timeLeft = stopwatch.ElapsedMilliseconds / 1000.0;

            VelLeftSetPointList.Add(timeLeft, set_vel_left);
            VelLeftCurrentList.Add(timeLeft, vel_left);

            Scale VelLeftScale = VelocityLeftGraph.GraphPane.XAxis.Scale;

            if (timeLeft > VelLeftScale.Max - VelLeftScale.MajorStep)
            {
                VelLeftScale.Max = timeLeft + VelLeftScale.MajorStep;
                VelLeftScale.Min = VelLeftScale.Max - 30.0;
            }

            IPointListEdit VelRightSetPointList = VelRightSetPointCurve.Points as IPointListEdit;
            IPointListEdit VelRightCurrentList = VelRightCurrentCurve.Points as IPointListEdit;

            if (VelRightSetPointList == null || VelRightCurrentList == null) return;

            double timeRight = stopwatch.ElapsedMilliseconds / 1000.0;

            VelRightSetPointList.Add(timeRight, set_vel_right);
            VelRightCurrentList.Add(timeRight, vel_right);

            Scale VelRightScale = VelocityRightGraph.GraphPane.XAxis.Scale;

            if (timeRight > VelRightScale.Max - VelRightScale.MajorStep)
            {
                VelRightScale.Max = timeRight + VelRightScale.MajorStep;
                VelRightScale.Min = VelRightScale.Max - 30.0;
            }

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
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "serialLog.txt");

            try
            {
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
                            byte[] buffer = new byte[1024];
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead > 0)
                            {
                                listRx.AddRange(buffer.Take(bytesRead));
                                ProcessFramesFromList(listRx);
                            }
                        }
                    }

                    Thread.Sleep(5);
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
            while (listRx.Count >= FRAME_LENGTH)
            {
                int startIndex = listRx.FindIndex(b => b == 0xAA);

                if (startIndex == -1)
                {
                    listRx.Clear();
                    break;
                }
                if (listRx.Count - startIndex < FRAME_LENGTH)
                    break;

                byte[] frame = listRx.Skip(startIndex).Take(FRAME_LENGTH).ToArray();

                if (frame[FRAME_LENGTH - 1] == 0x55)
                {
                    ushort crcReceived = (ushort)((frame[15] << 8) | frame[14]);
                    ushort crcCalculated = CRC16_Check_A(frame.Skip(1).Take(13).ToArray(), 13);

                    if (crcReceived == crcCalculated && frame[1] == 0x08)
                    {
                        this.BeginInvoke(new Action<byte[]>(SetText), frame);

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

                    listRx.RemoveRange(0, startIndex + 13);
                }
                else
                {
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

        // HÀM KẾT NỐI TCP CLIENT
        private async void StartConnectButton_Click(object sender, EventArgs e)
        {
            string ipText = IPComboBox.Text.Trim();
            if (string.IsNullOrEmpty(ipText))
            {
                MessageBox.Show("Please enter or select server IP address.");
                return;
            }

            // Nếu IP hiển thị dạng "192.168.1.10 (hostname)", chỉ lấy phần IP
            string serverIPString = ipText.Split(' ')[0];

            if (!IPAddress.TryParse(serverIPString, out IPAddress serverIP))
            {
                MessageBox.Show("Invalid IP address.");
                return;
            }

            int port = int.Parse(PortTextBox.Text.Trim());
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIP, port);
                stream = client.GetStream();

                LabelAnnouncement.ForeColor = Color.Green;
                LabelAnnouncement.Text = $"Connected to server {serverIP}:{port}";

                SetPointGroupBox.Enabled = true;
                CurrentValueGroupBox.Enabled = true;
                ControlGroupBox.Enabled = true;
                SerialGroupBox.Enabled = false;

                VelocityLeftGraph.Enabled = true;
                VelocityRightGraph.Enabled = true;

                commMode = CommunicationMode.TCP;
                isClientConnected = true;
                hasShownDisconnectMessage = false;

                if (bkgdWorker == null)
                {
                    bkgdWorker = new BackgroundWorker();
                    bkgdWorker.WorkerSupportsCancellation = true;
                    bkgdWorker.DoWork += bkgdWorker_DoWork;
                }

                if (!bkgdWorker.IsBusy)
                    bkgdWorker.RunWorkerAsync();

                // Theo dõi ngắt kết nối từ server
                _ = MonitorServerDisconnectAsync();
            }
            catch (Exception ex)
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Server connection error: " + ex.Message;
                isClientConnected = false;
            }
        }

        // HÀM THEO DÕI NGẮT KẾT NỐI TỪ SERVER
        private async Task MonitorServerDisconnectAsync()
        {
            try
            {
                byte[] buffer = new byte[1];
                while (client != null && client.Connected)
                {
                    int read = await stream.ReadAsync(buffer, 0, 1);
                    if (read == 0)
                        break;
                }
            }
            catch
            {
                // Bị ngắt kết nối
            }

            Invoke(new Action(() =>
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Server disconnected";
                isClientConnected = false;

                if (!hasShownDisconnectMessage)
                {
                    hasShownDisconnectMessage = true;
                    MessageBox.Show("Server disconnected", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }));
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Time_Left, Velocity Left Set Point, Current Velocity Left, Time_Right, Velocity Right Set Point, Current Velocity Right");

                    int count = Math.Min(
                        Math.Min(VelLeftSetPointCurve.Points.Count, VelLeftCurrentCurve.Points.Count),
                        Math.Min(VelRightSetPointCurve.Points.Count, VelRightCurrentCurve.Points.Count)
                    );

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
            state_run = 0x03;
            set_vel_left = 0.4;
            set_vel_right = 0.0;
        }

        private void UpButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void LeftButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x03;
            set_vel_left = 0.0;
            set_vel_right = 2.5;
        }

        private void LeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void DownButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x03;
            set_vel_left = -0.4;
            set_vel_right = 0.0;
        }

        private void DownButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void RightButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x03;
            set_vel_left = 0.0;
            set_vel_right = -2.5;
        }

        private void RightButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x01;
        }

        private void AutoModeButton_Click(object sender, EventArgs e)
        {
            state_run = 0x04;
        }

        private async void ScanIPButton_Click(object sender, EventArgs e)
        {
            IPComboBox.Items.Clear();
            LabelAnnouncement.ForeColor = Color.Green;
            LabelAnnouncement.Text = "Scanning LAN....";

            var localIPs = GetAllLocalIPAddresses();
            if (localIPs.Count == 0)
            {
                LabelAnnouncement.Text = "Local IP not found!";
                return;
            }

            string subnet = localIPs[0].ToString();
            var parts = subnet.Split('.');
            if (parts.Length != 4)
            {
                LabelAnnouncement.Text = "Invalid local IP!";
                return;
            }
            string baseIP = $"{parts[0]}.{parts[1]}.{parts[2]}.";

            List<string> foundDevices = new List<string>();
            int timeout = 100; // ms
            int portToCheck = 12345; // Đổi thành cổng server của bạn

            await Task.Run(() =>
            {
                Parallel.For(1, 255, (i) =>
                {
                    string ip = baseIP + i;
                    bool isAlive = false;
                    try
                    {
                        using (var ping = new System.Net.NetworkInformation.Ping())
                        {
                            var reply = ping.Send(ip, timeout);
                            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                                isAlive = true;
                        }
                    }
                    catch { }

                    // Nếu không trả lời ping, thử quét cổng TCP
                    if (!isAlive)
                    {
                        try
                        {
                            using (var tcp = new TcpClient())
                            {
                                var result = tcp.BeginConnect(ip, portToCheck, null, null);
                                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout));
                                if (success && tcp.Connected)
                                {
                                    isAlive = true;
                                }
                            }
                        }
                        catch { }
                    }

                    if (isAlive)
                    {
                        string hostname = "";
                        try
                        {
                            var hostEntry = Dns.GetHostEntry(ip);
                            hostname = hostEntry.HostName;
                        }
                        catch
                        {
                            hostname = "";
                        }
                        string display = string.IsNullOrEmpty(hostname) ? ip : $"{ip} ({hostname})";
                        lock (foundDevices)
                        {
                            foundDevices.Add(display);
                        }
                    }
                });
            });

            IPComboBox.Invoke(new Action(() =>
            {
                foreach (var item in foundDevices)
                    IPComboBox.Items.Add(item);

                if (foundDevices.Count > 0)
                {
                    LabelAnnouncement.ForeColor = Color.Green;
                    LabelAnnouncement.Text = $"Found {foundDevices.Count} devices on LAN";
                }
                else
                {
                    LabelAnnouncement.ForeColor = Color.Red;
                    LabelAnnouncement.Text = "No devices found!";
                }
            }));
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            // Only handle if currently in TCP mode and connected
            if (commMode == CommunicationMode.TCP && isClientConnected)
            {
                // Stop timers
                TimerGraph.Stop();
                TimerSend.Stop();

                // Stop background worker
                if (bkgdWorker != null && bkgdWorker.IsBusy)
                {
                    bkgdWorker.CancelAsync();
                    Thread.Sleep(50); // Give time to cancel
                }

                // Close network stream and client
                if (stream != null)
                {
                    try { stream.Close(); } catch { }
                    stream = null;
                }
                if (client != null)
                {
                    try { client.Close(); } catch { }
                    client = null;
                }

                isClientConnected = false;
                commMode = CommunicationMode.UART; // Reset to default or as needed

                // Update UI
                SetPointGroupBox.Enabled = false;
                CurrentValueGroupBox.Enabled = false;
                ControlGroupBox.Enabled = false;
                SerialGroupBox.Enabled = true;

                VelocityLeftGraph.Enabled = false;
                VelocityRightGraph.Enabled = false;

                TimerGraph.Stop();
                TimerSend.Stop();

                if (bkgdWorker.IsBusy)
                {
                    bkgdWorker.CancelAsync();
                }

                VelLeftSetPointCurve.Clear();
                VelLeftCurrentCurve.Clear();
                VelocityLeftGraph.Invalidate();
                VelocityLeftGraph.Enabled = false;

                VelRightSetPointCurve.Clear();
                VelRightCurrentCurve.Clear();
                VelocityRightGraph.Invalidate();
                VelocityRightGraph.Enabled = false;

                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Disconnected from server";
            }
        }

        private void OpenClampButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x05;
        }

        private void OpenClampButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x00;
        }

        private void CloseClampButton_MouseDown(object sender, MouseEventArgs e)
        {
            state_run = 0x06;
        }

        private void CloseClampButton_MouseUp(object sender, MouseEventArgs e)
        {
            state_run = 0x00;
        }

        private void FindBallButton_Click(object sender, EventArgs e)
        {
            state_run = 0x08;
        }

        private void TimerSend_Tick(object sender, EventArgs e)
        {
            if (!isSetValueFinish)
                return;

            byte[] frame = new byte[13];
            frame[0] = 0xAA;
            frame[1] = state_run;

            byte[] speed1Bytes = BitConverter.GetBytes((float)set_vel_left);
            byte[] speed2Bytes = BitConverter.GetBytes((float)set_vel_right);

            Array.Copy(speed1Bytes, 0, frame, 2, 4);
            Array.Copy(speed2Bytes, 0, frame, 6, 4);

            byte[] crcInput = frame.Skip(1).Take(9).ToArray();
            ushort crc = CRC16_Check_A(crcInput, crcInput.Length);
            frame[10] = (byte)(crc & 0xFF);
            frame[11] = (byte)((crc >> 8) & 0xFF);
            frame[12] = 0x55;

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
                        hasShownDisconnectMessage = false;
                    }
                    catch (Exception ex)
                    {
                        isClientConnected = false;

                        if (!hasShownDisconnectMessage)
                        {
                            hasShownDisconnectMessage = true;
                            MessageBox.Show("Server disconnected " + ex.Message, "TCP Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                TimerGraph.Stop();
                TimerSend.Stop();

                if (bkgdWorker != null && bkgdWorker.IsBusy)
                {
                    bkgdWorker.CancelAsync();
                    Thread.Sleep(50);
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
                }

                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}