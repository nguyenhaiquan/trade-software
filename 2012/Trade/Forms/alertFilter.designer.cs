namespace Trade.Forms
{
    partial class alertFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(alertFilter));
            this.codeChk = new common.controls.baseCheckBox();
            this.codeEd = new baseClass.controls.txtStockCode();
            this.strategyChk = new common.controls.baseCheckBox();
            this.strategyCb = new baseClass.controls.cbStrategy();
            this.portfolioChk = new common.controls.baseCheckBox();
            this.portfolioCb = new baseClass.controls.cbWatchList();
            this.statusChk = new common.controls.baseCheckBox();
            this.dateRangeChk = new common.controls.baseCheckBox();
            this.statusCb = new baseClass.controls.cbTradeAlertStatus();
            this.dateRange = new common.controls.dateRange2();
            this.dummyLabel = new common.controls.baseLabel();
            this.timeScaleCb = new baseClass.controls.cbTimeScale();
            this.timeScaleChk = new common.controls.baseCheckBox();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.closeBtn.Location = new System.Drawing.Point(485, 63);
            this.closeBtn.Size = new System.Drawing.Size(90, 39);
            this.closeBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // okBtn
            // 
            this.okBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.okBtn.Location = new System.Drawing.Point(485, 23);
            this.okBtn.Size = new System.Drawing.Size(90, 40);
            this.okBtn.Text = "Ok";
            this.okBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // codeChk
            // 
            this.codeChk.AutoSize = true;
            this.codeChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeChk.Location = new System.Drawing.Point(376, 3);
            this.codeChk.Name = "codeChk";
            this.codeChk.Size = new System.Drawing.Size(59, 20);
            this.codeChk.TabIndex = 5;
            this.codeChk.Text = "Code";
            this.codeChk.UseVisualStyleBackColor = true;
            this.codeChk.CheckedChanged += new System.EventHandler(this.stockChk_CheckedChanged);
            // 
            // codeEd
            // 
            this.codeEd.BackColor = System.Drawing.Color.White;
            this.codeEd.Enabled = false;
            this.codeEd.ForeColor = System.Drawing.Color.Black;
            this.codeEd.Location = new System.Drawing.Point(373, 25);
            this.codeEd.Name = "codeEd";
            this.codeEd.Size = new System.Drawing.Size(94, 20);
            this.codeEd.TabIndex = 6;
            // 
            // strategyChk
            // 
            this.strategyChk.AutoSize = true;
            this.strategyChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strategyChk.Location = new System.Drawing.Point(37, 52);
            this.strategyChk.Name = "strategyChk";
            this.strategyChk.Size = new System.Drawing.Size(85, 20);
            this.strategyChk.TabIndex = 12;
            this.strategyChk.Text = "Strategy";
            this.strategyChk.UseVisualStyleBackColor = true;
            this.strategyChk.CheckedChanged += new System.EventHandler(this.strategyChk_CheckedChanged);
            // 
            // strategyCb
            // 
            this.strategyCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.strategyCb.Enabled = false;
            this.strategyCb.FormattingEnabled = true;
            this.strategyCb.Location = new System.Drawing.Point(38, 74);
            this.strategyCb.myValue = "";
            this.strategyCb.Name = "strategyCb";
            this.strategyCb.SelectedValue = "";
            this.strategyCb.Size = new System.Drawing.Size(240, 21);
            this.strategyCb.TabIndex = 13;
            // 
            // portfolioChk
            // 
            this.portfolioChk.AutoSize = true;
            this.portfolioChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioChk.Location = new System.Drawing.Point(202, 3);
            this.portfolioChk.Name = "portfolioChk";
            this.portfolioChk.Size = new System.Drawing.Size(82, 20);
            this.portfolioChk.TabIndex = 3;
            this.portfolioChk.Text = "Portfolio";
            this.portfolioChk.UseVisualStyleBackColor = true;
            this.portfolioChk.CheckedChanged += new System.EventHandler(this.portfolioChk_CheckedChanged);
            // 
            // portfolioCb
            // 
            this.portfolioCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portfolioCb.Enabled = false;
            this.portfolioCb.FormattingEnabled = true;
            this.portfolioCb.Location = new System.Drawing.Point(199, 25);
            this.portfolioCb.myValue = "";
            this.portfolioCb.Name = "portfolioCb";
            this.portfolioCb.Size = new System.Drawing.Size(176, 21);
            this.portfolioCb.TabIndex = 4;
            // 
            // statusChk
            // 
            this.statusChk.AutoSize = true;
            this.statusChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusChk.Location = new System.Drawing.Point(374, 52);
            this.statusChk.Name = "statusChk";
            this.statusChk.Size = new System.Drawing.Size(70, 20);
            this.statusChk.TabIndex = 10;
            this.statusChk.Text = "Status";
            this.statusChk.UseVisualStyleBackColor = true;
            this.statusChk.CheckedChanged += new System.EventHandler(this.statusChk_CheckedChanged);
            // 
            // dateRangeChk
            // 
            this.dateRangeChk.AutoSize = true;
            this.dateRangeChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRangeChk.Location = new System.Drawing.Point(39, 3);
            this.dateRangeChk.Name = "dateRangeChk";
            this.dateRangeChk.Size = new System.Drawing.Size(91, 20);
            this.dateRangeChk.TabIndex = 1;
            this.dateRangeChk.Text = "Date Time";
            this.dateRangeChk.UseVisualStyleBackColor = true;
            this.dateRangeChk.CheckedChanged += new System.EventHandler(this.dateRangeChk_CheckedChanged);
            // 
            // statusCb
            // 
            this.statusCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusCb.Enabled = false;
            this.statusCb.FormattingEnabled = true;
            this.statusCb.Location = new System.Drawing.Point(373, 74);
            this.statusCb.myValue = commonTypes.AppTypes.CommonStatus.None;
            this.statusCb.Name = "statusCb";
            this.statusCb.SelectedValue = ((byte)(0));
            this.statusCb.Size = new System.Drawing.Size(94, 21);
            this.statusCb.TabIndex = 11;
            // 
            // dateRange
            // 
            this.dateRange.Enabled = false;
            this.dateRange.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRange.Location = new System.Drawing.Point(37, 4);
            this.dateRange.Margin = new System.Windows.Forms.Padding(4);
            this.dateRange.Name = "dateRange";
            this.dateRange.Size = new System.Drawing.Size(162, 46);
            this.dateRange.TabIndex = 2;
            // 
            // dummyLabel
            // 
            this.dummyLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dummyLabel.Location = new System.Drawing.Point(38, 0);
            this.dummyLabel.Name = "dummyLabel";
            this.dummyLabel.Size = new System.Drawing.Size(149, 23);
            this.dummyLabel.TabIndex = 161;
            // 
            // timeScaleCb
            // 
            this.timeScaleCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeScaleCb.Enabled = false;
            this.timeScaleCb.FormattingEnabled = true;
            this.timeScaleCb.Location = new System.Drawing.Point(276, 74);
            this.timeScaleCb.Name = "timeScaleCb";
            this.timeScaleCb.Size = new System.Drawing.Size(98, 21);
            this.timeScaleCb.TabIndex = 15;
            // 
            // timeScaleChk
            // 
            this.timeScaleChk.AutoSize = true;
            this.timeScaleChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeScaleChk.Location = new System.Drawing.Point(279, 51);
            this.timeScaleChk.Name = "timeScaleChk";
            this.timeScaleChk.Size = new System.Drawing.Size(93, 20);
            this.timeScaleChk.TabIndex = 14;
            this.timeScaleChk.Text = "Time scale";
            this.timeScaleChk.UseVisualStyleBackColor = true;
            this.timeScaleChk.CheckedChanged += new System.EventHandler(this.timeScaleChk_CheckedChanged);
            // 
            // alertFilter
            // 
            this.ClientSize = new System.Drawing.Size(613, 141);
            this.Controls.Add(this.timeScaleChk);
            this.Controls.Add(this.timeScaleCb);
            this.Controls.Add(this.codeChk);
            this.Controls.Add(this.codeEd);
            this.Controls.Add(this.strategyChk);
            this.Controls.Add(this.strategyCb);
            this.Controls.Add(this.portfolioChk);
            this.Controls.Add(this.portfolioCb);
            this.Controls.Add(this.statusChk);
            this.Controls.Add(this.dateRangeChk);
            this.Controls.Add(this.statusCb);
            this.Controls.Add(this.dummyLabel);
            this.Controls.Add(this.dateRange);
            
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "alertFilter";
            this.Text = "Loc du lieu";
            this.Controls.SetChildIndex(this.dateRange, 0);
            this.Controls.SetChildIndex(this.dummyLabel, 0);
            this.Controls.SetChildIndex(this.TitleLbl, 0);
            this.Controls.SetChildIndex(this.closeBtn, 0);
            this.Controls.SetChildIndex(this.okBtn, 0);
            this.Controls.SetChildIndex(this.statusCb, 0);
            this.Controls.SetChildIndex(this.dateRangeChk, 0);
            this.Controls.SetChildIndex(this.statusChk, 0);
            this.Controls.SetChildIndex(this.portfolioCb, 0);
            this.Controls.SetChildIndex(this.portfolioChk, 0);
            this.Controls.SetChildIndex(this.strategyCb, 0);
            this.Controls.SetChildIndex(this.strategyChk, 0);
            this.Controls.SetChildIndex(this.codeEd, 0);
            this.Controls.SetChildIndex(this.codeChk, 0);
            this.Controls.SetChildIndex(this.timeScaleCb, 0);
            this.Controls.SetChildIndex(this.timeScaleChk, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected common.controls.baseCheckBox codeChk;
        protected baseClass.controls.txtStockCode codeEd;
        protected common.controls.baseCheckBox strategyChk;
        protected baseClass.controls.cbStrategy strategyCb;
        protected common.controls.baseCheckBox portfolioChk;
        protected baseClass.controls.cbWatchList portfolioCb;
        protected common.controls.baseCheckBox statusChk;
        protected common.controls.baseCheckBox dateRangeChk;
        protected baseClass.controls.cbTradeAlertStatus statusCb;
        protected common.controls.baseLabel dummyLabel;
        protected common.controls.dateRange2 dateRange;
        private baseClass.controls.cbTimeScale timeScaleCb;
        protected common.controls.baseCheckBox timeScaleChk;


    }
}