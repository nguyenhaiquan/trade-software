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
    public partial class monthRange : UserControl
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

        public monthRange()
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
                    if (!DateTime.TryParse("01/" + this.frMonthCb.Text + "/" + this.yearEd.Text, FrenchCultureInfo,DateTimeStyles.NoCurrentDateDefault, out frDate))
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
                    DateRangeDesc = "QÚY 1 " + toDate.Year.ToString();  
                    break;
                case 2:
                    if (!DateTime.TryParse("01/04/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 2 " + toDate.Year.ToString();
                    break;
                case 3:
                    if (!DateTime.TryParse("01/07/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 3 " + toDate.Year.ToString();
                    break;
                case 4:
                    if (!DateTime.TryParse("01/10/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(3).AddSeconds(-1);
                    DateRangeDesc = "QÚY 4 " + toDate.Year.ToString();
                    break;

                case 5:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(6).AddSeconds(-1);
                    DateRangeDesc = "6 THÁNG " + toDate.Year.ToString();
                    break;

                case 6:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(9).AddSeconds(-1);
                    DateRangeDesc = "9 THÁNG " + toDate.Year.ToString();
                    break;
                case 7:
                    if (!DateTime.TryParse("01/01/" + this.yearEd.Text, FrenchCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = frDate.AddMonths(12).AddSeconds(-1);
                    DateRangeDesc = "";
                    break;
                case 8:
                    if (!DateTime.TryParse("01/"+this.frMonthCb.Text+"/"+yearEd.Text, MyCultureInfo, DateTimeStyles.NoCurrentDateDefault, out frDate))
                    {
                        HaveError = true; break;
                    }
                    if (!DateTime.TryParse("01/" + this.toMonthCb.Text + "/" + yearEd.Text, MyCultureInfo, DateTimeStyles.NoCurrentDateDefault, out toDate))
                    {
                        HaveError = true; break;
                    }
                    toDate = toDate.AddMonths(1).AddSeconds(-1);
                    if (frDate == toDate) DateRangeDesc = "THÁNG " + frMonthCb.Text.Trim();
                    else DateRangeDesc = "TỪ THÁNG " + frMonthCb.Text.Trim() + " ĐẾN THÁNG " + toMonthCb.Text.Trim();
                    break;
            }
            DateRangeDesc += " NĂM " + toDate.Year.ToString();
            frDate = frDate.Date;
            return !HaveError;
        }

        private void monthRange_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            this.timeTypeCb.SelectedIndex = 0;
            this.frMonthCb.SelectedIndex = today.Month - 1;
            this.toMonthCb.SelectedIndex = this.frMonthCb.SelectedIndex;
            this.yearEd.Text = today.Year.ToString();
        }

        private void timeTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.frMonthLbl.Text = (this.timeTypeCb.SelectedIndex == 0 ? "Tháng" : "Từ");
            this.frMonthCb.Enabled  = (this.timeTypeCb.SelectedIndex == 0) || 
                                      (this.timeTypeCb.SelectedIndex == 8);
            this.toMonthCb.Enabled = (this.timeTypeCb.SelectedIndex == 8);

            this.frMonthLbl.Enabled = this.frMonthCb.Enabled;
            this.toMonthLbl.Enabled = this.toMonthCb.Enabled;
        }
       
    }
}