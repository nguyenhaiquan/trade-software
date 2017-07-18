using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Investor
    {
        public string name { get; set; }
        public string code { get; set; }

        public Investor LogIn(string account, string password)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                Investor investor = null;

                SqlCommand myCommand =
                    new SqlCommand("select displayName, code " +
                    "from dbo.investor " +
                    "where account = '" + account + "' " +
                    "and password = '" + password + "'", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    investor = new Investor
                    {
                        name = myReader["displayName"].ToString(),
                        code = myReader["code"].ToString()
                    };
                }

                myConnection.Close();

                return investor;
            }
            catch
            {
                return null;
            }
                
        }
    }
}