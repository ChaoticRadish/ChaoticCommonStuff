namespace WinFormsTest
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MoitoringsListView = new System.Windows.Forms.ListView();
            this.MoitoringTimeLabel = new System.Windows.Forms.Label();
            this.GlobalListView = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Logger = new ChaoticWinformControl.LogShowerBox();
            this.GlobalItemRightMenuMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Value_Bool_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoSupport_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Value_String_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.GlobalItemRightMenuMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Logger);
            this.splitContainer1.Size = new System.Drawing.Size(1066, 778);
            this.splitContainer1.SplitterDistance = 514;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.MoitoringsListView);
            this.splitContainer2.Panel1.Controls.Add(this.MoitoringTimeLabel);
            this.splitContainer2.Panel1.Controls.Add(this.GlobalListView);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(1066, 514);
            this.splitContainer2.SplitterDistance = 278;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // MoitoringsListView
            // 
            this.MoitoringsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoitoringsListView.HideSelection = false;
            this.MoitoringsListView.Location = new System.Drawing.Point(13, 23);
            this.MoitoringsListView.Name = "MoitoringsListView";
            this.MoitoringsListView.Size = new System.Drawing.Size(250, 244);
            this.MoitoringsListView.TabIndex = 1;
            this.MoitoringsListView.UseCompatibleStateImageBehavior = false;
            this.MoitoringsListView.View = System.Windows.Forms.View.List;
            // 
            // MoitoringTimeLabel
            // 
            this.MoitoringTimeLabel.AutoSize = true;
            this.MoitoringTimeLabel.Location = new System.Drawing.Point(70, 8);
            this.MoitoringTimeLabel.Name = "MoitoringTimeLabel";
            this.MoitoringTimeLabel.Size = new System.Drawing.Size(53, 12);
            this.MoitoringTimeLabel.TabIndex = 4;
            this.MoitoringTimeLabel.Text = "轮询间隔";
            // 
            // GlobalListView
            // 
            this.GlobalListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GlobalListView.HideSelection = false;
            this.GlobalListView.Location = new System.Drawing.Point(13, 285);
            this.GlobalListView.Name = "GlobalListView";
            this.GlobalListView.Size = new System.Drawing.Size(250, 224);
            this.GlobalListView.TabIndex = 3;
            this.GlobalListView.UseCompatibleStateImageBehavior = false;
            this.GlobalListView.View = System.Windows.Forms.View.List;
            this.GlobalListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GlobalListView_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "全局对象";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "监听对象";
            // 
            // Logger
            // 
            this.Logger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Logger.Location = new System.Drawing.Point(0, 0);
            this.Logger.Name = "Logger";
            this.Logger.Size = new System.Drawing.Size(1064, 259);
            this.Logger.TabIndex = 0;
            // 
            // GlobalItemRightMenuMenuStrip
            // 
            this.GlobalItemRightMenuMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Value_Bool_ToolStripMenuItem,
            this.Value_String_ToolStripMenuItem,
            this.NoSupport_ToolStripMenuItem});
            this.GlobalItemRightMenuMenuStrip.Name = "GlobalItemRightMenuMenuStrip";
            this.GlobalItemRightMenuMenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // Value_Bool_ToolStripMenuItem
            // 
            this.Value_Bool_ToolStripMenuItem.Name = "Value_Bool_ToolStripMenuItem";
            this.Value_Bool_ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.Value_Bool_ToolStripMenuItem.Text = "切换bool值";
            this.Value_Bool_ToolStripMenuItem.Click += new System.EventHandler(this.Value_Bool_ToolStripMenuItem_Click);
            // 
            // NoSupport_ToolStripMenuItem
            // 
            this.NoSupport_ToolStripMenuItem.Enabled = false;
            this.NoSupport_ToolStripMenuItem.Name = "NoSupport_ToolStripMenuItem";
            this.NoSupport_ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NoSupport_ToolStripMenuItem.Text = "不支持的类型";
            // 
            // Value_String_ToolStripMenuItem
            // 
            this.Value_String_ToolStripMenuItem.Name = "Value_String_ToolStripMenuItem";
            this.Value_String_ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.Value_String_ToolStripMenuItem.Text = "修改字符串";
            this.Value_String_ToolStripMenuItem.Click += new System.EventHandler(this.Value_String_ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 778);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.GlobalItemRightMenuMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private ChaoticWinformControl.LogShowerBox Logger;
        private ListView GlobalListView;
        private Label label2;
        private ListView MoitoringsListView;
        private Label label1;
        private Label MoitoringTimeLabel;
        private ContextMenuStrip GlobalItemRightMenuMenuStrip;
        private ToolStripMenuItem Value_Bool_ToolStripMenuItem;
        private ToolStripMenuItem NoSupport_ToolStripMenuItem;
        private ToolStripMenuItem Value_String_ToolStripMenuItem;
    }
}