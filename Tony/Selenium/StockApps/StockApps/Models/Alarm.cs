using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockApps.Models
{
    public class Alarm
    {
        public string code { get; set; }
        public int type { get; set; }
        public int condition { get; set; }
        public int value { get; set; }
        public int status { get; set; }
        public List<Alarm> GetAll(string investor)
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

                List<Alarm> alarms = new List<Alarm>();

                SqlCommand myCommand = new SqlCommand(
                    "select a.stockCode, a.type, a.condition, a.value, a.status " +
                    "from dbo.investor i, dbo.alarm a " +
                    "where i.account = '" + investor + "' " +
                    "and i.code = a.investor", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    alarms.Add(
                        new Alarm
                        {
                            code = myReader["stockCode"].ToString(),
                            type = myReader.GetInt32(myReader.GetOrdinal("type")),
                            condition = myReader.GetInt32(myReader.GetOrdinal("condition")),
                            value = myReader.GetInt32(myReader.GetOrdinal("value")),
                            status = myReader.GetInt32(myReader.GetOrdinal("status"))
                        }
                    );
                }

                myConnection.Close();

                return alarms;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}