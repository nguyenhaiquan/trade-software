using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StockApps.Models
{
    public class Watchlist
    {
        public string code { get; set; }
        public string matched { get; set; }
        public string PL { get; set; }
        public string volume { get; set; }
        public List<Watchlist> GetAll(string investor)
        {
            SqlConnection myConnection = new SqlConnection("user id=Testing;" +
                              "password=123456;" +
                              "server=TONY;" +
                              "Trusted_Connection=yes;" +
                              "database=Stock; " +
                              "connection timeout=10");
            try
            {
                myConnection.Open();

                List<Watchlist> watchlists = new List<Watchlist>();

                SqlCommand myCommand = new SqlCommand("select s.stockCode, s.qty, s.buyAmt " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock s " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = '2' and i.code = p.investorCode and p.code = s.portfolio", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    watchlists.Add(
                        new Watchlist
                        {
                            code = myReader["stockCode"].ToString(),
                            matched = myReader["buyAmt"].ToString(),
                            PL = "0",
                            volume = myReader["qty"].ToString()
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
    }
}