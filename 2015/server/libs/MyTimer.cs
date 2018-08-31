using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databaseEntity;
using Imports;
using Imports.Stock;

namespace server
{
    public class MyTimer : System.Timers.Timer
    {
        string market;
        BackgroundWorker bgw;
        Boolean fRunning;
        //static readonly object _object = new object();

        public MyTimer(string _market, double _interval, Boolean _autoReset)
            : base(_interval)
        {
            market = _market;
            this.AutoReset = _autoReset;
            this.Elapsed += MyTimer_Elapsed;
            fRunning = false;
            bgw = new BackgroundWorker();
            bgw.DoWork += Bgw_DoWork;
            bgw.ProgressChanged += Bgw_ProgressChanged;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// Elapsed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!bgw.IsBusy)
            {
                //lock (_object)
                {
                    bgw.RunWorkerAsync();
                }
            }
            
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Background worker Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FetchData(market);
            }
            catch (Exception er)
            {
                fRunning = false;
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "Cannot Run Fetching Data from SSI Page", er);
            }
        }

        private void FetchData(string market)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (!fRunning)
            {
                fRunning = true;
                FetchRealTimeData(DateTime.Now, market);
                fRunning = false;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //myStatusStrip.Text = "Downloading " + market + " takes " + elapsedTime;
            bgw.ReportProgress(100, elapsedTime);
           

        }

        private void FetchRealTimeData(DateTime updateTime, string market)
        {
            //DataView myDataView = new DataView(application.SysLibs.myExchangeDetailTbl);
            //myDataView.Sort = application.SysLibs.myExchangeDetailTbl.orderIdColumn.ToString();

            string[] parts;

            //GetStockExchange Row
            StockDb db = new StockDb();
            stockExchange marketRaw = db.GetStockExchanges(market);

            if (libs.IsHolidays(updateTime, marketRaw.holidays)) return;

            // WorkTimes can have multipe parts separated by charater |
            parts = marketRaw.workTime.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            StringCollection confWorkTimes = new StringCollection();
            for (int idx2 = 0; idx2 < parts.Length; idx2++) confWorkTimes.Add(parts[idx2]);
            if (!libs.IsWorktime(updateTime, confWorkTimes)) return;

            bool retVal = true;

            //Main call to update price
            ssi_StockImport ssiImport = new ssi_StockImport(market);
            retVal = ssiImport.ImportFromWeb(updateTime, market);
            if (retVal == false)
                throw new Exception();
        }
    }
}
