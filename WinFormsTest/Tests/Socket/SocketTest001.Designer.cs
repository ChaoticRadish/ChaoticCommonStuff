namespace WinFormsTest.Tests
{
    partial class SocketTest001
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
            this.clientBox = new ChaoticWinformControl.LogShowerBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1_s_强制结束 = new System.Windows.Forms.Button();
            this.serverBox = new ChaoticWinformControl.LogShowerBox();
            this.button1_重新执行 = new System.Windows.Forms.Button();
            this.button1_s_长等待 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clientBox);
            this.groupBox1.Location = new System.Drawing.Point(27, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 406);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户端";
            // 
            // clientBox
            // 
            this.clientBox.Location = new System.Drawing.Point(6, 20);
            this.clientBox.Name = "clientBox";
            this.clientBox.Size = new System.Drawing.Size(300, 300);
            this.clientBox.TabIndex = 0;
            this.clientBox.TimeFormat = "yyyy-MM-dd hh:mm:ss:fff";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1_s_长等待);
            this.groupBox2.Controls.Add(this.button1_s_强制结束);
            this.groupBox2.Controls.Add(this.serverBox);
            this.groupBox2.Location = new System.Drawing.Point(379, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 406);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "服务端";
            // 
            // button1_s_强制结束
            // 
            this.button1_s_强制结束.Location = new System.Drawing.Point(6, 326);
            this.button1_s_强制结束.Name = "button1_s_强制结束";
            this.button1_s_强制结束.Size = new System.Drawing.Size(75, 23);
            this.button1_s_强制结束.TabIndex = 2;
            this.button1_s_强制结束.Text = "强制结束";
            this.button1_s_强制结束.UseVisualStyleBackColor = true;
            this.button1_s_强制结束.Click += new System.EventHandler(this.button1_s_强制结束_Click);
            // 
            // serverBox
            // 
            this.serverBox.Location = new System.Drawing.Point(6, 20);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(300, 300);
            this.serverBox.TabIndex = 1;
            this.serverBox.TimeFormat = "yyyy-MM-dd hh:mm:ss:fff";
            // 
            // button1_重新执行
            // 
            this.button1_重新执行.Location = new System.Drawing.Point(33, 444);
            this.button1_重新执行.Name = "button1_重新执行";
            this.button1_重新执行.Size = new System.Drawing.Size(75, 23);
            this.button1_重新执行.TabIndex = 3;
            this.button1_重新执行.Text = "重新执行";
            this.button1_重新执行.UseVisualStyleBackColor = true;
            this.button1_重新执行.Click += new System.EventHandler(this.button1_重新执行_Click);
            // 
            // button1_s_长等待
            // 
            this.button1_s_长等待.Location = new System.Drawing.Point(87, 326);
            this.button1_s_长等待.Name = "button1_s_长等待";
            this.button1_s_长等待.Size = new System.Drawing.Size(75, 23);
            this.button1_s_长等待.TabIndex = 3;
            this.button1_s_长等待.Text = "长等待";
            this.button1_s_长等待.UseVisualStyleBackColor = true;
            this.button1_s_长等待.Click += new System.EventHandler(this.button1_s_长等待_Click);
            // 
            // SocketTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 519);
            this.Controls.Add(this.button1_重新执行);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SocketTest001";
            this.Text = "SocketTest001";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ChaoticWinformControl.LogShowerBox clientBox;
        private ChaoticWinformControl.LogShowerBox serverBox;
        private Button button1_s_强制结束;
        private Button button1_重新执行;
        private Button button1_s_长等待;
    }
}