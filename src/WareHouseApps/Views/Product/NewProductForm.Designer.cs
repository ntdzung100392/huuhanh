using System.Windows.Forms;

namespace WareHouseApps
{
    partial class NewProduct
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
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.txtBaseCost = new System.Windows.Forms.TextBox();
            this.lblBaseCost = new System.Windows.Forms.Label();
            this.lblExpiredDate = new System.Windows.Forms.Label();
            this.dtPickerExpiredDate = new System.Windows.Forms.DateTimePicker();
            this.cbxStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.dtPickerIssuedDate = new System.Windows.Forms.DateTimePicker();
            this.lblIssuedDate = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.groupBoxInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInformation.Controls.Add(this.btnAddProduct);
            this.groupBoxInformation.Controls.Add(this.txtBaseCost);
            this.groupBoxInformation.Controls.Add(this.lblBaseCost);
            this.groupBoxInformation.Controls.Add(this.lblExpiredDate);
            this.groupBoxInformation.Controls.Add(this.dtPickerExpiredDate);
            this.groupBoxInformation.Controls.Add(this.cbxStatus);
            this.groupBoxInformation.Controls.Add(this.lblStatus);
            this.groupBoxInformation.Controls.Add(this.cbxCategory);
            this.groupBoxInformation.Controls.Add(this.lblCategory);
            this.groupBoxInformation.Controls.Add(this.txtStock);
            this.groupBoxInformation.Controls.Add(this.lblStock);
            this.groupBoxInformation.Controls.Add(this.cbxSupplier);
            this.groupBoxInformation.Controls.Add(this.lblSupplier);
            this.groupBoxInformation.Controls.Add(this.dtPickerIssuedDate);
            this.groupBoxInformation.Controls.Add(this.lblIssuedDate);
            this.groupBoxInformation.Controls.Add(this.txtCode);
            this.groupBoxInformation.Controls.Add(this.lblCode);
            this.groupBoxInformation.Controls.Add(this.txtName);
            this.groupBoxInformation.Controls.Add(this.lblName);
            this.groupBoxInformation.Location = new System.Drawing.Point(13, 13);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(515, 169);
            this.groupBoxInformation.TabIndex = 0;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Thông Tin";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(357, 129);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(152, 23);
            this.btnAddProduct.TabIndex = 34;
            this.btnAddProduct.Text = "Tạo";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.AddNewProduct);
            // 
            // txtBaseCost
            // 
            this.txtBaseCost.Location = new System.Drawing.Point(101, 45);
            this.txtBaseCost.MaxLength = 10;
            this.txtBaseCost.Name = "txtBaseCost";
            this.txtBaseCost.Size = new System.Drawing.Size(139, 20);
            this.txtBaseCost.TabIndex = 33;
            // 
            // lblBaseCost
            // 
            this.lblBaseCost.AutoSize = true;
            this.lblBaseCost.Location = new System.Drawing.Point(42, 48);
            this.lblBaseCost.Name = "lblBaseCost";
            this.lblBaseCost.Size = new System.Drawing.Size(52, 13);
            this.lblBaseCost.TabIndex = 32;
            this.lblBaseCost.Text = "Giá Nhập";
            // 
            // lblExpiredDate
            // 
            this.lblExpiredDate.AutoSize = true;
            this.lblExpiredDate.Location = new System.Drawing.Point(277, 73);
            this.lblExpiredDate.Name = "lblExpiredDate";
            this.lblExpiredDate.Size = new System.Drawing.Size(75, 13);
            this.lblExpiredDate.TabIndex = 31;
            this.lblExpiredDate.Text = "Ngày Hết Hạn";
            // 
            // dtPickerExpiredDate
            // 
            this.dtPickerExpiredDate.CustomFormat = "dd/MM/yyyy";
            this.dtPickerExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerExpiredDate.Location = new System.Drawing.Point(357, 73);
            this.dtPickerExpiredDate.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtPickerExpiredDate.Name = "dtPickerExpiredDate";
            this.dtPickerExpiredDate.Size = new System.Drawing.Size(152, 20);
            this.dtPickerExpiredDate.TabIndex = 30;
            // 
            // cbxStatus
            // 
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.Location = new System.Drawing.Point(357, 99);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(152, 21);
            this.cbxStatus.TabIndex = 29;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(294, 102);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 13);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "Tình Trạng";
            // 
            // cbxCategory
            // 
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(357, 45);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(152, 21);
            this.cbxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxCategory.TabIndex = 27;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(294, 49);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(57, 13);
            this.lblCategory.TabIndex = 26;
            this.lblCategory.Text = "Danh Mục";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(101, 126);
            this.txtStock.MaxLength = 4;
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(139, 20);
            this.txtStock.TabIndex = 25;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(42, 129);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(53, 13);
            this.lblStock.TabIndex = 24;
            this.lblStock.Text = "Số Lượng";
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(101, 99);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(139, 21);
            this.cbxSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxSupplier.TabIndex = 23;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(18, 102);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(77, 13);
            this.lblSupplier.TabIndex = 22;
            this.lblSupplier.Text = "Nhà Cung Cấp";
            // 
            // dtPickerIssuedDate
            // 
            this.dtPickerIssuedDate.CustomFormat = "dd/MM/yyyy";
            this.dtPickerIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerIssuedDate.Location = new System.Drawing.Point(101, 72);
            this.dtPickerIssuedDate.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtPickerIssuedDate.Name = "dtPickerIssuedDate";
            this.dtPickerIssuedDate.Size = new System.Drawing.Size(139, 20);
            this.dtPickerIssuedDate.TabIndex = 21;
            this.dtPickerIssuedDate.ValueChanged += new System.EventHandler(this.ResetMinDateExpiredDate);
            // 
            // lblIssuedDate
            // 
            this.lblIssuedDate.AutoSize = true;
            this.lblIssuedDate.Location = new System.Drawing.Point(34, 73);
            this.lblIssuedDate.Name = "lblIssuedDate";
            this.lblIssuedDate.Size = new System.Drawing.Size(61, 13);
            this.lblIssuedDate.TabIndex = 20;
            this.lblIssuedDate.Text = "Ngày Nhập";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(357, 19);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(152, 20);
            this.txtCode.TabIndex = 19;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(277, 22);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(74, 13);
            this.lblCode.TabIndex = 18;
            this.lblCode.Text = "Mã Hàng Hóa";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 19);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(139, 20);
            this.txtName.TabIndex = 17;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 13);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "Tên Hàng Hóa";
            // 
            // NewProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 194);
            this.Controls.Add(this.groupBoxInformation);
            this.MaximizeBox = false;
            this.Name = "NewProduct";
            this.Text = "Thêm Hàng Hóa";
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.ComboBox cbxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.ComboBox cbxSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.DateTimePicker dtPickerIssuedDate;
        private System.Windows.Forms.Label lblIssuedDate;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblExpiredDate;
        private System.Windows.Forms.DateTimePicker dtPickerExpiredDate;
        private System.Windows.Forms.TextBox txtBaseCost;
        private System.Windows.Forms.Label lblBaseCost;
        private System.Windows.Forms.Button btnAddProduct;
    }
}