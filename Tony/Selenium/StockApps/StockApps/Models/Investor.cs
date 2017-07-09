using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Investor
    {
        public bool LogIn(string account, string password)
        {
            bool result = false;

            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

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