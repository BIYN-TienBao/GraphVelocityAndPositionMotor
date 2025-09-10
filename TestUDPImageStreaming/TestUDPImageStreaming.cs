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

namespace TestUDPImageStreaming
{
    public partial class Form1: Form
    {
        private UdpClient udpClient;

        public Form1()
        {
            InitializeComponent();

            LabelAnnouncement.Text = "";
        }


        private void ConnectButton_Click(object sender, EventArgs e)
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

        private void SendButton_Click(object sender, EventArgs e)
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
    }
}
