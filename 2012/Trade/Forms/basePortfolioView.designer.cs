namespace stockTrade.forms
{
    partial class basePortfolioView
    {

        //common.libs.appAvailable myAvail = new common.libs.appAvailable();
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(basePortfolioView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolBarPnl = new common.control.basePanel();
            this.porfolioCb = new baseClass.controls.cbPorpolio();
            this.stockNAV = new System.Windows.Forms.BindingNavigator(this.components);
            this.portfolioListSource = new System.Windows.Forms.BindingSource(this.components);
            this.myTmpDS = new data.tmpDS();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshBtn = new common.control.baseButton();
            this.subjectColumn = new AdvancedDataGridView.TreeGridColumn();
            this.actionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeColunm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockGV = new common.control.baseDataGridView();
            this.stockCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockExCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boughtPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boughtAmtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitVariantAmtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitVariantPercColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceVariantColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myDataSet = new data.baseDS();
            this.toolBarPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stockNAV)).BeginInit();
            this.stockNAV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portfolioListSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTmpDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLbl
            // 
            this.TitleLbl.Location = new System.Drawing.Point(1051, 76);
            this.TitleLbl.Size = new System.Drawing.Size(62, 46);
            this.TitleLbl.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "tickerCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "stockExchange";
            this.dataGridViewTextBoxColumn2.HeaderText = "Sàn ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "noOpenShares";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.HeaderText = "SL niêm yết";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // toolBarPnl
            // 
            this.toolBarPnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.toolBarPnl.Controls.Add(this.porfolioCb);
            this.toolBarPnl.Controls.Add(this.stockNAV);
            this.toolBarPnl.Controls.Add(this.refreshBtn);
            this.toolBarPnl.haveCloseButton = false;
            this.toolBarPnl.Location = new System.Drawing.Point(-1, -2);
            this.toolBarPnl.Name = "toolBarPnl";
            this.toolBarPnl.Size = new System.Drawing.Size(1088, 33);
            this.toolBarPnl.TabIndex = 146;
            // 
            // porfolioCb
            // 
            this.porfolioCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.porfolioCb.FormattingEnabled = true;
            this.porfolioCb.Location = new System.Drawing.Point(0, 3);
            this.porfolioCb.myValue = "";
            this.porfolioCb.Name = "porfolioCb";
            this.porfolioCb.Size = new System.Drawing.Size(284, 26);
            this.porfolioCb.TabIndex = 1;
            this.porfolioCb.SelectedIndexChanged += new System.EventHandler(this.porfolioCb_SelectedIndexChanged);
            // 
            // stockNAV
            // 
            this.stockNAV.AddNewItem = null;
            this.stockNAV.BindingSource = this.portfolioListSource;
            this.stockNAV.CountItem = this.bindingNavigatorCountItem;
            this.stockNAV.CountItemFormat = "{0}";
            this.stockNAV.DeleteItem = null;
            this.stockNAV.Dock = System.Windows.Forms.DockStyle.None;
            this.stockNAV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.stockNAV.Location = new System.Drawing.Point(808, 3);
            this.stockNAV.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.stockNAV.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.stockNAV.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.stockNAV.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.stockNAV.Name = "stockNAV";
            this.stockNAV.PositionItem = this.bindingNavigatorPositionItem;
            this.stockNAV.Size = new System.Drawing.Size(195, 25);
            this.stockNAV.TabIndex = 150;
            this.stockNAV.Text = "bindingNavigator1";
            // 
            // portfolioListSource
            // 
            this.portfolioListSource.DataMember = "portfolioList";
            this.portfolioListSource.DataSource = this.myTmpDS;
            // 
            // myTmpDS
            // 
            this.myTmpDS.DataSetName = "tmpDS";
            this.myTmpDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorCountItem.Text = "{0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshBtn
            // 
            this.refreshBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshBtn.Image = global::stockTrade.Properties.Resources.refresh;
            this.refreshBtn.Location = new System.Drawing.Point(282, 5);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(27, 24);
            this.refreshBtn.TabIndex = 2;
            this.myToolTip.SetToolTip(this.refreshBtn, "Tải lại dữ liệu");
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // subjectColumn
            // 
            this.subjectColumn.DefaultNodeImage = null;
            this.subjectColumn.FillWeight = 8F;
            this.subjectColumn.HeaderText = "Diễn giải";
            this.subjectColumn.Name = "subjectColumn";
            this.subjectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.subjectColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // actionColumn
            // 
            this.actionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.actionColumn.FillWeight = 1F;
            this.actionColumn.HeaderText = "";
            this.actionColumn.Name = "actionColumn";
            this.actionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.actionColumn.Width = 50;
            // 
            // timeColunm
            // 
            this.timeColunm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.timeColunm.FillWeight = 1F;
            this.timeColunm.HeaderText = "Thời gian";
            this.timeColunm.Name = "timeColunm";
            this.timeColunm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.timeColunm.Width = 170;
            // 
            // idColumn
            // 
            this.idColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.idColumn.FillWeight = 1F;
            this.idColumn.HeaderText = "id";
            this.idColumn.Name = "idColumn";
            this.idColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idColumn.Width = 50;
            // 
            // stockGV
            // 
            this.stockGV.AllowUserToAddRows = false;
            this.stockGV.AllowUserToDeleteRows = false;
            this.stockGV.AutoGenerateColumns = false;
            this.stockGV.ColumnHeadersHeight = 46;
            this.stockGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stockCodeColumn,
            this.stockExCodeColumn,
            this.qtyColumn,
            this.boughtPriceColumn,
            this.priceColumn,
            this.boughtAmtColumn,
            this.amtColumn,
            this.profitVariantAmtColumn,
            this.profitVariantPercColumn,
            this.priceVariantColumn,
            this.volumeColumn,
            this.notesColumn});
            this.stockGV.DataSource = this.portfolioListSource;
            this.stockGV.Location = new System.Drawing.Point(0, 33);
            this.stockGV.Name = "stockGV";
            this.stockGV.ReadOnly = true;
            this.stockGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stockGV.Size = new System.Drawing.Size(1022, 579);
            this.stockGV.TabIndex = 147;
            this.stockGV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGV1_Scroll);
            this.stockGV.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.stockGV_CellContentDoubleClick);
            this.stockGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grid_DataError);
            this.stockGV.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGV1_Paint);
            this.stockGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.stockGV_DataBindingComplete);
            this.stockGV.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGV1_ColumnWidthChanged);
            // 
            // stockCodeColumn
            // 
            this.stockCodeColumn.DataPropertyName = "stockCode";
            this.stockCodeColumn.HeaderText = "stockCode";
            this.stockCodeColumn.Name = "stockCodeColumn";
            this.stockCodeColumn.ReadOnly = true;
            this.stockCodeColumn.Width = 70;
            // 
            // stockExCodeColumn
            // 
            this.stockExCodeColumn.DataPropertyName = "stockExCode";
            this.stockExCodeColumn.HeaderText = "stockExCode";
            this.stockExCodeColumn.Name = "stockExCodeColumn";
            this.stockExCodeColumn.ReadOnly = true;
            this.stockExCodeColumn.Width = 65;
            // 
            // qtyColumn
            // 
            this.qtyColumn.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.qtyColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.qtyColumn.HeaderText = "qty";
            this.qtyColumn.Name = "qtyColumn";
            this.qtyColumn.ReadOnly = true;
            this.qtyColumn.Width = 90;
            // 
            // boughtPriceColumn
            // 
            this.boughtPriceColumn.DataPropertyName = "boughtPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            this.boughtPriceColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.boughtPriceColumn.HeaderText = "boughtPrice";
            this.boughtPriceColumn.Name = "boughtPriceColumn";
            this.boughtPriceColumn.ReadOnly = true;
            this.boughtPriceColumn.Width = 70;
            // 
            // priceColumn
            // 
            this.priceColumn.DataPropertyName = "price";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.NullValue = null;
            this.priceColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.priceColumn.HeaderText = "price";
            this.priceColumn.Name = "priceColumn";
            this.priceColumn.ReadOnly = true;
            this.priceColumn.Width = 70;
            // 
            // boughtAmtColumn
            // 
            this.boughtAmtColumn.DataPropertyName = "boughtAmt";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.boughtAmtColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.boughtAmtColumn.HeaderText = "boughtAmt";
            this.boughtAmtColumn.Name = "boughtAmtColumn";
            this.boughtAmtColumn.ReadOnly = true;
            this.boughtAmtColumn.Width = 120;
            // 
            // amtColumn
            // 
            this.amtColumn.DataPropertyName = "amt";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.amtColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.amtColumn.HeaderText = "amt";
            this.amtColumn.Name = "amtColumn";
            this.amtColumn.ReadOnly = true;
            this.amtColumn.Width = 110;
            // 
            // profitVariantAmtColumn
            // 
            this.profitVariantAmtColumn.DataPropertyName = "profitVariantAmt";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            this.profitVariantAmtColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.profitVariantAmtColumn.HeaderText = "profitVariantAmt";
            this.profitVariantAmtColumn.Name = "profitVariantAmtColumn";
            this.profitVariantAmtColumn.ReadOnly = true;
            this.profitVariantAmtColumn.Width = 90;
            // 
            // profitVariantPercColumn
            // 
            this.profitVariantPercColumn.DataPropertyName = "profitVariantPerc";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.profitVariantPercColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.profitVariantPercColumn.HeaderText = "profitVariantPerc";
            this.profitVariantPercColumn.Name = "profitVariantPercColumn";
            this.profitVariantPercColumn.ReadOnly = true;
            this.profitVariantPercColumn.Width = 45;
            // 
            // priceVariantColumn
            // 
            this.priceVariantColumn.DataPropertyName = "priceVariant";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.priceVariantColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.priceVariantColumn.HeaderText = "priceVariant";
            this.priceVariantColumn.Name = "priceVariantColumn";
            this.priceVariantColumn.ReadOnly = true;
            this.priceVariantColumn.Width = 50;
            // 
            // volumeColumn
            // 
            this.volumeColumn.DataPropertyName = "volume";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.volumeColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.volumeColumn.HeaderText = "K/L";
            this.volumeColumn.Name = "volumeColumn";
            this.volumeColumn.ReadOnly = true;
            this.volumeColumn.Width = 80;
            // 
            // notesColumn
            // 
            this.notesColumn.DataPropertyName = "notes";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.notesColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.notesColumn.HeaderText = "notes";
            this.notesColumn.Name = "notesColumn";
            this.notesColumn.ReadOnly = true;
            // 
            // myDataSet
            // 
            this.myDataSet.DataSetName = "myDataSet";
            this.myDataSet.EnforceConstraints = false;
            this.myDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // basePortfolioView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 637);
            this.Controls.Add(this.toolBarPnl);
            this.Controls.Add(this.stockGV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.Name = "basePortfolioView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock list";
            this.Shown += new System.EventHandler(this.baseTradeAlertList_Shown);
            this.Resize += new System.EventHandler(this.baseTradeAlert_Resize);
            this.Controls.SetChildIndex(this.stockGV, 0);
            this.Controls.SetChildIndex(this.toolBarPnl, 0);
            this.Controls.SetChildIndex(this.TitleLbl, 0);
            this.toolBarPnl.ResumeLayout(false);
            this.toolBarPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stockNAV)).EndInit();
            this.stockNAV.ResumeLayout(false);
            this.stockNAV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portfolioListSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTmpDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected data.baseDS myDataSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        protected common.control.basePanel toolBarPnl;
        protected common.control.baseButton refreshBtn;
        private AdvancedDataGridView.TreeGridColumn subjectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeColunm;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        protected common.control.baseDataGridView stockGV;
        private System.Windows.Forms.BindingSource portfolioListSource;
        protected data.tmpDS myTmpDS;
        protected baseClass.controls.cbPorpolio porfolioCb;
        protected System.Windows.Forms.BindingNavigator stockNAV;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockExCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn boughtPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn boughtAmtColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amtColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitVariantAmtColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitVariantPercColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceVariantColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn volumeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesColumn;

    }
}