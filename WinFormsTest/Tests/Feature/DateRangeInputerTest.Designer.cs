namespace WinFormsTest.Tests
{
    partial class DateRangeInputerTest
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
            this.dateRangeSimpleInputBox1 = new ChaoticWinformControl.FeatureGroup.DateRangeSimpleInputBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateRangeSimpleInputBox1
            // 
            this.dateRangeSimpleInputBox1.CurrentOption = ChaoticWinformControl.FeatureGroup.DateRangeSimpleInputBox.OptionEnum.Option1;
            this.dateRangeSimpleInputBox1.DayOption1 = ((uint)(3u));
            this.dateRangeSimpleInputBox1.DayOption2 = ((uint)(7u));
            this.dateRangeSimpleInputBox1.DayOption3 = ((uint)(14u));
            this.dateRangeSimpleInputBox1.DayOption4 = ((uint)(30u));
            this.dateRangeSimpleInputBox1.DefaultDate = -3;
            this.dateRangeSimpleInputBox1.InnerText = "使用日期范围";
            this.dateRangeSimpleInputBox1.Location = new System.Drawing.Point(12, 25);
            this.dateRangeSimpleInputBox1.Name = "dateRangeSimpleInputBox1";
            this.dateRangeSimpleInputBox1.Size = new System.Drawing.Size(304, 49);
            this.dateRangeSimpleInputBox1.TabIndex = 0;
            this.dateRangeSimpleInputBox1.Working = true;
            this.dateRangeSimpleInputBox1.OnSelectedRangeChanged += new ChaoticWinformControl.FeatureGroup.DateRangeSimpleInputBox.OnSelectedRangeChangedDelegate(this.dateRangeSimpleInputBox1_OnSelectedRangeChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(345, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打印当前选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DateRangeInputerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateRangeSimpleInputBox1);
            this.Name = "DateRangeInputerTest";
            this.Text = "DateRangeSimpleInputBoxTest";
            this.ResumeLayout(false);

        }

        #endregion

        private ChaoticWinformControl.FeatureGroup.DateRangeSimpleInputBox dateRangeSimpleInputBox1;
        private Button button1;
    }
}