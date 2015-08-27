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

        public void ProcessRequest(HttpContext context)
        {
            if (wUser.currentUser == null)
            {
                context.Response.Redirect("Login.aspx");
            }            
            //string filename = context.Request.QueryString["File"];
            ////Validate the file name and make sure it is one that the user may access
            //context.Response.Buffer = true;
            //context.Response.Clear();
            //context.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            //context.Response.ContentType = "octet/stream";

            //context.Response.WriteFile("~/App_Data/" + filename);
            context.Response.Redirect("https://q-invest.googlecode.com/files/QuantumPro.zip");
            //context.Response.RedirectLocation = "DownloadSuccess.aspx";            
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
