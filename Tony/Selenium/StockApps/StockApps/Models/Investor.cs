using System.Data.SqlClient;

namespace StockApps.Models
{
    public class Investor
    {
        public bool LogIn(string account, string password)
        {
            bool result = false;

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

                SqlCommand myCommand =
                    new SqlCommand("select * " +
                    "from dbo.investor i " +
                    "where i.account = '" + account + "' " +
                    "and i.password = '" + password + "'", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    result = true;
                }

                myConnection.Close();

                return result;
            }
            catch
            {
                return false;
            }
                
        }
    }
}