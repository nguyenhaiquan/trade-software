using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockApps.Models
{
    public class Portfolio
    {
        public string code { get; set; }
        public string quantity { get; set; }
        public decimal cost { get; set; }
        public decimal matched { get; set; }
        public decimal PL { get; set; }

        public List<Portfolio> GetAll(string investor)
        {
            SqlConnection myConnection = new SqlConnection(
                "user id=Testing;" +
                "password=123456;" +
                "server=TONY;" +
                "Trusted_Connection=yes;" +
                "database=Stock;" +
                "connection timeout=10");
            try
            {
                myConnection.Open();

                List<Portfolio> portfolios = new List<Portfolio>();

                SqlCommand myCommand = new SqlCommand(
                    "select s.stockCode, s.qty, s.buyAmt, d.closePrice " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock s, dbo.priceData d " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = '1' " +
                    "and i.code = p.investorCode " +
                    "and p.code = s.portfolio " +
                    "and s.stockCode = d.stockCode", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    portfolios.Add(
                        new Portfolio
                        {
                            code = myReader["stockCode"].ToString(),
                            quantity = myReader["qty"].ToString(),
                            cost = myReader.GetDecimal(myReader.GetOrdinal("buyAmt")),
                            matched = myReader.GetDecimal(myReader.GetOrdinal("closePrice")),
                            PL = matched - cost
                        }
                    );
                }

                myConnection.Close();

                return portfolios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

        public bool InsertStock (string stock, string investor, int quantity, int price)
        {
            SqlConnection myConnection = new SqlConnection(
                "user id=Testing;" +
                "password=123456;" +
                "server=TONY;" +
                "Trusted_Connection=yes;" +
                "database=Stock;" +
                "connection timeout=10");
            try
            {
                myConnection.Open();

                List<Portfolio> portfolios = new List<Portfolio>();

                SqlCommand myCommand = new SqlCommand(
                    "execute dbo.addStockToPortfolio '" 
                    + stock + "', '"
                    + investor + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd hh:HH:ss") + "', " 
                    + quantity + ", " 
                    + price, 
                    myConnection);

                myCommand.ExecuteNonQuery();

                myConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return false;
            }
        }
    }
}