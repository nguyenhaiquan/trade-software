namespace server
{
    partial class frmSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedule));
            this.fetchStockLbl = new common.controls.baseLabel();
            this.tradeAlertLbl = new common.controls.baseLabel();
            this.scheduleGb = new System.Windows.Forms.GroupBox();
            this.cboxFetchDataHSTC = new common.controls.baseCheckBox();
            this.cboxDerivativeFetch = new common.controls.baseCheckBox();
            this.tradeAlertChk = new common.controls.baseCheckBox();
            this.cboxfetchDataHOSE = new common.controls.baseCheckBox();
            this.basePanel1 = new common.controls.basePanel();
            this.pauseBtn = new common.controls.baseButton();
            this.startBtn = new common.controls.baseButton();
            this.btnDevivativeFetch = new System.Windows.Forms.Button();
            this.viewLogBtn = new common.controls.baseButton();
            this.runBtn = new common.controls.baseButton();
            this.timerAlert = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.scheduleGb.SuspendLayout();
            this.basePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLbl
            // 
            this.TitleLbl.Location = new System.Drawing.Point(553, 71);
            this.TitleLbl.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TitleLbl.Size = new System.Drawing.Size(154, 33);
            // 
            // fetchStockLbl
            // 
            this.fetchStockLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fetchStockLbl.Location = new System.Drawing.Point(331, 42);
            this.fetchStockLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fetchStockLbl.Name = "fetchStockLbl";
            this.fetchStockLbl.Size = new System.Drawing.Size(77, 25);
            this.fetchStockLbl.TabIndex = 3;
            this.fetchStockLbl.Text = "secs";
            // 
            // tradeAlertLbl
            // 
            this.tradeAlertLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tradeAlertLbl.Location = new System.Drawing.Point(331, 82);
            this.tradeAlertLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tradeAlertLbl.Name = "tradeAlertLbl";
            this.tradeAlertLbl.Size = new System.Drawing.Size(55, 25);
            this.tradeAlertLbl.TabIndex = 6;
            this.tradeAlertLbl.Text = "secs";
            // 
            // scheduleGb
            // 
            this.scheduleGb.Controls.Add(this.cboxFetchDataHSTC);
            this.scheduleGb.Controls.Add(this.cboxDerivativeFetch);
            this.scheduleGb.Controls.Add(this.tradeAlertChk);
            this.scheduleGb.Controls.Add(this.cboxfetchDataHOSE);
            this.scheduleGb.Controls.Add(this.tradeAlertLbl);
            this.scheduleGb.Controls.Add(this.fetchStockLbl);
            this.scheduleGb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scheduleGb.Location = new System.Drawing.Point(1, 59);
            this.scheduleGb.Margin = new System.Windows.Forms.Padding(2);
            this.scheduleGb.Name = "scheduleGb";
            this.scheduleGb.Padding = new System.Windows.Forms.Padding(2);
            this.scheduleGb.Size = new System.Drawing.Size(600, 224);
            this.scheduleGb.TabIndex = 10;
            this.scheduleGb.TabStop = false;
            this.scheduleGb.Text = " Scheduling ";
            // 
            // cboxFetchDataHSTC
            // 
            this.cboxFetchDataHSTC.AutoSize = true;
            this.cboxFetchDataHSTC.Checked = true;
            this.cboxFetchDataHSTC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxFetchDataHSTC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxFetchDataHSTC.Location = new System.Drawing.Point(20, 121);
            this.cboxFetchDataHSTC.Margin = new System.Windows.Forms.Padding(2);
            this.cboxFetchDataHSTC.Name = "cboxFetchDataHSTC";
            this.cboxFetchDataHSTC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboxFetchDataHSTC.Size = new System.Drawing.Size(189, 25);
            this.cboxFetchDataHSTC.TabIndex = 12;
            this.cboxFetchDataHSTC.Text = "Fetch  data HASTC";
            this.cboxFetchDataHSTC.UseVisualStyleBackColor = true;
            // 
            // cboxDerivativeFetch
            // 
            this.cboxDerivativeFetch.AutoSize = true;
            this.cboxDerivativeFetch.Checked = true;
            this.cboxDerivativeFetch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxDerivativeFetch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxDerivativeFetch.Location = new System.Drawing.Point(11, 42);
            this.cboxDerivativeFetch.Margin = new System.Windows.Forms.Padding(2);
            this.cboxDerivativeFetch.Name = "cboxDerivativeFetch";
            this.cboxDerivativeFetch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboxDerivativeFetch.Size = new System.Drawing.Size(224, 25);
            this.cboxDerivativeFetch.TabIndex = 11;
            this.cboxDerivativeFetch.Text = "Fetch  Derivative data ";
            this.cboxDerivativeFetch.UseVisualStyleBackColor = true;
            // 
            // tradeAlertChk
            // 
            this.tradeAlertChk.AutoSize = true;
            this.tradeAlertChk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tradeAlertChk.Location = new System.Drawing.Point(20, 185);
            this.tradeAlertChk.Margin = new System.Windows.Forms.Padding(2);
            this.tradeAlertChk.Name = "tradeAlertChk";
            this.tradeAlertChk.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tradeAlertChk.Size = new System.Drawing.Size(131, 25);
            this.tradeAlertChk.TabIndex = 10;
            this.tradeAlertChk.Text = "Create alert";
            this.tradeAlertChk.UseVisualStyleBackColor = true;
            // 
            // cboxfetchDataHOSE
            // 
            this.cboxfetchDataHOSE.AutoSize = true;
            this.cboxfetchDataHOSE.Checked = true;
            this.cboxfetchDataHOSE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxfetchDataHOSE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxfetchDataHOSE.Location = new System.Drawing.Point(20, 80);
            this.cboxfetchDataHOSE.Margin = new System.Windows.Forms.Padding(2);
            this.cboxfetchDataHOSE.Name = "cboxfetchDataHOSE";
            this.cboxfetchDataHOSE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboxfetchDataHOSE.Size = new System.Drawing.Size(179, 25);
            this.cboxfetchDataHOSE.TabIndex = 1;
            this.cboxfetchDataHOSE.Text = "Fetch  data HOSE";
            this.cboxfetchDataHOSE.UseVisualStyleBackColor = true;
            // 
            // basePanel1
            // 
            this.basePanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.basePanel1.Controls.Add(this.pauseBtn);
            this.basePanel1.Controls.Add(this.startBtn);
            this.basePanel1.Controls.Add(this.btnDevivativeFetch);
            this.basePanel1.Controls.Add(this.viewLogBtn);
            this.basePanel1.Controls.Add(this.runBtn);
            this.basePanel1.haveCloseButton = false;
            this.basePanel1.isExpanded = true;
            this.basePanel1.Location = new System.Drawing.Point(0, 0);
            this.basePanel1.Margin = new System.Windows.Forms.Padding(2);
            this.basePanel1.myIconLocations = common.controls.basePanel.IconLocations.None;
            this.basePanel1.mySizingOptions = common.controls.basePanel.SizingOptions.None;
            this.basePanel1.myWeight = 0;
            this.basePanel1.Name = "basePanel1";
            this.basePanel1.Size = new System.Drawing.Size(327, 31);
            this.basePanel1.TabIndex = 1;
            // 
            // pauseBtn
            // 
            this.pauseBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseBtn.Image = global::server.Properties.Resources.pause;
            this.pauseBtn.isDownState = false;
            this.pauseBtn.Location = new System.Drawing.Point(219, 5);
            this.pauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(15, 16);
            this.pauseBtn.TabIndex = 145;
            this.myToolTip.SetToolTip(this.pauseBtn, "Run ");
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Visible = false;
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.Image = global::server.Properties.Resources.run;
            this.startBtn.isDownState = false;
            this.startBtn.Location = new System.Drawing.Point(255, 7);
            this.startBtn.Margin = new System.Windows.Forms.Padding(2);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(15, 16);
            this.startBtn.TabIndex = 3;
            this.myToolTip.SetToolTip(this.startBtn, "Run ");
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Visible = false;
            // 
            // btnDevivativeFetch
            // 
            this.btnDevivativeFetch.Image = global::server.Properties.Resources.data;
            this.btnDevivativeFetch.Location = new System.Drawing.Point(64, 3);
            this.btnDevivativeFetch.Name = "btnDevivativeFetch";
            this.btnDevivativeFetch.Size = new System.Drawing.Size(22, 23);
            this.btnDevivativeFetch.TabIndex = 10;
            this.btnDevivativeFetch.UseVisualStyleBackColor = true;
            // 
            // viewLogBtn
            // 
            this.viewLogBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewLogBtn.Image = global::server.Properties.Resources.schedule;
            this.viewLogBtn.isDownState = false;
            this.viewLogBtn.Location = new System.Drawing.Point(35, 2);
            this.viewLogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.viewLogBtn.Name = "viewLogBtn";
            this.viewLogBtn.Size = new System.Drawing.Size(24, 24);
            this.viewLogBtn.TabIndex = 2;
            this.myToolTip.SetToolTip(this.viewLogBtn, "View log");
            this.viewLogBtn.UseVisualStyleBackColor = true;
            this.viewLogBtn.Click += new System.EventHandler(this.viewLogBtn_Click);
            // 
            // runBtn
            // 
            this.runBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBtn.Image = global::server.Properties.Resources.run;
            this.runBtn.isDownState = false;
            this.runBtn.Location = new System.Drawing.Point(10, 2);
            this.runBtn.Margin = new System.Windows.Forms.Padding(2);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(24, 24);
            this.runBtn.TabIndex = 0;
            this.myToolTip.SetToolTip(this.runBtn, "Run ");
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // timerAlert
            // 
            this.timerAlert.Tick += new System.EventHandler(this.timerAlert_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(785, 25);
            this.toolStrip1.TabIndex = 146;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // frmSchedule
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(785, 323);
            this.Controls.Add(this.basePanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.scheduleGb);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.isLockEdit = false;
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmSchedule";
            this.Text = "Tool";
            this.Controls.SetChildIndex(this.TitleLbl, 0);
            this.Controls.SetChildIndex(this.scheduleGb, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.basePanel1, 0);
            this.scheduleGb.ResumeLayout(false);
            this.scheduleGb.PerformLayout();
            this.basePanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private common.controls.baseButton runBtn;
        private common.controls.baseLabel fetchStockLbl;
        private common.controls.baseLabel tradeAlertLbl;
        private common.controls.baseButton viewLogBtn;
        protected common.controls.baseButton pauseBtn;
        protected System.Windows.Forms.GroupBox scheduleGb;
        protected common.controls.basePanel basePanel1;
        private common.controls.baseButton startBtn;
        private common.controls.baseCheckBox tradeAlertChk;
        private common.controls.baseCheckBox cboxfetchDataHOSE;
        protected System.Windows.Forms.Timer timerAlert;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnDevivativeFetch;
        private common.controls.baseCheckBox cboxDerivativeFetch;
        private common.controls.baseCheckBox cboxFetchDataHSTC;
    }
}

