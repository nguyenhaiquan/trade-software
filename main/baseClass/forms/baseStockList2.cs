using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using application;

namespace baseClass.forms
{
    public partial class baseStockList2 : common.forms.baseForm
    {

        public baseStockList2()
        {
            try
            {
                InitializeComponent();
                //stockCodeGrid.DisableReadOnlyColumn = false;
                //stockListFilterCb.SelectedIndex = 1;
                //LoadData_StockCode();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }
        public data.baseDS.stockCodeRow myStockRow
        {
            get
            {
                if (stockCodeSource.Current == null) return null;
                return (data.baseDS.stockCodeRow)((DataRowView)stockCodeSource.Current).Row;
            }
        }
        
        public void Goto(string stockCode)
        {
            int idx = stockCodeSource.Find(myDataSet.stockCode.codeColumn.ColumnName, stockCode);
            if (idx >= 0)
            {
                stockCodeSource.Position = idx;
                return;
            }
            //Load all data and find again
            stockListFilterCb.SelectedIndex = 0;
            idx = stockCodeSource.Find(myDataSet.stockCode.codeColumn.ColumnName, stockCode);
            if (idx >= 0)  stockCodeSource.Position = idx;
        }

        private void LoadData_StockCode()
        {
            myDataSet.stockCode.Clear();
            switch (stockListFilterCb.SelectedIndex)
            {
                case 0: dataLibs.LoadData(myDataSet.stockCode);
                    break;
                //case 1: dataLibs.LoadStockCode_ByStockCode(myDataSet.stockCode, sysLibs.sysLoginInterestedStockCode);
                //    break;
                //case 2: dataLibs.LoadStockCode_ByBizSector(myDataSet.stockCode, sysLibs.sysLoginInterestedBizSectors);
                //    break;

            }
            this.stockCountlbl.Text = "[" + stockCodeSource.Count.ToString() + "]";
        }

        private void LoadPortpolioStock()
        {
            StringCollection stockList = new StringCollection();
            StringCollection tmpList;
            TreeNode node;
            myDataSet.portpolio.Clear();
            dataLibs.LoadPortpolioByInvestor(myDataSet.portpolio, sysLibs.sysLoginCode);
            stockTV.Nodes.Clear();
            for (int idx1 = 0; idx1 < myDataSet.portpolio.Count; idx1++)
            {
                node = stockTV.Nodes.Add(myDataSet.portpolio[idx1].name);
                myDataSet.stockCode.Clear();
                // Stock list
                tmpList = common.system.MultivalueString2List(myDataSet.portpolio[idx1].interestedSector);
                if (tmpList.Count > 0) dataLibs.LoadStockCode_ByBizSector(myDataSet.stockCodeExt, tmpList);
                tmpList = common.system.MultivalueString2List(myDataSet.portpolio[idx1].interestedStock);
                if (tmpList.Count > 0) dataLibs.LoadStockCode_ByStockCode(myDataSet.stockCodeExt, tmpList);
                
                stockList.Clear();
                for (int idx2 = 0; idx2 < myDataSet.stockCode.Count; idx2++)
                {
                    //Ignore duplicate stocks
                    if (stockList.Contains(myDataSet.stockCode[idx2].tickerCode)) continue;
                    stockList.Add(myDataSet.stockCode[idx2].tickerCode);
                    node.Nodes.Add(myDataSet.stockCode[idx2].tickerCode); 
                }
                node.Text = node.Text + "(" + node.Nodes.Count.ToString() + ")";
            }
        }

        #region event handler

        private void stockListFilterCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadData_StockCode();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }

        private void stockCodeGrid_DoubleClick(object sender, EventArgs e)
        {
            closeBtn_Click(null, null);
        }
        private void baseStockList_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPortpolioStock();
            }
            catch (Exception er)
            {
                this.ShowError(er);
            }
        }
        #endregion
    }
}