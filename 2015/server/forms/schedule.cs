using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using commonTypes;

namespace server
{
    

    public partial class frmSchedule : baseClass.forms.baseForm   
    {
        private bool fRunning = false;
        private bool fDerivativeRunning = false;

        Boolean fCreateAlertRunning = false;

        //common.TimerProcess fetchDataTimer = new common.TimerProcess();
        common.TimerProcess createTradeAlertTimer = new common.TimerProcess();

        public BackgroundWorker bWorkerDerivative;
        List<MyTimer> listTimers;
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
            listTimers = new List<MyTimer>();
            listTimers.Add(new MyTimer("HOSE",1000,true));
            listTimers.Add(new MyTimer("HASTC", 1000,true));
            listTimers.Add(new MyTimer("DERIVATIVE", 1000,true));
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
            //fFetchDataRunning = false;
            fCreateAlertRunning = false;
            try
            {
                fRunning = !fRunning;
                this.ShowMessage("");
                runBtn.Image = (fRunning ? pauseBtn.Image : startBtn.Image);
                scheduleGb.Enabled = !fRunning;

                //fetchDataTimer.WaitInSeconds = (short)(fetchDataChk.Checked?Settings.sysGlobal.RefreshDataInSecs:0);
                
                createTradeAlertTimer.WaitInSeconds = (short)(tradeAlertChk.Checked ? Settings.sysGlobal.CheckAlertInSeconds : 0);
                //createTradeAlertTimer.WaitInSeconds = 10;

                if (fRunning)
                {
                    if (cboxfetchDataHOSE.Checked)
                        //myTimerHOSE.Start();
                        listTimers[0].Start();
                    if (cboxFetchDataHSTC.Checked)
                        listTimers[1].Start();

                    if (cboxDerivativeFetch.Checked)
                        listTimers[2].Start();
                    //if (fetchDataChk)
                    //Init last price before importing
                    //databases.AppLibs.GetLastClosePrices();
                }
                else
                {
                    if (cboxfetchDataHOSE.Checked)
                        listTimers[0].Stop();
                    if (cboxFetchDataHSTC.Checked)
                        listTimers[1].Stop();
                    if (cboxDerivativeFetch.Checked)
                        listTimers[2].Stop();
                }
                
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

        private void myTimerDerivative_Tick(object sender, EventArgs e)
        {
            try
            {
                if ((!bWorkerDerivative.IsBusy) && (!fDerivativeRunning))
                    bWorkerDerivative.RunWorkerAsync();
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

    }
}
