using System.Collections.Generic;
using System.Web.Http;
using StockApps.Models;

namespace StockApps.Controllers
{
    public class FinancialDataController : ApiController
    {
        // Call API
        // http://localhost:63471/api/FinancialData?code=VNM
        // or SAB
        public List<FinancialData> Get(string code)
        {
            return new FinancialData().GetAll(code);
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
