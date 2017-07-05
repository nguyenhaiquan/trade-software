using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                "user id=Testing;" +
                "password=123456;" +
                "server=TONY;" +
                "Trusted_Connection=yes;" +
                "database=Stock;" +
                "connection timeout=10");
            try
            {
                myConnection.Open();

                List<TradeHistory> data = new List<TradeHistory>();

                SqlCommand myCommand = new SqlCommand(
                    "select top(" + days + ") d.onDate, d.closePrice, d.volume " +
                    "from dbo.priceData d " +
                    "where d.stockCode = '" + stock + "' " +
                    "order by d.onDate ASC", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    data.Add(
                        new TradeHistory
                        {
                            date = myReader["onDate"].ToString().Substring(0, 9),
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