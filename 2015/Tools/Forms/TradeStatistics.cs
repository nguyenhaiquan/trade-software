using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools.Forms
{
    public partial class TradeStatistics : baseClass.forms.baseForm
    {
        public TradeStatistics()
        {
            InitializeComponent();
        }

        public void AddStatisticInfo(string info, double number)
        {
            var index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = info;
            dataGridView1.Rows[index].Cells[1].Value = number;
        }

        public void ReAssignInfo(int index, double number)
        {
            dataGridView1.Rows[index].Cells[1].Value = number;
        }

        public void ReAssignInfo(int index, string s)
        {
            dataGridView1.Rows[index].Cells[1].Value = s;
        }

        public void AddStatisticInfo(string info, string number)
        {
            var index = dataGridView1.Rows.Add();           
            dataGridView1.Rows[index].Cells[0].Value = info;
            dataGridView1.Rows[index].Cells[1].Value = number;
        }
    }
}
