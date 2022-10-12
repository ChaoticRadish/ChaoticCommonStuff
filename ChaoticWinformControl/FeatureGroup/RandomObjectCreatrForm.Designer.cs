namespace ChaoticWinformControl.FeatureGroup
{
    partial class RandomObjectCreatrForm
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
            this.TypeShower = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            this.ObjShower = new System.Windows.Forms.DataGridView();
            this.TypeStringColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ObjShower)).BeginInit();
            this.SuspendLayout();
            // 
            // TypeShower
            // 
            this.TypeShower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeShower.Location = new System.Drawing.Point(514, 28);
            this.TypeShower.Name = "TypeShower";
            this.TypeShower.ReadOnly = true;
            this.TypeShower.Size = new System.Drawing.Size(170, 101);
            this.TypeShower.TabIndex = 11;
            this.TypeShower.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(512, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "当前类型: ";
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateButton.Location = new System.Drawing.Point(514, 135);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(170, 23);
            this.CreateButton.TabIndex = 9;
            this.CreateButton.Text = "生成随机数据";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
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
            this.ObjShower.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeStringColumn,
            this.NameColumn,
            this.ValueColumn});
            this.ObjShower.Location = new System.Drawing.Point(12, 12);
            this.ObjShower.Name = "ObjShower";
            this.ObjShower.ReadOnly = true;
            this.ObjShower.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ObjShower.RowTemplate.Height = 23;
            this.ObjShower.Size = new System.Drawing.Size(494, 426);
            this.ObjShower.TabIndex = 8;
            // 
            // TypeStringColumn
            // 
            this.TypeStringColumn.DataPropertyName = "TypeString";
            this.TypeStringColumn.HeaderText = "类型";
            this.TypeStringColumn.Name = "TypeStringColumn";
            this.TypeStringColumn.ReadOnly = true;
            this.TypeStringColumn.Width = 54;
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "属性名";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 66;
            // 
            // ValueColumn
            // 
            this.ValueColumn.DataPropertyName = "Value";
            this.ValueColumn.HeaderText = "值";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            this.ValueColumn.Width = 42;
            // 
            // RandomObjectCreatrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 450);
            this.Controls.Add(this.TypeShower);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.ObjShower);
            this.Name = "RandomObjectCreatrForm";
            this.Text = "随机对象生成窗口";
            ((System.ComponentModel.ISupportInitialize)(this.ObjShower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox TypeShower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.DataGridView ObjShower;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeStringColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
    }
}