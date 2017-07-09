using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Web.Configuration;

namespace StockApps.Models
{
    public class FinancialData
    {
        public string rubric { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }
        public List<FinancialData> GetAll(string code, int time1, int time2)
        {
            SqlConnection myConnection = new SqlConnection(
                            WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                myConnection.Open();

                List<FinancialData> data = new List<FinancialData>();

                SqlCommand myCommand = new SqlCommand(
                    "select d.time, d.value, r.description " +
                    "from dbo.financialData d, dbo.financialRubric r " +
                    "where d.stock = '" + code + "' " +
                    "and (d.time = '" + time1 + "' " +
                    "or d.time = '" + time2 + "') " +
                    "and d.rubric = r.id " +
                    "and r.id_parent is null", myConnection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                int i = 0; // column's index
                string[] row = new string[5];

                while (myReader.Read())
                {
                    if (i == 0)
                    {
                        // get rubric and data if i = 0
                        row[i] = myReader["description"].ToString();
                        row[i+1] = myReader["value"].ToString();
                    }
                    else
                    {
                        // get data if i = 1
                        row[i+1] = myReader["value"].ToString();

                        // add new financial data
                        data.Add(
                            new FinancialData
                            {
                                rubric = row[0],
                                value1 = row[1],
                                value2 = row[2]
                            });

                        // reset i
                        i = -1;
                    }
                    i++;
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

        public void SaveDB(string code)
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

                string url = "http://ivt.ssi.com.vn/CorporateFundBalanceSheet.aspx?Ticket=" + code;

                IWebDriver driver = new InternetExplorerDriver();
                driver.Url = url;

                // Change table data (by Quarter => by Year)
                driver.FindElement(By.Id("ctl00_mainContent_CorporateFundBalanceSheet1_LinkButton2")).Click();

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
                            // First column => Financial Rubric
                            SqlCommand myCommand =
                                new SqlCommand("insert into dbo.financialRubric (id, id_parent, category, description) " +
                                         "Values (" + i + ", NULL, 1, N'" + Table_data + "')", myConnection);
                            myCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            // Next columns => Financial Data
                            SqlCommand myCommand =
                                new SqlCommand("insert into dbo.financialData (stock, rubric, time, value) " +
                                         "Values (" + code + ", " + i + ", '" + time[j - 1] + "', '" + Table_data + "')", myConnection);
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