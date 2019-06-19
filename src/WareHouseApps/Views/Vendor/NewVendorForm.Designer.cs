namespace WareHouseApps
{
    partial class NewVendor
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
            this.lblInformation = new System.Windows.Forms.Label();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblTaxCode = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.lblFax = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.groupBoxInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Location = new System.Drawing.Point(37, 151);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(108, 13);
            this.lblInformation.TabIndex = 50;
            this.lblInformation.Text = "Thông Tin Loại Hàng";
            // 
            // txtInformation
            // 
            this.txtInformation.Location = new System.Drawing.Point(40, 167);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.Size = new System.Drawing.Size(407, 59);
            this.txtInformation.TabIndex = 9;
            // 
            // txtTaxCode
            // 
            this.txtTaxCode.Location = new System.Drawing.Point(40, 120);
            this.txtTaxCode.MaxLength = 16;
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Size = new System.Drawing.Size(183, 20);
            this.txtTaxCode.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(261, 104);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(81, 13);
            this.lblAddress.TabIndex = 44;
            this.lblAddress.Text = "Địa Chỉ Liên Hệ";
            // 
            // lblTaxCode
            // 
            this.lblTaxCode.AutoSize = true;
            this.lblTaxCode.Location = new System.Drawing.Point(37, 104);
            this.lblTaxCode.Name = "lblTaxCode";
            this.lblTaxCode.Size = new System.Drawing.Size(66, 13);
            this.lblTaxCode.TabIndex = 42;
            this.lblTaxCode.Text = "Mã Số Thuế";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(261, 26);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 41;
            this.lblEmail.Text = "Email";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(37, 65);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 13);
            this.lblPhone.TabIndex = 40;
            this.lblPhone.Text = "Số Điện Thoại";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(37, 26);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(99, 13);
            this.lblCompanyName.TabIndex = 38;
            this.lblCompanyName.Text = "Tên Nhà Cung Cấp";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(264, 120);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(183, 20);
            this.txtAddress.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(264, 42);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(183, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(40, 81);
            this.txtPhone.MaxLength = 14;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(183, 20);
            this.txtPhone.TabIndex = 3;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(40, 42);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(183, 20);
            this.txtCompany.TabIndex = 1;
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInformation.Controls.Add(this.txtFax);
            this.groupBoxInformation.Controls.Add(this.lblFax);
            this.groupBoxInformation.Controls.Add(this.btnAdd);
            this.groupBoxInformation.Controls.Add(this.txtCompany);
            this.groupBoxInformation.Controls.Add(this.lblInformation);
            this.groupBoxInformation.Controls.Add(this.txtInformation);
            this.groupBoxInformation.Controls.Add(this.txtPhone);
            this.groupBoxInformation.Controls.Add(this.txtEmail);
            this.groupBoxInformation.Controls.Add(this.txtAddress);
            this.groupBoxInformation.Controls.Add(this.lblCompanyName);
            this.groupBoxInformation.Controls.Add(this.txtTaxCode);
            this.groupBoxInformation.Controls.Add(this.lblAddress);
            this.groupBoxInformation.Controls.Add(this.lblPhone);
            this.groupBoxInformation.Controls.Add(this.lblEmail);
            this.groupBoxInformation.Controls.Add(this.lblTaxCode);
            this.groupBoxInformation.Location = new System.Drawing.Point(13, 13);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(484, 264);
            this.groupBoxInformation.TabIndex = 51;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Thông Tin";
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(264, 81);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(183, 20);
            this.txtFax.TabIndex = 4;
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.Location = new System.Drawing.Point(261, 65);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(24, 13);
            this.lblFax.TabIndex = 53;
            this.lblFax.Text = "Fax";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(264, 232);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.InsertNewVendor);
            // 
            // NewVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 289);
            this.Controls.Add(this.groupBoxInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewVendor";
            this.Text = "Thêm Nhà Cung Cấp";
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region Properties
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.TextBox txtInformation;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblTaxCode;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label lblFax;
        #endregion
    }
}