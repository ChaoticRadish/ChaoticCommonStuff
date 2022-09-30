namespace ChaoticWinformControl.FeatureGroup
{
    partial class DateRangeSimpleInputBox
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
            this.DateInput = new System.Windows.Forms.DateTimePicker();
            this.Option1Box = new System.Windows.Forms.RadioButton();
            this.Option3Box = new System.Windows.Forms.RadioButton();
            this.Option2Box = new System.Windows.Forms.RadioButton();
            this.Option4Box = new System.Windows.Forms.RadioButton();
            this.WorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DateInput
            // 
            this.DateInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInput.Location = new System.Drawing.Point(7, 3);
            this.DateInput.Name = "DateInput";
            this.DateInput.Size = new System.Drawing.Size(131, 21);
            this.DateInput.TabIndex = 0;
            // 
            // Option1Box
            // 
            this.Option1Box.AutoSize = true;
            this.Option1Box.Location = new System.Drawing.Point(144, 7);
            this.Option1Box.Name = "Option1Box";
            this.Option1Box.Size = new System.Drawing.Size(53, 16);
            this.Option1Box.TabIndex = 1;
            this.Option1Box.TabStop = true;
            this.Option1Box.Text = "选项1";
            this.Option1Box.UseVisualStyleBackColor = true;
            // 
            // Option3Box
            // 
            this.Option3Box.AutoSize = true;
            this.Option3Box.Location = new System.Drawing.Point(222, 8);
            this.Option3Box.Name = "Option3Box";
            this.Option3Box.Size = new System.Drawing.Size(53, 16);
            this.Option3Box.TabIndex = 2;
            this.Option3Box.TabStop = true;
            this.Option3Box.Text = "选项3";
            this.Option3Box.UseVisualStyleBackColor = true;
            // 
            // Option2Box
            // 
            this.Option2Box.AutoSize = true;
            this.Option2Box.Location = new System.Drawing.Point(144, 29);
            this.Option2Box.Name = "Option2Box";
            this.Option2Box.Size = new System.Drawing.Size(53, 16);
            this.Option2Box.TabIndex = 3;
            this.Option2Box.TabStop = true;
            this.Option2Box.Text = "选项2";
            this.Option2Box.UseVisualStyleBackColor = true;
            // 
            // Option4Box
            // 
            this.Option4Box.AutoSize = true;
            this.Option4Box.Location = new System.Drawing.Point(222, 29);
            this.Option4Box.Name = "Option4Box";
            this.Option4Box.Size = new System.Drawing.Size(53, 16);
            this.Option4Box.TabIndex = 4;
            this.Option4Box.TabStop = true;
            this.Option4Box.Text = "选项4";
            this.Option4Box.UseVisualStyleBackColor = true;
            // 
            // WorkingCheckBox
            // 
            this.WorkingCheckBox.AutoSize = true;
            this.WorkingCheckBox.Location = new System.Drawing.Point(7, 29);
            this.WorkingCheckBox.Name = "WorkingCheckBox";
            this.WorkingCheckBox.Size = new System.Drawing.Size(96, 16);
            this.WorkingCheckBox.TabIndex = 5;
            this.WorkingCheckBox.Text = "使用日期范围";
            this.WorkingCheckBox.UseVisualStyleBackColor = true;
            // 
            // DateRangeSimpleInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WorkingCheckBox);
            this.Controls.Add(this.Option4Box);
            this.Controls.Add(this.Option2Box);
            this.Controls.Add(this.Option3Box);
            this.Controls.Add(this.Option1Box);
            this.Controls.Add(this.DateInput);
            this.Name = "DateRangeSimpleInputBox";
            this.Size = new System.Drawing.Size(304, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateInput;
        private System.Windows.Forms.RadioButton Option1Box;
        private System.Windows.Forms.RadioButton Option3Box;
        private System.Windows.Forms.RadioButton Option2Box;
        private System.Windows.Forms.RadioButton Option4Box;
        private System.Windows.Forms.CheckBox WorkingCheckBox;
    }
}
