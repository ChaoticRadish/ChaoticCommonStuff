namespace WinFormsTest.Tests
{
    partial class SocketTest002
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LocalIpInput = new ChaoticWinformControl.HintTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TargetPortInput = new ChaoticWinformControl.HintTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TargetIpInput = new ChaoticWinformControl.HintTextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.textInput = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.button_conn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PortInput = new ChaoticWinformControl.HintTextBox();
            this.button_send60Str = new System.Windows.Forms.Button();
            this.clientBox = new ChaoticWinformControl.LogShowerBox();
            this.ReuseAddressCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ReuseAddressCheckBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.LocalIpInput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TargetPortInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TargetIpInput);
            this.groupBox1.Controls.Add(this.button_send);
            this.groupBox1.Controls.Add(this.textInput);
            this.groupBox1.Controls.Add(this.button_close);
            this.groupBox1.Controls.Add(this.button_conn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.PortInput);
            this.groupBox1.Controls.Add(this.button_send60Str);
            this.groupBox1.Controls.Add(this.clientBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(682, 426);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户端";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "本机IP";
            // 
            // LocalIpInput
            // 
            this.LocalIpInput.BorderGap = 1.5F;
            this.LocalIpInput.HintText = "IP";
            this.LocalIpInput.HintTextColor = System.Drawing.Color.LightGray;
            this.LocalIpInput.Location = new System.Drawing.Point(96, 61);
            this.LocalIpInput.Name = "LocalIpInput";
            this.LocalIpInput.Size = new System.Drawing.Size(100, 21);
            this.LocalIpInput.TabIndex = 12;
            this.LocalIpInput.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "目标端口号";
            // 
            // TargetPortInput
            // 
            this.TargetPortInput.BorderGap = 1.5F;
            this.TargetPortInput.HintText = "端口号";
            this.TargetPortInput.HintTextColor = System.Drawing.Color.LightGray;
            this.TargetPortInput.Location = new System.Drawing.Point(287, 31);
            this.TargetPortInput.Name = "TargetPortInput";
            this.TargetPortInput.Size = new System.Drawing.Size(100, 21);
            this.TargetPortInput.TabIndex = 10;
            this.TargetPortInput.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "目标IP";
            // 
            // TargetIpInput
            // 
            this.TargetIpInput.BorderGap = 1.5F;
            this.TargetIpInput.HintText = "IP";
            this.TargetIpInput.HintTextColor = System.Drawing.Color.LightGray;
            this.TargetIpInput.Location = new System.Drawing.Point(96, 31);
            this.TargetIpInput.Name = "TargetIpInput";
            this.TargetIpInput.Size = new System.Drawing.Size(100, 21);
            this.TargetIpInput.TabIndex = 8;
            this.TargetIpInput.Text = "127.0.0.1";
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(549, 389);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 7;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // textInput
            // 
            this.textInput.Location = new System.Drawing.Point(6, 391);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(537, 21);
            this.textInput.TabIndex = 6;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(585, 64);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 5;
            this.button_close.Text = "断开";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_conn
            // 
            this.button_conn.Location = new System.Drawing.Point(504, 64);
            this.button_conn.Name = "button_conn";
            this.button_conn.Size = new System.Drawing.Size(75, 23);
            this.button_conn.TabIndex = 4;
            this.button_conn.Text = "连接";
            this.button_conn.UseVisualStyleBackColor = true;
            this.button_conn.Click += new System.EventHandler(this.button_conn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "本机端口号";
            // 
            // PortInput
            // 
            this.PortInput.BorderGap = 1.5F;
            this.PortInput.HintText = "端口号";
            this.PortInput.HintTextColor = System.Drawing.Color.LightGray;
            this.PortInput.Location = new System.Drawing.Point(287, 61);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(100, 21);
            this.PortInput.TabIndex = 2;
            this.PortInput.Text = "11773";
            // 
            // button_send60Str
            // 
            this.button_send60Str.Location = new System.Drawing.Point(6, 362);
            this.button_send60Str.Name = "button_send60Str";
            this.button_send60Str.Size = new System.Drawing.Size(226, 23);
            this.button_send60Str.TabIndex = 1;
            this.button_send60Str.Text = "发送随机60位字符串";
            this.button_send60Str.UseVisualStyleBackColor = true;
            this.button_send60Str.Click += new System.EventHandler(this.button_send60Str_Click);
            // 
            // clientBox
            // 
            this.clientBox.Location = new System.Drawing.Point(6, 92);
            this.clientBox.Name = "clientBox";
            this.clientBox.Size = new System.Drawing.Size(655, 264);
            this.clientBox.TabIndex = 0;
            this.clientBox.TimeFormat = "yyyy-MM-dd hh:mm:ss:fff";
            // 
            // ReuseAddressCheckBox
            // 
            this.ReuseAddressCheckBox.AutoSize = true;
            this.ReuseAddressCheckBox.Checked = true;
            this.ReuseAddressCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReuseAddressCheckBox.Location = new System.Drawing.Point(479, 35);
            this.ReuseAddressCheckBox.Name = "ReuseAddressCheckBox";
            this.ReuseAddressCheckBox.Size = new System.Drawing.Size(72, 16);
            this.ReuseAddressCheckBox.TabIndex = 14;
            this.ReuseAddressCheckBox.Text = "端口复用";
            this.ReuseAddressCheckBox.UseVisualStyleBackColor = true;
            // 
            // SocketTest002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "SocketTest002";
            this.Text = "SocketTest0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private ChaoticWinformControl.LogShowerBox clientBox;
        private Button button_send60Str;
        private ChaoticWinformControl.HintTextBox PortInput;
        private Label label1;
        private Button button_conn;
        private Button button_close;
        private Button button_send;
        private TextBox textInput;
        private Label label2;
        private ChaoticWinformControl.HintTextBox TargetIpInput;
        private Label label3;
        private ChaoticWinformControl.HintTextBox TargetPortInput;
        private Label label4;
        private ChaoticWinformControl.HintTextBox LocalIpInput;
        private CheckBox ReuseAddressCheckBox;
    }
}