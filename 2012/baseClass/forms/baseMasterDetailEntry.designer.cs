namespace baseClass
{
    partial class baseMasterDetailEntry
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
            this.myDataSet = new data.baseDS();
            this.toolBarGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myMasterNavigatorSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // myDataSet
            // 
            this.myDataSet.DataSetName = "myDataSet";
            this.myDataSet.EnforceConstraints = false;
            this.myDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // baseMasterDetailEntry
            // 
            this.AddNewMenuEnable = false;
            this.ClientSize = new System.Drawing.Size(744, 555);
            this.DeleteMenuEnable = false;
            this.Name = "baseMasterDetailEntry";
            this.SaveMenuEnable = false;
            this.toolBarGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myMasterNavigatorSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected data.baseDS myDataSet;
       }
}