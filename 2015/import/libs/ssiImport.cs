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
using databaseEntity;

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
        public override databases.baseDS.priceDataDataTable GetImportFromWeb(DateTime updateTime, string market)
        {
            databases.importDS.importPriceDataTable importPriceTbl = GetPriceFromWeb(updateTime, market);
            if (importPriceTbl == null) return null;

            Imports.Libs.AddNewCode(market, importPriceTbl, null);
            databases.DbAccess.UpdateData(importPriceTbl);
            databases.baseDS.priceDataDataTable priceTbl = new databases.baseDS.priceDataDataTable();
            Imports.Libs.AddIdmportPrice(importPriceTbl, priceTbl);
            databases.DbAccess.UpdateData(priceTbl);
            return priceTbl;
        }
        
        /// <summary>
        /// Private - SU dung ASPWebservices
        /// </summary>
        /// <param name="updateTime"></param>
        /// <param name="exchangeDetailRow"></param>
        /// <returns></returns>
        private databases.importDS.importPriceDataTable GetPriceFromWeb(DateTime updateTime, string market)
        {
            try
            {
                databases.importDS.importPriceDataTable importPriceTbl = new databases.importDS.importPriceDataTable();

                if (ssiPage == null)
                    ssiPage = new SSIPage(market);
                //ssiPage = new SSIPage("file:///C:/Temp/selenium/HOSE%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html", "file:///C:/Temp/selenium/HNX%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html");

                if (market=="HOSE")
                    ssiPage.getHOSEData();
                else
                    if (market=="HASTC")
                        ssiPage.getHNXData();
                else
                    if (market=="DERIVATIVE")
                        ssiPage.getDerivativeData();
                //SaveDatatoImportPriceDataTable(updateTime,importPriceTbl);
                databases.importDS.importPriceRow importRow = null;
                databases.importDS.importPriceRow oldImportRow=null;

                foreach (var stock in ssiPage.dictStocks)
                {
                    importRow = importPriceTbl.NewimportPriceRow();
                    databases.AppLibs.InitData(importRow);
                    importRow.onDate = updateTime;
                    importRow.stockCode = stock.Key;
                    //importRow.to
                    //Doi de fix error #136 - Lỗi cập nhật HNX
                    //importRow.isTotalVolume = true;
                    importRow.isTotalVolume = false;

                    importRow.closePrice=(decimal)stock.Value.price;
                    importRow.totalVolume = (decimal)stock.Value.totalVolume;

                    //Doi de fix error #136 - Lỗi cập nhật HNX
                    

                    if ((importRow.closePrice > 0) && (stock.Value.actualVolume>0))
                    {
                        importRow.volume = (decimal)stock.Value.actualVolume;
                        importPriceTbl.AddimportPriceRow(importRow);
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

        public void DeleteStocksNotinDatabase()
        {
            //Step 1: Lay list danh sach cac Stock trong DataBase
            StringCollection stockCodeList = new StringCollection();
            databases.baseDS.stockCodeDataTable stockCodeTable = new databases.baseDS.stockCodeDataTable();
            databases.DbAccess.LoadData(stockCodeTable);
            for (int i = 0; i < stockCodeTable.Count; i++)
                stockCodeList.Add(stockCodeTable[i].code);


            //Step 2: Lay danh sach cac co phieu hien hanh
            databases.importDS.importPriceDataTable importPriceTbl = new databases.importDS.importPriceDataTable();

            if (ssiPage == null)
                ssiPage = new SSIPage("http://banggia2.ssi.com.vn/", "http://banggia2.ssi.com.vn/Hnx.aspx");
            //ssiPage = new SSIPage("file:///C:/Temp/selenium/HOSE%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html", "file:///C:/Temp/selenium/HNX%20-%20CTCP%20ch%E1%BB%A9ng%20kho%C3%A1n%20S%C3%A0i%20G%C3%B2n%20-%20B%E1%BA%A3ng%20gi%C3%A1%20tr%E1%BB%B1c%20tuy%E1%BA%BFn.html");

            ssiPage.getHOSEData();
            ssiPage.getHNXData();
            //SaveDatatoImportPriceDataTable(updateTime,importPriceTbl);

            StockDb stockdb = new StockDb();
            for (int i = 0; i < stockCodeList.Count; i++)
            {
                if (!ssiPage.dictStocks.ContainsKey(stockCodeList[i]))   //Step 3: neu ko ton tai
                {
                    System.Console.WriteLine(stockCodeList[i]);
                    //Dung Entity Framework de xoa toan bo du lieu
                    stockdb.DeleteStockCode(stockCodeList[i]);
                }
            }
            
        }      
    }
}
