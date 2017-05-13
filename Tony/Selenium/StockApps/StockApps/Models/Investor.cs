using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace StockApps.Models
{
    public class Investor
    {
        public List<Stock> LogIn(string account, string password)
        {
            string name = null;

            SqlConnection myConnection = new SqlConnection("user id=Testing;" +
                                       "password=123456;" +
                                       "server=TONY;" +
                                       "Trusted_Connection=yes;" +
                                       "database=Stock; " +
                                       "connection timeout=10");

            try
            {
                myConnection.Open();

                // get list of stock in investor's portfolio
                List<Stock> listStock = new List<Stock>();

                SqlCommand myCommand =
                    new SqlCommand("select s.code, s.name " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock x, dbo.stockCode s " +
                    "where i.code = p.investorCode and p.code = x.portfolio and x.stockCode = s.code " +
                    "and p.type = '1' " +
                    "and i.account = '" + account +
                    "' and i.password = '" + password +
                    "'", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    listStock.Add(
                        new Stock
                        {
                            code = myReader["code"].ToString(),
                            name = myReader["name"].ToString()
                        }
                    );
                }

                myConnection.Close();

                return listStock;
            }
            catch
            {
                return null;
            }
                
        }
    }
}