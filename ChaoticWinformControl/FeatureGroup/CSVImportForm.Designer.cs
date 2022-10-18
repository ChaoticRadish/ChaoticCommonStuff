namespace ChaoticWinformControl.FeatureGroup
{
    partial class CSVImportForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.TargetTypeShower = new System.Windows.Forms.TextBox();
            this.RelateTable = new System.Windows.Forms.DataGridView();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ColumnIndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetColumnNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyNameColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PropertyTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IgnoreColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LoggerBox = new ChaoticWinformControl.LogShowerBox();
            this.FileInput = new ChaoticWinformControl.FileSelectTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.RelateTable)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前编辑类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据源:";
            // 
            // TargetTypeShower
            // 
            this.TargetTypeShower.Location = new System.Drawing.Point(102, 35);
            this.TargetTypeShower.Name = "TargetTypeShower";
            this.TargetTypeShower.ReadOnly = true;
            this.TargetTypeShower.Size = new System.Drawing.Size(480, 21);
            this.TargetTypeShower.TabIndex = 3;
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
            this.ColumnIndexColumn,
            this.TargetColumnNameColumn,
            this.PropertyNameColumn,
            this.PropertyTypeColumn,
            this.IgnoreColumn});
            this.RelateTable.Location = new System.Drawing.Point(12, 64);
            this.RelateTable.Name = "RelateTable";
            this.RelateTable.RowTemplate.Height = 23;
            this.RelateTable.Size = new System.Drawing.Size(594, 457);
            this.RelateTable.TabIndex = 4;
            this.RelateTable.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.RelateTable_EditingControlShowing);
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(835, 33);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(121, 23);
            this.ImportButton.TabIndex = 6;
            this.ImportButton.Text = "开始导入";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // ColumnIndexColumn
            // 
            this.ColumnIndexColumn.DataPropertyName = "ColumnIndex";
            this.ColumnIndexColumn.HeaderText = "列索引";
            this.ColumnIndexColumn.Name = "ColumnIndexColumn";
            this.ColumnIndexColumn.ReadOnly = true;
            // 
            // TargetColumnNameColumn
            // 
            this.TargetColumnNameColumn.DataPropertyName = "ImportColumnName";
            this.TargetColumnNameColumn.HeaderText = "输入列";
            this.TargetColumnNameColumn.Name = "TargetColumnNameColumn";
            this.TargetColumnNameColumn.ReadOnly = true;
            this.TargetColumnNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetColumnNameColumn.Width = 120;
            // 
            // PropertyNameColumn
            // 
            this.PropertyNameColumn.DataPropertyName = "PropertyName";
            this.PropertyNameColumn.HeaderText = "映射属性名";
            this.PropertyNameColumn.Name = "PropertyNameColumn";
            this.PropertyNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PropertyNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PropertyNameColumn.Width = 120;
            // 
            // PropertyTypeColumn
            // 
            this.PropertyTypeColumn.DataPropertyName = "PropertyType";
            this.PropertyTypeColumn.HeaderText = "类型";
            this.PropertyTypeColumn.Name = "PropertyTypeColumn";
            this.PropertyTypeColumn.ReadOnly = true;
            this.PropertyTypeColumn.Width = 150;
            // 
            // IgnoreColumn
            // 
            this.IgnoreColumn.DataPropertyName = "Ignore";
            this.IgnoreColumn.HeaderText = "忽略";
            this.IgnoreColumn.Name = "IgnoreColumn";
            this.IgnoreColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IgnoreColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IgnoreColumn.Width = 60;
            // 
            // LoggerBox
            // 
            this.LoggerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggerBox.Location = new System.Drawing.Point(612, 64);
            this.LoggerBox.Name = "LoggerBox";
            this.LoggerBox.Size = new System.Drawing.Size(344, 457);
            this.LoggerBox.TabIndex = 5;
            // 
            // FileInput
            // 
            this.FileInput.FileMustExist = true;
            this.FileInput.Filter = "逗号分隔值文件|*.csv";
            this.FileInput.Location = new System.Drawing.Point(102, 6);
            this.FileInput.Name = "FileInput";
            this.FileInput.ReadOnly = true;
            this.FileInput.SelectFormTitle = "选择文按键";
            this.FileInput.Size = new System.Drawing.Size(480, 21);
            this.FileInput.TabIndex = 2;
            this.FileInput.OnFileSelected += new ChaoticWinformControl.FileSelectTextBox.OnFileSelectedHandle(this.FileInput_OnFileSelected);
            // 
            // CSVImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 533);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.LoggerBox);
            this.Controls.Add(this.RelateTable);
            this.Controls.Add(this.TargetTypeShower);
            this.Controls.Add(this.FileInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CSVImportForm";
            this.Text = "导入 CSV 文件";
            ((System.ComponentModel.ISupportInitialize)(this.RelateTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FileSelectTextBox FileInput;
        private System.Windows.Forms.TextBox TargetTypeShower;
        private System.Windows.Forms.DataGridView RelateTable;
        private LogShowerBox LoggerBox;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetColumnNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn PropertyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IgnoreColumn;
    }
}