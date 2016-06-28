using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Security;

namespace wStock2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DownloadHandler : IHttpHandler
    {
        const string sDownloadFile = "http://210.211.116.4/quantuminstall/setup.exe";

        public void ProcessRequest(HttpContext context)
        {
            if (wUser.currentUser == null)
            {
                context.Response.Redirect("Login.aspx");
            }            
            
            context.Response.Redirect(sDownloadFile);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
    }
}
