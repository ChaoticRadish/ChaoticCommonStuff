using ChaoticWinformControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Extension;
using Util.Random;

namespace WinFormsTest.Tests
{
    public partial class SocketTest001 : TestFormBase
    {
        public SocketTest001()
        {
            InitializeComponent();
        }

        Socket client;
        Socket server;

        Task clientTask;
        Task serverTask;

        bool stopFlag;

        bool closeServerTask = false;
        bool serverSleepLongTime = false;

        public override void TestContent()
        {
            base.TestContent();

            closeServerTask = false;
            serverSleepLongTime = false;

            serverBox.Clear();
            clientBox.Clear();

            stopFlag = false;
            serverTask = Task.Run(() =>
            {
                serverBox.SimpleLogAutoInvoke("服务端", "开始");
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11773));
                server.Listen(10);

                serverBox.SimpleLogAutoInvoke("服务端", "开启监听: " + server.LocalEndPoint);

                Socket temp = server.Accept();

                int index = 0;
                while (!stopFlag)
                {
                    string waitSend = Util.Random.RandomStringHelper.GetRandomEnglishString(60);
                    try
                    {
                        temp.Send(Util.String.StringHelper.ToByteArray(waitSend));
                    }
                    catch (Exception ex)
                    {
                        serverBox.SimpleLogAutoInvoke("发送失败", ex.ToString());
                        break;
                    }
                    
                    if (closeServerTask)
                    {
                        temp.Close();
                        server.Close();
                        return;
                    }

                    if (RandomValueTypeHelper.RandomTrue(index > 10 ? 0.2 : 0.01))
                    {
                        serverBox.SimpleLogAutoInvoke("随机关闭", "随机关闭");
                        break;
                    }
                    serverBox.SimpleLogAutoInvoke("发送", waitSend);

                    index++;
                    if (!serverSleepLongTime)
                    {
                        Thread.Sleep(900);
                    }
                    else
                    {
                        serverSleepLongTime = false;
                        Thread.Sleep(5000);
                    }
                }
                temp.Close();
                server.Close();
                serverBox.SimpleLogAutoInvoke("服务端", "结束");
            });
            clientTask = Task.Run(() =>
            {
                Thread.Sleep(500);
                clientBox.SimpleLogAutoInvoke("客户端", "开始");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27036));
                byte[] buffer = new byte[5];
                while (!stopFlag)
                {
                    Array.Clear(buffer);
                    //buffer.Clear();

                    int count = 0;
                    try
                    {
                        count = client.Receive(buffer);
                    }
                    catch (Exception ex)
                    {
                        clientBox.SimpleLogAutoInvoke("连接断开", "连接异常中断: " + ex);
                        break;
                    }

                    if (count == 0)
                    {
                        clientBox.SimpleLogAutoInvoke("连接断开", "服务端已断开");
                        break;
                    }
                    
                    clientBox.SimpleLogAutoInvoke("接收", $"[{count}]{buffer.ToHexString()}");
                }
                client.Close();
                clientBox.SimpleLogAutoInvoke("客户端", "结束");
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!stopFlag)
            {
                e.Cancel = true;
                stopFlag = true;
                Task.Run(() =>
                {
                    Task.WaitAll(clientTask, serverTask);
                    this.AutoInvoke(Close);
                });
            }

        }

        private void button1_s_强制结束_Click(object sender, EventArgs e)
        {
            closeServerTask = true;
        }

        private void button1_重新执行_Click(object sender, EventArgs e)
        {
            stopFlag = true;

            Task.Run(() =>
            {
                Task.WaitAll(new List<Task>() { clientTask, serverTask }.Where(i => i != null).ToArray());
                this.AutoInvoke(TestContent);
            });
        }

        private void button1_s_长等待_Click(object sender, EventArgs e)
        {
            serverSleepLongTime = true;
        }
    }
}
