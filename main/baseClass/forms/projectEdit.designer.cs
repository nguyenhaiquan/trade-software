namespace baseClass.forms
{
    partial class projectEdit
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(projectEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.projectsSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectTab = new System.Windows.Forms.TabControl();
            this.infoPg = new System.Windows.Forms.TabPage();
            this.subjectCodeCb = new baseClass.controls.clbSubjectCode();
            this.descriptionEd = new System.Windows.Forms.RichTextBox();
            this.subjectCodeLbl = new baseClass.controls.baseLabel();
            this.budgetUnitCb = new baseClass.controls.cbCurrency();
            this.budgetEd = new common.control.numberTextBox();
            this.endDateEd = new common.control.txtDateTime();
            this.endDateLbl = new baseClass.controls.baseLabel();
            this.ownerLbl = new baseClass.controls.baseLabel();
            this.projectTypeLbl = new baseClass.controls.baseLabel();
            this.projectTypeCb = new baseClass.controls.cbProjectType();
            this.label6 = new baseClass.controls.baseLabel();
            this.ownerEd = new common.control.baseTextBox();
            this.descriptionLbl = new baseClass.controls.baseLabel();
            this.codeLbl = new baseClass.controls.baseLabel();
            this.budgetLbl = new baseClass.controls.baseLabel();
            this.codeEd = new common.control.baseTextBox();
            this.projTitleEd = new common.control.baseTextBox();
            this.projTitleLbl = new baseClass.controls.baseLabel();
            this.startDateEd = new common.control.txtDateTime();
            this.startDateLbl = new baseClass.controls.baseLabel();
            this.commentPg = new System.Windows.Forms.TabPage();
            this.commentGrid = new common.control.baseDataGridView();
            this.onDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewBtn = new System.Windows.Forms.DataGridViewImageColumn();
            this.projCommentSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).BeginInit();
            this.toolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsSource)).BeginInit();
            this.projectTab.SuspendLayout();
            this.infoPg.SuspendLayout();
            this.commentPg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projCommentSource)).BeginInit();
            this.SuspendLayout();
            // 
            // myDataSet
            // 
            this.myDataSet.DataSetName = "myBaseDS";
            // 
            // toolBox
            // 
            this.toolBox.Location = new System.Drawing.Point(-25, -6);
            this.toolBox.Size = new System.Drawing.Size(541, 48);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(403, 7);
            this.exitBtn.Size = new System.Drawing.Size(80, 38);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(163, 7);
            this.saveBtn.Size = new System.Drawing.Size(80, 38);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(562, 9);
            this.deleteBtn.Visible = false;
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(243, 7);
            this.editBtn.Size = new System.Drawing.Size(80, 38);
            this.editBtn.Text = "&Xem";
            // 
            // addNewBtn
            // 
            this.addNewBtn.Location = new System.Drawing.Point(83, 7);
            this.addNewBtn.Size = new System.Drawing.Size(80, 38);
            // 
            // toExcelBtn
            // 
            this.toExcelBtn.Location = new System.Drawing.Point(570, 7);
            // 
            // findBtn
            // 
            this.findBtn.Location = new System.Drawing.Point(674, 9);
            this.findBtn.Visible = false;
            // 
            // reloadBtn
            // 
            this.reloadBtn.Location = new System.Drawing.Point(730, 9);
            this.reloadBtn.Visible = false;
            // 
            // printBtn
            // 
            this.printBtn.Location = new System.Drawing.Point(323, 7);
            this.printBtn.Size = new System.Drawing.Size(80, 38);
            // 
            // TitleLbl
            // 
            this.TitleLbl.Size = new System.Drawing.Size(525, 24);
            this.TitleLbl.TabIndex = 149;
            this.TitleLbl.Text = "THÔNG TIN DỰ ÁN";
            // 
            // projectsSource
            // 
            this.projectsSource.DataMember = "emProject";
            this.projectsSource.DataSource = this.myDataSet;
            // 
            // projectTab
            // 
            this.projectTab.Controls.Add(this.infoPg);
            this.projectTab.Controls.Add(this.commentPg);
            this.projectTab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectTab.Location = new System.Drawing.Point(-2, 42);
            this.projectTab.Name = "projectTab";
            this.projectTab.SelectedIndex = 0;
            this.projectTab.Size = new System.Drawing.Size(527, 453);
            this.projectTab.TabIndex = 237;
            // 
            // infoPg
            // 
            this.infoPg.Controls.Add(this.subjectCodeCb);
            this.infoPg.Controls.Add(this.descriptionEd);
            this.infoPg.Controls.Add(this.subjectCodeLbl);
            this.infoPg.Controls.Add(this.budgetUnitCb);
            this.infoPg.Controls.Add(this.budgetEd);
            this.infoPg.Controls.Add(this.endDateEd);
            this.infoPg.Controls.Add(this.endDateLbl);
            this.infoPg.Controls.Add(this.ownerLbl);
            this.infoPg.Controls.Add(this.projectTypeLbl);
            this.infoPg.Controls.Add(this.projectTypeCb);
            this.infoPg.Controls.Add(this.label6);
            this.infoPg.Controls.Add(this.ownerEd);
            this.infoPg.Controls.Add(this.descriptionLbl);
            this.infoPg.Controls.Add(this.codeLbl);
            this.infoPg.Controls.Add(this.budgetLbl);
            this.infoPg.Controls.Add(this.codeEd);
            this.infoPg.Controls.Add(this.projTitleEd);
            this.infoPg.Controls.Add(this.projTitleLbl);
            this.infoPg.Controls.Add(this.startDateEd);
            this.infoPg.Controls.Add(this.startDateLbl);
            this.infoPg.Location = new System.Drawing.Point(4, 25);
            this.infoPg.Name = "infoPg";
            this.infoPg.Padding = new System.Windows.Forms.Padding(3);
            this.infoPg.Size = new System.Drawing.Size(519, 424);
            this.infoPg.TabIndex = 0;
            this.infoPg.Text = "Thông tin";
            this.infoPg.UseVisualStyleBackColor = true;
            // 
            // subjectCodeCb
            // 
            this.subjectCodeCb.CheckOnClick = true;
            this.subjectCodeCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectCodeCb.FormattingEnabled = true;
            this.subjectCodeCb.Location = new System.Drawing.Point(25, 353);
            this.subjectCodeCb.MultiColumn = true;
            this.subjectCodeCb.myCheckedItems = ((System.Collections.ArrayList)(resources.GetObject("subjectCodeCb.myCheckedItems")));
            this.subjectCodeCb.myCheckedValues = ((System.Collections.Specialized.StringCollection)(resources.GetObject("subjectCodeCb.myCheckedValues")));
            this.subjectCodeCb.myItemString = "";
            this.subjectCodeCb.Name = "subjectCodeCb";
            this.subjectCodeCb.Size = new System.Drawing.Size(472, 61);
            this.subjectCodeCb.TabIndex = 40;
            // 
            // descriptionEd
            // 
            this.descriptionEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "description", true));
            this.descriptionEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionEd.Location = new System.Drawing.Point(24, 237);
            this.descriptionEd.Name = "descriptionEd";
            this.descriptionEd.Size = new System.Drawing.Size(474, 88);
            this.descriptionEd.TabIndex = 30;
            this.descriptionEd.Text = "";
            // 
            // subjectCodeLbl
            // 
            this.subjectCodeLbl.AutoSize = true;
            this.subjectCodeLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectCodeLbl.Location = new System.Drawing.Point(24, 334);
            this.subjectCodeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subjectCodeLbl.Name = "subjectCodeLbl";
            this.subjectCodeLbl.Size = new System.Drawing.Size(105, 16);
            this.subjectCodeLbl.TabIndex = 273;
            this.subjectCodeLbl.Text = "Thuộc lãnh vực";
            // 
            // budgetUnitCb
            // 
            this.budgetUnitCb.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.projectsSource, "budgetUnit", true));
            this.budgetUnitCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.budgetUnitCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budgetUnitCb.FormattingEnabled = true;
            this.budgetUnitCb.Location = new System.Drawing.Point(218, 185);
            this.budgetUnitCb.myValue = "";
            this.budgetUnitCb.Name = "budgetUnitCb";
            this.budgetUnitCb.Size = new System.Drawing.Size(82, 24);
            this.budgetUnitCb.TabIndex = 21;
            // 
            // budgetEd
            // 
            this.budgetEd.BeepOnError = true;
            this.budgetEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "budget", true));
            this.budgetEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budgetEd.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.budgetEd.Location = new System.Drawing.Point(24, 185);
            this.budgetEd.myFormat = "###,###,###,###,###";
            this.budgetEd.Name = "budgetEd";
            this.budgetEd.Size = new System.Drawing.Size(194, 24);
            this.budgetEd.TabIndex = 20;
            this.budgetEd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.budgetEd.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.budgetEd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // endDateEd
            // 
            this.endDateEd.BeepOnError = true;
            this.endDateEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "endDate", true));
            this.endDateEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDateEd.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.endDateEd.Location = new System.Drawing.Point(398, 185);
            this.endDateEd.Mask = "00/00/0000";
            this.endDateEd.myDateTime = new System.DateTime(((long)(0)));
            this.endDateEd.Name = "endDateEd";
            this.endDateEd.Size = new System.Drawing.Size(102, 24);
            this.endDateEd.TabIndex = 23;
            // 
            // endDateLbl
            // 
            this.endDateLbl.AutoSize = true;
            this.endDateLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDateLbl.Location = new System.Drawing.Point(399, 166);
            this.endDateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.endDateLbl.Name = "endDateLbl";
            this.endDateLbl.Size = new System.Drawing.Size(63, 16);
            this.endDateLbl.TabIndex = 271;
            this.endDateLbl.Text = "Kết thúc";
            // 
            // ownerLbl
            // 
            this.ownerLbl.AutoSize = true;
            this.ownerLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ownerLbl.Location = new System.Drawing.Point(24, 115);
            this.ownerLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ownerLbl.Name = "ownerLbl";
            this.ownerLbl.Size = new System.Drawing.Size(85, 16);
            this.ownerLbl.TabIndex = 269;
            this.ownerLbl.Text = "Chủ dự án *";
            // 
            // projectTypeLbl
            // 
            this.projectTypeLbl.AutoSize = true;
            this.projectTypeLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectTypeLbl.Location = new System.Drawing.Point(238, 11);
            this.projectTypeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.projectTypeLbl.Name = "projectTypeLbl";
            this.projectTypeLbl.Size = new System.Drawing.Size(78, 16);
            this.projectTypeLbl.TabIndex = 267;
            this.projectTypeLbl.Text = "Phân lọai *";
            // 
            // projectTypeCb
            // 
            this.projectTypeCb.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.projectsSource, "type", true));
            this.projectTypeCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectTypeCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectTypeCb.FormattingEnabled = true;
            this.projectTypeCb.Location = new System.Drawing.Point(237, 30);
            this.projectTypeCb.myValue = "";
            this.projectTypeCb.Name = "projectTypeCb";
            this.projectTypeCb.Size = new System.Drawing.Size(262, 26);
            this.projectTypeCb.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 216);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 261;
            this.label6.Text = "Mô tả";
            // 
            // ownerEd
            // 
            this.ownerEd.BackColor = System.Drawing.SystemColors.Window;
            this.ownerEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "owner", true));
            this.ownerEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ownerEd.Location = new System.Drawing.Point(24, 134);
            this.ownerEd.Margin = new System.Windows.Forms.Padding(4);
            this.ownerEd.Name = "ownerEd";
            this.ownerEd.Size = new System.Drawing.Size(475, 22);
            this.ownerEd.TabIndex = 11;
            // 
            // descriptionLbl
            // 
            this.descriptionLbl.AutoSize = true;
            this.descriptionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLbl.Location = new System.Drawing.Point(24, 218);
            this.descriptionLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descriptionLbl.Name = "descriptionLbl";
            this.descriptionLbl.Size = new System.Drawing.Size(41, 16);
            this.descriptionLbl.TabIndex = 262;
            this.descriptionLbl.Text = "Mô tả";
            // 
            // codeLbl
            // 
            this.codeLbl.AutoSize = true;
            this.codeLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeLbl.Location = new System.Drawing.Point(24, 11);
            this.codeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.codeLbl.Name = "codeLbl";
            this.codeLbl.Size = new System.Drawing.Size(58, 16);
            this.codeLbl.TabIndex = 259;
            this.codeLbl.Text = "Mã số *";
            // 
            // budgetLbl
            // 
            this.budgetLbl.AutoSize = true;
            this.budgetLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budgetLbl.Location = new System.Drawing.Point(24, 166);
            this.budgetLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.budgetLbl.Name = "budgetLbl";
            this.budgetLbl.Size = new System.Drawing.Size(99, 16);
            this.budgetLbl.TabIndex = 263;
            this.budgetLbl.Text = "Giá trị dự án *";
            // 
            // codeEd
            // 
            this.codeEd.BackColor = System.Drawing.SystemColors.Window;
            this.codeEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "projectCode", true));
            this.codeEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeEd.Location = new System.Drawing.Point(24, 30);
            this.codeEd.Margin = new System.Windows.Forms.Padding(4);
            this.codeEd.Name = "codeEd";
            this.codeEd.Size = new System.Drawing.Size(215, 24);
            this.codeEd.TabIndex = 1;
            // 
            // projTitleEd
            // 
            this.projTitleEd.BackColor = System.Drawing.SystemColors.Window;
            this.projTitleEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "title", true));
            this.projTitleEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projTitleEd.Location = new System.Drawing.Point(24, 83);
            this.projTitleEd.Margin = new System.Windows.Forms.Padding(4);
            this.projTitleEd.Name = "projTitleEd";
            this.projTitleEd.Size = new System.Drawing.Size(473, 22);
            this.projTitleEd.TabIndex = 10;
            // 
            // projTitleLbl
            // 
            this.projTitleLbl.AutoSize = true;
            this.projTitleLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projTitleLbl.Location = new System.Drawing.Point(22, 63);
            this.projTitleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.projTitleLbl.Name = "projTitleLbl";
            this.projTitleLbl.Size = new System.Drawing.Size(84, 16);
            this.projTitleLbl.TabIndex = 257;
            this.projTitleLbl.Text = "Tên dự án *";
            // 
            // startDateEd
            // 
            this.startDateEd.BeepOnError = true;
            this.startDateEd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsSource, "startDate", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
            this.startDateEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDateEd.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.startDateEd.Location = new System.Drawing.Point(298, 185);
            this.startDateEd.Mask = "00/00/0000";
            this.startDateEd.myDateTime = new System.DateTime(((long)(0)));
            this.startDateEd.Name = "startDateEd";
            this.startDateEd.Size = new System.Drawing.Size(102, 24);
            this.startDateEd.TabIndex = 22;
            // 
            // startDateLbl
            // 
            this.startDateLbl.AutoSize = true;
            this.startDateLbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDateLbl.Location = new System.Drawing.Point(297, 166);
            this.startDateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.startDateLbl.Name = "startDateLbl";
            this.startDateLbl.Size = new System.Drawing.Size(70, 16);
            this.startDateLbl.TabIndex = 266;
            this.startDateLbl.Text = "Bắt đầu *";
            // 
            // commentPg
            // 
            this.commentPg.Controls.Add(this.commentGrid);
            this.commentPg.Location = new System.Drawing.Point(4, 25);
            this.commentPg.Name = "commentPg";
            this.commentPg.Padding = new System.Windows.Forms.Padding(3);
            this.commentPg.Size = new System.Drawing.Size(519, 424);
            this.commentPg.TabIndex = 1;
            this.commentPg.Text = "Đánh giá";
            this.commentPg.UseVisualStyleBackColor = true;
            // 
            // commentGrid
            // 
            this.commentGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.commentGrid.AllowUserToAddRows = false;
            this.commentGrid.AutoGenerateColumns = false;
            this.commentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.onDateDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn,
            this.ratedDataGridViewTextBoxColumn,
            this.viewBtn});
            this.commentGrid.DataSource = this.projCommentSource;
            this.commentGrid.Location = new System.Drawing.Point(0, 3);
            this.commentGrid.Name = "commentGrid";
            this.commentGrid.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.commentGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.commentGrid.Size = new System.Drawing.Size(522, 423);
            this.commentGrid.TabIndex = 173;
            this.commentGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.commentGrid_DataError);
            this.commentGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.commentGrid_CellContentClick);
            // 
            // onDateDataGridViewTextBoxColumn
            // 
            this.onDateDataGridViewTextBoxColumn.DataPropertyName = "onDate";
            this.onDateDataGridViewTextBoxColumn.HeaderText = "Ngày";
            this.onDateDataGridViewTextBoxColumn.Name = "onDateDataGridViewTextBoxColumn";
            this.onDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.onDateDataGridViewTextBoxColumn.Width = 80;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Nội dung đánh giá";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            this.commentDataGridViewTextBoxColumn.Width = 310;
            // 
            // ratedDataGridViewTextBoxColumn
            // 
            this.ratedDataGridViewTextBoxColumn.DataPropertyName = "rated";
            this.ratedDataGridViewTextBoxColumn.HeaderText = "";
            this.ratedDataGridViewTextBoxColumn.Name = "ratedDataGridViewTextBoxColumn";
            this.ratedDataGridViewTextBoxColumn.ReadOnly = true;
            this.ratedDataGridViewTextBoxColumn.Width = 45;
            // 
            // viewBtn
            // 
            this.viewBtn.HeaderText = "";
            this.viewBtn.Image = global::baseClass.Properties.Resources.info;
            this.viewBtn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.viewBtn.Name = "viewBtn";
            this.viewBtn.ReadOnly = true;
            this.viewBtn.Width = 25;
            // 
            // projCommentSource
            // 
            this.projCommentSource.DataMember = "emProjComment";
            this.projCommentSource.DataSource = this.myDataSet;
            // 
            // projectEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(521, 523);
            this.Controls.Add(this.projectTab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "projectEdit";
            this.Tag = "";
            this.Text = "Du an";
            this.Controls.SetChildIndex(this.unLockBtn, 0);
            this.Controls.SetChildIndex(this.lockBtn, 0);
            this.Controls.SetChildIndex(this.toolBox, 0);
            this.Controls.SetChildIndex(this.TitleLbl, 0);
            this.Controls.SetChildIndex(this.projectTab, 0);
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).EndInit();
            this.toolBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectsSource)).EndInit();
            this.projectTab.ResumeLayout(false);
            this.infoPg.ResumeLayout(false);
            this.infoPg.PerformLayout();
            this.commentPg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projCommentSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected baseClass.controls.baseButton checkIdCardBtn;
        protected System.Windows.Forms.BindingSource projectsSource;
        private System.Windows.Forms.TabPage commentPg;
        private common.control.baseDataGridView commentGrid;
        protected System.Windows.Forms.BindingSource projCommentSource;
        protected System.Windows.Forms.TabControl projectTab;
        private System.Windows.Forms.TabPage infoPg;
        private System.Windows.Forms.RichTextBox descriptionEd;
        protected baseClass.controls.baseLabel subjectCodeLbl;
        private baseClass.controls.cbCurrency budgetUnitCb;
        private common.control.numberTextBox budgetEd;
        protected common.control.txtDateTime endDateEd;
        protected baseClass.controls.baseLabel endDateLbl;
        protected baseClass.controls.baseLabel ownerLbl;
        protected baseClass.controls.baseLabel projectTypeLbl;
        private baseClass.controls.cbProjectType projectTypeCb;
        protected baseClass.controls.baseLabel label6;
        protected common.control.baseTextBox ownerEd;
        protected baseClass.controls.baseLabel descriptionLbl;
        protected baseClass.controls.baseLabel codeLbl;
        protected baseClass.controls.baseLabel budgetLbl;
        protected common.control.baseTextBox codeEd;
        protected common.control.baseTextBox projTitleEd;
        protected baseClass.controls.baseLabel projTitleLbl;
        protected common.control.txtDateTime startDateEd;
        protected baseClass.controls.baseLabel startDateLbl;
        protected baseClass.controls.clbSubjectCode subjectCodeCb;
        private System.Windows.Forms.DataGridViewTextBoxColumn onDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn viewBtn;
    }
}