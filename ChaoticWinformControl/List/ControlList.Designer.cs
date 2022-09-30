namespace ChaoticWinformControl
{
    partial class ControlList<T>
        where T : ControlListItem
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
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ShowingArea = new System.Windows.Forms.Panel();
            this.InnerArea = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TableLayout.SuspendLayout();
            this.ShowingArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayout
            // 
            this.TableLayout.ColumnCount = 2;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.TableLayout.Controls.Add(this.ShowingArea, 0, 0);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(0, 0);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 2;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.TableLayout.Size = new System.Drawing.Size(398, 434);
            this.TableLayout.TabIndex = 0;
            this.TableLayout.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.TableLayout_CellPaint);
            // 
            // ShowingArea
            // 
            this.ShowingArea.AutoSize = true;
            this.ShowingArea.BackColor = System.Drawing.Color.Transparent;
            this.ShowingArea.Controls.Add(this.InnerArea);
            this.ShowingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowingArea.Location = new System.Drawing.Point(0, 0);
            this.ShowingArea.Margin = new System.Windows.Forms.Padding(0);
            this.ShowingArea.Name = "ShowingArea";
            this.ShowingArea.Size = new System.Drawing.Size(388, 424);
            this.ShowingArea.TabIndex = 0;
            // 
            // InnerArea
            // 
            this.InnerArea.AutoSize = true;
            this.InnerArea.BackColor = System.Drawing.Color.Transparent;
            this.InnerArea.Location = new System.Drawing.Point(0, 0);
            this.InnerArea.Margin = new System.Windows.Forms.Padding(0);
            this.InnerArea.Name = "InnerArea";
            this.InnerArea.Size = new System.Drawing.Size(87, 137);
            this.InnerArea.TabIndex = 0;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // ControlList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TableLayout);
            this.DoubleBuffered = true;
            this.Name = "ControlList";
            this.Size = new System.Drawing.Size(398, 434);
            this.TableLayout.ResumeLayout(false);
            this.TableLayout.PerformLayout();
            this.ShowingArea.ResumeLayout(false);
            this.ShowingArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.Panel ShowingArea;
        private System.Windows.Forms.Panel InnerArea;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
