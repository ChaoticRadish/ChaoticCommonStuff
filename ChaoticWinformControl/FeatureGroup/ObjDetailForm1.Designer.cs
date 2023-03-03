namespace ChaoticWinformControl.FeatureGroup
{
    partial class ObjDetailForm1
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
            this.ShowerBox = new System.Windows.Forms.RichTextBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ShowerBox
            // 
            this.ShowerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowerBox.Location = new System.Drawing.Point(0, 0);
            this.ShowerBox.Name = "ShowerBox";
            this.ShowerBox.ReadOnly = true;
            this.ShowerBox.Size = new System.Drawing.Size(446, 253);
            this.ShowerBox.TabIndex = 0;
            this.ShowerBox.Text = "";
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // ObjDetailForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 253);
            this.Controls.Add(this.ShowerBox);
            this.Name = "ObjDetailForm1";
            this.Text = "对象详情窗口v1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ShowerBox;
        private System.Windows.Forms.Timer MainTimer;
    }
}