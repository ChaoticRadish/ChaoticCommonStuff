namespace WinFormsTest.Tests
{
    partial class RandomTest001
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
            this.ObjShower = new System.Windows.Forms.DataGridView();
            this.TypeShower = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ObjShower)).BeginInit();
            this.SuspendLayout();
            // 
            // ObjShower
            // 
            this.ObjShower.AllowUserToAddRows = false;
            this.ObjShower.AllowUserToDeleteRows = false;
            this.ObjShower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjShower.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ObjShower.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ObjShower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjShower.Location = new System.Drawing.Point(12, 12);
            this.ObjShower.Name = "ObjShower";
            this.ObjShower.ReadOnly = true;
            this.ObjShower.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ObjShower.RowTemplate.Height = 23;
            this.ObjShower.Size = new System.Drawing.Size(531, 426);
            this.ObjShower.TabIndex = 1;
            // 
            // TypeShower
            // 
            this.TypeShower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeShower.Location = new System.Drawing.Point(551, 27);
            this.TypeShower.Name = "TypeShower";
            this.TypeShower.ReadOnly = true;
            this.TypeShower.Size = new System.Drawing.Size(170, 101);
            this.TypeShower.TabIndex = 7;
            this.TypeShower.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(549, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "当前类型: ";
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateButton.Location = new System.Drawing.Point(551, 134);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(170, 23);
            this.CreateButton.TabIndex = 5;
            this.CreateButton.Text = "生成随机数据";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // RandomTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.TypeShower);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.ObjShower);
            this.Name = "RandomTest001";
            this.Text = "RandomTest001";
            ((System.ComponentModel.ISupportInitialize)(this.ObjShower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView ObjShower;
        private RichTextBox TypeShower;
        private Label label1;
        private Button CreateButton;
    }
}