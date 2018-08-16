using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Imports.Stock
{
    public class vndFinancialDataPage
    {
        public IWebDriver driverVND;
        private string URL;
        private string tongquan="https://www.vndirect.com.vn/portal/tong-quan/";//+HPG.shtml
        private string candoiketoan = "https://www.vndirect.com.vn/portal/bang-can-doi-ke-toan/";
        private string kqkd="https://www.vndirect.com.vn/portal/bao-cao-ket-qua-kinh-doanh/";
        private string lctt = "https://www.vndirect.com.vn/portal/bao-cao-luu-chuyen-tien-te/";

        //public Dictionary<string, PageStockRow> dictStocks;

        public vndFinancialDataPage(string _URL)
        {
            

        }

        ~vndFinancialDataPage()
        {
            if (driverVND != null) driverVND.Quit();
        }

        public void Refresh(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        /// <summary>
        /// Verified if HOSE page is fully loaded
        /// </summary>
        /// <returns></returns>
        public bool IsVNDLoaded()
        {
            try
            {
                //Refresh(driverHOSE);
                ////*[@id="container"]/div[7]/div/div[1]/div[1]/ul/li[5]/div[2]
                string vnd = GetTextByXPath(driverVND, "//*[@id='container']/div[7]/div/div[1]/div[1]/ul/li[5]/div[2]", 100, 10000);
                if (vnd == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                //driverHOSE.Quit();
                return false;
                // Ignore errors if unable to close the browser
            }
        }


        public void Destroy()
        {
            driverVND.Quit();
           
        }

        private void GetStockTableData(HtmlAgilityPack.HtmlDocument html, string stockExchange)
        {
            double value = 0;

            //Stocks
            HtmlNodeCollection Rows = html.DocumentNode.SelectNodes("//*[@id='tableQuote']/tbody/tr");

            //foreach (HtmlNode row in Rows)
            for (int iRow = 0; iRow < Rows.Count; iRow++)
            {
                HtmlNode row = Rows[iRow];
                //Console.WriteLine("Row=" + row.InnerText);
                if ((stockExchange == "DERIVATIVE") && (iRow == 0))
                    continue;

                HtmlNodeCollection Cols = row.SelectNodes("td");
                PageStockRow stockRow = new PageStockRow();
                stockRow.stockExchange = stockExchange;
                if (Cols[0].InnerText != "" && Cols[0].InnerText != null) stockRow.stockCode = Cols[0].InnerText;
                for (int i = 1; i <= 24; i++)
                {
                    if (Double.TryParse(Cols[i].InnerText, out value))
                    {
                        switch (i)
                        {
                            case 1:
                                stockRow.ceiling = value; break;
                            case 2:
                                stockRow.floor = value; break;
                            case 3:
                                stockRow.reference = value; break;
                            case 4:
                                stockRow.bidVolume3 = value; break;
                            case 5:
                                stockRow.bidPrice3 = value; break;
                            case 6:
                                stockRow.bidVolume2 = value; break;
                            case 7:
                                stockRow.bidPrice2 = value; break;
                            case 8:
                                stockRow.bidVolume1 = value; break;
                            case 9:
                                stockRow.bidPrice1 = value; break;
                            case 10:
                                stockRow.price = value; break;
                            case 11:
                                stockRow.actualVolume = value; break;
                            case 12:
                                stockRow.valueChange = value; break;
                            case 13:
                                stockRow.askPrice1 = value; break;
                            case 14:
                                stockRow.askVolume1 = value; break;
                            case 15:
                                stockRow.askPrice2 = value; break;
                            case 16:
                                stockRow.askVolume2 = value; break;
                            case 17:
                                stockRow.askPrice3 = value; break;
                            case 18:
                                stockRow.askVolume3 = value; break;
                            case 19:
                                stockRow.high = value; break;
                            case 20:
                                stockRow.low = value; break;
                            case 21:
                                stockRow.average = value; break;
                            case 22:
                                stockRow.totalVolume = value; break;
                            case 23:
                                stockRow.boughtForeign = value; break;
                            case 24:
                                stockRow.sellForeign = value; break;
                            case 25:
                                stockRow.room = value; break;
                            default:
                                break;
                        }
                    }
                }
                //stockRow.Print();
                //AddStock(stockRow.stockCode, stockRow);
            }
        }

        /// <summary>
        /// Get HOSE Data from SSI Page
        /// </summary>
        public void getVNDFinancialData()
        {
            driverVND = new ChromeDriver();
            URL = tongquan+"/HPG.shtml";
            driverVND.Manage().Window.Maximize();
            driverVND.Navigate().GoToUrl(URL);

            if (!IsVNDLoaded()) return;

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driverVND.PageSource);

            //GetStockTableData(html, "HOSE");
            string vnd = GetTextByXPath(driverVND, "//*[@id='container']/div[7]/div/div[1]/div[1]/ul/li[5]/div[2]", 100, 10000);
            System.Console.WriteLine("Free Float "+vnd);

        }


        /// <summary>
        /// Get IWebElement using XPath
        /// </summary>
        /// <param name="sXPath"></param>
        /// <param name="waitTime"></param>
        /// <param name="totalTime"></param>
        /// <returns></returns>
        public IWebElement GetWebElementByXPath(IWebDriver driver, string sXPath, int waitTime, int totalTime)
        {
            IWebElement s1 = null;
            int i = totalTime;
            while ((s1 == null) && (i > 0))
            {
                try
                {
                    s1 = driver.FindElement(By.XPath(sXPath));
                }
                catch (Exception)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(waitTime));
                    i = i - waitTime;
                }
            }
            return s1;
        }

        /// <summary>
        /// Get IWebElement using ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="waitTime"></param>
        /// <param name="totalTime"></param>
        /// <returns></returns>
        public IWebElement GetWebElementByID(IWebDriver driver, string ID, int waitTime, int totalTime)
        {
            IWebElement s1 = null;
            int i = totalTime;
            while ((s1 == null) && (i > 0))
            {
                try
                {
                    s1 = driver.FindElement(By.Id(ID));
                }
                catch (Exception)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(waitTime));
                    i = i - waitTime;
                }
            }
            return s1;
        }

        public string GetTextByXPath(IWebDriver driver, string sXPath, int waitTime, int totalTime)
        {
            string s1 = null;
            int i = totalTime;
            while ((s1 == null) && (i > 0))
            {
                try
                {
                    s1 = driver.FindElement(By.XPath(sXPath)).Text;
                }
                catch (Exception)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(waitTime));
                    i = i - waitTime;
                }
            }
            return s1;
        }

        public string GetTextByID(IWebDriver driver, string ID, int waitTime, int totalTime)
        {
            string s1 = null;
            int i = totalTime;
            while ((s1 == null) && (i > 0))
            {
                try
                {
                    s1 = driver.FindElement(By.Id(ID)).Text;
                }
                catch (Exception)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(waitTime));
                    i = i - waitTime;
                }
            }
            return s1;
        }
    }

}
