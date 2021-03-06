﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Alarm
    {
        public string code { get; set; }
        public string type { get; set; }
        public string condition { get; set; }
        public int value { get; set; }
        public int status { get; set; }
        public List<Alarm> GetAll(string investor)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
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
                            type = myReader["type"].ToString(),
                            condition = myReader["condition"].ToString(),
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

        public bool InsertAlarm(string stock, string investor, string type, string condition, int value, string comment, int status, string expiry, int notification)
        {
            SqlConnection myConnection = new SqlConnection(
                                        WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(
                    "execute dbo.addStockToAlarm '"
                    + stock + "', '"
                    + investor + "', '"
                    + type + "', '"
                    + condition + "', "
                    + value + ", '"
                    + comment + "', "
                    + status + ", '"
                    + expiry + "', "
                    + notification,
                    myConnection);

                myCommand.ExecuteNonQuery();

                myConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return false;
            }
        }

        public bool DeleteAlarm(string stock, string investor)
        {
            SqlConnection myConnection = new SqlConnection(
                                        WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(
                    "delete from dbo.alarm " +
                    "where investor=" +
                    "(select i.code " +
                    "from investor i " +
                    "where i.account = '" + investor + "')" +
                    "and stockCode = '" + stock + "'",
                    myConnection);

                myCommand.ExecuteNonQuery();

                myConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return false;
            }
        }
    }
}