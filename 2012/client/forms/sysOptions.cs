using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using commonClass;

namespace client.forms
{
    public partial class sysOptions : baseClass.forms.baseDialogForm    
    {
        public sysOptions()
        {
            try
            {
                InitializeComponent();
                optionTab.SendToBack();
                this.myOnProcess += new onProcess(ProcessHandler);  
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
        public override void SetLanguage()
        {
            base.SetLanguage();
            this.Text = Languages.Libs.GetString("option");

            investmentPg.Text = Languages.Libs.GetString("investment");
            systemPg.Text = Languages.Libs.GetString("system");
            colorPg.Text = Languages.Libs.GetString("color");

            maxBuyQtyPercLbl.Text = Languages.Libs.GetString("maxBuyQtyPercent");
            totalCapitalLbl.Text = Languages.Libs.GetString("totalCapital");
            maxBuyAmtPercLbl.Text = Languages.Libs.GetString("maxBuyAmtPercent");
            qtyReducePercLbl.Text = Languages.Libs.GetString("maxQtyReducePercent");
            qtyAccumulatePercLbl.Text = Languages.Libs.GetString("maxQtyAccumulatePercent");
            totalAmountLbl.Text = Languages.Libs.GetString("totalAmt");

            refreshRateLbl.Text = Languages.Libs.GetString("refreshRate");
            secondLbl.Text = Languages.Libs.GetString("seconds");

            foreColorLbl.Text = Languages.Libs.GetString("foreground");
            backColorLbl.Text = Languages.Libs.GetString("background");
            colorPg.Text = Languages.Libs.GetString("color");
            bgColorIncreaseLbl.Text = Languages.Libs.GetString("increase");
            bgColorDecreaseLbl.Text = Languages.Libs.GetString("decrease");
            bgColorNotChangeLbl.Text = Languages.Libs.GetString("notChange");

            //Format
            formatPg.Text = Languages.Libs.GetString("format");
            localAmtMaskLbl.Text = Languages.Libs.GetString("localAmtMask");
            foreignAmtMaskLbl.Text = Languages.Libs.GetString("foreignAmtMask");
            qtyMaskLbl.Text = Languages.Libs.GetString("qtyMask");
            percentMaskLbl.Text = Languages.Libs.GetString("percentMask");

        }
        public static client.forms.sysOptions GetForm(string formName)
        {
            string cacheKey = typeof(client.forms.sysOptions).FullName + (formName == null || formName.Trim() == "" ? "" : "-" + formName);
            client.forms.sysOptions form = (client.forms.sysOptions)common.Data.dataCache.Find(cacheKey);
            if (form != null && !form.IsDisposed) return form;
            form = new client.forms.sysOptions();
            common.Data.dataCache.Add(cacheKey, form);
            return form;
        }

        protected override bool LoadConfigure()
        {
            maxBuyQtyPercEd.Value = Settings.sysStockMaxBuyQtyPerc;
            totalCapitalEd.Value = Settings.sysStockTotalCapAmt;
            maxBuyAmtPercEd.Value = Settings.sysStockMaxBuyAmtPerc;
            qtyAccumulatePercEd.Value = Settings.sysStockAccumulateQtyPerc;
            qtyReducePercEd.Value = Settings.sysStockReduceQtyPerc;

            refreshRateEd.Value = Settings.sysTimerIntervalInSecs;


            fgColorDecreaseCb.Color = Settings.sysPriceColor_Decrease_FG;
            bgColorDecreaseCb.Color = Settings.sysPriceColor_Decrease_BG;

            bgColorIncreaseCb.Color = Settings.sysPriceColor_Increase_BG;
            fgColorIncreaseCb.Color = Settings.sysPriceColor_Increase_FG;

            bgColorNotChangeCb.Color = Settings.sysPriceColor_NotChange_BG;
            fgColorNotChangeCb.Color = Settings.sysPriceColor_NotChange_FG;
            return true;
        }
        // Do not use SaveConfigure because it always run when the form is closed.
        protected void SaveSettings()
        {
            Settings.sysStockTotalCapAmt = totalCapitalEd.Value;
            Settings.sysStockMaxBuyAmtPerc = maxBuyAmtPercEd.Value;
            Settings.sysStockAccumulateQtyPerc = qtyAccumulatePercEd.Value;
            Settings.sysStockReduceQtyPerc = qtyReducePercEd.Value;

            Settings.sysTimerIntervalInSecs = (int)refreshRateEd.Value;

            Settings.sysPriceColor_Decrease_BG = bgColorDecreaseCb.Color;
            Settings.sysPriceColor_Decrease_FG = fgColorDecreaseCb.Color;

            Settings.sysPriceColor_Increase_BG = bgColorIncreaseCb.Color;
            Settings.sysPriceColor_Increase_FG = fgColorIncreaseCb.Color;

            Settings.sysPriceColor_NotChange_BG = bgColorNotChangeCb.Color;
            Settings.sysPriceColor_NotChange_FG = fgColorNotChangeCb.Color;

            application.Configuration.Save_Local_Settings_SYSTEM();
        }

        protected override bool BeforeAcceptProcess()
        {
            bool retVal = true;
            ClearNotifyError();
            if (totalCapitalEd.Value <= 0)
            {
                NotifyError(totalCapitalEd);
                retVal = false;
            }
            if (maxBuyAmtPercEd.Value <= 0)
            {
                NotifyError(maxBuyAmtPercEd);
                retVal = false;
            }

            if (qtyAccumulatePercEd.Value <= 0)
            {
                NotifyError(qtyAccumulatePercEd);
                retVal = false;
            }

            if (qtyReducePercEd.Value <= 0)
            {
                NotifyError(qtyReducePercEd);
                retVal = false;
            }
            if (maxBuyQtyPercEd.Value <= 0)
            {
                NotifyError(maxBuyQtyPercEd);
                retVal = false;
            }
            return retVal;
        }
        private void ProcessHandler(object sender,common.baseDialogEvent e)
        {
            try
            {
                if (e.isCloseClick) return;
                SaveSettings();
                this.Close();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }
    }    
}