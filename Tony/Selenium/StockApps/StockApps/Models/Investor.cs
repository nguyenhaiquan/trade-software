using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Investor
    {
        public string code { get; set; }
        public string name { get; set; }

        public Investor LogIn(string account, string password)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                Investor investor = null;

                SqlCommand myCommand =
                    new SqlCommand("select code, displayName " +
                    "from dbo.investor " +
                    "where account = '" + account + "' " +
                    "and password = '" + password + "'", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    investor = new Investor
                    {
                        code = myReader["code"].ToString(),
                        name = myReader["displayName"].ToString()
                    };
                }

                myConnection.Close();

                return investor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
                
        }

        public List<string> GetAsset(string code)
        {
            SqlConnection myConnection = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                List<string> asset = new List<string>();

                SqlCommand myCommand = new SqlCommand(
                    "select p.startCapAmt, p.usedCapAmt " +
                    "from dbo.investor i, dbo.portfolio p " +
                    "where i.code = '" + code + "' " +
                    "and i.code = p.investorCode " +
                    "and p.type = 1", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    asset.Add(myReader["startCapAmt"].ToString());
                    asset.Add(myReader["usedCapAmt"].ToString());
                }

                myConnection.Close();

                return asset;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}