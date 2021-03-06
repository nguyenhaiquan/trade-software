using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace baseClass.forms
{
    public partial class projCommentEdit : baseMasterEdit 
    {
        public projCommentEdit()
        {
            try
            {
                InitializeComponent();
                rateCb.LoadData();
                myMasterSource = projCommentSource;
                userNameEd.isToUpperCase = true;
                userNameEd.maxlen = myDataSet.projects.projectCodeColumn.MaxLength;

                rateCb.LoadData();

                this.CloseOnSave = true;
                this.allowToChangeDeleteCode = true;
                this.myFormCode = this.Name;

                data.system.projCommentTA.FillByProjectCode(myDataSet.projComment, "01");
            }
            catch (Exception ex)
            {
                common.system.ShowErrorMessage(ex.Message.ToString());
            }
        }
       
        public void ShowProjectComment(int id)
        {
            try
            {
                if (id == int.MinValue)
                {
                    AddNew("");
                    LockEdit(false);
                    return;
                }
                LockEdit(true);
                data.system.projCommentTA.FillById(myDataSet.projComment, id);
                if (myDataSet.projComment.Count <= 0) return;
                this.SetFirstFocus();
                this.ShowDialog();
            }
            catch (Exception ex)
            {
                common.system.ShowErrorMessage(ex.Message.ToString());
            }
        }

        public override void LockEdit(bool lockState)
        {
            base.LockEdit(lockState);
            userNameEd.Enabled = !lockState;
            rateCb.Enabled = !lockState;
            projTitleEd.Enabled = !lockState;
            commentEd.Enabled = !lockState;
            onDateEd.Enabled = !lockState;
        }
        protected override void SetFirstFocus() { userNameEd.Focus(); }
        protected override bool DataValid(bool showMsg)
        {
            if (userNameEd.Text.Trim() == "") 
            {
                common.system.ShowErrorMessage("Chưa nhập ["+ codeLbl.Text + "]");
                this.userNameEd.Focus(); return false; 
            }
            if (projTitleEd.Text.Trim() == "")
            {
                common.system.ShowErrorMessage("Chưa nhập [" + projTitleLbl.Text + "]");
                this.projTitleEd.Focus(); return false;
            }
            return true;
        }
        protected override void UpdateData(DataRow row)
        {
            if (row == null)
            {
                data.system.projCommentTA.Update(myDataSet.projComment);
                myDataSet.projects.AcceptChanges();
            }
            else
            {
                data.system.projCommentTA.Update(row);
                row.AcceptChanges();
            }
        }
        public override void AddNew(string code)
        {
            this.AddNewRow();
            data.baseDS.projCommentRow projectsRow = (data.baseDS.projCommentRow)((DataRowView)myMasterSource.Current).Row;
            if (projectsRow == null) return;
            projectsRow.projectCode = code;
        }
        //Get key value in a row.
        protected override string GetKeyValue(DataRow dr)
        {
            return ((data.baseDS.projCommentRow)dr).projectCode;
        }
        //When delete a code, deleted code in existing data must be changed to retain the intergrity
        protected override bool ChangeCode(DataRowView drOld, string toCode, bool deleteAfterChange)
        {
            //toCode = toCode.Trim();
            //if (toCode == "") return true;

            //data.baseDS.projectsRow oldRow = (data.baseDS.projectsRow)drOld.Row;
            //data.baseDS.projectsRow newRow = application.dataLibs.Getprojects(toCode);
            //if (newRow == null) newRow = CreateDuplicate(oldRow, toCode);
            ////??projectsTA.ChangeprojectsCode(oldRow.code, newRow.code);

            //if (deleteAfterChange) RemoveCurrent();
            return true;
        }
        private data.baseDS.projCommentRow CreateDuplicate(data.baseDS.projectsRow oldRow, string newCode)
        {
            data.baseDS.projCommentRow newRow = myDataSet.projComment.NewprojCommentRow();
            newRow.ItemArray = oldRow.ItemArray;
            newRow.projectCode = newCode;
            myDataSet.projComment.AddprojCommentRow(newRow);
            data.system.projectsTA.Update(newRow);
            return newRow;
        }

        protected override DataRow FindCode(string code, bool showSelectionIfnotFound)
        {
            return null;
            //baseClass.interfaces.myprojectsFind.Find(code, showSelectionIfnotFound);
            //return baseClass.interfaces.myprojectsFind.selectedDataRow;
        }
        
        private void codeEd_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = this.CheckDuplicateKey(userNameEd.Text.Trim(), myDataSet.projects.projectCodeColumn.ColumnName);
        }
    }    
}