namespace ChaoticWinformControl.FeatureGroup
{
    partial class InputFormFrame
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
            this.HintLabel = new System.Windows.Forms.Label();
            this.ButtonArea = new System.Windows.Forms.FlowLayoutPanel();
            this.BodyPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // HintLabel
            // 
            this.HintLabel.AutoSize = true;
            this.HintLabel.Location = new System.Drawing.Point(12, 9);
            this.HintLabel.Name = "HintLabel";
            this.HintLabel.Size = new System.Drawing.Size(53, 12);
            this.HintLabel.TabIndex = 1;
            this.HintLabel.Text = "提示文本";
            // 
            // ButtonArea
            // 
            this.ButtonArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonArea.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.ButtonArea.Location = new System.Drawing.Point(12, 190);
            this.ButtonArea.Name = "ButtonArea";
            this.ButtonArea.Size = new System.Drawing.Size(381, 31);
            this.ButtonArea.TabIndex = 2;
            // 
            // BodyPanel
            // 
            this.BodyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BodyPanel.Location = new System.Drawing.Point(14, 29);
            this.BodyPanel.Name = "BodyPanel";
            this.BodyPanel.Size = new System.Drawing.Size(379, 158);
            this.BodyPanel.TabIndex = 3;
            // 
            // InputFormFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 224);
            this.Controls.Add(this.HintLabel);
            this.Controls.Add(this.BodyPanel);
            this.Controls.Add(this.ButtonArea);
            this.Name = "InputFormFrame";
            this.Text = "输入窗口的框架";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label HintLabel;
        private System.Windows.Forms.FlowLayoutPanel ButtonArea;
        private System.Windows.Forms.Panel BodyPanel;
    }
}