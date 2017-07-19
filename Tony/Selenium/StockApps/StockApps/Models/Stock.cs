using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class Stock
    {
        public string code { get; set; }
        public string name { get; set; }
        public decimal openPrice { get; set; }
        public decimal closePrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal volume { get; set; }
        public decimal PB { get; set; }
        public decimal EPS { get; set; }
        public decimal PE { get; set; }
        public decimal ROA { get; set; }
        public decimal ROE { get; set; }
        public decimal BETA { get; set; }

        public List<string> GetAll()
        {
            SqlConnection myConnection = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                List<string> listStock = new List<string>();

                SqlCommand myCommand = new SqlCommand(
                    "select code " +
                    "from dbo.stockCode", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    listStock.Add(myReader["code"].ToString());
                }

                myConnection.Close();

                return listStock;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }            
        }

        public Stock GetStock(string code)
        {
            SqlConnection myConnection = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                Stock stock = null;

                SqlCommand myCommand = new SqlCommand(
                    "select s.code, s.nameEn, p.openPrice, p.closePrice, p.lowPrice, p.highPrice, p.volume, s.PB, s.EPS, s.PE, s.ROA,s.ROE, s.BETA " +
                    "from dbo.stockCode s, dbo.priceData p " +
                    "where p.onDate = (select max(onDate) from dbo.priceData) " +
                    "and s.code = '" + code + "'" +
                    "and s.code = p.stockCode", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    stock = new Stock
                    {
                        code = myReader["code"].ToString(),
                        name = myReader["nameEn"].ToString(),
                        openPrice = myReader.GetDecimal(myReader.GetOrdinal("openPrice")),
                        closePrice = myReader.GetDecimal(myReader.GetOrdinal("closePrice")),
                        lowPrice = myReader.GetDecimal(myReader.GetOrdinal("lowPrice")),
                        highPrice = myReader.GetDecimal(myReader.GetOrdinal("highPrice")),
                        volume = myReader.GetDecimal(myReader.GetOrdinal("volume")),
                        PB = myReader.GetDecimal(myReader.GetOrdinal("PB")),
                        EPS = myReader.GetDecimal(myReader.GetOrdinal("EPS")),
                        PE = myReader.GetDecimal(myReader.GetOrdinal("PE")),
                        ROA = myReader.GetDecimal(myReader.GetOrdinal("ROA")),
                        ROE = myReader.GetDecimal(myReader.GetOrdinal("ROE")),
                        BETA = myReader.GetDecimal(myReader.GetOrdinal("BETA")),
                    };
                }

                myConnection.Close();

                return stock;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}