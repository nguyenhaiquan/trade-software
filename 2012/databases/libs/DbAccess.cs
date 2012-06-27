using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using commonTypes;

namespace databases
{
    public class DbAccess
    {
        public static void ClearDbConnection() { }

        #region system
        private static baseDSTableAdapters.sysLogTA syslogTA = new baseDSTableAdapters.sysLogTA();
        private static baseDSTableAdapters.sysConfigTA sysConfigTA = new baseDSTableAdapters.sysConfigTA();
        private static baseDSTableAdapters.sysCodeCatTA sysCodeCatTA = new baseDSTableAdapters.sysCodeCatTA();
        private static baseDSTableAdapters.sysCodeTA sysCodeTA = new baseDSTableAdapters.sysCodeTA();
        private static baseDSTableAdapters.sysAutoKeyTA sysAutoKeyTA = new baseDSTableAdapters.sysAutoKeyTA();
        private static baseDSTableAdapters.sysAutoKeyPendingTA sysAutoKeyPendingTA = new baseDSTableAdapters.sysAutoKeyPendingTA();

        private static baseDS.sysLogDataTable syslogTbl = new baseDS.sysLogDataTable();
        private static baseDS.sysLogRow syslogRow;
        public static void WriteSyslog(byte logType, string investorCode, string desc, string source, string msg)
        {
            syslogRow = syslogTbl.NewsysLogRow();
            AppLibs.InitData(syslogRow);
            syslogRow.investorCode = investorCode;
            syslogRow.type = logType;
            if (desc != null) syslogRow.description = desc;
            if (source!=null) syslogRow.source = source;
            if (msg != null) syslogRow.message = msg;
            syslogTbl.AddsysLogRow(syslogRow);
            UpdateData(syslogRow);
        }
        public static void WriteSyslog(AppTypes.SyslogTypes logType, string investorCode, string desc, string source, string msg)
        {
            syslogRow = syslogTbl.NewsysLogRow();
            AppLibs.InitData(syslogRow);
            syslogRow.investorCode = investorCode;
            syslogRow.type = (byte)logType;
            if (desc != null) syslogRow.description = desc;
            if (source != null) syslogRow.source = source;
            if (msg != null) syslogRow.message = msg;
            syslogTbl.AddsysLogRow(syslogRow);
            UpdateData(syslogRow);
        }
        public static void WriteSyslog(Exception er,string investorCode)
        {
            WriteSyslog(AppTypes.SyslogTypes.Exception,investorCode, er.TargetSite.ToString(), er.Source, er.Message.Trim() + " " + er.StackTrace.Trim());
        }

        /// <summary>
        /// Return NULL if the [key] not found
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            baseDS.sysConfigDataTable tbl = sysConfigTA.GetByKey(key);
            if (tbl.Count <= 0) return "";
            return tbl[0].value;
        }
        public static void SaveConfig(string key, string value)
        {
            baseDS.sysConfigDataTable sysConfigTbl = sysConfigTA.GetByKey(key);
            baseDS.sysConfigRow row;
            if (sysConfigTbl.Count == 0)
            {
                row = sysConfigTbl.NewsysConfigRow();
                row.key = key; row.value = value;
                sysConfigTbl.AddsysConfigRow(row);
            }
            else
            {
                row = sysConfigTbl[0];
                row.value = value;
            }
            sysConfigTA.Update(row);
            row.AcceptChanges();
        }

        public static void GetConfig(ref StringCollection aFields)
        {
            for (int idx = 0; idx < aFields.Count; idx++)
            {
                aFields[idx] = DbAccess.GetConfig(aFields[idx].ToString());
                if (common.configuration.withEncryption && aFields[idx].ToString().Trim() != "")
                {
                    aFields[idx] = common.cryption.Decrypt(aFields[idx].ToString());
                }
            }
        }
        public static void SaveConfig(StringCollection aFields, StringCollection aValues)
        {
            string value;
            for (int idx = 0; idx < aFields.Count; idx++)
            {
                value = aValues[idx].ToString();
                if (common.configuration.withEncryption && (value.Trim() != ""))
                {
                    value = common.cryption.Encrypt(value);
                }
                DbAccess.SaveConfig(aFields[idx].ToString(), value);
            }
        }

        public static baseDS.sysAutoKeyPendingRow UpdateAutoKeyPending(string key, string value, DateTime timestamp)
        {
            baseDS.sysAutoKeyPendingDataTable tbl = sysAutoKeyPendingTA.GetByKeyValue(key, value);
            baseDS.sysAutoKeyPendingRow row;
            if (tbl.Count > 0)
            {
                row = tbl[0]; row.timeStamp = timestamp;
            }
            else
            {
                row = tbl.NewsysAutoKeyPendingRow();
                row.key = key; row.value = value; row.timeStamp = timestamp;
                tbl.AddsysAutoKeyPendingRow(row);
            }
            sysAutoKeyPendingTA.Update(row);
            row.AcceptChanges();
            return row;
        }
        public static baseDS.sysAutoKeyRow NewAutoKeyValue(string key)
        {
            baseDS.sysAutoKeyDataTable sysAutoKeyTbl = sysAutoKeyTA.GetByKey(key);
            baseDS.sysAutoKeyRow sysAutoKeyRow;
            if (sysAutoKeyTbl.Count == 0)
            {
                sysAutoKeyRow = sysAutoKeyTbl.NewsysAutoKeyRow();
                sysAutoKeyRow.key = key; sysAutoKeyRow.value = 0;
                sysAutoKeyTbl.AddsysAutoKeyRow(sysAutoKeyRow);
            }
            else sysAutoKeyRow = sysAutoKeyTbl[0];
            sysAutoKeyRow.value = sysAutoKeyRow.value + 1;
            sysAutoKeyTA.Update(sysAutoKeyRow);
            sysAutoKeyRow.AcceptChanges();
            return sysAutoKeyRow;
        }
        #endregion system

