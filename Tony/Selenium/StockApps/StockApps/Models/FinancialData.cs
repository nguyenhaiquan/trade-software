using System;
using System.Data.SqlClient;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace StockApps.Models
{
    public class FinancialData
    {
        public string[,] Get(int id)
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

                int rows = 40, columns = 5;

                string[,] data = new string[rows, columns];

                // Get Stock's Code
                data[0, 0] = new Stock().GetStock(id).code;

                SqlCommand myCommand =
                    new SqlCommand("select * " +
                    "from dbo.FinancialData d, dbo.FinancialRubric r " +
                    "where d.ID_Stock = " + id +
                    " and d.ID_Rubric = r.ID", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                int r = 1, c = 1;

                while (myReader.Read())
                {
                    data[r, c] = myReader["Value"].ToString();
                    c++;

                    if (c > 4)
                    {
                        data[r, 0] = myReader["Description"].ToString();
                        r++;

                        c = 1;
                    }
                }

                myConnection.Close();

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

        public void SaveDB(int id)
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

                string code = new Stock().GetStock(id).code;
                string url = "http://ivt.ssi.com.vn/CorporateFundBalanceSheet.aspx?Ticket=" + code;

                IWebDriver driver = new InternetExplorerDriver();
                driver.Url = url;

                Thread.Sleep(1000);

                // Change table data (by Quarter => by Year)
                driver.FindElement(By.Id("ctl00_mainContent_CorporateFundBalanceSheet1_LinkButton2")).Click();

                Thread.Sleep(1000);

                //Get number of rows In table.
                int Row_count = driver.FindElements(By.XPath("//*[@class='content_chart_20']/table/tbody/tr")).Count;
                Console.WriteLine("Number Of Rows = " + Row_count);

                //Get number of columns In table.
                int Col_count = driver.FindElements(By.XPath("//*[@class='content_chart_20']/table/tbody/tr[2]/td")).Count;
                Console.WriteLine("Number Of Columns = " + Col_count);

                //divided header in two parts to pass 1 and Col_count values.
                String header_begin = "//*[@class='content_chart_20']/table/tbody/tr[1]/th[";
                String header_end = "]";

                String[] time = new String[Col_count];

                // get time data
                for (int k = 1; k <= Col_count; k++)
                {
                    String xpath = header_begin + k + header_end;
                    time[k - 1] = driver.FindElement(By.XPath(xpath)).Text;
                }

                //divided xpath In three parts to pass Row_count and Col_count values.
                String first_part = "//*[@class='content_chart_20']/table/tbody/tr[";
                String second_part = "]/td[";
                String third_part = "]";

                //Used for loop for number of rows.
                for (int i = 2; i <= Row_count; i++)
                {
                    //Used for loop for number of columns.
                    for (int j = 1; j <= Col_count; j++)
                    {
                        //Prepared final xpath of specific cell as per values of i and j.
                        String final_xpath = first_part + i + second_part + j + third_part;
                        //Will retrieve value from located cell and print It.
                        String Table_data = driver.FindElement(By.XPath(final_xpath)).Text;

                        if (Table_data.Equals("") || Table_data.Equals(" "))
                        {
                            // Null Value => Do nothing
                        }
                        else if (j == 1)
                        {
                            /* First column => Financial Rubric
                            SqlCommand myCommand =
                                new SqlCommand("INSERT INTO dbo.FinancialRubric (ID, ID_Parent, ID_Category, Description) " +
                                         "Values (" + i + ", NULL, 1, N'" + Table_data + "')", myConnection);
                            myCommand.ExecuteNonQuery();
                            */
                        }
                        else
                        {
                            // Next columns => Financial Data
                            SqlCommand myCommand =
                                new SqlCommand("INSERT INTO dbo.FinancialData (ID_Stock, ID_Rubric, Time, Value) " +
                                         "Values (" + id + ", " + i + ", '" + time[j - 1] + "', '" + Table_data + "')", myConnection);
                            myCommand.ExecuteNonQuery();
                        }
                    }
                }

                // Close the driver
                driver.Quit();

                // Close the connection
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}