using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace baseClass.forms
{
    public partial class chartProperties : baseDialogForm  
    {
        public chartProperties()
        {
            try
            {
                InitializeComponent();
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
            this.Text = Languages.Libs.GetString("property");

            colorPg.Text = Languages.Libs.GetString("color");
            backgroundLbl.Text = Languages.Libs.GetString("background");
            foreGroundLbl.Text = Languages.Libs.GetString("foreground");
            gridLbl.Text = Languages.Libs.GetString("grid");
            barUpLbl.Text = Languages.Libs.GetString("barUp");
            barDnLbl.Text = Languages.Libs.GetString("barDn");
            bullCandleLbl.Text = Languages.Libs.GetString("bullCandle");
            bearCandleLbl.Text = Languages.Libs.GetString("bearCandle");
            lineGraphLbl.Text = Languages.Libs.GetString("lineGraph");
            volumeLbl.Text = Languages.Libs.GetString("volume");

            chartPg.Text = Languages.Libs.GetString("system");
            showGridChk.Text = Languages.Libs.GetString("showGrid");
            showVolumeChk.Text = Languages.Libs.GetString("showVolume");
            showDescriptionChk.Text = Languages.Libs.GetString("showDescription");
            showLegendChk.Text = Languages.Libs.GetString("showLegend");

            zoomRateLbl.Text = Languages.Libs.GetString("zoomRate");
            panRateLbl.Text = Languages.Libs.GetString("panRate");

            panMouseRateLbl.Text = Languages.Libs.GetString("mouseRate");
            movePerPanLbl.Text = Languages.Libs.GetString("movesPerPan");

            marginAndSpaceLbl.Text = Languages.Libs.GetString("marginAndSpace");
            leftMarginLbl.Text = Languages.Libs.GetString("left");
            rightMarginLbl.Text = Languages.Libs.GetString("right");
            topMarginLbl.Text = Languages.Libs.GetString("top");
            bottomMarginLbl.Text = Languages.Libs.GetString("bottom");

            leadingSpaceLbl.Text = Languages.Libs.GetString("leading");
            trailingSpaceLbl.Text = Languages.Libs.GetString("trailing");
        }

        public static chartProperties GetForm(string formName)
        {
            string cacheKey = typeof(chartProperties).FullName + (formName == null || formName.Trim() == "" ? "" : "-" + formName);
            chartProperties form = (chartProperties)common.Data.dataCache.Find(cacheKey);
            if (form != null && !form.IsDisposed) return form;
            form = new chartProperties();
            common.Data.dataCache.Add(cacheKey, form);
            return form;
        }

        protected override bool LoadConfigure()
        {
            bgColorCb.Color = commonClass.Settings.sysChartBgColor;
            fgColorCb.Color = commonClass.Settings.sysChartFgColor;
            gridColorCb.Color = commonClass.Settings.sysChartGridColor;

            volumesColorCb.Color = commonClass.Settings.sysChartVolumesColor;
            lineCandleColorCb.Color = commonClass.Settings.sysChartLineCandleColor;
            bearCandleColorCb.Color = commonClass.Settings.sysChartBearCandleColor;
            bullCandleColorCb.Color = commonClass.Settings.sysChartBullCandleColor;
            barDnColorCb.Color = commonClass.Settings.sysChartBarDnColor;
            barUpColorCb.Color = commonClass.Settings.sysChartBarUpColor;

            showDescriptionChk.Checked = commonClass.Settings.sysChartShowDescription;
            showVolumeChk.Checked = commonClass.Settings.sysChartShowVolume;
            showGridChk.Checked = commonClass.Settings.sysChartShowGrid;
            showLegendChk.Checked = commonClass.Settings.sysChartShowLegend;

            zoomPercEd.Value = Charts.Settings.sysZoom_Percent;
            zoomMinCountEd.Value = Charts.Settings.sysZoom_MinCount;

            panMouseRateEd.Value = Charts.Settings.sysPAN_MouseRate;
            panMoveMinCountEd.Value = Charts.Settings.sysPAN_MovePercent;
            panMoveMinCountEd.Value = Charts.Settings.sysPAN_MoveMinCount;

            leftMarginEd.Value =  Charts.Settings.sysChartMarginLEFT;
            rightMarginEd.Value = Charts.Settings.sysChartMarginRIGHT;
            topMarginEd.Value = Charts.Settings.sysChartMarginTOP;
            bottomMarginEd.Value = Charts.Settings.sysChartMarginBOT;

            leadingSpaceEd.Value = Charts.Settings.sysViewSpaceAtLEFT;
            trailingSpaceEd.Value = Charts.Settings.sysViewSpaceAtRIGHT;

            return true;
        }
        protected override bool SaveConfigure()
        {
            commonClass.Settings.sysChartBgColor = bgColorCb.Color;
            commonClass.Settings.sysChartFgColor = fgColorCb.Color;
            commonClass.Settings.sysChartGridColor = gridColorCb.Color;

            commonClass.Settings.sysChartVolumesColor = volumesColorCb.Color;
            commonClass.Settings.sysChartLineCandleColor = lineCandleColorCb.Color;
            commonClass.Settings.sysChartBearCandleColor = bearCandleColorCb.Color;
            commonClass.Settings.sysChartBullCandleColor = bullCandleColorCb.Color;
            commonClass.Settings.sysChartBarDnColor = barDnColorCb.Color;
            commonClass.Settings.sysChartBarUpColor = barUpColorCb.Color;

            commonClass.Settings.sysChartShowDescription = showDescriptionChk.Checked;
            commonClass.Settings.sysChartShowVolume = showVolumeChk.Checked;
            commonClass.Settings.sysChartShowGrid = showGridChk.Checked;
            commonClass.Settings.sysChartShowLegend = showLegendChk.Checked;

            Charts.Settings.sysZoom_Percent = (int)zoomPercEd.Value;
            Charts.Settings.sysZoom_MinCount = (int)zoomMinCountEd.Value;

            Charts.Settings.sysPAN_MouseRate = (int)panMouseRateEd.Value;
            Charts.Settings.sysPAN_MovePercent = (int)panMovePercEd.Value;
            Charts.Settings.sysPAN_MoveMinCount = (int)panMoveMinCountEd.Value;

            Charts.Settings.sysChartMarginLEFT = (int)leftMarginEd.Value;
            Charts.Settings.sysChartMarginRIGHT = (int)rightMarginEd.Value;
            Charts.Settings.sysChartMarginTOP = (int)topMarginEd.Value;
            Charts.Settings.sysChartMarginBOT = (int)bottomMarginEd.Value;

            Charts.Settings.sysViewSpaceAtLEFT = (int)trailingSpaceEd.Value;
            Charts.Settings.sysViewSpaceAtRIGHT = (int)leadingSpaceEd.Value;

            application.Configuration.Save_Local_Settings_CHART();
            return true;
        }

        protected override bool BeforeAcceptProcess()
        {
            bool retVal = true;
            ClearNotifyError();
            return retVal;
        }
        private void ProcessHandler(object sender,common.baseDialogEvent e)
        {
            if (e.isCloseClick) return;
            myFormStatus.acceptClose = true;
            SaveConfigure();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }
    }    
}