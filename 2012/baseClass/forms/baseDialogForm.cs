using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using commonTypes;

namespace baseClass.forms
{
    public partial class baseDialogForm : common.forms.baseDialogForm  
    {
        public baseDialogForm()
        {
            InitializeComponent();
        }
        protected override void ShowError(Exception er)
        {
            switch(Settings.sysWriteLogException)
            {
                case AppTypes.SyslogMedia.Database: 
                     DataAccess.Libs.WriteSyslog(er,commonClass.SysLibs.sysLoginCode);
                     break;
                case AppTypes.SyslogMedia.File:
                     commonClass.SysLibs.WriteSysLog(er, commonClass.SysLibs.sysLoginCode);
                     break;
            }
        }
    }    
}