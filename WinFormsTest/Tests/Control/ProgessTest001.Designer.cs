namespace WinFormsTest.Tests
{
    partial class ProgessTest001
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
            this.components = new System.ComponentModel.Container();
            this.freedomFlowProgressPanel1 = new ChaoticWinformControl.FreedomFlowProgressPanel();
            this.Item3 = new ChaoticWinformControl.FreedomFlowProgressPanelItem();
            this.Item2 = new ChaoticWinformControl.FreedomFlowProgressPanelItem();
            this.Item1 = new ChaoticWinformControl.FreedomFlowProgressPanelItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.freedomFlowProgressPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // freedomFlowProgressPanel1
            // 
            this.freedomFlowProgressPanel1.ColorDone = System.Drawing.Color.Green;
            this.freedomFlowProgressPanel1.ColorError = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.freedomFlowProgressPanel1.ColorWaiting = System.Drawing.Color.Gray;
            this.freedomFlowProgressPanel1.ColorWarning = System.Drawing.Color.Orange;
            this.freedomFlowProgressPanel1.Controls.Add(this.Item3);
            this.freedomFlowProgressPanel1.Controls.Add(this.Item2);
            this.freedomFlowProgressPanel1.Controls.Add(this.Item1);
            this.freedomFlowProgressPanel1.CrossHeadLength = 12;
            this.freedomFlowProgressPanel1.CrossHeadWidth = 8;
            this.freedomFlowProgressPanel1.CrossWidth = 5;
            this.freedomFlowProgressPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.freedomFlowProgressPanel1.Location = new System.Drawing.Point(0, 0);
            this.freedomFlowProgressPanel1.Name = "freedomFlowProgressPanel1";
            this.freedomFlowProgressPanel1.Size = new System.Drawing.Size(800, 450);
            this.freedomFlowProgressPanel1.TabIndex = 0;
            this.freedomFlowProgressPanel1.ToolTip = null;
            this.freedomFlowProgressPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.freedomFlowProgressPanel1_MouseMove);
            // 
            // Item3
            // 
            this.Item3.AutoSize = true;
            this.Item3.BackColor = System.Drawing.Color.Gray;
            this.Item3.ColorDone = System.Drawing.Color.Green;
            this.Item3.ColorError = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Item3.ColorWaiting = System.Drawing.Color.Gray;
            this.Item3.ColorWarning = System.Drawing.Color.Orange;
            this.Item3.CustomColor = false;
            this.Item3.Location = new System.Drawing.Point(544, 95);
            this.Item3.MainFont = new System.Drawing.Font("宋体", 16F);
            this.Item3.MainText = "测试3";
            this.Item3.MinorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Item3.MinorForeColor = System.Drawing.Color.DarkGray;
            this.Item3.MinorText = "次标签";
            this.Item3.Name = "Item3";
            this.Item3.Padding = new System.Windows.Forms.Padding(5);
            this.Item3.Size = new System.Drawing.Size(123, 81);
            this.Item3.State = ChaoticWinformControl.FreedomFlowProgressPanel.ItemState.Waiting;
            this.Item3.TabIndex = 2;
            this.Item3.ToolTipInfo = null;
            // 
            // Item2
            // 
            this.Item2.AutoSize = true;
            this.Item2.BackColor = System.Drawing.Color.Gray;
            this.Item2.ColorDone = System.Drawing.Color.Green;
            this.Item2.ColorError = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Item2.ColorWaiting = System.Drawing.Color.Gray;
            this.Item2.ColorWarning = System.Drawing.Color.Orange;
            this.Item2.CustomColor = false;
            this.Item2.Location = new System.Drawing.Point(313, 95);
            this.Item2.MainFont = new System.Drawing.Font("宋体", 16F);
            this.Item2.MainText = "测试2";
            this.Item2.MinorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Item2.MinorForeColor = System.Drawing.Color.DarkGray;
            this.Item2.MinorText = "次标签";
            this.Item2.Name = "Item2";
            this.Item2.Padding = new System.Windows.Forms.Padding(5);
            this.Item2.Size = new System.Drawing.Size(111, 43);
            this.Item2.State = ChaoticWinformControl.FreedomFlowProgressPanel.ItemState.Waiting;
            this.Item2.TabIndex = 1;
            this.Item2.ToolTipInfo = null;
            // 
            // Item1
            // 
            this.Item1.AutoSize = true;
            this.Item1.BackColor = System.Drawing.Color.Gray;
            this.Item1.ColorDone = System.Drawing.Color.Green;
            this.Item1.ColorError = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Item1.ColorWaiting = System.Drawing.Color.Gray;
            this.Item1.ColorWarning = System.Drawing.Color.Orange;
            this.Item1.CustomColor = false;
            this.Item1.Location = new System.Drawing.Point(142, 149);
            this.Item1.MainFont = new System.Drawing.Font("宋体", 16F);
            this.Item1.MainText = "测试1";
            this.Item1.MinorFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Item1.MinorForeColor = System.Drawing.Color.DarkGray;
            this.Item1.MinorText = "次标签";
            this.Item1.Name = "Item1";
            this.Item1.Padding = new System.Windows.Forms.Padding(5);
            this.Item1.Size = new System.Drawing.Size(104, 38);
            this.Item1.State = ChaoticWinformControl.FreedomFlowProgressPanel.ItemState.Waiting;
            this.Item1.TabIndex = 0;
            this.Item1.ToolTipInfo = null;
            // 
            // ProgessTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.freedomFlowProgressPanel1);
            this.Name = "ProgessTest001";
            this.Text = "ProgessTest001";
            this.freedomFlowProgressPanel1.ResumeLayout(false);
            this.freedomFlowProgressPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ChaoticWinformControl.FreedomFlowProgressPanel freedomFlowProgressPanel1;
        private ToolTip toolTip1;
        private ChaoticWinformControl.FreedomFlowProgressPanelItem Item2;
        private ChaoticWinformControl.FreedomFlowProgressPanelItem Item1;
        private ChaoticWinformControl.FreedomFlowProgressPanelItem Item3;
    }
}