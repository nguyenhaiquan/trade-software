using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StockApps.Models
{
    public class Portfolio
    {
        public string code { get; set; }
        public string quantity { get; set; }
        public string cost { get; set; }
        public string matched { get; set; }
        public string PL { get; set; }
        public List<Portfolio> GetAll(string investor)
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

                List<Portfolio> portfolios = new List<Portfolio>();

                SqlCommand myCommand = new SqlCommand("select s.stockCode, s.qty, s.buyAmt " +
                    "from dbo.investor i, dbo.portfolio p, dbo.investorStock s " +
                    "where i.account = '" + investor + "' " +
                    "and p.type = '1' and i.code = p.investorCode and p.code = s.portfolio", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    portfolios.Add(
                        new Portfolio
                        {
                            code = myReader["stockCode"].ToString(),
                            quantity = myReader["qty"].ToString(),
                            cost = myReader["buyAmt"].ToString(),
                            matched = myReader["buyAmt"].ToString(),
                            PL = "0"
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
    }
}