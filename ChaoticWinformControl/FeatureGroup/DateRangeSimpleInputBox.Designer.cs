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
            this.components = new System.ComponentModel.Container();
            this.DateInput = new System.Windows.Forms.DateTimePicker();
            this.Option1Box = new System.Windows.Forms.RadioButton();
            this.Option3Box = new System.Windows.Forms.RadioButton();
            this.Option2Box = new System.Windows.Forms.RadioButton();
            this.Option4Box = new System.Windows.Forms.RadioButton();
            this.WorkingCheckBox = new System.Windows.Forms.CheckBox();
            this.RightMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetThisMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenuContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DateInput
            // 
            this.DateInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateInput.Checked = false;
            this.DateInput.Location = new System.Drawing.Point(7, 3);
            this.DateInput.Name = "DateInput";
            this.DateInput.Size = new System.Drawing.Size(131, 21);
            this.DateInput.TabIndex = 0;
            this.DateInput.ValueChanged += new System.EventHandler(this.DateInput_ValueChanged);
            this.DateInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
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
            this.Option1Box.CheckedChanged += new System.EventHandler(this.Option1Box_CheckedChanged);
            this.Option1Box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
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
            this.Option3Box.CheckedChanged += new System.EventHandler(this.Option3Box_CheckedChanged);
            this.Option3Box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
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
            this.Option2Box.CheckedChanged += new System.EventHandler(this.Option2Box_CheckedChanged);
            this.Option2Box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
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
            this.Option4Box.CheckedChanged += new System.EventHandler(this.Option4Box_CheckedChanged);
            this.Option4Box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
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
            this.WorkingCheckBox.CheckedChanged += new System.EventHandler(this.WorkingCheckBox_CheckedChanged);
            this.WorkingCheckBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildControl_MouseUp);
            // 
            // RightMenuContextMenuStrip
            // 
            this.RightMenuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetThisMonthToolStripMenuItem});
            this.RightMenuContextMenuStrip.Name = "RightMenuContextMenuStrip";
            this.RightMenuContextMenuStrip.Size = new System.Drawing.Size(137, 26);
            // 
            // SetThisMonthToolStripMenuItem
            // 
            this.SetThisMonthToolStripMenuItem.Name = "SetThisMonthToolStripMenuItem";
            this.SetThisMonthToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.SetThisMonthToolStripMenuItem.Text = "设为该月份";
            this.SetThisMonthToolStripMenuItem.Click += new System.EventHandler(this.SetThisMonthToolStripMenuItem_Click);
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
            this.RightMenuContextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip RightMenuContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SetThisMonthToolStripMenuItem;
    }
}
