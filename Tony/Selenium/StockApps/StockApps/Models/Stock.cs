using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockApps.Models
{
    public class Stock
    {
        public string code { get; set; }
        public string name { get; set; }

        public List<Stock> GetAll()
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

                List<Stock> listStock = new List<Stock>();

                SqlCommand myCommand = new SqlCommand(
                    "select s.code, s.name " +
                    "from dbo.stockCode s", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    listStock.Add(
                        new Stock
                        {
                            code = myReader["code"].ToString(),
                            name = myReader["name"].ToString()
                        }
                    );
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
            return GetAll().Where(x => x.code == code).FirstOrDefault();
        }

        public List<Stock> Create(Stock s)
        {
            List<Stock> listStock = GetAll();
            listStock.Add(s);
            return listStock;
        }

        public List<Stock> Update(Stock s)
        {
            List<Stock> listStock = GetAll().Where(x => x.code != s.code).ToList();
            Stock t = GetStock(s.code);
            listStock.Remove(t);
            listStock.Add(s);
            return listStock;
        }

        public List<Stock> Delete(string code)
        {
            List<Stock> listStock = GetAll().Where(x => x.code != code).ToList();
            return listStock;
        }
    }
}