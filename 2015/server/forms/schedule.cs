using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using commonTypes;
using System.Diagnostics;

namespace server
{
    public partial class frmSchedule : baseClass.forms.baseForm   
    {
        private bool fRunning = false;
        private bool fDerivativeRunning = false;
        
        common.TimerProcess fetchDataTimer = new common.TimerProcess();
        common.TimerProcess createTradeAlertTimer = new common.TimerProcess();

        BackgroundWorker bWorkerDerivative;

        public frmSchedule()
        {
            try
            {
                InitializeComponent();
                Init();                
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
        public void Init()
        {
            tradeAlertLbl.Text = Settings.sysGlobal.CheckAlertInSeconds.ToString() + " " + Languages.Libs.GetString("seconds");
            fetchStockLbl.Text = Settings.sysGlobal.RefreshDataInSecs.ToString() + " " + Languages.Libs.GetString("seconds");
            //fetchDataTimer.OnProcess += new common.TimerProcess.OnProcessEvent(FetchData);
            //fetchDataTimerHOSE.OnProcess += new common.TimerProcess.OnProcessEvent(FetchDataHOSE);(
            //fetchDataTimerHASTC.OnProcess += new common.TimerProcess.OnProcessEvent(FetchDataHASTC);
            //fetchDataTimerGOLD.OnProcess += new common.TimerProcess.OnProcessEvent(FetchDataGOLD);

            //createTradeAlertTimer.OnProcess += new common.TimerProcess.OnProcessEvent(CreateTradeAlert);

            bWorkerDerivative = new BackgroundWorker();
            bWorkerDerivative.DoWork += new DoWorkEventHandler(bWorkerDerivative_DoWork);
            bWorkerDerivative.ProgressChanged += new ProgressChangedEventHandler(bWorkerDerivative_ProgressChanged);
            bWorkerDerivative.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorkerDerivative_DoWork_RunWorkerCompleted);

            bWorkerDerivative.WorkerReportsProgress = true;
            bWorkerDerivative.WorkerSupportsCancellation = true;

            //Derivative
            fDerivativeRunning = false;

        }

        void bWorkerDerivative_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (fFetchDataHOSERunning) myStatusStrip.Text = "Downloading HOSE data..";
            if (fFetchDataHASTCRunning) myStatusStrip.Text = "Downloading HASTC data..";
            if (fFetchDataGOLDRunning) myStatusStrip.Text = "Downloading Gold data..";
        }

        void bWorkerDerivative_DoWork(object sender, DoWorkEventArgs e)
        {
            FetchData("DERIVATIVE");

        }

