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
using Imports.Stock;

namespace Imports.Stock
{
    /// <summary>
    /// Import stock data of HOSE and HASTC using SSIPage class
    /// </summary>
    public class ssi_StockImport : generalImport
    {
        static SSIPage ssiPage=null;

        public override databases.baseDS.priceDataDataTable GetImportFromCSV(string fileName, string market, OnUpdatePriceData onUpdateDataFunc)
        {
            return null;
        }

        /// <summary>
        /// Override method - /// Import stock data of HOSE and HASTC from  http://banggia2.ssi.com.vn/#
        /// </summary>
        /// <param name="updateTime"></param>
        /// <param name="exchangeDetailRow"></param>
        /// <returns></returns>
        public override databases.baseDS.priceDataDataTable GetImportFromWeb(DateTime updateTime, databases.baseDS.exchangeDetailRow exchangeDetailRow)
        {
            databases.importDS.importPriceDataTable importPriceTbl = GetPriceFromWeb(updateTime, exchangeDetailRow);
            if (importPriceTbl == null) return null;

            Imports.Libs.AddNewCode(exchangeDetailRow.marketCode, importPriceTbl, null);
            databases.DbAccess.UpdateData(importPriceTbl);
            databases.baseDS.priceDataDataTable priceTbl = new databases.baseDS.priceDataDataTable();
            Imports.Libs.AddImportPrice(importPriceTbl, priceTbl);
            databases.DbAccess.UpdateData(priceTbl);
            return priceTbl;
        }

        /// <summary>
        /// Private - SU dung ASPWebservices
        /// </summary>
        /// <param name="updateTime"></param>
        /// <param name="exchangeDetailRow"></param>
        /// <returns></returns>
        private databases.importDS.importPriceDataTable GetPriceFromWeb(DateTime updateTime, databases.baseDS.exchangeDetailRow exchangeDetailRow)
        {
            try
            {
                databases.importDS.importPriceDataTable importPriceTbl = new databases.importDS.importPriceDataTable();
                
                if (ssiPage==null)
                    ssiPage = new SSIPage("http://banggia2.ssi.com.vn/", "http://banggia2.ssi.com.vn/Hnx.aspx");
                    //ssiPage = new SSIPage("file:///C:/Temp/selenium/HOSE%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html", "file:///C:/Temp/selenium/HNX%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html");
                
                ssiPage.getHOSEData();
                ssiPage.getHNXData();
                //SaveDatatoImportPriceDataTable(updateTime,importPriceTbl);
                databases.importDS.importPriceRow importRow = null;
                databases.importDS.importPriceRow oldImportRow;

                foreach (var stock in ssiPage.dictStocks)
                {
                    importRow = importPriceTbl.NewimportPriceRow();
                    databases.AppLibs.InitData(importRow);
                    importRow.onDate = updateTime;
                    importRow.stockCode = stock.Key;
                    importRow.isTotalVolume = true;
                    importRow.closePrice=(decimal)stock.Value.price;
                    importRow.volume = (decimal)stock.Value.totalVolume;
                
                    if (importRow.closePrice > 0)
                    {
                        //Only add new when there are some changes 
                        oldImportRow = lastImportData.Find(importRow);
                        if (!lastImportData.IsSameData(importRow, oldImportRow))
                        {
                            importPriceTbl.AddimportPriceRow(importRow);
                            lastImportData.Update(importRow);
                        }
                        else importRow.CancelEdit();
                    }
                    else importRow.CancelEdit();
                }

                return importPriceTbl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
