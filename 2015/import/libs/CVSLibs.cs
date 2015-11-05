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
using HtmlAgilityPack;
using System.Linq;
using commonTypes;
using System.Threading;
using System.Net;

namespace Imports
{
    public class CVSLibs
    {
        
        /// <summary>
        /// Use the idea from http://www.codeproject.com/KB/database/CsvReader.aspx by Sebastien Lorion
        /// </summary>
        /// <param name="csvFileName"></param>
        /// <param name="delimiter"></param>
        /// <param name="dateDataFormat"></param>
        /// <param name="marketCode"></param>
        /// <param name="dataCulture"></param>
        /// <param name="priceDataTbl"></param>
        /// <param name="ImportRowFunc"></param>
        /// <param name="onUpdateDataFunc"></param>
        /// <param name="onEndImportFunc"></param>
        /// <returns></returns>
        public static bool CSV_ImportParse(string csvFileName, char delimiter,
                                           common.dateTimeLibs.DateTimeFormats dateDataFormat,
                                           string marketCode, CultureInfo dataCulture,
                                           databases.baseDS.priceDataDataTable priceDataTbl,
                                           ImportRowHandler ImportRowFunc,
                                           OnUpdatePriceData onUpdateDataFunc,
                                           OnEndImportPriceData onEndImportFunc)
        {
            importStat myImportStat = new importStat();
            myImportStat.Reset();
            myImportStat.dateDataFormat = dateDataFormat;
            myImportStat.srcCulture = dataCulture;
            databases.baseDS.stockCodeDataTable stockCodeTbl = new databases.baseDS.stockCodeDataTable();
            databases.baseDS.priceDataRow priceDataRow;

            DataRowView[] foundRows;
            databases.DbAccess.LoadData(stockCodeTbl, AppTypes.CommonStatus.Enable);
            DataView stockCodeView = new DataView(stockCodeTbl);
            stockCodeView.Sort = stockCodeTbl.codeColumn.ColumnName;

            bool fCanceled = false;
            DateTime lastPriceDate = common.Consts.constNullDate;

            importOHLCV data;
            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(new StreamReader(csvFileName), true, delimiter))
            {
                // missing fields will not throw an exception,
                // but will instead be treated as if there was a null value
                csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                int fieldCount = csv.FieldCount;
                if (fieldCount < 7) return false;
                while (csv.ReadNextRecord())
                {
                    Application.DoEvents();
                    myImportStat.dataCount++;
                    data = ImportRowFunc(csv, myImportStat);
                    if (myImportStat.cancel)
                    {
                        fCanceled = true; break;
                    }
                    if (data == null)
                    {
                        myImportStat.errorCount++;
                        continue;
                    }
                    //Assume that all price must be valid
                    if (data.Open <= 0 || data.High <= 0 || data.Low <= 0 || data.Close <= 0) continue;

                    foundRows = stockCodeView.FindRows(data.code);
                    if (foundRows.Length == 0)
                    {
                        //Try to add new stock code
                        Libs.AddNewCode(data.code, marketCode, stockCodeTbl);
                        databases.DbAccess.UpdateData(stockCodeTbl);
                    }

                    // Ignore all data that was in database
                    //if (!foundLastPriceDate)
                    //{
                    //    lastPriceDate = FindLastPriceDate(data.code);
                    //    foundLastPriceDate = true;
                    //}
                    if (lastPriceDate != common.Consts.constNullDate && data.dateTime <= lastPriceDate)
                    {
                        continue;
                    }
                    if (priceDataTbl.FindBystockCodeonDate(data.code, data.dateTime) != null)
                    {
                        myImportStat.errorCount++;
                        continue;
                    }
                    myImportStat.updateCount++;
                    priceDataRow = priceDataTbl.NewpriceDataRow();
                    databases.AppLibs.InitData(priceDataRow);
                    priceDataRow.stockCode = data.code;
                    priceDataRow.onDate = data.dateTime;
                    //Try to fix some error in data
                    priceDataRow.openPrice = (decimal)data.Open;
                    priceDataRow.highPrice = (decimal)data.High;
                    priceDataRow.lowPrice = (decimal)data.Low;
                    priceDataRow.closePrice = (decimal)data.Close;
                    priceDataRow.volume = (decimal)data.Volume;
                    priceDataTbl.AddpriceDataRow(priceDataRow);
                    if (onUpdateDataFunc != null) onUpdateDataFunc(priceDataRow, myImportStat);
                }
            }
            if (fCanceled)
            {
                priceDataTbl.Clear();
                return false;
            }
            if (onEndImportFunc != null) onEndImportFunc(priceDataTbl);
            return true;
        }

        public static bool ImportFromCVS(string csvFileName, string marketCode, CultureInfo dataCulture,
                                         databases.baseDS.priceDataDataTable priceDataTbl, OnUpdatePriceData onUpdateDataFunc)
        {
            return CSV_ImportParse(csvFileName, ',', common.dateTimeLibs.DateTimeFormats.YYMMDD,
                                                marketCode, dataCulture, priceDataTbl, DoImportRow, onUpdateDataFunc, null);

        }

        public static bool ImportFromCVS_BVSC(string csvFileName, string marketCode, CultureInfo dataCulture,
                                         databases.baseDS.priceDataDataTable priceDataTbl, OnUpdatePriceData onUpdateDataFunc)
        {
            return CSV_ImportParse(csvFileName, ',', common.dateTimeLibs.DateTimeFormats.YYMMDD,
                                                marketCode, dataCulture, priceDataTbl, DoImportRow, onUpdateDataFunc, null);

        }
        static DateTime tmpDate = common.Consts.constNullDate;
        static double tmpVal = 0;
        public static importOHLCV DoImportRow(LumenWorks.Framework.IO.Csv.CsvReader csv, importStat importStat)
        {
            importOHLCV data = new importOHLCV();
            if (csv[0] == null) return null;
            data.code = csv[0];

            if (!common.dateTimeLibs.Str2Date(csv[1], importStat.dateDataFormat, out tmpDate)) return null;
            data.dateTime = tmpDate;

            if (!double.TryParse(csv[2], NumberStyles.Number, importStat.srcCulture, out tmpVal)) return null;
            data.Open = tmpVal;

            if (!double.TryParse(csv[3], NumberStyles.Number, importStat.srcCulture, out tmpVal)) return null;
            data.High = tmpVal;

            if (!double.TryParse(csv[4], NumberStyles.Number, importStat.srcCulture, out tmpVal)) return null;
            data.Low = tmpVal;

            if (!double.TryParse(csv[5], NumberStyles.Number, importStat.srcCulture, out tmpVal)) return null;
            data.Close = tmpVal;

            if (!double.TryParse(csv[6], NumberStyles.Number, importStat.srcCulture, out tmpVal)) return null;
            data.Volume = tmpVal;
            return data;
        }
    }
}