        void bWorkerDerivative_DoWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void onTradeAlertProcessStart(int count)
        {
            if (myStatusStrip.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    DoOnTradeAlertProcessStart(count);
                });
            }
            else
            {
                DoOnTradeAlertProcessStart(count);
            }
        }
        private bool onTradeAlertProcessItem(string code)
        {
            if (myStatusStrip.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    DoOnTradeAlertProcessItem(code);
                });
            }
            else
            {
                DoOnTradeAlertProcessItem(code);
            }
            return fRunning;
        }
        private void onTradeAlertProcessEnd()
        {
            if (myStatusStrip.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    DoOnTradeAlertProcessEnd();
                });
            }
            else
            {
                DoOnTradeAlertProcessEnd();
            }
        }

        private void DoOnTradeAlertProcessStart(int count)
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = count;
        }
        private bool DoOnTradeAlertProcessItem(string stockcode)
        {
            Application.DoEvents();
            progressBar.Value++;
            this.ShowMessage(stockcode);
            return fRunning;
        }
        private void DoOnTradeAlertProcessEnd()
        {
            progressBar.Visible = false;
            this.ShowMessage("");
        }


        Boolean fFetchDataRunning = false;
        Boolean fFetchDataHOSERunning = false;
        Boolean fFetchDataHASTCRunning = false;
        Boolean fFetchDataGOLDRunning = false;

        Boolean fCreateAlertRunning = false;
        private void FetchData(string market)
        {
            try
            {
                if (fFetchDataRunning)  return;
                
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();            
 
                fFetchDataRunning = true;

                if (market == "HOSE") libs.FetchRealTimeData(DateTime.Now, "HOSE");
                else
                if (market == "HASTC") libs.FetchRealTimeData(DateTime.Now, "HASTC");
                else
                if (market == "DERIVATIVE") libs.FetchRealTimeData(DateTime.Now, "HASTC");

                fFetchDataRunning = false;

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
            }
            catch (Exception er)
            {
                fFetchDataRunning = false;
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "SRV002",er);
            }
            
        }


        private void CreateTradeAlert()
        {
            try
            {
                if (fCreateAlertRunning)
                {
                    commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Informational, "", "Ignore Trade alert");
                    return;
                }
                fCreateAlertRunning = true;
                int noAlertCreated = application.TradeAlert.CreateTradeAlert(onTradeAlertProcessStart, onTradeAlertProcessItem, onTradeAlertProcessEnd);
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Informational, "", " - Trade alert run successfully : " + noAlertCreated + " alerts created");
                fCreateAlertRunning = false;
            }
            catch (Exception er)
            {
                fCreateAlertRunning = false;
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "SRV003", " - Trader Alert error " + er.Message);
                ShowError(er);
            }
        }

        #region event handler

        /// <summary>
        /// Xu ly hanh vi nhan chuot. 
        /// Khoi tao hanh vi cua Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runBtn_Click(object sender, EventArgs e)
        {
            fFetchDataRunning = false;
            fCreateAlertRunning = false;
            try
            {
                fRunning = !fRunning;
                this.ShowMessage("");
                runBtn.Image = (fRunning ? pauseBtn.Image : startBtn.Image);
                scheduleGb.Enabled = !fRunning;

                fetchDataTimer.WaitInSeconds = (short)(fetchDataChk.Checked?Settings.sysGlobal.RefreshDataInSecs:0);
                
                createTradeAlertTimer.WaitInSeconds = (short)(tradeAlertChk.Checked ? Settings.sysGlobal.CheckAlertInSeconds : 0);
                //createTradeAlertTimer.WaitInSeconds = 10;

                if (fRunning)
                {
                    myTimer.Start();

                    //Init last price before importing
                    databases.AppLibs.GetLastClosePrices();
                }
                else myTimer.Stop();
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
        
        private void viewLogBtn_Click(object sender, EventArgs e)
        {
            try
            {
                common.fileFuncs.DisplayFile(Settings.sysFileUserLog);
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!bWorkerDerivative.IsBusy)
                    bWorkerDerivative.RunWorkerAsync();

                //if (createTradeAlertTimer.IsEndWaitTime()) 
                //    createTradeAlertTimer.Execute();
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
        #endregion

        private void timerAlert_Tick(object sender, EventArgs e)
        {
            try
            {                
                if (createTradeAlertTimer.IsEndWaitTime())
                    createTradeAlertTimer.Execute();
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }

        private void btnDevivativeFetch_Click(object sender, EventArgs e)
        {
            try
            {
                fDerivativeRunning = !fDerivativeRunning;
                this.ShowMessage("");
                btnDevivativeFetch.Image = (fRunning ? pauseBtn.Image : startBtn.Image);
                scheduleGb.Enabled = !fDerivativeRunning;

                fetchDataTimer.WaitInSeconds = (short)(fetchDataChk.Checked ? Settings.sysGlobal.RefreshDataInSecs : 0);

                if (fFetchDataRunning)
                {
                    myTimerDerivative.Start();

                    //Init last price before importing
                    //databases.AppLibs.GetLastClosePrices();
                }
                else myTimerDerivative.Stop();
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
    }
}
