using System.Windows.Forms;

namespace WareHouseApps
{
    partial class ProductList
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
            this.grBoxProduct = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblExpiredDate = new System.Windows.Forms.Label();
            this.txtBaseCost = new System.Windows.Forms.TextBox();
            this.lblBaseCost = new System.Windows.Forms.Label();
            this.cbxIsActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grBoxSearch = new System.Windows.Forms.GroupBox();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.cbxCategorySearch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCategorySearch = new System.Windows.Forms.Label();
            this.cbxSupplierSearch = new System.Windows.Forms.ComboBox();
            this.lblSupplierSearch = new System.Windows.Forms.Label();
            this.txtProductNameSearch = new System.Windows.Forms.TextBox();
            this.lblProductSearch = new System.Windows.Forms.Label();
            this.dtPickerTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtPickerFrom = new System.Windows.Forms.DateTimePicker();
            this.lblIssuedDateSearch = new System.Windows.Forms.Label();
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
            this.dataGridProducts = new System.Windows.Forms.DataGridView();
            this.productCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtWarranty = new System.Windows.Forms.TextBox();
            this.cbxCurrency = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.grBoxProduct.SuspendLayout();
            this.grBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataSource = typeof(WareHouseApps.Models.ProductViewModel);
            // 
            // grBoxProduct
            // 
            this.grBoxProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grBoxProduct.Controls.Add(this.label1);
            this.grBoxProduct.Controls.Add(this.cbxCurrency);
            this.grBoxProduct.Controls.Add(this.txtWarranty);
            this.grBoxProduct.Controls.Add(this.btnUpdate);
            this.grBoxProduct.Controls.Add(this.lblExpiredDate);
            this.grBoxProduct.Controls.Add(this.txtBaseCost);
            this.grBoxProduct.Controls.Add(this.lblBaseCost);
            this.grBoxProduct.Controls.Add(this.cbxIsActive);
            this.grBoxProduct.Controls.Add(this.btnAdd);
            this.grBoxProduct.Controls.Add(this.grBoxSearch);
            this.grBoxProduct.Controls.Add(this.cbxStatus);
            this.grBoxProduct.Controls.Add(this.lblStatus);
            this.grBoxProduct.Controls.Add(this.cbxCategory);
            this.grBoxProduct.Controls.Add(this.lblCategory);
            this.grBoxProduct.Controls.Add(this.txtStock);
            this.grBoxProduct.Controls.Add(this.lblStock);
            this.grBoxProduct.Controls.Add(this.cbxSupplier);
            this.grBoxProduct.Controls.Add(this.lblSupplier);
            this.grBoxProduct.Controls.Add(this.dtPickerIssuedDate);
            this.grBoxProduct.Controls.Add(this.lblIssuedDate);
            this.grBoxProduct.Controls.Add(this.txtCode);
            this.grBoxProduct.Controls.Add(this.lblCode);
            this.grBoxProduct.Controls.Add(this.txtName);
            this.grBoxProduct.Controls.Add(this.lblName);
            this.grBoxProduct.Controls.Add(this.dataGridProducts);
            this.grBoxProduct.Location = new System.Drawing.Point(13, 13);
            this.grBoxProduct.Name = "grBoxProduct";
            this.grBoxProduct.Size = new System.Drawing.Size(834, 470);
            this.grBoxProduct.TabIndex = 0;
            this.grBoxProduct.TabStop = false;
            this.grBoxProduct.Text = "Danh Sách";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(347, 148);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(121, 23);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.UpdateProduct);
            // 
            // lblExpiredDate
            // 
            this.lblExpiredDate.AutoSize = true;
            this.lblExpiredDate.Location = new System.Drawing.Point(263, 99);
            this.lblExpiredDate.Name = "lblExpiredDate";
            this.lblExpiredDate.Size = new System.Drawing.Size(78, 13);
            this.lblExpiredDate.TabIndex = 20;
            this.lblExpiredDate.Text = "Hạn Bảo Hành";
            // 
            // txtBaseCost
            // 
            this.txtBaseCost.Enabled = false;
            this.txtBaseCost.Location = new System.Drawing.Point(347, 70);
            this.txtBaseCost.MaxLength = 10;
            this.txtBaseCost.Name = "txtBaseCost";
            this.txtBaseCost.Size = new System.Drawing.Size(121, 20);
            this.txtBaseCost.TabIndex = 19;
            // 
            // lblBaseCost
            // 
            this.lblBaseCost.AutoSize = true;
            this.lblBaseCost.Location = new System.Drawing.Point(289, 73);
            this.lblBaseCost.Name = "lblBaseCost";
            this.lblBaseCost.Size = new System.Drawing.Size(52, 13);
            this.lblBaseCost.TabIndex = 18;
            this.lblBaseCost.Text = "Giá Nhập";
            // 
            // cbxIsActive
            // 
            this.cbxIsActive.AutoSize = true;
            this.cbxIsActive.Enabled = false;
            this.cbxIsActive.Location = new System.Drawing.Point(250, 126);
            this.cbxIsActive.Name = "cbxIsActive";
            this.cbxIsActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbxIsActive.Size = new System.Drawing.Size(111, 17);
            this.cbxIsActive.TabIndex = 17;
            this.cbxIsActive.Text = "Đang Kinh Doanh";
            this.cbxIsActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(220, 148);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Tạo Mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.LoadAddProduct);
            // 
            // grBoxSearch
            // 
            this.grBoxSearch.Controls.Add(this.btnResetFilter);
            this.grBoxSearch.Controls.Add(this.cbxCategorySearch);
            this.grBoxSearch.Controls.Add(this.btnSearch);
            this.grBoxSearch.Controls.Add(this.lblCategorySearch);
            this.grBoxSearch.Controls.Add(this.cbxSupplierSearch);
            this.grBoxSearch.Controls.Add(this.lblSupplierSearch);
            this.grBoxSearch.Controls.Add(this.txtProductNameSearch);
            this.grBoxSearch.Controls.Add(this.lblProductSearch);
            this.grBoxSearch.Controls.Add(this.dtPickerTo);
            this.grBoxSearch.Controls.Add(this.lblTo);
            this.grBoxSearch.Controls.Add(this.dtPickerFrom);
            this.grBoxSearch.Controls.Add(this.lblIssuedDateSearch);
            this.grBoxSearch.Location = new System.Drawing.Point(514, 17);
            this.grBoxSearch.Name = "grBoxSearch";
            this.grBoxSearch.Size = new System.Drawing.Size(314, 158);
            this.grBoxSearch.TabIndex = 16;
            this.grBoxSearch.TabStop = false;
            this.grBoxSearch.Text = "Tìm Kiếm";
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(222, 121);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(92, 23);
            this.btnResetFilter.TabIndex = 17;
            this.btnResetFilter.Text = "Mặc Định";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.ResetFilter);
            // 
            // cbxCategorySearch
            // 
            this.cbxCategorySearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategorySearch.FormattingEnabled = true;
            this.cbxCategorySearch.Location = new System.Drawing.Point(6, 123);
            this.cbxCategorySearch.Name = "cbxCategorySearch";
            this.cbxCategorySearch.Size = new System.Drawing.Size(129, 21);
            this.cbxCategorySearch.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(141, 121);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.SearchProduct);
            // 
            // lblCategorySearch
            // 
            this.lblCategorySearch.AutoSize = true;
            this.lblCategorySearch.Location = new System.Drawing.Point(9, 107);
            this.lblCategorySearch.Name = "lblCategorySearch";
            this.lblCategorySearch.Size = new System.Drawing.Size(57, 13);
            this.lblCategorySearch.TabIndex = 8;
            this.lblCategorySearch.Text = "Danh Mục";
            // 
            // cbxSupplierSearch
            // 
            this.cbxSupplierSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupplierSearch.FormattingEnabled = true;
            this.cbxSupplierSearch.Location = new System.Drawing.Point(141, 79);
            this.cbxSupplierSearch.Name = "cbxSupplierSearch";
            this.cbxSupplierSearch.Size = new System.Drawing.Size(167, 21);
            this.cbxSupplierSearch.TabIndex = 7;
            // 
            // lblSupplierSearch
            // 
            this.lblSupplierSearch.AutoSize = true;
            this.lblSupplierSearch.Location = new System.Drawing.Point(138, 64);
            this.lblSupplierSearch.Name = "lblSupplierSearch";
            this.lblSupplierSearch.Size = new System.Drawing.Size(77, 13);
            this.lblSupplierSearch.TabIndex = 6;
            this.lblSupplierSearch.Text = "Nhà Cung Cấp";
            // 
            // txtProductNameSearch
            // 
            this.txtProductNameSearch.Location = new System.Drawing.Point(6, 80);
            this.txtProductNameSearch.MaxLength = 100;
            this.txtProductNameSearch.Name = "txtProductNameSearch";
            this.txtProductNameSearch.Size = new System.Drawing.Size(129, 20);
            this.txtProductNameSearch.TabIndex = 5;
            // 
            // lblProductSearch
            // 
            this.lblProductSearch.AutoSize = true;
            this.lblProductSearch.Location = new System.Drawing.Point(6, 64);
            this.lblProductSearch.Name = "lblProductSearch";
            this.lblProductSearch.Size = new System.Drawing.Size(78, 13);
            this.lblProductSearch.TabIndex = 4;
            this.lblProductSearch.Text = "Tên Hàng Hóa";
            // 
            // dtPickerTo
            // 
            this.dtPickerTo.CustomFormat = "dd/MM/yyyy";
            this.dtPickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerTo.Location = new System.Drawing.Point(141, 36);
            this.dtPickerTo.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtPickerTo.Name = "dtPickerTo";
            this.dtPickerTo.Size = new System.Drawing.Size(97, 20);
            this.dtPickerTo.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(109, 42);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "đến";
            // 
            // dtPickerFrom
            // 
            this.dtPickerFrom.CustomFormat = "dd/MM/yyyy";
            this.dtPickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerFrom.Location = new System.Drawing.Point(6, 36);
            this.dtPickerFrom.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtPickerFrom.Name = "dtPickerFrom";
            this.dtPickerFrom.Size = new System.Drawing.Size(97, 20);
            this.dtPickerFrom.TabIndex = 1;
            // 
            // lblIssuedDateSearch
            // 
            this.lblIssuedDateSearch.AutoSize = true;
            this.lblIssuedDateSearch.Location = new System.Drawing.Point(6, 20);
            this.lblIssuedDateSearch.Name = "lblIssuedDateSearch";
            this.lblIssuedDateSearch.Size = new System.Drawing.Size(61, 13);
            this.lblIssuedDateSearch.TabIndex = 0;
            this.lblIssuedDateSearch.Text = "Ngày Nhập";
            // 
            // cbxStatus
            // 
            this.cbxStatus.Enabled = false;
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.Location = new System.Drawing.Point(91, 124);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(139, 21);
            this.cbxStatus.TabIndex = 15;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(26, 127);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Tình Trạng";
            // 
            // cbxCategory
            // 
            this.cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategory.Enabled = false;
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(347, 43);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(121, 21);
            this.cbxCategory.TabIndex = 13;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(284, 47);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(57, 13);
            this.lblCategory.TabIndex = 12;
            this.lblCategory.Text = "Danh Mục";
            // 
            // txtStock
            // 
            this.txtStock.Enabled = false;
            this.txtStock.Location = new System.Drawing.Point(347, 17);
            this.txtStock.MaxLength = 4;
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(121, 20);
            this.txtStock.TabIndex = 11;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(288, 20);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(53, 13);
            this.lblStock.TabIndex = 10;
            this.lblStock.Text = "Số Lượng";
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupplier.Enabled = false;
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(91, 97);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(139, 21);
            this.cbxSupplier.TabIndex = 9;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(8, 100);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(77, 13);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Nhà Cung Cấp";
            // 
            // dtPickerIssuedDate
            // 
            this.dtPickerIssuedDate.CustomFormat = "dd/MM/yyyy";
            this.dtPickerIssuedDate.Enabled = false;
            this.dtPickerIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerIssuedDate.Location = new System.Drawing.Point(91, 70);
            this.dtPickerIssuedDate.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.dtPickerIssuedDate.Name = "dtPickerIssuedDate";
            this.dtPickerIssuedDate.Size = new System.Drawing.Size(139, 20);
            this.dtPickerIssuedDate.TabIndex = 7;
            // 
            // lblIssuedDate
            // 
            this.lblIssuedDate.AutoSize = true;
            this.lblIssuedDate.Location = new System.Drawing.Point(24, 76);
            this.lblIssuedDate.Name = "lblIssuedDate";
            this.lblIssuedDate.Size = new System.Drawing.Size(61, 13);
            this.lblIssuedDate.TabIndex = 5;
            this.lblIssuedDate.Text = "Ngày Nhập";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(91, 44);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(139, 20);
            this.txtCode.TabIndex = 4;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(11, 47);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(74, 13);
            this.lblCode.TabIndex = 3;
            this.lblCode.Text = "Mã Hàng Hóa";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(91, 17);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(139, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(7, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Tên Hàng Hóa";
            // 
            // dataGridProducts
            // 
            this.dataGridProducts.AllowUserToAddRows = false;
            this.dataGridProducts.AllowUserToDeleteRows = false;
            this.dataGridProducts.AllowUserToResizeColumns = false;
            this.dataGridProducts.AllowUserToResizeRows = false;
            this.dataGridProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridProducts.AutoGenerateColumns = false;
            this.dataGridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productCodeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.Category,
            this.Supplier,
            this.statusDisplayDataGridViewTextBoxColumn,
            this.issuedDateDataGridViewTextBoxColumn,
            this.stockDataGridViewTextBoxColumn});
            this.dataGridProducts.DataSource = this.mainBindingSource;
            this.dataGridProducts.Location = new System.Drawing.Point(7, 181);
            this.dataGridProducts.MultiSelect = false;
            this.dataGridProducts.Name = "dataGridProducts";
            this.dataGridProducts.ReadOnly = true;
            this.dataGridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProducts.Size = new System.Drawing.Size(821, 283);
            this.dataGridProducts.TabIndex = 0;
            this.dataGridProducts.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GetProductDetails);
            // 
            // productCodeDataGridViewTextBoxColumn
            // 
            this.productCodeDataGridViewTextBoxColumn.DataPropertyName = "ProductCode";
            this.productCodeDataGridViewTextBoxColumn.HeaderText = "Mã Hàng";
            this.productCodeDataGridViewTextBoxColumn.Name = "productCodeDataGridViewTextBoxColumn";
            this.productCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên Hàng Hóa";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 200;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CategoryName";
            this.Category.HeaderText = "Danh Mục Hàng";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Width = 200;
            // 
            // Supplier
            // 
            this.Supplier.DataPropertyName = "VendorName";
            this.Supplier.HeaderText = "Nhà Cung Cấp";
            this.Supplier.Name = "Supplier";
            this.Supplier.ReadOnly = true;
            this.Supplier.Width = 200;
            // 
            // statusDisplayDataGridViewTextBoxColumn
            // 
            this.statusDisplayDataGridViewTextBoxColumn.DataPropertyName = "StatusDisplay";
            this.statusDisplayDataGridViewTextBoxColumn.HeaderText = "Tình Trạng";
            this.statusDisplayDataGridViewTextBoxColumn.Name = "statusDisplayDataGridViewTextBoxColumn";
            this.statusDisplayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // issuedDateDataGridViewTextBoxColumn
            // 
            this.issuedDateDataGridViewTextBoxColumn.DataPropertyName = "IssuedDate";
            this.issuedDateDataGridViewTextBoxColumn.HeaderText = "Ngày Nhập";
            this.issuedDateDataGridViewTextBoxColumn.Name = "issuedDateDataGridViewTextBoxColumn";
            this.issuedDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockDataGridViewTextBoxColumn
            // 
            this.stockDataGridViewTextBoxColumn.DataPropertyName = "Stock";
            this.stockDataGridViewTextBoxColumn.HeaderText = "Số Lượng Tồn";
            this.stockDataGridViewTextBoxColumn.Name = "stockDataGridViewTextBoxColumn";
            this.stockDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // txtWarranty
            // 
            this.txtWarranty.Enabled = false;
            this.txtWarranty.Location = new System.Drawing.Point(347, 96);
            this.txtWarranty.MaxLength = 1;
            this.txtWarranty.Name = "txtWarranty";
            this.txtWarranty.Size = new System.Drawing.Size(121, 20);
            this.txtWarranty.TabIndex = 23;
            // 
            // cbxCurrency
            // 
            this.cbxCurrency.FormattingEnabled = true;
            this.cbxCurrency.Location = new System.Drawing.Point(91, 149);
            this.cbxCurrency.Name = "cbxCurrency";
            this.cbxCurrency.Size = new System.Drawing.Size(121, 21);
            this.cbxCurrency.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Tiền Tệ";
            // 
            // ProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 495);
            this.Controls.Add(this.grBoxProduct);
            this.MaximizeBox = false;
            this.Name = "ProductList";
            this.Text = "Danh Sách Hàng Hóa";
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.grBoxProduct.ResumeLayout(false);
            this.grBoxProduct.PerformLayout();
            this.grBoxSearch.ResumeLayout(false);
            this.grBoxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grBoxProduct;
        private System.Windows.Forms.DataGridView dataGridProducts;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblIssuedDate;
        private System.Windows.Forms.DateTimePicker dtPickerIssuedDate;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.ComboBox cbxSupplier;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.ComboBox cbxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grBoxSearch;
        private System.Windows.Forms.DateTimePicker dtPickerFrom;
        private System.Windows.Forms.Label lblIssuedDateSearch;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtPickerTo;
        private System.Windows.Forms.Label lblProductSearch;
        private System.Windows.Forms.TextBox txtProductNameSearch;
        private System.Windows.Forms.Label lblSupplierSearch;
        private System.Windows.Forms.Label lblCategorySearch;
        private System.Windows.Forms.ComboBox cbxSupplierSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basePriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiredDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox cbxCategorySearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn productCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDisplayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.CheckBox cbxIsActive;
        private System.Windows.Forms.TextBox txtBaseCost;
        private System.Windows.Forms.Label lblBaseCost;
        private System.Windows.Forms.Label lblExpiredDate;
        private System.Windows.Forms.Button btnUpdate;
        private TextBox txtWarranty;
        private Label label1;
        private ComboBox cbxCurrency;
    }
}