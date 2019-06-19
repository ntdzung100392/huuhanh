namespace WareHouseApps
{
    partial class VendorListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendorListForm));
            this.GroupBoxSupplierList = new System.Windows.Forms.GroupBox();
            this.dataGridVendors = new System.Windows.Forms.DataGridView();
            this.vendorViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supplierViewModelBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.lblFax = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblInformation = new System.Windows.Forms.Label();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblTaxCode = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.cbxIsActive = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.VendorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.GroupBoxSupplierList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendorViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierViewModelBindingNavigator)).BeginInit();
            this.supplierViewModelBindingNavigator.SuspendLayout();
            this.groupBoxInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataSource = typeof(WareHouseApps.Models.VendorViewModel);
            // 
            // GroupBoxSupplierList
            // 
            this.GroupBoxSupplierList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxSupplierList.Controls.Add(this.dataGridVendors);
            this.GroupBoxSupplierList.Location = new System.Drawing.Point(0, 212);
            this.GroupBoxSupplierList.Name = "GroupBoxSupplierList";
            this.GroupBoxSupplierList.Size = new System.Drawing.Size(661, 250);
            this.GroupBoxSupplierList.TabIndex = 0;
            this.GroupBoxSupplierList.TabStop = false;
            this.GroupBoxSupplierList.Text = "Danh Sách";
            // 
            // dataGridVendors
            // 
            this.dataGridVendors.AllowUserToAddRows = false;
            this.dataGridVendors.AllowUserToDeleteRows = false;
            this.dataGridVendors.AllowUserToResizeRows = false;
            this.dataGridVendors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVendors.AutoGenerateColumns = false;
            this.dataGridVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVendors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VendorName,
            this.Phone,
            this.Address,
            this.Email,
            this.IsActive});
            this.dataGridVendors.DataSource = this.vendorViewModelBindingSource;
            this.dataGridVendors.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridVendors.Location = new System.Drawing.Point(0, 19);
            this.dataGridVendors.MultiSelect = false;
            this.dataGridVendors.Name = "dataGridVendors";
            this.dataGridVendors.ReadOnly = true;
            this.dataGridVendors.RowHeadersVisible = false;
            this.dataGridVendors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            this.dataGridVendors.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVendors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridVendors.ShowEditingIcon = false;
            this.dataGridVendors.Size = new System.Drawing.Size(661, 202);
            this.dataGridVendors.TabIndex = 1;
            this.dataGridVendors.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GetVendorDetails);
            // 
            // vendorViewModelBindingSource
            // 
            this.vendorViewModelBindingSource.DataSource = typeof(WareHouseApps.Models.VendorViewModel);
            // 
            // supplierViewModelBindingNavigator
            // 
            this.supplierViewModelBindingNavigator.AddNewItem = null;
            this.supplierViewModelBindingNavigator.BindingSource = this.vendorViewModelBindingSource;
            this.supplierViewModelBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.supplierViewModelBindingNavigator.DeleteItem = null;
            this.supplierViewModelBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.supplierViewModelBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel1,
            this.txtSearch});
            this.supplierViewModelBindingNavigator.Location = new System.Drawing.Point(0, 430);
            this.supplierViewModelBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.supplierViewModelBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.supplierViewModelBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.supplierViewModelBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.supplierViewModelBindingNavigator.Name = "supplierViewModelBindingNavigator";
            this.supplierViewModelBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.supplierViewModelBindingNavigator.Size = new System.Drawing.Size(661, 25);
            this.supplierViewModelBindingNavigator.TabIndex = 2;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
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
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel1.Text = "Tìm Kiếm (*)";
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 25);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchSupplier);
            this.txtSearch.TextChanged += new System.EventHandler(this.SearchSupplier);
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Controls.Add(this.lblFax);
            this.groupBoxInformation.Controls.Add(this.txtFax);
            this.groupBoxInformation.Controls.Add(this.btnAdd);
            this.groupBoxInformation.Controls.Add(this.lblInformation);
            this.groupBoxInformation.Controls.Add(this.txtInformation);
            this.groupBoxInformation.Controls.Add(this.txtTaxCode);
            this.groupBoxInformation.Controls.Add(this.lblAddress);
            this.groupBoxInformation.Controls.Add(this.lblTaxCode);
            this.groupBoxInformation.Controls.Add(this.lblEmail);
            this.groupBoxInformation.Controls.Add(this.lblPhone);
            this.groupBoxInformation.Controls.Add(this.lblCompanyName);
            this.groupBoxInformation.Controls.Add(this.cbxIsActive);
            this.groupBoxInformation.Controls.Add(this.btnDelete);
            this.groupBoxInformation.Controls.Add(this.btnUpdate);
            this.groupBoxInformation.Controls.Add(this.txtAddress);
            this.groupBoxInformation.Controls.Add(this.txtEmail);
            this.groupBoxInformation.Controls.Add(this.txtPhone);
            this.groupBoxInformation.Controls.Add(this.txtCompany);
            this.groupBoxInformation.Location = new System.Drawing.Point(13, 13);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(636, 193);
            this.groupBoxInformation.TabIndex = 1;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Thông Tin";
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.Location = new System.Drawing.Point(76, 89);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(24, 13);
            this.lblFax.TabIndex = 36;
            this.lblFax.Text = "Fax";
            // 
            // txtFax
            // 
            this.txtFax.Enabled = false;
            this.txtFax.Location = new System.Drawing.Point(117, 86);
            this.txtFax.MaxLength = 16;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(183, 20);
            this.txtFax.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(376, 161);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 23);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.LoadAddForm);
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Location = new System.Drawing.Point(306, 89);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(108, 13);
            this.lblInformation.TabIndex = 32;
            this.lblInformation.Text = "Thông Tin Loại Hàng";
            // 
            // txtInformation
            // 
            this.txtInformation.Enabled = false;
            this.txtInformation.Location = new System.Drawing.Point(420, 86);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.Size = new System.Drawing.Size(204, 68);
            this.txtInformation.TabIndex = 8;
            // 
            // txtTaxCode
            // 
            this.txtTaxCode.Enabled = false;
            this.txtTaxCode.Location = new System.Drawing.Point(420, 34);
            this.txtTaxCode.MaxLength = 16;
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Size = new System.Drawing.Size(138, 20);
            this.txtTaxCode.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(333, 63);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(81, 13);
            this.lblAddress.TabIndex = 24;
            this.lblAddress.Text = "Địa Chỉ Liên Hệ";
            // 
            // lblTaxCode
            // 
            this.lblTaxCode.AutoSize = true;
            this.lblTaxCode.Location = new System.Drawing.Point(348, 37);
            this.lblTaxCode.Name = "lblTaxCode";
            this.lblTaxCode.Size = new System.Drawing.Size(66, 13);
            this.lblTaxCode.TabIndex = 22;
            this.lblTaxCode.Text = "Mã Số Thuế";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(76, 115);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 19;
            this.lblEmail.Text = "Email";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(36, 63);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 13);
            this.lblPhone.TabIndex = 18;
            this.lblPhone.Text = "Số Điện Thoại";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(12, 37);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(99, 13);
            this.lblCompanyName.TabIndex = 16;
            this.lblCompanyName.Text = "Tên Nhà Cung Cấp";
            // 
            // cbxIsActive
            // 
            this.cbxIsActive.AutoSize = true;
            this.cbxIsActive.Enabled = false;
            this.cbxIsActive.Location = new System.Drawing.Point(120, 138);
            this.cbxIsActive.Name = "cbxIsActive";
            this.cbxIsActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbxIsActive.Size = new System.Drawing.Size(180, 17);
            this.cbxIsActive.TabIndex = 10;
            this.cbxIsActive.Text = "Nhà Cung Cấp Đang Hoạt Động";
            this.cbxIsActive.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(552, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteSupplier);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(464, 160);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(82, 23);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.UpdateVendor);
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(420, 60);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(204, 20);
            this.txtAddress.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(117, 112);
            this.txtEmail.MaxLength = 20;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(183, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.Enabled = false;
            this.txtPhone.Location = new System.Drawing.Point(117, 60);
            this.txtPhone.MaxLength = 16;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(183, 20);
            this.txtPhone.TabIndex = 2;
            // 
            // txtCompany
            // 
            this.txtCompany.Enabled = false;
            this.txtCompany.Location = new System.Drawing.Point(117, 34);
            this.txtCompany.MaxLength = 100;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(183, 20);
            this.txtCompany.TabIndex = 0;
            // 
            // VendorName
            // 
            this.VendorName.DataPropertyName = "VendorName";
            this.VendorName.HeaderText = "Nhà Cung Cấp";
            this.VendorName.Name = "VendorName";
            this.VendorName.ReadOnly = true;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "Điện Thoại";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Địa Chỉ";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Đang Hoạt Động";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            // 
            // VendorListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 455);
            this.Controls.Add(this.supplierViewModelBindingNavigator);
            this.Controls.Add(this.groupBoxInformation);
            this.Controls.Add(this.GroupBoxSupplierList);
            this.Name = "VendorListForm";
            this.Text = "Danh Sách Nhà Cung Cấp";
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.GroupBoxSupplierList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendorViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierViewModelBindingNavigator)).EndInit();
            this.supplierViewModelBindingNavigator.ResumeLayout(false);
            this.supplierViewModelBindingNavigator.PerformLayout();
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Properties
        private System.Windows.Forms.GroupBox GroupBoxSupplierList;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblTaxCode;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.CheckBox cbxIsActive;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.TextBox txtInformation;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.DataGridView dataGridVendors;
        private System.Windows.Forms.BindingSource vendorViewModelBindingSource;

        private System.Windows.Forms.BindingNavigator supplierViewModelBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        #endregion
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn directorNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn homeTownDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
    }
}