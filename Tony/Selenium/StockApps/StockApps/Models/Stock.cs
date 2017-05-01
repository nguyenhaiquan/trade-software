using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockApps.Models
{
    public class Stock
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }

        public List<Stock> GetAll()
        {
            SqlConnection myConnection = new SqlConnection("user id=Testing;" +
                              "password=123456;" +
                              "server=TONY;" +
                              "Trusted_Connection=yes;" +
                              "database=QStockDB; " +
                              "connection timeout=10");

            try
            {
                myConnection.Open();

                List<Stock> listStock = new List<Stock>();

                SqlCommand myCommand = new SqlCommand("select * from dbo.Stock", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    listStock.Add(
                        new Stock
                        {
                            id = Int32.Parse(myReader["ID"].ToString()),
                            code = myReader["StockCode"].ToString(),
                            description = myReader["Description"].ToString()
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

        public Stock GetStock(int id)
        {
            return GetAll().Where(x => x.id == id).FirstOrDefault();
        }

        public List<Stock> Create(Stock s)
        {
            List<Stock> listStock = GetAll();
            listStock.Add(s);
            return listStock;
        }

        public List<Stock> Update(Stock s)
        {
            List<Stock> listStock = GetAll().Where(x => x.id != s.id).ToList();
            Stock t = GetStock(s.id);
            listStock.Remove(t);
            listStock.Add(s);
            return listStock;
        }

        public List<Stock> Delete(int id)
        {
            List<Stock> listStock = GetAll().Where(x => x.id != id).ToList();
            return listStock;
        }
    }
}