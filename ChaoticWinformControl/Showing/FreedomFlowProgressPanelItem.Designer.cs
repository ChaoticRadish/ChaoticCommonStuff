namespace ChaoticWinformControl
{
    partial class FreedomFlowProgressPanelItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MinorLabel = new System.Windows.Forms.Label();
            this.MainLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MinorLabel
            // 
            this.MinorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MinorLabel.Location = new System.Drawing.Point(0, 107);
            this.MinorLabel.Name = "MinorLabel";
            this.MinorLabel.Size = new System.Drawing.Size(148, 43);
            this.MinorLabel.TabIndex = 3;
            this.MinorLabel.Text = "次标签";
            this.MinorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainLabel
            // 
            this.MainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLabel.Location = new System.Drawing.Point(0, 0);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(150, 107);
            this.MainLabel.TabIndex = 2;
            this.MainLabel.Text = "主标签";
            this.MainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FreedomFlowProgressPanelItem
            // 
            this.Controls.Add(this.MinorLabel);
            this.Controls.Add(this.MainLabel);
            this.Name = "FreedomFlowProgressPanelItem";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MinorLabel;
        private System.Windows.Forms.Label MainLabel;
    }
}
