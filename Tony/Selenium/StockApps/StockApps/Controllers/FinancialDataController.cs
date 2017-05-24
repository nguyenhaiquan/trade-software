using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class FinancialDataController : ApiController
    {
        // Call API
        // http://localhost:63471/api/FinancialData?code=VNM&time1=2015&time2=2016
        // or SAB
        public List<FinancialData> Get(string code, int time1, int time2)
        {
            return new FinancialData().GetAll(code, time1, time2);
        }

        // Call API
        // http://localhost:63471/api/FinancialData?code=HPG
        // or HSG
        public void Post(string code)
        {
            new FinancialData().SaveDB(code);
        }
    }
}
