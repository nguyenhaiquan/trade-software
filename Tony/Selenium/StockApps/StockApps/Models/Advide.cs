using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Advide
    {
        public string code { get; set; }
        public decimal current { get; set; }
        public decimal target { get; set; }
        public decimal cutloss { get; set; }
        public string explication { get; set; }
        public string source { get; set; }

        public Advide Get()
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                myConnection.Open();

                Advide newestAdvide = null;

                SqlCommand myCommand = new SqlCommand(
                    "select top(1) * " +
                    "from dbo.evaluation e, dbo.priceData p " +
                    "where p.onDate = (select max(onDate) from dbo.priceData) " +
                    "and e.stockCode = p.stockCode " +
                    "order by (p.closePrice / e.targetPrice) asc", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    newestAdvide = new Advide
                    {
                        code = myReader["stockCode"].ToString(),
                        current = myReader.GetDecimal(myReader.GetOrdinal("closePrice")),
                        target = myReader.GetDecimal(myReader.GetOrdinal("targetPrice")),
                        cutloss = myReader.GetDecimal(myReader.GetOrdinal("cutlossPrice")),
                        explication = myReader["explication"].ToString(),
                        source = myReader["source"].ToString()
                    };
                }

                myConnection.Close();

                return newestAdvide;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}