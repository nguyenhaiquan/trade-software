using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using commonTypes;
using commonClass;

namespace Imports
{
    public class Libs
    {
        Stock.ssi_StockImport ssiStockImport = null;
        //public static bool ImportFromWeb(DateTime updateTime, databases.baseDS.exchangeDetailRow exchangeDetailRow)
        public bool ImportFromWeb(DateTime updateTime, string market)
        {
            bool retVal = true;

            if (ssiStockImport == null)
                ssiStockImport = new Stock.ssi_StockImport(market);

            //Import price from SSI page , then aggregate data to table PriceDataSum follow minutes, hours, date
            retVal = ssiStockImport.ImportFromWeb(updateTime, market);
            return retVal;
        }


        public static void ReAggregatePriceData(string code,CultureInfo stockCulture)
        {
            //Load main  pricedata
            databases.baseDS.priceDataDataTable priceTbl = new databases.baseDS.priceDataDataTable();
            databases.DbAccess.LoadData(priceTbl, AppTypes.MainDataTimeScale.Code, DateTime.MinValue, DateTime.MaxValue, code);
            if (priceTbl == null) return;
            //Delete all summ pricedata
            databases.DbAccess.DeletePriceSumData(code);
            databases.AppLibs.AggregatePriceData(priceTbl, stockCulture, null);
        }

        //Detect new stockCode and create new one
        public static void AddNewCode(string market, databases.importDS.importPriceDataTable tbl, OnCodeAdded onAddstockCodeFunc)
        {
            databases.baseDS.stockCodeDataTable stockCodeTbl = new databases.baseDS.stockCodeDataTable();
            for (int count = 0; count < tbl.Count; count++)
            {
                if (tbl[count].RowState == DataRowState.Deleted) continue;
                if (AddNewCode(tbl[count].stockCode, market, stockCodeTbl) != null &&
                    onAddstockCodeFunc != null) onAddstockCodeFunc(tbl[count].stockCode);
            }
            databases.DbAccess.UpdateData(stockCodeTbl);
        }
        public static void AddNewCode(databases.importDS.importPriceDataTable tbl, OnCodeAdded onAddstockCodeFunc)
        {
            databases.tmpDS.stockCodeRow shortCodeRow;
            databases.baseDS.stockCodeDataTable stockCodeTbl = new databases.baseDS.stockCodeDataTable();
            for (int count = 0; count < tbl.Count; count++)
            {
                if (tbl[count].RowState == DataRowState.Deleted) continue;
                shortCodeRow = databases.AppLibs.FindAndCache_StockCodeShort(tbl[count].stockCode);
                if (shortCodeRow == null) continue;

                if (AddNewCode(tbl[count].stockCode, shortCodeRow.stockExchange, stockCodeTbl) != null &&
                    onAddstockCodeFunc != null) onAddstockCodeFunc(tbl[count].stockCode);
            }
            databases.DbAccess.UpdateData(stockCodeTbl);
        }

        public static void AddNewCode(databases.baseDS.priceDataDataTable tbl, string stockEchangeCode, OnCodeAdded onAddstockCodeFunc)
        {
            databases.baseDS.stockCodeDataTable stockCodeTbl = new databases.baseDS.stockCodeDataTable();
            for (int count = 0; count < tbl.Count; count++)
            {
                if (tbl[count].RowState == DataRowState.Deleted) continue;
                if (AddNewCode(tbl[count].stockCode, stockEchangeCode, stockCodeTbl) != null &&
                    onAddstockCodeFunc != null)
                    onAddstockCodeFunc(tbl[count].stockCode);
            }
            databases.DbAccess.UpdateData(stockCodeTbl);
        }
        public static databases.baseDS.stockCodeRow AddNewCode(string comCode, string stockEchangeCode,
                                                               databases.baseDS.stockCodeDataTable toStockCodeTbl)
        {
            databases.baseDS.stockCodeRow stockCodeRow;
            stockCodeRow = application.SysLibs.FindAndCache(toStockCodeTbl, comCode);
            if (stockCodeRow == null)
            {
                stockCodeRow = toStockCodeTbl.NewstockCodeRow();
                databases.AppLibs.InitData(stockCodeRow);
                stockCodeRow.code = comCode;
                stockCodeRow.name = "<New>";
                stockCodeRow.code = comCode;
                stockCodeRow.tickerCode = comCode;
                stockCodeRow.stockExchange = stockEchangeCode;
                stockCodeRow.regDate = DateTime.Today;
                toStockCodeTbl.AddstockCodeRow(stockCodeRow);
            }
            return stockCodeRow;
        }

