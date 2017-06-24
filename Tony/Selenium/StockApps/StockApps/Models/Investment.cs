using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockApps.Models
{
    public class Investment
    {
        public string code { get; set; }
        public string quantity { get; set; }
        public decimal cost { get; set; }
        public decimal matched { get; set; }

        public List<Investment> GetAll(string investor)
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

                List<Investment> investments = new List<Investment>();

                SqlCommand myCommand = new SqlCommand(
                    "exec dbo.getInvestment "
                    + investor, myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    investments.Add(
                        new Investment
                        {
                            code = myReader["stockCode"].ToString(),
                            quantity = myReader["quantity"].ToString(),
                            cost = myReader.GetDecimal(myReader.GetOrdinal("avgCost")),
                            matched = myReader.GetDecimal(myReader.GetOrdinal("closePrice"))
                        }
                    );
                }

                myConnection.Close();

                return investments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}