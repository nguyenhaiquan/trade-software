using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Transactions;
namespace baseClass.forms
{
    public partial class baseMasterEdit : common.forms.baseMasterEditForm 
    {
        public baseMasterEdit()
        {
            InitializeComponent();
            this.FormBorderStyle = (commonClass.Settings.sysGlobal.DebugMode ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle); 
        }
        #region Abtrstract functions
        public override int GetFormPermission()
        {
            return common.Consts.constPermissionADD + common.Consts.constPermissionEDIT + common.Consts.constPermissionDEL;
        }
        protected override void CancelAllChanges() 
        { 
            myDataSet.RejectChanges();
        }
        protected override bool DataChanged() 
        {
            myMasterSource.EndEdit();
            return myDataSet.HasChanges();
        }
        #endregion Abtrstract functions

        protected override void ShowError(Exception er)
        {
            application.AppLibs.WriteSyslog(commonClass.Settings.sysWriteLogException, commonClass.Settings.sysAccessMode, er);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                application.AppLibs.WriteSyslog(commonClass.Settings.sysWriteLogAccess, commonClass.Settings.sysAccessMode,
                                                commonClass.AppTypes.SyslogTypes.Access, commonClass.SysLibs.sysLoginCode, "Open : " + this.Name, null, null);
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                application.AppLibs.WriteSyslog(commonClass.Settings.sysWriteLogAccess, commonClass.Settings.sysAccessMode,
                                commonClass.AppTypes.SyslogTypes.Access, commonClass.SysLibs.sysLoginCode, "Closed : " + this.Name, null, null);
            }
            catch (Exception er)
            {
                ShowError(er);
            }
        }
    }
}