namespace WareHouseApps
{
    partial class MainMenu
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
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBoxExit = new System.Windows.Forms.GroupBox();
            this.groupBoxReport = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.btnCategory = new System.Windows.Forms.Button();
            this.groupBoxProduct = new System.Windows.Forms.GroupBox();
            this.btnProduct = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.groupBoxNote = new System.Windows.Forms.GroupBox();
            this.btnNote = new System.Windows.Forms.Button();
            this.groupBoxTool = new System.Windows.Forms.GroupBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.groupBoxExit.SuspendLayout();
            this.groupBoxReport.SuspendLayout();
            this.groupBoxCategory.SuspendLayout();
            this.groupBoxProduct.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxNote.SuspendLayout();
            this.groupBoxTool.SuspendLayout();
            this.groupBoxInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::WareHouseApps.Properties.Resources.application_exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(20, 19);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(107, 93);
            this.btnExit.TabIndex = 0;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBoxExit
            // 
            this.groupBoxExit.Controls.Add(this.btnExit);
            this.groupBoxExit.Location = new System.Drawing.Point(503, 155);
            this.groupBoxExit.Name = "groupBoxExit";
            this.groupBoxExit.Size = new System.Drawing.Size(146, 125);
            this.groupBoxExit.TabIndex = 1;
            this.groupBoxExit.TabStop = false;
            this.groupBoxExit.Text = "Thoát";
            // 
            // groupBoxReport
            // 
            this.groupBoxReport.Controls.Add(this.btnReport);
            this.groupBoxReport.Location = new System.Drawing.Point(503, 13);
            this.groupBoxReport.Name = "groupBoxReport";
            this.groupBoxReport.Size = new System.Drawing.Size(146, 125);
            this.groupBoxReport.TabIndex = 2;
            this.groupBoxReport.TabStop = false;
            this.groupBoxReport.Text = "Báo Cáo";
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImage = global::WareHouseApps.Properties.Resources.Report_Icon;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.Location = new System.Drawing.Point(20, 19);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(107, 93);
            this.btnReport.TabIndex = 0;
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.LoadReportForm);
            // 
            // groupBoxCategory
            // 
            this.groupBoxCategory.Controls.Add(this.btnCategory);
            this.groupBoxCategory.Location = new System.Drawing.Point(351, 155);
            this.groupBoxCategory.Name = "groupBoxCategory";
            this.groupBoxCategory.Size = new System.Drawing.Size(146, 125);
            this.groupBoxCategory.TabIndex = 3;
            this.groupBoxCategory.TabStop = false;
            this.groupBoxCategory.Text = "Danh Mục Hàng Hóa";
            // 
            // btnCategory
            // 
            this.btnCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnCategory.BackgroundImage = global::WareHouseApps.Properties.Resources._1384943818;
            this.btnCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCategory.Location = new System.Drawing.Point(20, 19);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(107, 93);
            this.btnCategory.TabIndex = 0;
            this.btnCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCategory.UseVisualStyleBackColor = false;
            this.btnCategory.Click += new System.EventHandler(this.LoadCategoryForm);
            // 
            // groupBoxProduct
            // 
            this.groupBoxProduct.Controls.Add(this.btnProduct);
            this.groupBoxProduct.Location = new System.Drawing.Point(199, 13);
            this.groupBoxProduct.Name = "groupBoxProduct";
            this.groupBoxProduct.Size = new System.Drawing.Size(146, 125);
            this.groupBoxProduct.TabIndex = 4;
            this.groupBoxProduct.TabStop = false;
            this.groupBoxProduct.Text = "Hàng Hóa";
            // 
            // btnProduct
            // 
            this.btnProduct.BackColor = System.Drawing.Color.Transparent;
            this.btnProduct.BackgroundImage = global::WareHouseApps.Properties.Resources.free_shipping;
            this.btnProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProduct.Location = new System.Drawing.Point(20, 19);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(107, 93);
            this.btnProduct.TabIndex = 0;
            this.btnProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.LoadProductForm);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSupplier);
            this.groupBox1.Location = new System.Drawing.Point(351, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 125);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhà Cung Cấp";
            // 
            // btnSupplier
            // 
            this.btnSupplier.BackColor = System.Drawing.Color.Transparent;
            this.btnSupplier.BackgroundImage = global::WareHouseApps.Properties.Resources.outsourcing_outside_foreign_supplier_people_340a2c5b01012deb_192x192;
            this.btnSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSupplier.Location = new System.Drawing.Point(20, 19);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(107, 93);
            this.btnSupplier.TabIndex = 0;
            this.btnSupplier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSupplier.UseVisualStyleBackColor = false;
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // groupBoxNote
            // 
            this.groupBoxNote.Controls.Add(this.btnNote);
            this.groupBoxNote.Location = new System.Drawing.Point(199, 155);
            this.groupBoxNote.Name = "groupBoxNote";
            this.groupBoxNote.Size = new System.Drawing.Size(146, 125);
            this.groupBoxNote.TabIndex = 6;
            this.groupBoxNote.TabStop = false;
            this.groupBoxNote.Text = "Xuất/Nhập Kho";
            // 
            // btnNote
            // 
            this.btnNote.BackColor = System.Drawing.Color.Transparent;
            this.btnNote.BackgroundImage = global::WareHouseApps.Properties.Resources.kho_hang;
            this.btnNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNote.Location = new System.Drawing.Point(20, 19);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(107, 93);
            this.btnNote.TabIndex = 0;
            this.btnNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNote.UseVisualStyleBackColor = false;
            this.btnNote.Click += new System.EventHandler(this.LoadImportExportForm);
            // 
            // groupBoxTool
            // 
            this.groupBoxTool.Controls.Add(this.btnAddProduct);
            this.groupBoxTool.Controls.Add(this.btnAddCategory);
            this.groupBoxTool.Controls.Add(this.btnAddSupplier);
            this.groupBoxTool.Controls.Add(this.btnExport);
            this.groupBoxTool.Controls.Add(this.btnImport);
            this.groupBoxTool.Location = new System.Drawing.Point(12, 155);
            this.groupBoxTool.Name = "groupBoxTool";
            this.groupBoxTool.Size = new System.Drawing.Size(181, 137);
            this.groupBoxTool.TabIndex = 7;
            this.groupBoxTool.TabStop = false;
            this.groupBoxTool.Text = "Công Cụ";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(7, 107);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(168, 23);
            this.btnAddProduct.TabIndex = 5;
            this.btnAddProduct.Text = "Thêm Hàng Hóa";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(6, 78);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(169, 23);
            this.btnAddCategory.TabIndex = 4;
            this.btnAddCategory.Text = "Thêm Danh Mục Hàng";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Location = new System.Drawing.Point(7, 49);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(168, 23);
            this.btnAddSupplier.TabIndex = 2;
            this.btnAddSupplier.Text = "Thêm Nhà Cung Cấp";
            this.btnAddSupplier.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(93, 20);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(82, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Xuất Kho";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportProduct);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(7, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Nhập Kho";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.ImportProduct);
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Controls.Add(this.lblDateTime);
            this.groupBoxInformation.Location = new System.Drawing.Point(12, 13);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(181, 125);
            this.groupBoxInformation.TabIndex = 8;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Thông Tin";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(7, 20);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(51, 13);
            this.lblDateTime.TabIndex = 0;
            this.lblDateTime.Text = "Ngày Giờ";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(577, 283);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 13);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Version: 1.0.0";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 299);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.groupBoxInformation);
            this.Controls.Add(this.groupBoxTool);
            this.Controls.Add(this.groupBoxNote);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxProduct);
            this.Controls.Add(this.groupBoxCategory);
            this.Controls.Add(this.groupBoxReport);
            this.Controls.Add(this.groupBoxExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "Khung Điều Khiển";
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.groupBoxExit.ResumeLayout(false);
            this.groupBoxReport.ResumeLayout(false);
            this.groupBoxCategory.ResumeLayout(false);
            this.groupBoxProduct.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxNote.ResumeLayout(false);
            this.groupBoxTool.ResumeLayout(false);
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBoxExit;
        private System.Windows.Forms.GroupBox groupBoxReport;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.GroupBox groupBoxProduct;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSupplier;
        private System.Windows.Forms.GroupBox groupBoxNote;
        private System.Windows.Forms.Button btnNote;
        private System.Windows.Forms.GroupBox groupBoxTool;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Label lblVersion;
    }
}