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
            this.SuspendLayout();
            // 
            // dateRangeSimpleInputBox1
            // 
            this.dateRangeSimpleInputBox1.Location = new System.Drawing.Point(12, 12);
            this.dateRangeSimpleInputBox1.Name = "dateRangeSimpleInputBox1";
            this.dateRangeSimpleInputBox1.Size = new System.Drawing.Size(304, 49);
            this.dateRangeSimpleInputBox1.TabIndex = 0;
            // 
            // DateRangeInputerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dateRangeSimpleInputBox1);
            this.Name = "DateRangeInputerTest";
            this.Text = "DateRangeSimpleInputBoxTest";
            this.ResumeLayout(false);

        }

        #endregion

        private ChaoticWinformControl.FeatureGroup.DateRangeSimpleInputBox dateRangeSimpleInputBox1;
    }
}