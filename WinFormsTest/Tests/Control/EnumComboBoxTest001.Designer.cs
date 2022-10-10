namespace WinFormsTest.Tests
{
    partial class EnumComboBoxTest001
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
            this.enumComboBox1 = new ChaoticWinformControl.EnumComboBox();
            this.SuspendLayout();
            // 
            // enumComboBox1
            // 
            this.enumComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumComboBox1.FormattingEnabled = true;
            this.enumComboBox1.Location = new System.Drawing.Point(124, 74);
            this.enumComboBox1.Name = "enumComboBox1";
            this.enumComboBox1.Size = new System.Drawing.Size(121, 20);
            this.enumComboBox1.TabIndex = 0;
            // 
            // EnumComboBoxTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.enumComboBox1);
            this.Name = "EnumComboBoxTest001";
            this.Text = "EnumComboBoxEnum";
            this.ResumeLayout(false);

        }

        #endregion

        private ChaoticWinformControl.EnumComboBox enumComboBox1;
    }
}