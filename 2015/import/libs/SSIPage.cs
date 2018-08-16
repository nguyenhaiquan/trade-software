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
    public class PageStockRow
    {
        public string stockCode = "";
        public string stockExchange = "";
        public double ceiling = 0;
        public double floor = 0;
        public double reference = 0;
        public double bidVolume1 = 0, bidVolume2 = 0, bidVolume3 = 0;
        public double bidPrice1 = 0, bidPrice2 = 0, bidPrice3 = 0;
        public double askVolume1 = 0, askVolume2 = 0, askVolume3 = 0;
        public double askPrice1 = 0, askPrice2 = 0, askPrice3 = 0;

        public double price = 0;
        public double actualVolume = 0;
        public double valueChange = 0;
        public double high = 0;
        public double low = 0;
        public double average = 0;
        public double totalVolume = 0;
        public double boughtForeign = 0;
        public double sellForeign = 0;
        public double room = 0;
        public PageStockRow(string _stockExchange, string _stockCode, double _price, double _actualVolume, double _totalVolume)
        {
            stockExchange = _stockExchange;
            stockCode = _stockCode;
            price = _price;
            actualVolume = _actualVolume;
            totalVolume = _totalVolume;
        }

        public PageStockRow(string _stockExchange, string _stockCode, double _price, double _actualVolume)
        {
            stockExchange = _stockExchange;
            stockCode = _stockCode;
            price = _price;
            actualVolume = _actualVolume;
        }

        public PageStockRow(string _stockExchange, string _stockCode, double _price)
        {
            stockExchange = _stockExchange;
            stockCode = _stockCode;
            price = _price;
        }

        public PageStockRow(string _stockExchange, string _stockCode)
        {
            stockExchange = _stockExchange;
            stockCode = _stockCode;
        }

        public PageStockRow()
        {
        }

        public void Print()
        {
            Console.WriteLine("stock:" + stockCode + "price:" + price.ToString() + ";volume:" + totalVolume.ToString());
        }
    }

    public class PageDerivativeRow : PageStockRow
    {
        public double OI;// Open Interest
        public DateTime expiredDate; //Exprired Date of the Future
        public double difference=0;
    }
        
    public class SSIPage
    {
        public IWebDriver driverHOSE, driverHNX,driverDerivative;
        private string hoseURL, hxnURL, upcomURL,derivativeURL;
        public double dVNIndex = 0, dVNIndexVolume = 0, dVNIndex30 = 0,
        dVNIndex30Volume = 0, dHNIndex = 0, dHNIndexVolume = 0, dHNIndex30 = 0, dHNIndex30Volume = 0,
        dUpComIndex = 0, dUpComIndexVolume = 0;

        public Dictionary<string, PageStockRow> dictStocks;

        public SSIPage(string _hoseURL, string _hnxURL)
        {
            driverHOSE = new ChromeDriver();
            hoseURL = _hoseURL;
            hxnURL = _hnxURL;
            driverHOSE.Manage().Window.Maximize();
            driverHOSE.Navigate().GoToUrl(hoseURL);

            driverHNX = new ChromeDriver();
            driverHNX.Manage().Window.Maximize();
            driverHNX.Navigate().GoToUrl(hxnURL);

            dictStocks = new Dictionary<string, PageStockRow>();

        }

        public SSIPage(string _hoseURL, string _hnxURL,string _derURL)
        {
            hoseURL = _hoseURL;
            driverHOSE = new ChromeDriver();
            driverHOSE.Manage().Window.Maximize();
            driverHOSE.Navigate().GoToUrl(hoseURL);

            hxnURL = _hnxURL;
            driverHNX = new ChromeDriver();
            driverHNX.Manage().Window.Maximize();
            driverHNX.Navigate().GoToUrl(hxnURL);

            derivativeURL = _derURL;
            driverDerivative = new ChromeDriver();
            driverDerivative.Manage().Window.Maximize();
            driverDerivative.Navigate().GoToUrl(derivativeURL);

            dictStocks = new Dictionary<string, PageStockRow>();

        }
        ~SSIPage()
        {
            if (driverHNX!=null) driverHNX.Quit();
            if (driverHOSE != null) driverHOSE.Quit();
            if (driverDerivative != null) driverDerivative.Quit();
        }

        public void Refresh(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        /// <summary>
        /// Verified if HOSE page is fully loaded
        /// </summary>
        /// <returns></returns>
        public bool IsHOSELoaded()
        {
            try
            {
                //Refresh(driverHOSE);
                string ssi = GetTextByXPath(driverHOSE, "//tr[@id='trSSI']/td[10]", 100, 10000);
                if (ssi == null) return false;
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

        /// <summary>
        /// Verified if HASTC/HNX page is fully loaded
        /// </summary>
        /// <returns></returns>
        public bool IsHNXLoaded()
        {
            try
            {
                //Refresh(driverHNX);
                string acb = GetTextByXPath(driverHNX, "//tr[@id='trACB']/td[10]", 100, 10000);
                if (acb == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                //driverHNX.Quit();
                return false;
                // Ignore errors if unable to close the browser
            }
        }

        public bool IsDerivativeLoaded()
        {
            try
            {
                //Refresh(driverHOSE);
                string ssi = GetTextByXPath(driverDerivative, "//*[@id='spanIndexHOSE30']", 100, 10000);
                if (ssi == null) return false;
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
            driverHOSE.Quit();
            driverHNX.Quit();
            driverDerivative.Quit();
        }

        public void AddStock(string key, PageStockRow value)
        {
            if (dictStocks.ContainsKey(key))
            {
                dictStocks[key] = value;
            }
            else
            {
                dictStocks.Add(key, value);
            }
        }

        private void GetStockTableData(HtmlAgilityPack.HtmlDocument html, string stockExchange)
        {
            double value = 0;
            
                //Stocks
                HtmlNodeCollection Rows = html.DocumentNode.SelectNodes("//*[@id='tableQuote']/tbody/tr");

            //foreach (HtmlNode row in Rows)
            for (int iRow=0;iRow<Rows.Count;iRow++)
            {
                HtmlNode row = Rows[iRow];
                //Console.WriteLine("Row=" + row.InnerText);
                if ((stockExchange == "DERIVATIVE")&&(iRow==0))
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
                AddStock(stockRow.stockCode, stockRow);
            }
        }

        /// <summary>
        /// Get Derivative data
        /// </summary>
        /// <param name="html"></param>
        /// <param name="stockExchange"></param>
        private void GetDerivativeTableData(HtmlAgilityPack.HtmlDocument html, string stockExchange)
        {
            double value = 0;

            //Stocks
            HtmlNodeCollection Rows = html.DocumentNode.SelectNodes("//*[@id='tableQuote']/tbody/tr");

            //foreach (HtmlNode row in Rows)
            for (int iRow = 1; iRow < Rows.Count; iRow++)
            {
                HtmlNode row = Rows[iRow];

                HtmlNodeCollection Cols = row.SelectNodes("td");
                PageDerivativeRow stockRow = new PageDerivativeRow();
                stockRow.stockExchange = stockExchange;
                if (Cols[0].InnerText != "" && Cols[0].InnerText != null) stockRow.stockCode = Cols[0].InnerText;
                if (Cols[0].InnerText != "" && Cols[0].InnerText != null)   DateTime.TryParse(Cols[1].InnerText, out stockRow.expiredDate);

                for (int i = 3; i <= 28; i++)
                {
                    if (Double.TryParse(Cols[i].InnerText, out value))
                    {
                        switch (i)
                        {
                            case 3:
                                stockRow.OI = value;break;
                            case 4:
                                stockRow.ceiling = value; break;
                            case 5:
                                stockRow.floor = value; break;
                            case 6:
                                stockRow.reference = value; break;
                            case 7:
                                stockRow.difference = value;break;
                            case 8:
                                stockRow.bidVolume3 = value; break;
                            case 9:
                                stockRow.bidPrice3 = value; break;
                            case 10:
                                stockRow.bidVolume2 = value; break;
                            case 11:
                                stockRow.bidPrice2 = value; break;
                            case 12:
                                stockRow.bidVolume1 = value; break;
                            case 13:
                                stockRow.bidPrice1 = value; break;
                            case 14:
                                stockRow.price = value; break;
                            case 15:
                                stockRow.actualVolume = value; break;
                            case 16:
                                stockRow.valueChange = value; break;
                            case 17:
                                stockRow.askPrice1 = value; break;
                            case 18:
                                stockRow.askVolume1 = value; break;
                            case 19:
                                stockRow.askPrice2 = value; break;
                            case 20:
                                stockRow.askVolume2 = value; break;
                            case 21:
                                stockRow.askPrice3 = value; break;
                            case 22:
                                stockRow.askVolume3 = value; break;
                            case 23:
                                stockRow.high = value; break;
                            case 24:
                                stockRow.low = value; break;
                            case 25:
                                stockRow.average = value; break;
                            case 26:
                                stockRow.totalVolume = value; break;
                            case 27:
                                stockRow.boughtForeign = value; break;
                            case 28:
                                stockRow.sellForeign = value; break;
                            default:
                                break;
                        }
                    }
                }
                //stockRow.Print();
                AddStock(stockRow.stockCode, stockRow);
            }
        }
        /// <summary>
        /// Get HOSE Data from SSI Page
        /// </summary>
        public void getHOSEData()
        {
            if (!IsHOSELoaded()) return;

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driverHOSE.PageSource);

            //VNIndex & Volume
            //dVNIndex = Double.Parse(html.DocumentNode.SelectSingleNode("//*[@id='tdHoseVnIndex']").InnerText);
            bool result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHoseVnIndex']").InnerText, out dVNIndex);
            if (!result) dVNIndex = 0;

            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHoseTotalQtty']").InnerText, out dVNIndexVolume);
            if (!result) dVNIndexVolume = 0;
            AddStock("VN-IDX", new PageStockRow("HOSE", "VN-IDX", dVNIndex, 0,dVNIndexVolume));

            //VNIndex 30 & Volume
            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHose30VnIndex']").InnerText, out dVNIndex30);
            if (!result) dVNIndex30 = 0;
            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHose30TotalQtty']").InnerText,out dVNIndex30Volume);
            if (!result) dVNIndex30Volume = 0;

            AddStock("VN30-IDX", new PageStockRow("HOSE", "VN30-IDX", dVNIndex30,0, dVNIndex30Volume));

            //HNIndex, HNIndex Volume
            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHnxIndex']").InnerText,out dHNIndex);
            if (!result) dHNIndex = 0;
            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHnxTotalQtty']").InnerText, out dHNIndexVolume);
            if (!result) dHNIndexVolume = 0;
            AddStock("HNX-IDX", new PageStockRow("HASTC", "HNX-IDX", dHNIndex, 0,dHNIndexVolume));

            //HNX30
            result= Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHnxIndex']").InnerText, out dHNIndex30);
            result = Double.TryParse(html.DocumentNode.SelectSingleNode("//*[@id='tdHnxTotalQtty']").InnerText, out dHNIndex30Volume);
            AddStock("HNX30-IDX", new PageStockRow("HASTC", "HNX30-IDX", dHNIndex, 0,dHNIndexVolume));
            //Console.WriteLine("VNIndex=" + dictStocks["VN-IDX"].price);
            //Console.WriteLine("VNIndex30=" + dictStocks["VN30-IDX"].price);

            GetStockTableData(html, "HOSE");
        }

        /// <summary>
        /// Get HASTC/HNX Data from SSI Page
        /// </summary>
        public void getHNXData()
        {
            if (!IsHNXLoaded()) return;

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driverHNX.PageSource);
            GetStockTableData(html, "HASTC");
        }

        public void getDerivativeData()
        {
            if (!IsDerivativeLoaded()) return;

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driverDerivative.PageSource);

            GetDerivativeTableData(html, "DERIVATIVE");
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
