namespace WareHouseApps
{
    partial class CategoryList
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
            this.grBoxInformation = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.grBoxDataGrid = new System.Windows.Forms.GroupBox();
            this.dataGridCategory = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grBoxInformation.SuspendLayout();
            this.grBoxDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grBoxInformation
            // 
            this.grBoxInformation.Controls.Add(this.btnUpdate);
            this.grBoxInformation.Controls.Add(this.btnAdd);
            this.grBoxInformation.Controls.Add(this.txtName);
            this.grBoxInformation.Controls.Add(this.lblName);
            this.grBoxInformation.Controls.Add(this.txtCode);
            this.grBoxInformation.Controls.Add(this.lblCode);
            this.grBoxInformation.Location = new System.Drawing.Point(13, 13);
            this.grBoxInformation.Name = "grBoxInformation";
            this.grBoxInformation.Size = new System.Drawing.Size(259, 100);
            this.grBoxInformation.TabIndex = 0;
            this.grBoxInformation.TabStop = false;
            this.grBoxInformation.Text = "Thông Tin";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(139, 71);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.UpdateCategoryContent);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(58, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.InsertNewCategory);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(114, 45);
            this.txtName.MaxLength = 36;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.GeneratedCategoryCode);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(23, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(79, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Tên Danh Mục";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(114, 19);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(27, 22);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(75, 13);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Mã Danh Mục";
            // 
            // grBoxDataGrid
            // 
            this.grBoxDataGrid.Controls.Add(this.dataGridCategory);
            this.grBoxDataGrid.Location = new System.Drawing.Point(13, 120);
            this.grBoxDataGrid.Name = "grBoxDataGrid";
            this.grBoxDataGrid.Size = new System.Drawing.Size(259, 129);
            this.grBoxDataGrid.TabIndex = 1;
            this.grBoxDataGrid.TabStop = false;
            this.grBoxDataGrid.Text = "Danh Sách";
            // 
            // dataGridCategory
            // 
            this.dataGridCategory.AllowUserToAddRows = false;
            this.dataGridCategory.AllowUserToDeleteRows = false;
            this.dataGridCategory.AllowUserToResizeRows = false;
            this.dataGridCategory.AutoGenerateColumns = false;
            this.dataGridCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridCategory.DataSource = this.categoryViewModelBindingSource;
            this.dataGridCategory.Location = new System.Drawing.Point(7, 20);
            this.dataGridCategory.Name = "dataGridCategory";
            this.dataGridCategory.ReadOnly = true;
            this.dataGridCategory.RowHeadersVisible = false;
            this.dataGridCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridCategory.ShowEditingIcon = false;
            this.dataGridCategory.Size = new System.Drawing.Size(240, 103);
            this.dataGridCategory.TabIndex = 0;
            this.dataGridCategory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GetCategoryDetails);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã";
            this.codeDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 180;
            // 
            // categoryViewModelBindingSource
            // 
            this.categoryViewModelBindingSource.DataSource = typeof(WareHouseApps.Models.CategoryViewModel);
            // 
            // CategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.grBoxDataGrid);
            this.Controls.Add(this.grBoxInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CategoryList";
            this.Text = "CategoryList";
            this.grBoxInformation.ResumeLayout(false);
            this.grBoxInformation.PerformLayout();
            this.grBoxDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grBoxInformation;
        private System.Windows.Forms.GroupBox grBoxDataGrid;
        private System.Windows.Forms.BindingSource categoryViewModelBindingSource;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dataGridCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    }
}