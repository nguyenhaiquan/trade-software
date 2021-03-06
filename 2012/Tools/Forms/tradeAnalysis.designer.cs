namespace Tools.Forms
{
    partial class tradeAnalysis
    {
        //common.libs.appAvailable myAvail = new common.libs.appAvailable();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tradeAnalysis));
            this.activeIndicatorLV = new common.controls.ListViewAdv();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editIndicatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeIndicatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLbl
            // 
            this.TitleLbl.Location = new System.Drawing.Point(717, 47);
            this.TitleLbl.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TitleLbl.Size = new System.Drawing.Size(27, 24);
            // 
            // activeIndicatorLV
            // 
            this.activeIndicatorLV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.activeIndicatorLV.BackColor = System.Drawing.SystemColors.Control;
            this.activeIndicatorLV.ContextMenuStrip = this.contextMenu;
            this.activeIndicatorLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.activeIndicatorLV.Location = new System.Drawing.Point(0, 0);
            this.activeIndicatorLV.Name = "activeIndicatorLV";
            this.activeIndicatorLV.ShowItemToolTips = true;
            this.activeIndicatorLV.Size = new System.Drawing.Size(682, 26);
            this.activeIndicatorLV.TabIndex = 247;
            this.activeIndicatorLV.UseCompatibleStateImageBehavior = false;
            this.activeIndicatorLV.View = System.Windows.Forms.View.SmallIcon;
            this.activeIndicatorLV.DoubleClick += new System.EventHandler(this.removeIndicatorMenuItem_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editIndicatorMenuItem,
            this.removeIndicatorMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // editIndicatorMenuItem
            // 
            this.editIndicatorMenuItem.Name = "editIndicatorMenuItem";
            this.editIndicatorMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editIndicatorMenuItem.Text = "Edit";
            this.editIndicatorMenuItem.Click += new System.EventHandler(this.editIndicatorMenuItem_Click);
            // 
            // removeIndicatorMenuItem
            // 
            this.removeIndicatorMenuItem.Name = "removeIndicatorMenuItem";
            this.removeIndicatorMenuItem.Size = new System.Drawing.Size(124, 22);
            this.removeIndicatorMenuItem.Text = "Remove";
            this.removeIndicatorMenuItem.Click += new System.EventHandler(this.removeIndicatorMenuItem_Click);
            // 
            // tradeAnalysis
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(681, 500);
            this.Controls.Add(this.activeIndicatorLV);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "tradeAnalysis";
            this.Controls.SetChildIndex(this.TitleLbl, 0);
            this.Controls.SetChildIndex(this.activeIndicatorLV, 0);
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private common.controls.ListViewAdv activeIndicatorLV;
        protected System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem editIndicatorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeIndicatorMenuItem;
    }
}