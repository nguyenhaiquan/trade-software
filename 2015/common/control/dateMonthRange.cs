using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace common.controls
{
    public partial class dateMonthRange : UserControl
    {
        public DateTime frDate, toDate;
        public string DateRangeDesc;

        //Make Enter key works as Tabkey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Make ENTER key work as TAB key for all controls except BUTTON
            if ((common.settings.sysEnterSameAsTabKey && (msg.WParam.ToInt32() == (int)Keys.Enter) &&
                (this.ActiveControl != null) && (this.ActiveControl.GetType().Name.ToUpper().Trim() != "BUTTON")))
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public dateMonthRange()
        {
            InitializeComponent();
            this.timeTypeCb.SelectedIndex = 0;  
        }

        public bool GetDateRange()
        {
            bool HaveError = false;
            CultureInfo MyCultureInfo = globalSettings.GetCurrentCulture();
            CultureInfo FrenchCultureInfo = new CultureInfo("fr-FR");
            switch (this.timeTypeCb.SelectedIndex)
            {
                case 0:
                    if (!DateTime.TryParse("01/" + this.monthCb.Text + "/" + this.yearEd.Text, FrenchCultureInfo,DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(1).AddSeconds(-1);
                    DateRangeDesc = "THÁNG " + toDate.Month.ToString() + "/" + toDate.Year.ToString();  
                    break;
                case 1:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 1 NĂM " + toDate.Year.ToString();  
                    break;
                case 2:
                    if (!DateTime.TryParse("01/04/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 2 NĂM " + toDate.Year.ToString();
                    break;
                case 3:
                    if (!DateTime.TryParse("01/07/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 3 NĂM " + toDate.Year.ToString();
                    break;
                case 4:
                    if (!DateTime.TryParse("01/10/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 4 NĂM " + toDate.Year.ToString();
                    break;

                case 5:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(6).AddSeconds(-1);
                    DateRangeDesc = "6 THÁNG NĂM " + toDate.Year.ToString();
                    break;

                case 6:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(9).AddSeconds(-1);
                    DateRangeDesc = "9 THÁNG NĂM " + toDate.Year.ToString();
                    break;
                case 7:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(12).AddSeconds(-1);
                    DateRangeDesc = "NĂM " + toDate.Year.ToString();
                    break;
            }
            return !HaveError;
        }

        private void dateRange_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            this.timeTypeCb.SelectedIndex = 0;
            this.monthCb.SelectedIndex = today.Month - 1;
            this.yearEd.Text = today.Year.ToString();
        }

        private void timeTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.monthCb.Enabled  = (this.timeTypeCb.SelectedIndex == 0);
            this.yearEd.Enabled = true;
        }
    }
}