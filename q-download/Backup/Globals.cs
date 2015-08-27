using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace wStock2
{
    public class Globals
    {
        public static string vContent = String.Empty;
        public static string formTitle = String.Empty;
        public static string subTitle = String.Empty;
        public static bool emailValid(string s)
        {
            Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Match match = regex.Match(s);
            return match.Success;
        }
    }
}