        #region import
        private static importDSTableAdapters.importPriceTA importPriceTA = new importDSTableAdapters.importPriceTA();
        public static void UpdateData(importDS.importPriceDataTable tbl)
        {
            importPriceTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(importDS.importPriceRow row)
        {
            importPriceTA.Update(row);
            row.AcceptChanges();
        }

        #endregion

        #region access objects
        private static baseDSTableAdapters.bizIndustryTA bizIndustryTA = new baseDSTableAdapters.bizIndustryTA();
        private static baseDSTableAdapters.bizSuperSectorTA bizSuperSectorTA = new baseDSTableAdapters.bizSuperSectorTA();
        private static baseDSTableAdapters.bizSectorTA bizSectorTA = new baseDSTableAdapters.bizSectorTA();
        private static baseDSTableAdapters.bizSubSectorTA bizSubSectorTA = new baseDSTableAdapters.bizSubSectorTA();

        private static baseDSTableAdapters.feedbackCatTA feedbackCatTA = new baseDSTableAdapters.feedbackCatTA();
        private static baseDSTableAdapters.countryTA countryTA = new baseDSTableAdapters.countryTA();
        private static baseDSTableAdapters.currencyTA currencyTA = new baseDSTableAdapters.currencyTA();
        private static baseDSTableAdapters.investorCatTA investorCatTA = new baseDSTableAdapters.investorCatTA();
        private static baseDSTableAdapters.employeeRangeTA employeeRangeTA = new baseDSTableAdapters.employeeRangeTA();
        private static baseDSTableAdapters.stockExchangeTA stockExchangeTA = new baseDSTableAdapters.stockExchangeTA();
        private static baseDSTableAdapters.exchangeDetailTA exchangeDetailTA = new baseDSTableAdapters.exchangeDetailTA();

        private static tmpDSTableAdapters.stockCodeTA shortStockCodeTA = new tmpDSTableAdapters.stockCodeTA();
        private static tmpDSTableAdapters.investorStockTA sumInvestorStockTA = new databases.tmpDSTableAdapters.investorStockTA();
        private static tmpDSTableAdapters.interestedCodeTA interestedCodeTA = new databases.tmpDSTableAdapters.interestedCodeTA();

        private static baseDSTableAdapters.stockCodeTA stockCodeTA = new baseDSTableAdapters.stockCodeTA();
        private static baseDSTableAdapters.investorTA investorTA = new baseDSTableAdapters.investorTA();
        private static baseDSTableAdapters.investorStockTA investorStockTA = new baseDSTableAdapters.investorStockTA();
        private static baseDSTableAdapters.transactionsTA transactionsTA = new baseDSTableAdapters.transactionsTA();

        private static baseDSTableAdapters.portfolioTA portfolioTA = new baseDSTableAdapters.portfolioTA();
        private static baseDSTableAdapters.portfolioDetailTA portfolioDetailTA = new baseDSTableAdapters.portfolioDetailTA();

        private static baseDSTableAdapters.priceDataTA priceDataTA = new baseDSTableAdapters.priceDataTA();
        private static baseDSTableAdapters.priceDataSumTA priceDataSumTA = new baseDSTableAdapters.priceDataSumTA();
        private static baseDSTableAdapters.priceDataStatTA priceDataStatTA = new baseDSTableAdapters.priceDataStatTA();
        private static baseDSTableAdapters.lastPriceDataTA lastPriceDataTA = new baseDSTableAdapters.lastPriceDataTA();

        private static baseDSTableAdapters.tradeAlertTA tradeAlertTA = new baseDSTableAdapters.tradeAlertTA();
        private static baseDSTableAdapters.messagesTA messagesTA = new baseDSTableAdapters.messagesTA();
        #endregion access object

        #region get/load method
        public static void LoadFromSQL(DataTable tbl, string sqlCmd)
        {
            SqlDataAdapter dataTA = new SqlDataAdapter(sqlCmd, SysLibs.dbConnectionString);
            dataTA.Fill(tbl);
        }

        public static void LoadData(baseDS.sysLogDataTable tbl,DateTime frDate,DateTime toDate)
        {
            syslogTA.ClearBeforeFill = false;
            syslogTA.FillByDate(tbl,frDate,toDate);
        }

        public static void LoadData(baseDS.sysAutoKeyDataTable tbl)
        {
            sysAutoKeyTA.ClearBeforeFill = false;
            sysAutoKeyTA.Fill(tbl);
        }

        public static void LoadData(baseDS.stockExchangeDataTable tbl)
        {
            stockExchangeTA.ClearBeforeFill = false;
            stockExchangeTA.Fill(tbl);
        }
        public static void LoadData(baseDS.exchangeDetailDataTable tbl, string marketCode)
        {
            exchangeDetailTA.ClearBeforeFill = false;
            exchangeDetailTA.FillByMarketCode(tbl, marketCode);
        }
        public static void LoadData(baseDS.exchangeDetailDataTable tbl)
        {
            exchangeDetailTA.ClearBeforeFill = false;
            exchangeDetailTA.Fill(tbl);
        }

        public static void LoadData(baseDS.employeeRangeDataTable tbl)
        {
            employeeRangeTA.ClearBeforeFill = false;
            employeeRangeTA.Fill(tbl);
        }

        public static void LoadData(baseDS.bizIndustryDataTable tbl)
        {
            bizIndustryTA.ClearBeforeFill = false;
            bizIndustryTA.Fill(tbl);
        }
        public static void LoadData(baseDS.bizSuperSectorDataTable tbl)
        {
            bizSuperSectorTA.ClearBeforeFill = false;
            bizSuperSectorTA.Fill(tbl);
        }
        public static void LoadData(baseDS.bizSectorDataTable tbl)
        {
            bizSectorTA.ClearBeforeFill = false;
            bizSectorTA.Fill(tbl);
        }
        
        public static void LoadData(baseDS.bizSubSectorDataTable tbl)
        {
            bizSubSectorTA.ClearBeforeFill = false;
            bizSubSectorTA.Fill(tbl);
        }
        public static void LoadDataByIndustryCode(baseDS.bizSubSectorDataTable tbl, string industryCode)
        {
            bizSubSectorTA.ClearBeforeFill = false;
            bizSubSectorTA.FillByIndustryCode(tbl, industryCode);
        }
        public static void LoadDataBySuperSectorCode(baseDS.bizSubSectorDataTable tbl, string superSectorCode)
        {
            bizSubSectorTA.ClearBeforeFill = false;
            bizSubSectorTA.FillBySuperSector(tbl, superSectorCode);
        }
        public static void LoadDataBySectorCode(baseDS.bizSubSectorDataTable tbl, string sectorCode)
        {
            bizSubSectorTA.ClearBeforeFill = false;
            bizSubSectorTA.FillBySectorCode(tbl, sectorCode);
        }

        public static void LoadData(baseDS.feedbackCatDataTable tbl,string language)
        {
            feedbackCatTA.ClearBeforeFill = false;
            feedbackCatTA.Fill(tbl, language);
        }

        public static void LoadData(baseDS.countryDataTable tbl)
        {
            countryTA.ClearBeforeFill = false;
            countryTA.Fill(tbl);
        }
        public static void LoadData(baseDS.currencyDataTable tbl)
        {
            currencyTA.ClearBeforeFill = false;
            currencyTA.Fill(tbl);
        }
        public static void LoadData(baseDS.investorCatDataTable tbl)
        {
            investorCatTA.ClearBeforeFill = false;
            investorCatTA.Fill(tbl);
        }
        public static void LoadData(baseDS.sysAutoKeyPendingDataTable tbl,string key)
        {
            sysAutoKeyPendingTA.ClearBeforeFill = false;
            sysAutoKeyPendingTA.FillByKey(tbl, key);
        }

        public static void LoadData(baseDS.sysCodeDataTable tbl,string catCode)
        {
            sysCodeTA.ClearBeforeFill = false;
            sysCodeTA.FillByCat(tbl,catCode);
        }
        public static void LoadData(baseDS.sysCodeCatDataTable tbl)
        {
            sysCodeCatTA.ClearBeforeFill = false;
            sysCodeCatTA.Fill(tbl);
        }
        public static void LoadData(baseDS.stockCodeDataTable tbl, AppTypes.CommonStatus status)
        {
            stockCodeTA.ClearBeforeFill = false;
            stockCodeTA.FillByStatusMask(tbl, ((byte)status).ToString());
        }

        public static void LoadData(baseDS.stockCodeDataTable tbl, string code)
        {
            stockCodeTA.ClearBeforeFill = false;
            if (code == null) stockCodeTA.Fill(tbl);
            else stockCodeTA.FillByCode(tbl, code);
        }
        public static void LoadData(baseDS.stockCodeDataTable tbl)
        {
            stockCodeTA.ClearBeforeFill = false;
            stockCodeTA.Fill(tbl);
        }

        public static void LoadData(tmpDS.stockCodeDataTable tbl, string code)
        {
            shortStockCodeTA.ClearBeforeFill = false;
            shortStockCodeTA.FillByCode(tbl, code);
        }
        public static void LoadData(tmpDS.stockCodeDataTable tbl, AppTypes.CommonStatus status)
        {
            shortStockCodeTA.ClearBeforeFill = false;
            shortStockCodeTA.FillByStatusMask(tbl, ((byte)status).ToString());
        }

        public static void LoadData(tmpDS.investorDataTable tbl)
        {
            tmpDSTableAdapters.investorTA investorTA = new tmpDSTableAdapters.investorTA();
            investorTA.ClearBeforeFill = false;
            investorTA.Fill(tbl);
        }
        public static void LoadData(baseDS.investorDataTable tbl, string code)
        {
            investorTA.ClearBeforeFill = false;
            if (code == null) investorTA.Fill(tbl);
            else investorTA.FillByCode(tbl, code);
        }

        public static void LoadData(tmpDS.investorStockDataTable tbl)
        {
            sumInvestorStockTA.ClearBeforeFill = false;
            sumInvestorStockTA.Fill(tbl);
        }
        public static void LoadData(tmpDS.investorStockDataTable tbl,string investorCode)
        {
            sumInvestorStockTA.ClearBeforeFill = false;
            sumInvestorStockTA.FillByInvestor(tbl, investorCode);
        }
        public static void LoadData(tmpDS.interestedCodeDataTable tbl,string investorCode)
        {
            interestedCodeTA.ClearBeforeFill = false;
            interestedCodeTA.FillByInvestor(tbl, investorCode);
        }

        public static void LoadData(baseDS.investorStockDataTable tbl, string portfolioCode)
        {
            investorStockTA.ClearBeforeFill = false;
            investorStockTA.FillByPortfolio(tbl, portfolioCode);
        }
        public static void LoadData(baseDS.investorStockDataTable tbl, string stockCode, string portfolio)
        {
            investorStockTA.ClearBeforeFill = false;
            investorStockTA.FillByPortfolioStock(tbl, portfolio, stockCode);
        }
        public static void LoadData(baseDS.investorStockDataTable tbl, string stockCode, string portfolio,DateTime buyDate)
        {
            investorStockTA.ClearBeforeFill = false;
            investorStockTA.FillByPortfolioStockBuyDate(tbl, portfolio, stockCode,buyDate);
        }

        public static void LoadData(baseDS.transactionsDataTable tbl,string portfolio,string stockCode)
        {
            transactionsTA.ClearBeforeFill = false;
            transactionsTA.FillByPortfolioStockCode(tbl, portfolio,stockCode);
        }

        public static void LoadData(baseDS.portfolioDataTable tbl, AppTypes.PortfolioTypes type)
        {
            portfolioTA.ClearBeforeFill = false;
            portfolioTA.FillByTypeMask(tbl, ((byte)type).ToString());
        }
        public static void LoadData(baseDS.portfolioDataTable tbl, string code)
        {
            portfolioTA.ClearBeforeFill = false;
            portfolioTA.FillByCode(tbl, code);
        }

        public static void LoadData(baseDS.portfolioDetailDataTable tbl, string portfolioCode)
        {
            portfolioDetailTA.ClearBeforeFill = false;
            portfolioDetailTA.FillByPortfolio(tbl, portfolioCode);
        }
        public static void LoadData(baseDS.portfolioDetailDataTable tbl, byte typeMask)
        {
            portfolioDetailTA.ClearBeforeFill = false;
            portfolioDetailTA.FillByTypeMask(tbl, ((byte)typeMask).ToString());
        }
        public static baseDS.portfolioDetailDataTable GetPortfolioDetail_ByType(AppTypes.PortfolioTypes[] types)
        {
            baseDS.portfolioDetailDataTable tbl = new baseDS.portfolioDetailDataTable();
            byte typeMask = 0;
            for (int idx = 0; idx < types.Length; idx++) typeMask += (byte)types[idx];
            DbAccess.LoadData(tbl, typeMask);
            return tbl;
        }


        //public static void LoadAbnormalData(baseDS.priceDataDataTable tbl, string code, DateTime frDate, DateTime toDate, string timeScale)
        //{
        //    priceDataTA.ClearBeforeFill = false;
        //    if (timeScale == AppTypes.MainDataTimeScale.Code)
        //    {
        //        priceDataTA.FillAbnormal(tbl, code, frDate, toDate);
        //    }
        //    else
        //    {
        //        priceDataTA.FillAbnomalSumData(tbl, code, timeScale, frDate, toDate);
        //    }
        //}
        public static void LoadData(baseDS.priceDataDataTable tbl, string timeScale,DateTime frDate,DateTime toDate,string stockCode)
        {
            priceDataTA.ClearBeforeFill = false;
            if (timeScale == AppTypes.MainDataTimeScale.Code)
            {
                if (frDate == DateTime.MinValue && toDate == DateTime.MaxValue)
                     priceDataTA.FillByCode(tbl, stockCode);
                else priceDataTA.FillByDateCode(tbl, frDate, toDate, stockCode);
            }            
            else
            {
                if (frDate == DateTime.MinValue && toDate == DateTime.MaxValue)
                     priceDataTA.FillByTypeCode(tbl, timeScale, stockCode);
                else priceDataTA.FillByTypeDateCode(tbl, timeScale, frDate, toDate, stockCode);
            }
        }
        public static void LoadData(baseDS.priceDataDataTable tbl, string timeScale, DateTime frDate,string stockCode)
        {
            try
            {
                if (tbl.Count>0)
                    common.SysLog.WriteLog(DateTime.Now.ToString() + common.Consts.constTab + "test 00: LoadData ", Settings.sysFileUserLog);

                priceDataTA.ClearBeforeFill = false;
                if (timeScale == AppTypes.MainDataTimeScale.Code)
                {
                    priceDataTA.FillByCodeFromDate(tbl, stockCode, frDate);
                }
                else
                {
                    priceDataTA.FillByTypeCodeFromDate(tbl, timeScale, stockCode, frDate);
                }
            }
            catch (Exception er)
            {
                common.SysLog.WriteLog(DateTime.Now.ToString() + common.Consts.constTab + "test 01: LoadData ", Settings.sysFileUserLog);
            }
        }

        public static void LoadLastPrice(baseDS.priceDataSumDataTable tbl, string timeScale,string stockCode)
        {
            priceDataSumTA.ClearBeforeFill = false;
            priceDataSumTA.FillLastPrice(tbl,stockCode, timeScale);
        }
        public static baseDS.priceDataSumRow GetLastPrice(string timeScale, string stockCode)
        {
            baseDS.priceDataSumDataTable tbl = priceDataSumTA.GetLastPrice(stockCode, timeScale);
            if (tbl.Count > 0) return tbl[0];
            return null;
        }

        public static void LoadData(baseDS.priceDataDataTable tbl, string timeScale,string stockCode,int maxCount)
        {
            string dataTbl = "";
            string cond = "stockCode=N'" + stockCode.Trim() + "'";
            if (timeScale == AppTypes.MainDataTimeScale.Code)
            {
                dataTbl = "priceData";
            }
            else
            {
                dataTbl = "priceDataSum";
                cond += (cond==""?"" : " AND ")+ "type=N'" + timeScale + "'";
            }
            string sqlCmd =
                "SELECT * FROM " +
                "(SELECT TOP " + maxCount.ToString() + " onDate,stockCode,openPrice,closePrice,lowPrice,highPrice,volume FROM " + dataTbl + common.Consts.constCRLF +
                  "WHERE " + cond + " ORDER BY onDate DESC)a" + common.Consts.constCRLF +
                "ORDER BY onDate";
            LoadFromSQL(tbl, sqlCmd);
        }


        public static int GetTotalPriceRow(AppTypes.TimeScale timeScale, DateTime frDate, DateTime toDate, string stockCode)
        {
            switch (timeScale.Type)
            {
                case AppTypes.TimeScaleTypes.RealTime:
                    return (int)priceDataTA.GetTotalRow(frDate, toDate, stockCode);
                default:
                    return (int)priceDataTA.GetTotalSumRow(timeScale.Code, frDate, toDate, stockCode);
            }
        }

        public static void LoadDataOneRow(baseDS.priceDataDataTable tbl,DateTime frDate, DateTime toDate, string stockCode)
        {
            priceDataTA.ClearBeforeFill = false;
            priceDataTA.FillOneByDateStockCode(tbl, frDate, toDate, stockCode);
        }
        
        public static void LoadData(baseDS.tradeAlertDataTable tbl, string portfolio, DateTime frDate, DateTime toDate,byte statusMask)
        {
            tradeAlertTA.ClearBeforeFill = false;
            tradeAlertTA.Fill(tbl,portfolio,frDate,toDate,statusMask.ToString());
        }
        public static baseDS.tradeAlertRow GetOneAlert(DateTime onDateTime, string portfolio,
                                                       string stockCode, string strategy, string timeScale, AppTypes.CommonStatus status)
        {
            baseDS.tradeAlertDataTable tbl = tradeAlertTA.GetOne(onDateTime, onDateTime, portfolio, stockCode, strategy, timeScale, ((byte)status).ToString());
            if (tbl == null || tbl.Count == 0) return null;
            return tbl[0];
        }

        public static baseDS.tradeAlertDataTable GetTradeAlert(DateTime frDate, DateTime toDate,string investor, byte statusMask)
        {
            return tradeAlertTA.GetByInvestorDateStatus(frDate, toDate, statusMask.ToString(), investor);
        }

        public static void LoadPortfolioByInvestor(baseDS.portfolioDataTable tbl, string investorCode,AppTypes.PortfolioTypes type)
        {
            portfolioTA.ClearBeforeFill = false;
            portfolioTA.FillByInvestorCodeAndTypeMask(tbl, investorCode,((byte)type).ToString());
        }
        public static void LoadPortfolioByInvestor(baseDS.portfolioDataTable tbl, string investorCode)
        {
            portfolioTA.ClearBeforeFill = false;
            portfolioTA.FillByInvestorCode(tbl, investorCode);
        }

        public static void LoadStockOwnedByInvestor(baseDS.investorStockDataTable tbl, string investorCode)
        {
            investorStockTA.ClearBeforeFill = false;
            investorStockTA.FillByInvestor(tbl, investorCode);
        }
        public static void LoadStockOwnedByAll(tmpDS.investorStockDataTable tbl)
        {
            sumInvestorStockTA.ClearBeforeFill = false;
            sumInvestorStockTA.Fill(tbl);
        }

        public static void LoadInvestorByAccount(baseDS.investorDataTable tbl, string account)
        {
            investorTA.ClearBeforeFill = false;
            investorTA.FillByAccount(tbl, account);
        }
        public static void LoadInvestorByEmail(baseDS.investorDataTable tbl, string email)
        {
            investorTA.ClearBeforeFill = false;
            investorTA.FillByEmail(tbl, email);
        }
        public static void LoadInvestor(baseDS.investorDataTable tbl, string cond)
        {
            string sqlCmd = "SELECT * FROM investor WHERE " + cond;
            LoadFromSQL(tbl, sqlCmd);
        }

        public static void LoadStockCode_ByStatus(baseDS.stockCodeDataTable tbl,AppTypes.CommonStatus status)
        {
            stockCodeTA.FillByStatusMask(tbl, ((byte)status).ToString());
        }

        public static void LoadStockCode_ByStockExchange(tmpDS.stockCodeDataTable tbl, string stockExchange, AppTypes.CommonStatus status)
        {
            shortStockCodeTA.FillByStockExchange(tbl, stockExchange,((byte)status).ToString()); 
        }
        public static void LoadStockCode_ByBizSectors(tmpDS.stockCodeDataTable tbl, StringCollection bizSectors)
        {
            baseDS.stockCodeDataTable comTbl = new baseDS.stockCodeDataTable();
            string cond = common.system.MakeConditionStr(bizSectors,
                                                         comTbl.bizSectorsColumn.ColumnName + " LIKE N'" +
                                                         common.Consts.SQL_CMD_ALL_MARKER + common.Settings.sysListSeparatorPrefix,
                                                         common.Settings.sysListSeparatorPostfix + common.Consts.SQL_CMD_ALL_MARKER + "'",
                                                         "OR");
            string sqlCmd = "SELECT code, stockExchange, tickerCode, name,nameEn,0 AS price,0 AS priceVariant FROM stockCode WHERE " + cond;
            LoadFromSQL(tbl, sqlCmd);
        }

        public static void LoadStockCode_ByCodeList(tmpDS.stockCodeDataTable tbl, StringCollection stockCode)
        {
            string cond = common.system.MakeConditionStr(stockCode, "" + tbl.codeColumn.ColumnName + "=N'", "'", "OR");
            string sqlCmd = "SELECT code, stockExchange, tickerCode, name,nameEn FROM stockCode WHERE " + cond;
            LoadFromSQL(tbl, sqlCmd);
        }
        public static void LoadStockCode_ByPortfolios(tmpDS.stockCodeDataTable tbl, StringCollection portfolios)
        {
            baseDS.investorStockDataTable tmpTbl = new baseDS.investorStockDataTable();
            string cond = common.system.MakeConditionStr(portfolios, "b." + tmpTbl.portfolioColumn.ColumnName + "=N'", "'", "OR");
            if (cond.Trim() == "") return;

            tmpTbl.Dispose();

            string sqlCmd = "SELECT DISTINCT a.code, a.stockExchange, a.tickerCode, a.name,nameEn" +
                            " FROM stockCode a INNER JOIN investorStock b ON a.code = b.stockCode" +
                            " WHERE " + cond;
            LoadFromSQL(tbl, sqlCmd);
        }

        public static void LoadStockCode_ByWatchList(tmpDS.stockCodeDataTable stockCodeTbl, StringCollection codes)
        {
            StringCollection retList = new StringCollection();
            StringCollection list;

            tmpDS.stockCodeDataTable tmpTbl = new tmpDS.stockCodeDataTable();
            baseDS.portfolioDataTable portfolioTbl = new baseDS.portfolioDataTable();
            baseDS.portfolioRow portfolioRow;
            for (int idx1 = 0; idx1 < codes.Count; idx1++)
            {
                portfolioRow = AppLibs.FindAndCache(portfolioTbl, codes[idx1]);
                if (portfolioRow == null) continue;
                list = common.MultiValueString.String2List(portfolioRow.interestedStock);
                if (list.Count <= 0) continue;
                tmpTbl.Clear();
                LoadStockCode_ByCodeList(tmpTbl, list);
                for (int idx2 = 0; idx2 < tmpTbl.Count; idx2++)
                {
                    if (stockCodeTbl.FindBycode(tmpTbl[idx2].code)==null) stockCodeTbl.ImportRow(tmpTbl[idx2]);
                }
            }
            portfolioTbl.Dispose();
        }

        public static baseDS.portfolioRow GetPortfolio(string portfolioCode)
        {
            baseDS.portfolioDataTable tbl = portfolioTA.GetByCode(portfolioCode);
            if (tbl.Count == 0) return null;
            return tbl[0];
        }
        public static DateTime GetLastAlertTime(string investorCode)
        {
            object obj = tradeAlertTA.GetLastTimeByInvestor(investorCode);
            if (obj == null) return common.Consts.constNullDate;
            return (DateTime)obj;
        }

        public static baseDS.priceDataStatDataTable GetPriceDataStat(AppTypes.TimeScale timeScale, string code)
        {
            if (timeScale == AppTypes.MainDataTimeScale)
                return priceDataStatTA.GetStatByCode(code);
            return priceDataStatTA.GetStatByTypeAndCode(timeScale.Code, code); 
        }

        public static void LoadData(baseDS.messagesDataTable tbl, DateTime frDate,DateTime toDate)
        {
            messagesTA.FillByDate(tbl, frDate, toDate);
        }
        public static void LoadMessageBySQL(baseDS.messagesDataTable tbl, string sql)
        {
            LoadFromSQL(tbl, sql);
        }


        /// <summary>
        /// Get the QTY that available to sell. 
        /// Stock applicable to sell is the ones that had bought [buySellInterval] days before (or later) the [sellDate] data
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="portfolio"></param>
        /// <param name="buySellInterval"></param>
        /// <param name="sellDate"></param>
        /// <returns></returns>
        public static decimal GetAvailableStock(string stockCode, string portfolio, int buySellInterval, DateTime sellDate)
        {
            decimal qty = 0;
            decimal buyAmt = 0;
            GetOwnStock(stockCode, portfolio, buySellInterval, sellDate, out qty, out buyAmt);
            return qty;
        }
        public static bool GetOwnStock(string stockCode, string portfolio, int buySellInterval, DateTime sellDate,
                                       out decimal qty, out decimal buyAmt)
        {
            baseDS.stockExchangeRow marketRow = GetStockExchange(stockCode);
            qty = 0; buyAmt = 0;
            baseDS.investorStockDataTable dataTbl = new baseDS.investorStockDataTable();
            LoadData(dataTbl, stockCode, portfolio);
            if (dataTbl.Count == 0) return false;
            DateTime applicableDate = sellDate.AddDays(-marketRow.minBuySellDay);
            for (int idx = 0; idx < dataTbl.Count; idx++)
            {
                if (dataTbl[idx].buyDate > applicableDate) continue;
                qty += dataTbl[idx].qty;
                buyAmt += dataTbl[idx].buyAmt;
            }
            return true;
        }

        public static baseDS.stockExchangeRow GetStockExchange(string stockCode)
        {
            baseDS.stockCodeRow stockRow = AppLibs.FindAndCache_StockCode(stockCode);
            if (stockRow == null) return null;
            baseDS.stockExchangeRow stockExchangeRow = AppLibs.FindAndCache_StockExchange(stockRow.stockExchange);
            return stockExchangeRow;
        }

        public static tmpDS.marketDataDataTable GetMarketData(DateTime frDate, DateTime toDate, string codeList, AppTypes.TimeScale timeScale, AppTypes.MarketDataTypes type)
        {
            string sqlCond = "";
            if (timeScale != AppTypes.MainDataTimeScale)
            {
                sqlCond += (sqlCond == "" ? "" : " AND ") + " type='" + timeScale.Code + "'";
            }
            sqlCond += (sqlCond == "" ? "" : " AND ") +
                        "onDate BETWEEN '" + common.system.ConvertToSQLDateString(frDate) + "'" +
                        " AND '" + common.system.ConvertToSQLDateString(toDate) + "'";

            sqlCond += (sqlCond == "" ? "" : " AND ");
            switch (type)
            {
                case AppTypes.MarketDataTypes.Advancing: sqlCond += "closePrice>openPrice"; break;
                case AppTypes.MarketDataTypes.Declining: sqlCond += "closePrice<openPrice"; break;
                default: sqlCond += "closePrice=openPrice"; break;
            }

            if (codeList != null && codeList != "")
            {
                sqlCond += (sqlCond == "" ? "" : " AND ") + " stockCode IN  (" + codeList + ")";
            }
            else
            {
                sqlCond += (sqlCond == "" ? "" : " AND ") +
                    " stockCode IN  (SELECT code FROM stockCode WHERE status & " + ((byte)AppTypes.CommonStatus.Enable).ToString() + ">0)";
            }
            string sqlCmd =
                "SELECT onDate,COUNT(*) AS val0,SUM(volume) AS val1" +
                " FROM " + (timeScale == AppTypes.MainDataTimeScale ? "priceData" : "priceDataSum") +
                " WHERE " + sqlCond +
                " GROUP BY onDate ORDER BY onDate";
            tmpDS.marketDataDataTable tbl = new tmpDS.marketDataDataTable();
            DbAccess.LoadFromSQL(tbl, sqlCmd);
            return tbl;
        }

        #endregion

        #region Others
        public static baseDS.sysAutoKeyPendingRow CreateAutoPendingKey(string key, string value, DateTime timeStamp)
        {
            baseDS.sysAutoKeyPendingDataTable tbl = sysAutoKeyPendingTA.GetByKeyValue(key, value);
            baseDS.sysAutoKeyPendingRow row;
            if (tbl.Count == 0)
            {
                row = tbl.NewsysAutoKeyPendingRow();
                row.key = key; row.value = value;
                row.timeStamp = timeStamp;
                tbl.AddsysAutoKeyPendingRow(row);
            }
            else
            {
                row = tbl[0];
                row.timeStamp = timeStamp;
            }
            sysAutoKeyPendingTA.Update(row);
            return row;
        }

        //public static baseDS.lastPriceDataDataTable GetLastPrice(AppTypes.PriceDataType type,DateTime fromDate)
        //{
        //    switch (type)
        //    {
        //        case AppTypes.PriceDataType.High: return lastPriceDataTA.GetHigh(fromDate);
        //        case AppTypes.PriceDataType.Low: return lastPriceDataTA.GetLow(fromDate);
        //        case AppTypes.PriceDataType.Close: return lastPriceDataTA.GetClose(fromDate);
        //        case AppTypes.PriceDataType.Open: return lastPriceDataTA.GetOpen(fromDate);
        //        case AppTypes.PriceDataType.Volume: return lastPriceDataTA.GetVolume(fromDate);
        //    }
        //    return null;
        //}

        public static baseDS.lastPriceDataDataTable GetLastPrice(AppTypes.PriceDataType type, string timeScaleCode,  DateTime fromDate)
        {
            switch (type)
            {
                case AppTypes.PriceDataType.High: return lastPriceDataTA.GetHighSum(timeScaleCode,fromDate);
                case AppTypes.PriceDataType.Low: return lastPriceDataTA.GetLowSum(timeScaleCode, fromDate);
                case AppTypes.PriceDataType.Close: return lastPriceDataTA.GetCloseSum(timeScaleCode, fromDate);
                case AppTypes.PriceDataType.Open: return lastPriceDataTA.GetOpenSum(timeScaleCode, fromDate);
                case AppTypes.PriceDataType.Volume: return lastPriceDataTA.GetVolumeSum(timeScaleCode, fromDate);
            }
            return null;
        }


        public static baseDS.priceDataRow GetLastPriceData(string stockCode)
        {
            baseDS.priceDataDataTable priceTbl = priceDataTA.GetLastPrice(stockCode);
            if (priceTbl.Count == 0) return null;
            return priceTbl[0];
        }

        #endregion

        //Update
        #region Update
        public static void UpdateData(baseDS.priceDataSumDataTable tbl)
        {
            priceDataSumTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.priceDataSumRow row)
        {
            priceDataSumTA.Update(row);
            row.AcceptChanges();
        }

        public static void UpdateData(baseDS.priceDataDataTable tbl)
        {
            priceDataTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.priceDataRow row)
        {
            priceDataTA.Update(row);
            row.AcceptChanges();
        }
        public static void UpdateData(baseDS.exchangeDetailDataTable tbl)
        {
            exchangeDetailTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.stockExchangeDataTable tbl)
        {
            stockExchangeTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.stockExchangeRow data)
        {
            stockExchangeTA.Update(data);
            data.AcceptChanges();
        }

        public static void UpdateData(baseDS.stockCodeDataTable tbl)
        {
            stockCodeTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.investorDataTable tbl)
        {
            investorTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.transactionsDataTable tbl)
        {
            transactionsTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.investorStockDataTable tbl)
        {
            investorStockTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.portfolioDataTable tbl)
        {
            portfolioTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.portfolioRow data)
        {
            portfolioTA.Update(data);
            data.AcceptChanges();
        }

        public static void UpdateData(baseDS.portfolioDetailDataTable tbl)
        {
            portfolioDetailTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.tradeAlertDataTable tbl)
        {
            tradeAlertTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.sysCodeCatDataTable tbl)
        {
            sysCodeCatTA.Update(tbl);
            tbl.AcceptChanges();
        }

        public static void UpdateData(baseDS.sysCodeDataTable tbl)
        {
            sysCodeTA.Update(tbl);
            tbl.AcceptChanges();
        }
        public static void UpdateData(baseDS.sysAutoKeyPendingDataTable data)
        {
            sysAutoKeyPendingTA.Update(data);
            data.AcceptChanges();
        }
        public static void UpdateData(baseDS.sysAutoKeyPendingRow data)
        {
            sysAutoKeyPendingTA.Update(data);
            data.AcceptChanges();
        }

        public static void UpdateData(baseDS.messagesDataTable data)
        {
            messagesTA.Update(data);
            data.AcceptChanges();
        }

        public static void UpdateData(baseDS.sysLogRow row)
        {
            syslogTA.Update(row);
            row.AcceptChanges();
        }


        #endregion Update

        #region delete
        public static void DeleteSysCode_ByCategory(string category)
        {
            sysCodeTA.DeleteByCategory(category);
        }
        public static void DeleteSysCode(string category, string code)
        {
            sysCodeTA.Delete(category, code);
        }
        public static void DeleteSysCodeCat(string category)
        {
            sysCodeCatTA.Delete(category);
        }

        public static void DeleteStockExchange(string code)
        {
            stockExchangeTA.Delete(code);
        }

        public static void DeleteStock(string stockCode)
        {
            stockCodeTA.Delete(stockCode);
        }
        public static void DeleteInvestor(string code)
        {
            investorTA.Delete(code);
        }
        public static void DeletePortfolio(string code)
        {
            portfolioTA.Delete(code);
        }
        public static void DeletePriceSumData()
        {
            priceDataSumTA.DeleteAll();
        }
        public static void DeletePriceSumData(string stockCode)
        {
            priceDataSumTA.DeleteByStockCode(stockCode);
        }
        public static void DeletePortfolioData(string porfolioCode, string stockCode)
        {
            portfolioDetailTA.DeleteByPortfolioAndCode(porfolioCode, stockCode);
        }
        public static void DeleteTradeAlert(int id)
        {
            tradeAlertTA.Delete(id);
        }

        //Remove the used key in pending list
        public static void DeleteAutoKeyPending(string key,string value)
        {
            sysAutoKeyPendingTA.DeleteByKeyValue(key, value);
        }

        #endregion
    }
}
