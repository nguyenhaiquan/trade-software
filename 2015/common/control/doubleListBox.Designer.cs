namespace common.controls
{
    partial class doubleListBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.availableCatCb = new System.Windows.Forms.ComboBox();
            this.availableItemLb = new System.Windows.Forms.ListBox();
            this.removeBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.selectedItemLb = new System.Windows.Forms.ListBox();
            this.selectedLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // availableCatCb
            // 
            this.availableCatCb.BackColor = System.Drawing.SystemColors.Window;
            this.availableCatCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.availableCatCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableCatCb.FormattingEnabled = true;
            this.availableCatCb.Location = new System.Drawing.Point(1, 0);
            this.availableCatCb.Margin = new System.Windows.Forms.Padding(4);
            this.availableCatCb.Name = "availableCatCb";
            this.availableCatCb.Size = new System.Drawing.Size(386, 24);
            this.availableCatCb.TabIndex = 1;
            this.availableCatCb.SelectedIndexChanged += new System.EventHandler(this.availableCatCb_SelectedIndexChanged);
            // 
            // availableItemLb
            // 
            this.availableItemLb.Location = new System.Drawing.Point(1, 25);
            this.availableItemLb.Name = "availableItemLb";
            this.availableItemLb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.availableItemLb.Size = new System.Drawing.Size(382, 147);
            this.availableItemLb.TabIndex = 2;
            this.availableItemLb.DoubleClick += new System.EventHandler(this.availableItemLb_DoubleClick);
            // 
            // removeBtn
            // 
            this.removeBtn.Image = global::common.Properties.Resources.up;
            this.removeBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.removeBtn.Location = new System.Drawing.Point(355, 157);
            this.removeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(28, 29);
            this.removeBtn.TabIndex = 14;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Image = global::common.Properties.Resources.down;
            this.addBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addBtn.Location = new System.Drawing.Point(318, 157);
            this.addBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(28, 29);
            this.addBtn.TabIndex = 13;
            this.addBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // selectedItemLb
            // 
            this.selectedItemLb.Location = new System.Drawing.Point(0, 186);
            this.selectedItemLb.Name = "selectedItemLb";
            this.selectedItemLb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedItemLb.Size = new System.Drawing.Size(384, 186);
            this.selectedItemLb.TabIndex = 3;
            this.selectedItemLb.DoubleClick += new System.EventHandler(this.selectedItemLb_DoubleClick);
            // 
            // selectedLbl
            // 
            this.selectedLbl.AutoSize = true;
            this.selectedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedLbl.Location = new System.Drawing.Point(3, 167);
            this.selectedLbl.Name = "selectedLbl";
            this.selectedLbl.Size = new System.Drawing.Size(140, 16);
            this.selectedLbl.TabIndex = 15;
            this.selectedLbl.Text = "Danh sách đã chọn";
            // 
            // doubleListBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.selectedLbl);
            this.Controls.Add(this.availableCatCb);
            this.Controls.Add(this.selectedItemLb);
            this.Controls.Add(this.availableItemLb);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Name = "doubleListBox";
            this.Size = new System.Drawing.Size(386, 378);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox availableCatCb;
        private System.Windows.Forms.ListBox availableItemLb;
        private System.Windows.Forms.Button removeBtn;
        internal System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.ListBox selectedItemLb;
        private System.Windows.Forms.Label selectedLbl;
    }
}
