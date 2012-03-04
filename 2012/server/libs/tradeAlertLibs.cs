using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using commonTypes;
using commonClass;

namespace server
{
    public class AlertLibs
    {
        #region strategy estimation
        public delegate void onProcessStart(int stockCodeCount);
        public delegate bool onProcessItem(string stockcode);
        public delegate void onProcessEnd();

        private class TradeAlert
        {
            public TradePointInfo TradePoint = null;
            public AppTypes.TimeScale TimeScale =  AppTypes.MainDataTimeScale;
            public string StockCode="", Strategy="";
            public DateTime OnDateTime = common.Consts.constNullDate;
            public double Price=0,Volume=0;
            public TradeAlert(string stockCode, string strategy, AppTypes.TimeScale timeScale, DateTime onDateTime,
                               double price, double volume, TradePointInfo tradePoint)
            {
                this.StockCode = stockCode;
                this.TimeScale = timeScale;
                this.Strategy = strategy;
                this.OnDateTime = onDateTime;
                this.Price = price;
                this.Volume = volume;
                this.TradePoint = tradePoint;
            }
        }

        //private static void SaveLastRunTime(DateTime onTime)
        //{
        //    StringCollection aFields = new StringCollection();
        //    StringCollection aValues = new StringCollection();
        //    aFields.Add(Configuration.configKeys.sysTradeAlertLastRun.ToString());
        //    aValues.Add(onTime.ToString());
        //    application.Configuration.SaveConfig(aFields, aValues);
        //}
        
        //withAplicableCheckInAlert = true : Sell alerts only create when user owned stock that is applible to sell
        private static bool withAplicableCheckInAlert = false;

       
        /// <summary>
        /// Create alerts for all stock in portfolio
        /// </summary>
        /// <param name="alertList"> all alert resulted from analysis </param>
        private static void CreateTradeAlert(TradeAlert[] alertList)
        {
            decimal availabeQty;
            string msg;
            StringCollection timeScaleList;

            databases.baseDS.tradeAlertRow tradeAlertRow;
            databases.baseDS.tradeAlertDataTable tradeAlertTbl = new databases.baseDS.tradeAlertDataTable();
            databases.baseDS.portfolioDetailDataTable portfolioDetailTbl = databases.DbAccess.GetPortfolioDetail_ByType(new AppTypes.PortfolioTypes[] { AppTypes.PortfolioTypes.WatchList, AppTypes.PortfolioTypes.Portfolio }); 
            DataView portfolioDetailView = new DataView(portfolioDetailTbl);

            //Sort by  Stock code + Strategy code
            portfolioDetailView.Sort = portfolioDetailTbl.codeColumn.ColumnName + "," + portfolioDetailTbl.subCodeColumn.ColumnName;
            DataRowView[] portfolioDetailFound;
            databases.baseDS.portfolioDetailRow portfolioDataRow;

            // Only alert on stock codes that were selected by user. 
            for (int alertId = 0; alertId < alertList.Length; alertId++)
            {
                // Check if alert's strategy in user's wish list ??
                portfolioDetailFound = portfolioDetailView.FindRows(new object[] { alertList[alertId].StockCode, alertList[alertId].Strategy.Trim() });
                for (int dataIdx = 0; dataIdx < portfolioDetailFound.Length; dataIdx++)
                {
                    // Check if time alert's time scale in user's wish list ??
                    portfolioDataRow = ((databases.baseDS.portfolioDetailRow)portfolioDetailFound[dataIdx].Row);
                    timeScaleList = common.MultiValueString.String2List(portfolioDataRow.data.Trim());
                    if (!timeScaleList.Contains(alertList[alertId].TimeScale.Code)) continue;
                    
                    //Ignore duplicate alerts.
                    tradeAlertRow = databases.DbAccess.GetAlert(alertList[alertId].OnDateTime, 
                                                                  portfolioDataRow.portfolio,
                                                                  alertList[alertId].StockCode,
                                                                  alertList[alertId].Strategy,
                                                                  alertList[alertId].TimeScale.Code,
                                                                  AppTypes.CommonStatus.All);
                    if (tradeAlertRow != null) continue;

                    //Availabe stock
                    if (withAplicableCheckInAlert)
                    {
                        databases.baseDS.stockExchangeRow stockExchangeRow = databases.DbAccess.GetStockExchange(alertList[alertId].StockCode);
                        int sell2BuyInterval = (stockExchangeRow==null?0:stockExchangeRow.minBuySellDay);
                        availabeQty = databases.DbAccess.GetAvailableStock(alertList[alertId].StockCode, portfolioDataRow.portfolio,
                                                                             sell2BuyInterval, alertList[alertId].OnDateTime);

                        //Aplicable to sell
                        if ((alertList[alertId].TradePoint.TradeAction == AppTypes.TradeActions.Sell ||
                              alertList[alertId].TradePoint.TradeAction == AppTypes.TradeActions.ClearAll) && (availabeQty <= 0)) continue;

                    }
                    else availabeQty = 0;


                    string infoText = alertList[alertId].TradePoint.BusinessInfo.ToText().Trim();
                    infoText = (infoText != "" ? infoText : common.Consts.constNotAvailable);

                    //Create alert template message, AlertMessageText() will convert it to specified-language text.
                    msg = Consts.constTextMergeMarkerLEFT + "price" + Consts.constTextMergeMarkerRIGHT + " : " + alertList[alertId].Price.ToString() + common.Consts.constCRLF +
                          Consts.constTextMergeMarkerLEFT + "volume" + Consts.constTextMergeMarkerRIGHT + " : " + alertList[alertId].Volume.ToString() + common.Consts.constCRLF +
                          Consts.constTextMergeMarkerLEFT + "marketInfo" + Consts.constTextMergeMarkerRIGHT + " : " + infoText + common.Consts.constCRLF;
                    if (availabeQty >0)
                    {
                        msg += Consts.constTextMergeMarkerLEFT + "ownedQty" + Consts.constTextMergeMarkerRIGHT + " : " + availabeQty.ToString() + common.Consts.constCRLF;
                    }

                    CreateTradeAlert(tradeAlertTbl, portfolioDataRow.portfolio, alertList[alertId].StockCode, alertList[alertId].Strategy,
                                     alertList[alertId].TimeScale, alertList[alertId].TradePoint, alertList[alertId].OnDateTime, msg);
                }
            }
            databases.DbAccess.UpdateData(tradeAlertTbl);
        }

