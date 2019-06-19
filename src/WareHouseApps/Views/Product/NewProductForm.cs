using AutoMapper;
using HHCoApps.Libs;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class NewProduct : BaseMethod
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IVendorServices _vendorServices;

        public NewProduct(IProductServices productServices, ICategoryServices categoryServices, IVendorServices vendorServices)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
            _vendorServices = vendorServices;
            InitializeComponent();
            CenterToParent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();

            txtName.Validating += TextBoxValidateNotEmpty;

            txtBaseCost.Validating += TextBoxValidateNotEmpty;
            txtBaseCost.KeyPress += NumericOnly;

            txtStock.Validating += TextBoxValidateNotEmpty;
            txtStock.KeyPress += NumericOnly;

            FormClosing += PreventClosingFormWithErrorProvider;
        }

        private void LoadData()
        {
            dtPickerIssuedDate.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
            dtPickerIssuedDate.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);

            dtPickerExpiredDate.MinDate = dtPickerIssuedDate.Value.AddDays(1);

            var categoryList = _categoryServices.GetCategories().ToList();
            cbxCategory.DataSource = categoryList;
            cbxCategory.DisplayMember = "Name";
            cbxCategory.ValueMember = "Id";
            cbxCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            var supplierList = _vendorServices.GetVendors().ToList();
            cbxSupplier.DataSource = supplierList;
            cbxSupplier.DisplayMember = "CompanyName";
            cbxSupplier.ValueMember = "Id";

            cbxStatus.DataSource = Enum.GetValues(typeof(ProductStatus)).Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value).ToList();

            cbxStatus.DisplayMember = "Description";
            cbxStatus.ValueMember = "value";
        }

        private void ResetMinDateExpiredDate(object sender, EventArgs e)
        {
            dtPickerExpiredDate.MinDate = dtPickerIssuedDate.Value.AddDays(1);
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            var viewModel = new ProductModel
            {
                BaseCost = Convert.ToDecimal(txtBaseCost.Text),
                InputCost = Convert.ToDecimal(txtBaseCost.Text),
                CategoryId = (int)cbxCategory.SelectedValue,
                SupplierId = (int)cbxSupplier.SelectedValue,
                Stock = Convert.ToInt32(txtStock.Text),
                Name = txtName.Text,
                IssuedDate = dtPickerIssuedDate.Value,
                ExpiredDate = dtPickerExpiredDate.Value,
                Status = cbxStatus.SelectedValue.ToString()
            };
            try
            {
                _productServices.InsertProduct(Mapper.Map<ProductModel>(viewModel));
                if (YesNoDialog("Thành Công!", "Bạn có muốn tiếp tục không ?") == DialogResult.Yes)
                {
                    ClearForm();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                ErrorMessage();
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtBaseCost.Clear();
            txtCode.Clear();
            txtStock.Clear();

            dtPickerIssuedDate.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
            dtPickerIssuedDate.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
            dtPickerIssuedDate.Value = DateTime.Now;

            dtPickerExpiredDate.MinDate = dtPickerIssuedDate.Value.AddDays(1);
            dtPickerExpiredDate.Value = dtPickerIssuedDate.Value.AddDays(2);
        }
    }
}
