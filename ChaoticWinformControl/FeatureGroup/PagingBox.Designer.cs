namespace ChaoticWinformControl.FeatureGroup
{
    partial class PagingBox
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
            this.FirstButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.PageInput = new ChaoticWinformControl.ValueBox.IntegerBox();
            this.JumpButton = new System.Windows.Forms.Button();
            this.TotalPageShower = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PageButtonArea = new System.Windows.Forms.Panel();
            this.PageButtonInnerArea = new System.Windows.Forms.Panel();
            this.PageButtonArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // FirstButton
            // 
            this.FirstButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FirstButton.Location = new System.Drawing.Point(3, 2);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(56, 29);
            this.FirstButton.TabIndex = 14;
            this.FirstButton.Text = "首页";
            this.FirstButton.UseVisualStyleBackColor = true;
            this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
            // 
            // LastButton
            // 
            this.LastButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastButton.Location = new System.Drawing.Point(414, 2);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(56, 29);
            this.LastButton.TabIndex = 13;
            this.LastButton.Text = "尾页";
            this.LastButton.UseVisualStyleBackColor = true;
            this.LastButton.Click += new System.EventHandler(this.LastButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Location = new System.Drawing.Point(333, 2);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 29);
            this.NextButton.TabIndex = 12;
            this.NextButton.Text = "下一页";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PrevButton.Location = new System.Drawing.Point(65, 2);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(75, 29);
            this.PrevButton.TabIndex = 11;
            this.PrevButton.Text = "上一页";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // PageInput
            // 
            this.PageInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PageInput.DefaultValue = 1;
            this.PageInput.Location = new System.Drawing.Point(476, 7);
            this.PageInput.Name = "PageInput";
            this.PageInput.Range = ChaoticWinformControl.ValueBox.NumberRangeEnum.Positive;
            this.PageInput.Size = new System.Drawing.Size(43, 21);
            this.PageInput.TabIndex = 15;
            this.PageInput.Text = "1";
            // 
            // JumpButton
            // 
            this.JumpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JumpButton.Location = new System.Drawing.Point(585, 2);
            this.JumpButton.Name = "JumpButton";
            this.JumpButton.Size = new System.Drawing.Size(56, 29);
            this.JumpButton.TabIndex = 16;
            this.JumpButton.Text = "跳转";
            this.JumpButton.UseVisualStyleBackColor = true;
            this.JumpButton.Click += new System.EventHandler(this.JumpButton_Click);
            // 
            // TotalPageShower
            // 
            this.TotalPageShower.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalPageShower.Location = new System.Drawing.Point(535, 7);
            this.TotalPageShower.Name = "TotalPageShower";
            this.TotalPageShower.ReadOnly = true;
            this.TotalPageShower.Size = new System.Drawing.Size(46, 21);
            this.TotalPageShower.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(522, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "/";
            // 
            // PageButtonArea
            // 
            this.PageButtonArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PageButtonArea.Controls.Add(this.PageButtonInnerArea);
            this.PageButtonArea.Location = new System.Drawing.Point(142, 0);
            this.PageButtonArea.Name = "PageButtonArea";
            this.PageButtonArea.Size = new System.Drawing.Size(189, 33);
            this.PageButtonArea.TabIndex = 19;
            // 
            // PageButtonInnerArea
            // 
            this.PageButtonInnerArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PageButtonInnerArea.Location = new System.Drawing.Point(6, 3);
            this.PageButtonInnerArea.Name = "PageButtonInnerArea";
            this.PageButtonInnerArea.Size = new System.Drawing.Size(177, 28);
            this.PageButtonInnerArea.TabIndex = 0;
            // 
            // PagingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PageButtonArea);
            this.Controls.Add(this.TotalPageShower);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JumpButton);
            this.Controls.Add(this.PageInput);
            this.Controls.Add(this.FirstButton);
            this.Controls.Add(this.LastButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PrevButton);
            this.Name = "PagingBox";
            this.Size = new System.Drawing.Size(649, 33);
            this.PageButtonArea.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FirstButton;
        private System.Windows.Forms.Button LastButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private ValueBox.IntegerBox PageInput;
        private System.Windows.Forms.Button JumpButton;
        private System.Windows.Forms.TextBox TotalPageShower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PageButtonArea;
        private System.Windows.Forms.Panel PageButtonInnerArea;
    }
}
