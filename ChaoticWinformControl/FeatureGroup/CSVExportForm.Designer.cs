namespace ChaoticWinformControl.FeatureGroup
{
    partial class CSVExportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.RelateTable = new System.Windows.Forms.DataGridView();
            this.ExportButton = new System.Windows.Forms.Button();
            this.FileInput = new ChaoticWinformControl.FileSelectTextBox();
            this.PropertyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetColumnNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsExportColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RelateTable)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出文件: ";
            // 
            // RelateTable
            // 
            this.RelateTable.AllowUserToAddRows = false;
            this.RelateTable.AllowUserToDeleteRows = false;
            this.RelateTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RelateTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RelateTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyNameColumn,
            this.PropertyTypeColumn,
            this.TargetColumnNameColumn,
            this.IsExportColumn});
            this.RelateTable.Location = new System.Drawing.Point(12, 34);
            this.RelateTable.Name = "RelateTable";
            this.RelateTable.RowTemplate.Height = 23;
            this.RelateTable.Size = new System.Drawing.Size(776, 404);
            this.RelateTable.TabIndex = 1;
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(713, 6);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 3;
            this.ExportButton.Text = "导出";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // FileInput
            // 
            this.FileInput.Filter = "逗号分隔值文件|*.csv";
            this.FileInput.Location = new System.Drawing.Point(83, 6);
            this.FileInput.Name = "FileInput";
            this.FileInput.ReadOnly = true;
            this.FileInput.SelectFormTitle = "设置输出文件名";
            this.FileInput.Size = new System.Drawing.Size(396, 21);
            this.FileInput.TabIndex = 2;
            // 
            // PropertyNameColumn
            // 
            this.PropertyNameColumn.DataPropertyName = "PropertyName";
            this.PropertyNameColumn.HeaderText = "属性名";
            this.PropertyNameColumn.Name = "PropertyNameColumn";
            this.PropertyNameColumn.Width = 120;
            // 
            // PropertyTypeColumn
            // 
            this.PropertyTypeColumn.DataPropertyName = "PropertyType";
            this.PropertyTypeColumn.HeaderText = "类型";
            this.PropertyTypeColumn.Name = "PropertyTypeColumn";
            this.PropertyTypeColumn.Width = 150;
            // 
            // TargetColumnNameColumn
            // 
            this.TargetColumnNameColumn.DataPropertyName = "TargetColumnName";
            this.TargetColumnNameColumn.HeaderText = "目标列";
            this.TargetColumnNameColumn.Name = "TargetColumnNameColumn";
            this.TargetColumnNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetColumnNameColumn.Width = 120;
            // 
            // IsExportColumn
            // 
            this.IsExportColumn.DataPropertyName = "IsExport";
            this.IsExportColumn.HeaderText = "是否输出";
            this.IsExportColumn.Name = "IsExportColumn";
            this.IsExportColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsExportColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CSVExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.FileInput);
            this.Controls.Add(this.RelateTable);
            this.Controls.Add(this.label1);
            this.Name = "CSVExportForm";
            this.Text = "导出 CSV 文件";
            ((System.ComponentModel.ISupportInitialize)(this.RelateTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView RelateTable;
        private FileSelectTextBox FileInput;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetColumnNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExportColumn;
    }
}