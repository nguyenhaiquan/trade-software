using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace wStock2
{
    public partial class DownloadSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (wUser.currentUser==null)
            {
                Response.Redirect("Login.aspx");
            }
            this.LoadComplete += new EventHandler(DownloadSuccess_LoadComplete);
            if (IsPostBack)
            {
                Response.Redirect("DownloadHandler.ashx?File=Quantum_0202beta.zip");
            }
        }       

        void DownloadSuccess_LoadComplete(object sender, EventArgs e)
        {
            
            
        }       
    }
}
