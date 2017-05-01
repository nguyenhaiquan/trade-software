using System;
using System.Data.SqlClient;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace StockApps.Models
{
    public class Investor
    {
        public string LogIn(string account, string password)
        {
            string name = null;

            SqlConnection myConnection = new SqlConnection("user id=Testing;" +
                                       "password=123456;" +
                                       "server=TONY;" +
                                       "Trusted_Connection=yes;" +
                                       "database=Stock; " +
                                       "connection timeout=10");

            try
            {
                myConnection.Open();

                SqlCommand myCommand =
                    new SqlCommand("select * " +
                    "from dbo.investor i " +
                    "where i.account = '" + account +
                    "' and i.password = '" + password +
                    "'", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    name = myReader["displayName"].ToString();
                }

                myConnection.Close();

                return name;
            }
            catch
            {
                return null;
            }
                
        }
    }
}