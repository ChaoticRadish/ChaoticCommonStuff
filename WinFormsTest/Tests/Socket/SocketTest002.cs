using ChaoticWinformControl;
using NPOI.SS.Formula.Functions;
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
using static ChaoticWinformControl.LogShowerBox;

namespace WinFormsTest.Tests
{
    public partial class SocketTest002 : TestFormBase
    {
        public SocketTest002()
        {
            InitializeComponent();
        }

        Socket client;
        Task clientTask;
        public bool 停止运行标志 { get; set; }
        public string 任务状态 { get => clientTask == null ? "null" : (clientTask.IsCompleted ? "结束" : "未结束"); }

        public bool 任务中止信号 { get; set; } = false;

        public Queue<string> 等待发送队列 { get; private set; } = new Queue<string>();

        protected override List<NeedMoitoringItem> GetNeedMoitorings()
        {
            var output = base.GetNeedMoitorings();
            output.AddRange(NeedMoitoringItem.From("数据", this,
                nameof(等待发送队列)));
            output.AddRange(NeedMoitoringItem.From("数量", 等待发送队列,
                nameof(等待发送队列.Count)));
            output.AddRange(NeedMoitoringItem.From("状态", this,
                nameof(任务状态),
                nameof(停止运行标志),
                nameof(任务中止信号)));
            return output;
        }

        public override void TestContent()
        {
            base.TestContent();

        }

        private void Conn(string targetIp, int targetPort, string localIp, int localPort, bool reuseAddress)
        {

            clientBox.Clear();

            停止运行标志 = false;
            任务中止信号 = false;
            clientTask = Task.Run(() =>
            {
                Thread.Sleep(500);
                clientBox.SimpleLogAutoInvoke("客户端", "开始");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, reuseAddress);
                    client.Bind(new IPEndPoint(IPAddress.Parse(localIp), localPort));
                }
                catch (Exception ex)
                {
                    clientBox.SimpleLogAutoInvoke("无法绑定地址", ex.ToString());
                }
                try
                {
                    client.Connect(new IPEndPoint(IPAddress.Parse(targetIp), targetPort));
                }
                catch (Exception ex)
                {
                    clientBox.SimpleLogAutoInvoke("无法建立连接", ex.ToString());
                }
                byte[] buffer = new byte[5];
                while (!停止运行标志 && !任务中止信号)
                {
                    buffer.Clear();

                    if (等待发送队列.Count > 0)
                    {
                        string waitSend = 等待发送队列.Dequeue();

                        try
                        {
                            client.Send(Util.String.StringHelper.ToByteArray(waitSend).Append((byte)0x0D).ToArray());
                            clientBox.SimpleLogAutoInvoke("发送成功", waitSend);
                        }
                        catch (Exception ex)
                        {
                            clientBox.SimpleLogAutoInvoke("发送失败", ex.ToString());
                            break;
                        }
                    }
                    Thread.Sleep(1000);

                }
                client.Close();
                clientBox.SimpleLogAutoInvoke("客户端", "结束");

                任务中止信号 = false;
            });

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!停止运行标志)
            {
                e.Cancel = true;
                停止运行标志 = true;
                Task.Run(() =>
                {
                    Task.WaitAll(clientTask);
                    this.AutoInvoke(Close);
                });
            }

        }

        private void button_send60Str_Click(object sender, EventArgs e)
        {
            等待发送队列.Enqueue(Util.Random.RandomStringHelper.GetRandomEnglishString(60).ToUpper());
        }

        private void button_conn_Click(object sender, EventArgs e)
        {
            if (clientTask != null && !clientTask.IsCompleted)
            {
                clientBox.SimpleLogAutoInvoke("无法连接", $"原有任务未结束");
                return;
            }
            string ipStr = TargetIpInput.Text;
            string ipLocalStr = LocalIpInput.Text;
            string portStr = TargetPortInput.Text;
            string portLocalStr = PortInput.Text;

            int portlocal;
            int port;
            if (!int.TryParse(portLocalStr, out portlocal) || !(portlocal <= 65535 && portlocal > 0))
            {
                clientBox.SimpleLogAutoInvoke("无法连接", $"输入值 {portLocalStr} 不是端口号");
                return;
            }

            if (!int.TryParse(portStr, out port) || !(port <= 65535 && port > 0))
            {
                clientBox.SimpleLogAutoInvoke("无法连接", $"输入值 {portStr} 不是端口号");
                return;
            }

            Conn(ipStr, port, ipLocalStr, portlocal, ReuseAddressCheckBox.Checked);

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            clientBox.SimpleLogAutoInvoke("操作", $"任务中止信号");
            任务中止信号 = true;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            等待发送队列.Enqueue(textInput.Text);
        }
    }
}
