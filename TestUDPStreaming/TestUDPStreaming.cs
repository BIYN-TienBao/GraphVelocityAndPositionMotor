using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.IO;

namespace TestUDPStreaming
{
    public partial class TestUDPStreaming : Form
    {
        private UdpClient udpClient;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public TestUDPStreaming()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;

            LabelAnnouncement.Text = "";
        }

        private void StartCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Không tìm thấy camera.");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
            videoSource.Start();
        }

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                // 🖼️ HIỂN THỊ ảnh lên PictureBox
                CamPictureBox.Invoke(new Action(() =>
                {
                    CamPictureBox.Image?.Dispose(); // giải phóng ảnh cũ
                    CamPictureBox.Image = (Bitmap)bitmap.Clone(); // clone thêm để giữ ảnh
                }));

                // 🛰️ GỬI ảnh qua UDP
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();

                    SendImageOverUDP(imageBytes);
                }

                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
            }
        }


        private void SendImageOverUDP(byte[] imageBytes)
        {
            string destinationIP = IPTextBox.Text.Trim();
            int destinationPort = int.Parse(PortTextBox.Text);

            const int packetSize = 1400; // Kích thước mỗi gói (bytes)
            int totalPackets = (int)Math.Ceiling((double)imageBytes.Length / packetSize);

            using (UdpClient client = new UdpClient())
            {
                for (int i = 0; i < totalPackets; i++)
                {
                    int currentPacketSize = Math.Min(packetSize, imageBytes.Length - i * packetSize);

                    // Header: 2 bytes cho tổng số gói, 2 bytes cho số thứ tự gói
                    byte[] header = new byte[4];
                    header[0] = (byte)(totalPackets >> 8);
                    header[1] = (byte)(totalPackets & 0xFF);
                    header[2] = (byte)(i >> 8);
                    header[3] = (byte)(i & 0xFF);

                    byte[] packet = new byte[4 + currentPacketSize];
                    Buffer.BlockCopy(header, 0, packet, 0, 4);
                    Buffer.BlockCopy(imageBytes, i * packetSize, packet, 4, currentPacketSize);

                    client.Send(packet, packet.Length, destinationIP, destinationPort);
                }
            }
        }

        private void ConnectButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                string destinationIP = IPTextBox.Text.Trim();
                string portText = PortTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(destinationIP) || string.IsNullOrWhiteSpace(portText))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ IP và Port.");
                    return;
                }

                int destinationPort = int.Parse(portText);
                IPAddress.Parse(destinationIP); // kiểm tra IP hợp lệ

                udpClient = new UdpClient(); // tạo UDP client (không cần bind)

                LabelAnnouncement.ForeColor = Color.Green;
                LabelAnnouncement.Text = $"UDP Client sẵn sàng gửi đến {destinationIP}:{destinationPort}";

            }
            catch (Exception ex)
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Lỗi thiết lập UDP Client: " + ex.Message;
            }
        }

        private void SendButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (udpClient == null)
                {
                    MessageBox.Show("UDP Client chưa được thiết lập.");
                    return;
                }

                string message = SendTextBox.Text.Trim();
                string destinationIP = IPTextBox.Text.Trim();
                int destinationPort = int.Parse(PortTextBox.Text.Trim());

                byte[] data = Encoding.UTF8.GetBytes(message);
                udpClient.Send(data, data.Length, destinationIP, destinationPort);

                LabelAnnouncement.ForeColor = Color.Blue;
                LabelAnnouncement.Text = "Đã gửi: " + message;
            }
            catch (Exception ex)
            {
                LabelAnnouncement.ForeColor = Color.Red;
                LabelAnnouncement.Text = "Lỗi gửi UDP: " + ex.Message;
            }
        }

        private void StartCameraButton_Click(object sender, EventArgs e)
        {
            StartCamera();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.NewFrame -= Video_NewFrame; // Hủy đăng ký sự kiện
                videoSource.SignalToStop();

                // Thay vì WaitForStop blocking, dùng polling với timeout nhỏ
                int waitCount = 0;
                while (videoSource.IsRunning && waitCount < 50) // tối đa 5 giây
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    waitCount++;
                }
            }
        }
    }
}
