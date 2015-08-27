using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace wStock2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                wUser user = new wUser();
                user.Username = Request.Form["username"];
                user.Password = Request.Form["password"];
                if (user.Login())
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("FAIL");
                }
            }
            catch
            {
                Response.Write("FAIL");
            }
        }
    }
}
