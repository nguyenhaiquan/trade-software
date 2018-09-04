using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using commonTypes;
using commonClass;

namespace Imports
{
    public abstract class generalImport
    {
        //Keep the last import price to excluse non-changed data
        public ImportData lastImportData = new ImportData();
        public abstract databases.baseDS.priceDataDataTable GetImportFromWeb(DateTime updateTime, string market);
        public abstract databases.baseDS.priceDataDataTable GetImportFromCSV(string fileName, string market, OnUpdatePriceData onUpdateDataFunc);
        public virtual bool ImportFromWeb(DateTime updateTime, string market)
        {
            try
            {
                databases.baseDS.priceDataDataTable priceTbl = GetImportFromWeb(updateTime, market);
                if (priceTbl == null) return false;

                // Different culture has different start of week, ie in VN culture : start of week is Monday (not Sunday) 
                CultureInfo exchangeCulture = application.AppLibs.GetExchangeCulture(market);
                databases.AppLibs.AggregatePriceData(priceTbl, exchangeCulture, null);
                return true;
            }
            catch (Exception er)
            {
                //retVal = false;
                commonClass.SysLibs.WriteSysLog(common.SysSeverityLevel.Error, "SRV004", er);
                return false;
            }
        }
    }
}
