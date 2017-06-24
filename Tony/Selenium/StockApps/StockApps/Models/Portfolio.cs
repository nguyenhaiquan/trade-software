using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockApps.Models
{
    public class Portfolio
    {
        public string code { get; set; }
        public string time { get; set; }
        public string quantity { get; set; }
        public decimal cost { get; set; }

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
                    "select s.stockCode, s.buyDate, s.qty, s.buyAmt " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock s " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = '1' " +
                    "and i.code = p.investorCode " +
                    "and p.code = s.portfolio " +
                    "order by s.buyDate DESC", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    portfolios.Add(
                        new Portfolio
                        {
                            code = myReader["stockCode"].ToString(),
                            time = myReader["buyDate"].ToString(),
                            quantity = myReader["qty"].ToString(),
                            cost = myReader.GetDecimal(myReader.GetOrdinal("buyAmt"))
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

                SqlCommand myCommand = new SqlCommand(
                    "execute dbo.addStockToPortfolio '" 
                    + stock + "', '"
                    + investor + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " 
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

        public bool DeleteStock(string stock, string investor, int quantity, int price)
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

                SqlCommand myCommand = new SqlCommand(
                    "execute dbo.deleteStockFromPortfolio '"
                    + stock + "', '"
                    + investor + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', "
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