        public static databases.baseDS.priceDataSumRow FindAndCache(databases.baseDS.priceDataSumDataTable tbl, string stockCode, string timeScale, DateTime onDate)
        {
            databases.baseDS.priceDataSumRow row = tbl.FindBytypestockCodeonDate(timeScale, stockCode, onDate);
            if (row != null) return row;
            databases.baseDSTableAdapters.priceDataSumTA dataTA = new databases.baseDSTableAdapters.priceDataSumTA();
            dataTA.ClearBeforeFill = false;
            dataTA.Fill(tbl, stockCode, timeScale, onDate, onDate);
            row = tbl.FindBytypestockCodeonDate(timeScale, stockCode, onDate);
            if (row != null) return row;
            return null;
        }

        // string(0001 Oil & Gas) ->  (0001,Oil & Gas)
        public static common.myKeyValueItem SplitKeyValue(string str,string separator)
        {
            int pos = str.IndexOf(separator);
            if (pos <= 0) return null;
            return new common.myKeyValueItem(str.Substring(0, pos).Trim(), str.Substring(pos + 1).Trim());
        }


        public static void Reset()
        {
            myDailyData.Reset();
        }
        // myDailyPrice is used to get transaction volume data from total daily volume data
        public class DailyData
        {
            DateTime lastTime = DateTime.Today;
            string timeType = "D1";
            databases.baseDS.priceDataSumDataTable priceSumTbl = new databases.baseDS.priceDataSumDataTable();
            public void UpdateData(databases.importDS.importPriceRow row)
            {
                if (lastTime != row.onDate.Date)
                {
                    Reset();
                    lastTime = row.onDate.Date;
                }
                databases.baseDS.priceDataSumRow priceRow = GetData(row);
                if (priceRow == null)
                {
                    priceRow = priceSumTbl.NewpriceDataSumRow();
                    databases.AppLibs.InitData(priceRow);
                    priceRow.type = timeType;
                    priceRow.stockCode = row.stockCode;
                    priceRow.onDate = row.onDate.Date;
                    priceSumTbl.AddpriceDataSumRow(priceRow);
                }
                //Now only volume need to be processed
                priceRow.volume = row.volume;
                priceRow.closePrice = row.closePrice;
            }
            public databases.baseDS.priceDataSumRow GetData(databases.importDS.importPriceRow row)
            {
                return FindAndCache(priceSumTbl, row.stockCode, timeType, row.onDate.Date);
            }
            public databases.baseDS.priceDataSumRow GetData(string stockCode,DateTime onDate)
            {
                return FindAndCache(priceSumTbl,stockCode, timeType,onDate.Date);
            }
            // Importing can run continuously for many days, 
            // after each day the last data should be reset to reduce system resources
            public void Reset()
            {
                priceSumTbl.Clear();
            }
        }

        // Keep last imported data by day
        private static DailyData myDailyData = new DailyData();
        // importPrice->priceData
        private static void AddImportPrice(databases.importDS.importPriceDataTable importPriceTbl,
                                           DailyData dailyPrice, databases.baseDS.priceDataDataTable priceDataTbl)
        {
            databases.baseDS.priceDataSumRow dailyPriceRow;
            databases.baseDS.priceDataRow priceDataRow;
            decimal volume = 0;
            for (int idx = 0; idx < importPriceTbl.Count; idx++)
            {
                //Invalid price, ignore
                if (importPriceTbl[idx].closePrice <= 0) continue;

                volume = importPriceTbl[idx].volume;
                // If this is total volume then minus the last volume to get the real volume in the period
                if (importPriceTbl[idx].isTotalVolume)
                {
                    dailyPriceRow = dailyPrice.GetData(importPriceTbl[idx]);
                    if (dailyPriceRow != null)
                    {
                        volume -= dailyPriceRow.volume;
                    }
                    if (volume <= 0) continue;
                }

                priceDataRow = priceDataTbl.NewpriceDataRow();
                databases.AppLibs.InitData(priceDataRow);
                priceDataRow.onDate = importPriceTbl[idx].onDate;
                priceDataRow.stockCode = importPriceTbl[idx].stockCode;
                priceDataRow.closePrice = importPriceTbl[idx].closePrice;
                priceDataRow.openPrice = importPriceTbl[idx].closePrice;
                priceDataRow.highPrice = importPriceTbl[idx].closePrice;
                priceDataRow.lowPrice = importPriceTbl[idx].closePrice;
                priceDataRow.volume = volume;
                //Fix other invalid price
                if (priceDataRow.highPrice <= 0)  priceDataRow.highPrice = priceDataRow.closePrice;
                if (priceDataRow.lowPrice <= 0) priceDataRow.lowPrice = priceDataRow.closePrice;
                if (priceDataRow.openPrice <= 0) priceDataRow.openPrice = priceDataRow.closePrice;

                priceDataRow.lowPrice = Math.Min(priceDataRow.lowPrice, priceDataRow.closePrice);
                priceDataRow.lowPrice = Math.Min(priceDataRow.lowPrice, priceDataRow.openPrice);
                priceDataRow.lowPrice = Math.Min(priceDataRow.lowPrice, priceDataRow.highPrice);

                priceDataRow.highPrice = Math.Max(priceDataRow.highPrice, priceDataRow.closePrice);
                priceDataRow.highPrice = Math.Max(priceDataRow.highPrice, priceDataRow.openPrice);
                priceDataRow.highPrice = Math.Max(priceDataRow.highPrice,priceDataRow.lowPrice);

                priceDataTbl.AddpriceDataRow(priceDataRow);

                //Update the last row
                dailyPrice.UpdateData(importPriceTbl[idx]);
            }
        }
        public static void AddIdmportPrice(databases.importDS.importPriceDataTable importPriceTbl,
                                          databases.baseDS.priceDataDataTable priceDataTbl)
        {
            AddImportPrice(importPriceTbl, myDailyData, priceDataTbl);
        }


