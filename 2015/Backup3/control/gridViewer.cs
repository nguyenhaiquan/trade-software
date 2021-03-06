using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using common;

namespace common.controls
{
    public partial class gridViewer : UserControl
    {
        private bool fSizeChange=false;
        public gridViewer()
        {
            InitializeComponent();
            this.dataGrid.Location = new Point(0,0); 
        }
        public bool Export()
        {
            try
            {
                this.saveFileDialog.CheckPathExists = true;
                this.saveFileDialog.DefaultExt = "xls";
                this.saveFileDialog.Filter = "Dang Excel(*.XLS)|*.XLS|Tat ca(*.*)|*.*";
                this.saveFileDialog.AddExtension = true;
                this.saveFileDialog.RestoreDirectory = true;
                this.saveFileDialog.Title = "Thu muc luu tap tin ?";
                if (this.saveFileDialog.ShowDialog() != DialogResult.OK) return false;
                DataTable tbl = ((DataTable)this.dataGrid.DataSource).Copy();
                common.Export.ExportToExcel(tbl, this.saveFileDialog.FileName);
                common.sysLibs.ShowMessage("Đã xuất dữ liệu ra tập tin : " + this.saveFileDialog.FileName);
                return true;
            }
            catch (Exception er)
            {
                common.errorHandler.lastErrorMessage = er.Message.ToString();
            }
            return false;
        }
        public void SetGridSize(int w, int h)
        {
            this.Size = new Size(w, h);
            this.dataGrid.Size = new Size(w - 5, h - 5);

            int colCount = 0, fillWidth = 25;
            for (int idx = 0; idx < this.dataGrid.ColumnCount; idx++)
            {
                if (this.dataGrid.Columns[idx].Visible)
                {
                    fillWidth += this.dataGrid.Columns[idx].Width;
                    colCount++;
                }
            }
            int gridWidth = this.dataGrid.Width - this.dataGrid.RowHeadersWidth;
            if (fillWidth == 0) return;
            decimal d;
            for (int idx = 0; idx < this.dataGrid.ColumnCount; idx++)
            {
                if (this.dataGrid.Columns[idx].Visible)
                {
                    d = ((decimal)this.dataGrid.Columns[idx].Width / fillWidth) * (gridWidth - fillWidth);
                    this.dataGrid.Columns[idx].Width += (int)Math.Ceiling(d);
                }
            }
        }
        public void SetDataGrid(DataTable dataTbl, ArrayList arrList)
        {
            int colCount = arrList.Count;
            ViewerCellSpec cellSpec = new ViewerCellSpec();
            DataGridViewTextBoxColumn[] col = new DataGridViewTextBoxColumn[colCount];
            for (int idx = 0; idx < colCount; idx++)
            {
                cellSpec = (ViewerCellSpec)arrList[idx];
            }
            dataGrid.DataSource = dataTbl;
            dataGrid.Columns.Clear();
            dataGrid.ReadOnly = true;
            for (int idx = 0; idx < colCount; idx++)
            {
                cellSpec = (ViewerCellSpec)arrList[idx];
                col[idx] = new DataGridViewTextBoxColumn();
                col[idx].Name = cellSpec.fieldName;
                col[idx].HeaderText = cellSpec.headerText;
                col[idx].Width = cellSpec.columnWidth;
                col[idx].Visible = cellSpec.Visible;
                col[idx].DefaultCellStyle = cellSpec.viewCellStyle;
                col[idx].DataPropertyName = cellSpec.fieldName;
                dataGrid.Columns.Add(col[idx]);
                dataGrid.Columns[idx].Visible = cellSpec.Visible;
            }
            SetGridSize(this.Width, this.Height);
        }

        private void gridViewer_SizeChanged(object sender, EventArgs e)
        {
            if (fSizeChange) return;
            fSizeChange = true;
            SetGridSize(this.Size.Width, this.Size.Height);
            fSizeChange = false;
        }
    }

    public class ViewerCellSpec
    {
        public string headerText;
        public string fieldName;
        public int columnWidth;
        public DataGridViewCellStyle viewCellStyle;
        public bool Visible;

        public ViewerCellSpec()
        {
            viewCellStyle = new DataGridViewCellStyle();
        }
    }

}
