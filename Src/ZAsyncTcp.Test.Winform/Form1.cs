using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

using ZAsyncTcp;

namespace ZAsyncTcp.Test.Winform
{
    public partial class Form1 : Form
    {
        ZAsyncTcpServer _server;

        public Form1()
        {
            InitializeComponent();

            InitializeUI();
        }

        private void InitializeUI()
        {
            comboBox_Type.SelectedIndex = 0;
            button_Connect.Text = "Start";
            textBox_IP.Text = "127.0.0.1";
            textBox_Port.Text = "12345";
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (button_Connect.Text == "Start")
            {
                if (!int.TryParse(textBox_Port.Text, out int port))
                {
                    return;
                }
                if (!IPAddress.TryParse(textBox_IP.Text, out IPAddress ip))
                {
                    return;
                }

                _server = new ZAsyncTcpServer(ip, port);
                _server.ClientConnected += _server_ClientConnected;
                _server.ClientDisconnected += _server_ClientDisconnected;
                _server.DataReceived += _server_DataReceived;
                _server.Start();
                button_Connect.Text = "Stop";
            }
            else
            {
                _server.Stop();
                button_Connect.Text = "Start";
            }

            comboBox_Type.Enabled = !_server.IsRunning;
            textBox_IP.Enabled = !_server.IsRunning;
            textBox_Port.Enabled = !_server.IsRunning;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {

        }

        private void _server_DataReceived(object sender, ZAsyncTcpEventArgs e)
        {
            var data = Encoding.Default.GetString(e._state.Buffer);
            Invoke(new Action(() =>
            {
                textBox_Received.AppendText(data + Environment.NewLine);
            }));
        }

        private void _server_ClientDisconnected(object sender, ZAsyncTcpEventArgs e)
        {
            Invoke(new Action(() =>
            {
                textBox_Received.AppendText("Disconnected" + Environment.NewLine);
            }));
        }

        private void _server_ClientConnected(object sender, ZAsyncTcpEventArgs e)
        {
            Invoke(new Action(() =>
            {
                textBox_Received.AppendText("Connected" + Environment.NewLine);
            }));
        }
    }
}