        // Maybe there are someting wrong with price importing, 
        // use ReImportPricedata to add [importPrice] to [priceData,priceDataSum]
        public static void ReImportPricedata(DateTime frDate, DateTime toDate, string stockCode, CultureInfo culture)
        {
            databases.importDS myImportDS = new databases.importDS();
            databases.importDSTableAdapters.importPriceTA importPriceTA = new databases.importDSTableAdapters.importPriceTA();

            if (stockCode.Trim() == "") importPriceTA.FillByDate(myImportDS.importPrice, frDate, toDate);
            else importPriceTA.FillByDateAndCode(myImportDS.importPrice, frDate, toDate, stockCode);
            AddNewCode(myImportDS.importPrice, null);
            databases.DbAccess.UpdateData(myImportDS.importPrice);

            databases.baseDS myBaseDS = new databases.baseDS();
            AddImportPrice(myImportDS.importPrice, new DailyData(), myBaseDS.priceData);
            databases.DbAccess.UpdateData(myBaseDS.priceData);
            // In VN culture : start of week is Monday (not Sunday) 
            databases.AppLibs.AggregatePriceData(myBaseDS.priceData, culture, null);
        }
        
        // Stock Exchange
        public static databases.baseDS.stockExchangeRow AddStockExchange(databases.baseDS.stockExchangeDataTable tbl, string code)
        {
            databases.baseDS.stockExchangeRow stockExchangeRow = application.SysLibs.FindAndCache_StockExchange(code);
            if (stockExchangeRow == null)
            {
                stockExchangeRow = tbl.NewstockExchangeRow();
                databases.AppLibs.InitData(stockExchangeRow);
                stockExchangeRow.code = code;
                tbl.AddstockExchangeRow(stockExchangeRow);
                databases.DbAccess.UpdateData(stockExchangeRow);
                common.SysLog.WriteLog(" - Add stockExchange" + common.Consts.constTab + code);
            }
            return stockExchangeRow;
        }

     
        /// <summary>
        /// Get aggregation date/time from a date/time
        /// </summary>
        /// <param name="type"></param>
        /// <param name="onDateTime"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static DateTime AggregateDateTime(AppTypes.TimeScale timeScale, DateTime onDateTime, CultureInfo ci)
        {
            if (timeScale.Type == AppTypes.TimeScaleTypes.RealTime) return onDateTime;
            switch (timeScale.Type)
            {
                case AppTypes.TimeScaleTypes.Minnute:
                     int newMin = ((int)(onDateTime.Minute / timeScale.AggregationValue)) * timeScale.AggregationValue;
                     return onDateTime.Date.AddHours(newMin);
                case AppTypes.TimeScaleTypes.Hour:
                     int newHour = ((int)(onDateTime.Hour / timeScale.AggregationValue)) * timeScale.AggregationValue;
                     return onDateTime.Date.AddHours(newHour);
                case AppTypes.TimeScaleTypes.Day:
                     int newDay = ((int)((onDateTime.Day - 1) / timeScale.AggregationValue)) * timeScale.AggregationValue + 1;
                     return common.dateTimeLibs.MakeDate(newDay, onDateTime.Month, onDateTime.Year);
                case AppTypes.TimeScaleTypes.Week:
                     int weekNo = onDateTime.DayOfYear / 7;
                     int newWeek = ((int)(weekNo / timeScale.AggregationValue)) * timeScale.AggregationValue;
                     DateTime newDate = common.dateTimeLibs.MakeDate(1, 1, onDateTime.Year).AddDays(newWeek * 7);
                     return common.dateTimeLibs.StartOfWeek(newDate, ci);
                case AppTypes.TimeScaleTypes.Month:
                     int newMonth = ((int)((onDateTime.Month - 1) / timeScale.AggregationValue)) * timeScale.AggregationValue + 1;
                     return common.dateTimeLibs.MakeDate(1, newMonth, onDateTime.Year);
                case AppTypes.TimeScaleTypes.Year:
                     int newYear = ((int)((onDateTime.Year - 1) / timeScale.AggregationValue)) * timeScale.AggregationValue + 1;
                     return common.dateTimeLibs.MakeDate(1, 1, newYear);
                default:
                    common.system.ThrowException("Invalid argument in AggregateDateTime()");
                    break;
            }
            return onDateTime;
        }
    }
}