        private static void CreateTradeAlert(databases.baseDS.tradeAlertDataTable tradeAlertTbl,string portfolioCode,
                                             string stockCode, string strategy, AppTypes.TimeScale timeScale, TradePointInfo info, DateTime onTime, string msg)
        {
            databases.baseDS.tradeAlertRow row = tradeAlertTbl.NewtradeAlertRow();
            databases.AppLibs.InitData(row);
            row.onTime = onTime;
            row.portfolio = portfolioCode;
            row.stockCode = stockCode;
            row.timeScale = timeScale.Code; 
            row.strategy = strategy; 
            row.status = (byte)AppTypes.CommonStatus.New; 
            row.tradeAction = (byte)info.TradeAction;
            row.subject = info.TradeAction.ToString();
            row.msg = msg;
            tradeAlertTbl.AddtradeAlertRow(row);
        }

        public static void CreateTradeAlert()
        {
            CreateTradeAlert(null, null, null);
        }
        public static void CreateTradeAlert(onProcessStart onStartFunc, onProcessItem onProcessItemFunc, onProcessEnd onEndFunc)
        {
            DateTime frDate = common.Consts.constNullDate;
            DateTime toDate = DateTime.Now;
            
            //Run all strategy analysis for all stocks.
            databases.tmpDS.stockCodeDataTable stockCodeTbl = new databases.tmpDS.stockCodeDataTable();
            databases.DbAccess.LoadData(stockCodeTbl, AppTypes.CommonStatus.Enable);

            //application.AnalysisData data = new application.AnalysisData(commonClass.Settings.sysDefaultTimeScale, "", AppTypes.DataAccessMode.WebService);
            application.AnalysisData data = new application.AnalysisData();
            data.DataTimeRange = AppTypes.TimeRanges.None;
            data.DataMaxCount = Settings.sysGlobal.AlertDataCount;

            TradeAlert[] tradeAlertList = new TradeAlert[0];
            StringCollection strategyList = new StringCollection();
            for (int idx = 0; idx < application.Strategy.Data.MetaList.Values.Length; idx++)
            {
                application.Strategy.Meta meta = (application.Strategy.Meta)application.Strategy.Data.MetaList.Values[idx];
                if (meta.Type != AppTypes.StrategyTypes.Strategy) continue;
                strategyList.Add(((application.Strategy.Meta)application.Strategy.Data.MetaList.Values[idx]).Code);
            }

            if (onStartFunc != null) onStartFunc(stockCodeTbl.Count);
            
            DateTime alertDate;
            DateTime alertFrDate = toDate.Date;
            DateTime alertToDate = toDate;
            for (int stockCodeIdx = 0; stockCodeIdx < stockCodeTbl.Count; stockCodeIdx++)
            {
                if (onProcessItemFunc != null)
                    if (!onProcessItemFunc(stockCodeTbl[stockCodeIdx].code)) break;

                //foreach (AppTypes.TimeScale timeScale in AppTypes.myTimeScales) //???
                AppTypes.TimeScale timeScale = AppTypes.TimeScaleFromCode("D1");
                {
                    data.DataStockCode = stockCodeTbl[stockCodeIdx].code;
                    data.DataTimeScale = timeScale;
                    data.LoadData();
                    for (int strategyIdx = 0; strategyIdx < strategyList.Count; strategyIdx++)
                    {
                        application.Strategy.Data.ClearCache();
                        application.Strategy.Data.TradePoints advices = application.Strategy.Libs.Analysis(data, strategyList[strategyIdx].Trim());
                        
                        if ( (advices == null) || (advices.Count==0)) continue;
                        
                        //Only check the last advices for alert
                        TradePointInfo tradeInfo = (TradePointInfo)advices[advices.Count-1];
                        alertDate = DateTime.FromOADate(data.DateTime[tradeInfo.DataIdx]);
                        
                        //Ignore alerts that out of date range.
                        if (alertDate < alertFrDate || alertDate > alertToDate) continue;
                        Array.Resize(ref tradeAlertList, tradeAlertList.Length + 1);

                        tradeAlertList[tradeAlertList.Length-1] = new TradeAlert(stockCodeTbl[stockCodeIdx].code.Trim(), strategyList[strategyIdx].Trim(),
                                                                                 timeScale, alertDate,
                                                                                 data.Close[tradeInfo.DataIdx],
                                                                                 data.Volume[tradeInfo.DataIdx],tradeInfo);
                    }
                }
            }
            stockCodeTbl.Dispose();

            //Create alerts in the day
            CreateTradeAlert(tradeAlertList);

            //Save last lun date
            //SaveLastRunTime(toDate);
            if (onEndFunc != null) onEndFunc();
        }
        #endregion
    }
}
