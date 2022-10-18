namespace WinFormsTest.Tests
{
    partial class CSVFormTest001
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
            this.ExportForm = new System.Windows.Forms.Button();
            this.DataShower = new System.Windows.Forms.DataGridView();
            this.ImportButton = new System.Windows.Forms.Button();
            this.AddTestButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataShower)).BeginInit();
            this.SuspendLayout();
            // 
            // ExportForm
            // 
            this.ExportForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportForm.Location = new System.Drawing.Point(660, 12);
            this.ExportForm.Name = "ExportForm";
            this.ExportForm.Size = new System.Drawing.Size(128, 23);
            this.ExportForm.TabIndex = 0;
            this.ExportForm.Text = "导出";
            this.ExportForm.UseVisualStyleBackColor = true;
            this.ExportForm.Click += new System.EventHandler(this.ExportForm_Click);
            // 
            // DataShower
            // 
            this.DataShower.AllowUserToAddRows = false;
            this.DataShower.AllowUserToDeleteRows = false;
            this.DataShower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataShower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataShower.Location = new System.Drawing.Point(12, 12);
            this.DataShower.Name = "DataShower";
            this.DataShower.ReadOnly = true;
            this.DataShower.RowTemplate.Height = 23;
            this.DataShower.Size = new System.Drawing.Size(642, 426);
            this.DataShower.TabIndex = 1;
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(660, 41);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(128, 23);
            this.ImportButton.TabIndex = 2;
            this.ImportButton.Text = "导入";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // AddTestButton
            // 
            this.AddTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTestButton.Location = new System.Drawing.Point(660, 132);
            this.AddTestButton.Name = "AddTestButton";
            this.AddTestButton.Size = new System.Drawing.Size(128, 23);
            this.AddTestButton.TabIndex = 3;
            this.AddTestButton.Text = "写入测试";
            this.AddTestButton.UseVisualStyleBackColor = true;
            this.AddTestButton.Click += new System.EventHandler(this.AddTestButton_Click);
            // 
            // CSVFormTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AddTestButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.DataShower);
            this.Controls.Add(this.ExportForm);
            this.Name = "CSVFormTest001";
            this.Text = "CSVFormTest001";
            ((System.ComponentModel.ISupportInitialize)(this.DataShower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button ExportForm;
        private DataGridView DataShower;
        private Button ImportButton;
        private Button AddTestButton;
    }
}