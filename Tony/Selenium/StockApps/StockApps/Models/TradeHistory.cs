using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class TradeHistory
    {
        public string date { get; set; }
        public decimal price { get; set; }
        public decimal volume { get; set; }

        public List<TradeHistory> GetAll(string stock, int days)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                myConnection.Open();

                List<TradeHistory> data = new List<TradeHistory>();

                SqlCommand myCommand = new SqlCommand(
                    "select top(" + days + ") convert(varchar(10), d.onDate, 103) as onDate, d.closePrice, d.volume " +
                    "from dbo.priceData d " +
                    "where d.stockCode = '" + stock + "' " +
                    "order by d.onDate DESC", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    data.Add(
                        new TradeHistory
                        {
                            date = myReader["onDate"].ToString(),
                            price = myReader.GetDecimal(myReader.GetOrdinal("closePrice")),
                            volume = myReader.GetDecimal(myReader.GetOrdinal("volume"))
                        }
                    );
                }

                myConnection.Close();

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}