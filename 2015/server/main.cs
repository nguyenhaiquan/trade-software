﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using commonTypes;
using Imports;
using Imports.Stock;

namespace server
{
    public partial class frmServer : common.forms.baseApplication
    //public partial class frmServer : baseClass.forms.baseApplication
    {
        public frmServer()
        {
            try
            {
                common.language.myCulture = new System.Globalization.CultureInfo("vi-VN");
                InitializeComponent();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }
        protected override bool CheckValid()
        {
            return Init();
        }

        private static bool Init()
        {
            common.configuration.withEncryption = true;
            application.Configuration.Load_User_Envir();
            //Check data connection after db-setting were loaded
            if (!databases.SysLibs.CheckAllDbConnection())
            {
                common.system.ShowMessage("Không thể kết nối đến nguồn dữ liệu.Xin vui lòng chạy lại chương trình [Setup].");
                return false;
            }
            GlobalSettings globalSetting = Settings.sysGlobal;
            application.Configuration.Load_Global_Settings(ref globalSetting);
            Settings.sysGlobal = globalSetting;

            application.Configuration.Load_Local_Settings();
            return true;
        }

     
        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.QuitApplication();
        }

        private void importPriceDataMenu_Click(object sender, EventArgs e)
        {
            Form myForm = this.FindForm("importPriceData");
            if (myForm == null || myForm.IsDisposed)
            {
                myForm = new Imports.Forms.frmImportPriceData();
                myForm.Name = "importPriceData";
            }
            this.ShowForm(myForm, false);
        }
       
        private void loginMenu_Click(object sender, EventArgs e)
        {
            this.ShowLogin();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void importIcbMenu_Click(object sender, EventArgs e)
        {
            Form myForm = this.FindForm("importIcbCode");
            if (myForm == null || myForm.IsDisposed)
            {
                myForm = new Imports.Forms.importIcbCode();
                myForm.Name = "importIcbCode";
            }
            this.ShowForm(myForm, false);
        }

        private void importCompanyMenu_Click(object sender, EventArgs e)
        {
            Form myForm = this.FindForm("importCompany");
            if (myForm == null || myForm.IsDisposed)
            {
                myForm = new Imports.Forms.frmImportCompany();
                myForm.Name = "importCompany";
            }
            this.ShowForm(myForm, false);
        }

        private void importComSectorMenu_Click(object sender, EventArgs e)
        {
            Form myForm = this.FindForm("importComSector");
            if (myForm == null || myForm.IsDisposed)
            {
                myForm = new Imports.Forms.frmImportComSector();
                myForm.Name = "importComSector";
            }
            this.ShowForm(myForm, false);
        }
        
        private void reUpdatePriceMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Imports.Forms.frmReUpdatePrice form = new Imports.Forms.frmReUpdatePrice();
                form.ShowDialog();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }

        static frmSchedule scheduleForm = null;
        private void updateDataMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (scheduleForm == null || scheduleForm.IsDisposed) scheduleForm = new frmSchedule();
                scheduleForm.Show();
                scheduleForm.Activate();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }

        private void reAggregateMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Imports.Forms.frmReAggregatePrice form = new Imports.Forms.frmReAggregatePrice();
                form.ShowDialog();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }

        //public frmSchedule scheduleForm1
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        //public libs libs
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        /// <summary>
        /// Menu Tạo chiến lược tốt nhất cho từng loại cổ phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bestStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 700;
            BackgroundWorker bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.DoWork += new DoWorkEventHandler(createBestStrategyBackGround);
            bWorker.ProgressChanged += new ProgressChangedEventHandler(bWorker_ProgressChangedforBestStrategy);
            if (!bWorker.IsBusy)
                bWorker.RunWorkerAsync();            
        }

        void createBestStrategyBackGround(object sender, DoWorkEventArgs e)
        {
            application.Strategy.StrategyLibs.createBestStrategy(sender,e);
        }

        void bWorker_ProgressChangedforBestStrategy(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = e.ProgressPercentage.ToString();
        }

        /// <summary>
        /// Delete Stocks that don't exist any more
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteStocksMenuItem_Click(object sender, EventArgs e)
        {
            ssi_StockImport ssiStockImport = new ssi_StockImport(null);
            ssiStockImport.DeleteStocksNotinDatabase();
        }

        private void FAImport_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
