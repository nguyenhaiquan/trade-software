using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Watchlist
    {
        public string code { get; set; }
        public decimal open { get; set; }
        public decimal matched { get; set; }
        public decimal volume { get; set; }

        public List<Watchlist> GetAll(string investor)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                myConnection.Open();

                List<Watchlist> watchlists = new List<Watchlist>();

                SqlCommand myCommand = new SqlCommand(
                    "select d.stockCode, d.openPrice, d.closePrice, d.volume " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock s, dbo.priceData d " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = '2' " +
                    "and i.code = p.investorCode " +
                    "and p.code = s.portfolio " +
                    "and s.stockCode = d.stockCode " +
                    "and d.onDate = (select MAX(onDate) from dbo.priceData)", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    watchlists.Add(
                        new Watchlist
                        {
                            code = myReader["stockCode"].ToString(),
                            open = myReader.GetDecimal(myReader.GetOrdinal("openPrice")),
                            matched = myReader.GetDecimal(myReader.GetOrdinal("closePrice")),
                            volume = myReader.GetDecimal(myReader.GetOrdinal("volume"))
                        }
                    );
                }

                myConnection.Close();

                return watchlists;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

        public bool InsertStock(string stock, string investor)
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
                    "execute dbo.addStockToWatchlist '"
                    + stock + "', '"
                    + investor + "', '"
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'",
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

        public bool DeleteStock(string stock, string investor)
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
                    "delete from dbo.investorStock " +
                    "where portfolio=" +
                    "(select p.code " +
                    "from investor i, portfolio p " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = 2 " +
                    "and p.investorCode = i.code)" +
                    "and stockCode = '" + stock + "'", 
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