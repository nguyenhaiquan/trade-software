using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class MyTimer : System.Timers.Timer
    {
        string market;
        BackgroundWorker bgw;
        Boolean fRunning;
        static readonly object _object = new object();

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

        private void MyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((!fRunning) && (!bgw.IsBusy))
            {
                lock (_object)
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

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FetchData(market);
            }
            catch (Exception er)
            {
                //ShowError(er);
            }
        }

        private void FetchData(string market)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                if (!fRunning)
                {
                    fRunning = true;
                    libs.FetchRealTimeData(DateTime.Now, market);
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
            catch (Exception er)
            {
                fRunning = false;
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "Cannot Run Fetching Data from SSI Page", er);
            }

        }
    }
}
