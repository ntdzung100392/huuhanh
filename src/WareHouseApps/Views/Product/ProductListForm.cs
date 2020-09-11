using HHCoApps.Libs;
using HHCoApps.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AutoMapper;
using HHCoApps.Services.Models;
using WareHouseApps.Helper;
using WareHouseApps.Models;

namespace WareHouseApps
{
    public partial class ProductList : BaseMethod
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IVendorServices _vendorServices;
        private readonly IProductServices _productServices;
        private IList<ProductViewModel> _productList;
        private ProductViewModel _currentProduct;

        public ProductList(ICategoryServices categoryServices, IVendorServices vendorServices, IProductServices productServices, IMapper mapper) : base(mapper)
        {
            _categoryServices = categoryServices;
            _vendorServices = vendorServices;
            _productServices = productServices;
            InitializeComponent();
            CenterToParent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
            LoadProducts();

            txtName.Validating += TextBoxValidateNotEmpty;

            txtBaseCost.Validating += TextBoxValidateNotEmpty;
            txtBaseCost.KeyPress += NumericOnly;

            txtStock.Validating += TextBoxValidateNotEmpty;
            txtStock.KeyPress += NumericOnly;

            txtWarranty.Validating += TextBoxValidateNotEmpty;
            txtWarranty.KeyPress += NumericOnly;
        }

        private void LoadData()
        {
            try
            {
                dtPickerIssuedDate.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);

                dtPickerFrom.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                dtPickerFrom.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);

                dtPickerTo.MinDate = DateTime.Now.AddDays(1);

                var categoryList = _categoryServices.GetCategories();

                cbxCategory.DataSource = categoryList;
                cbxCategory.DisplayMember = "Name";
                cbxCategory.ValueMember = "UniqueId";

                var filterCategoryList = categoryList.ToList();
                filterCategoryList.Add(new CategoryModel
                {
                    Name = "Tất Cả",
                    UniqueId = Guid.Empty
                });
                cbxCategorySearch.DataSource = filterCategoryList;
                cbxCategorySearch.DisplayMember = "Name";
                cbxCategorySearch.ValueMember = "UniqueId";

                var vendorList = _vendorServices.GetVendors();

                cbxSupplier.DataSource = vendorList;
                cbxSupplier.DisplayMember = "Name";
                cbxSupplier.ValueMember = "UniqueId";

                var filterVendorList = vendorList.ToList();
                filterVendorList.Add(new VendorModel
                {
                    Name = "Tất Cả",
                    UniqueId = Guid.Empty
                });
                cbxSupplierSearch.DataSource = filterVendorList;
                cbxSupplierSearch.DisplayMember = "Name";
                cbxSupplierSearch.ValueMember = "UniqueId";

                cbxStatus.DataSource =
                    Enum.GetValues(typeof(ProductStatus)).Cast<Enum>().Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                        typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                        value
                    })
                .OrderBy(item => item.value)
                .ToList();

                cbxStatus.DisplayMember = "Description";
                cbxStatus.ValueMember = "value";
            }
            catch (Exception ex)
            {
                ErrorMessage();
                Close();
            }
        }

        private void LoadProducts()
        {
            try
            {
                var result = _productServices.GetProductsOrderByIssuedDate();
                if (result.Any())
                {
                    _productList = result.Select(_mapper.Map<ProductViewModel>).ToList();
                }

                dataGridProducts.DataSource = _productList;
            }
            catch (Exception ex)
            {
                ErrorMessage();
                Close();
            }
        }

        private void SearchProduct(object sender, EventArgs e)
        {

        }

        private void LoadAddProduct(object sender, EventArgs e)
        {
            var addProductForm = new NewProduct(_productServices, _categoryServices, _vendorServices, _mapper);
            addProductForm.FormClosed += AddProductFormClosed;
            addProductForm.ShowDialog();
        }

        private void AddProductFormClosed(object sender, FormClosedEventArgs e)
        {
            LoadProducts();
        }

        private void GetProductDetails(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            _currentProduct = _productList[e.RowIndex];
            txtBaseCost.Text = _currentProduct.CostDisplay;
            txtCode.Text = _currentProduct.ProductCode;
            txtName.Text = _currentProduct.Name;
            txtStock.Text = _currentProduct.Stock.ToString();
            txtWarranty.Text = _currentProduct.Warranty.ToString();

            cbxCategory.Text = _currentProduct.CategoryName;
            cbxSupplier.Text = _currentProduct.VendorName;
            cbxIsActive.Checked = _currentProduct.IsActive;
            cbxStatus.Text = _currentProduct.StatusDisplay;

            dtPickerIssuedDate.Value = _currentProduct.IssuedDate;

            txtBaseCost.Enabled = true;
            txtCode.Enabled = true;
            txtName.Enabled = true;
            txtStock.Enabled = true;
            txtWarranty.Enabled = true;

            cbxCategory.Enabled = true;
            cbxSupplier.Enabled = true;
            cbxIsActive.Enabled = true;
            cbxStatus.Enabled = true;

            dtPickerIssuedDate.Enabled = true;
        }

        private void UpdateProduct(object sender, EventArgs e)
        {
            if (YesNoDialog("Thông Báo!", "Bạn có muốn tiếp tục không ?") != DialogResult.Yes)
                return;

            try
            {
                _currentProduct.InputCost = Convert.ToDecimal(txtBaseCost.Text);
                _currentProduct.CategoryUniqueId = Guid.Parse(cbxCategory.SelectedValue.ToString());
                _currentProduct.VendorUniqueId = Guid.Parse(cbxSupplier.SelectedValue.ToString());
                _currentProduct.Stock = Convert.ToInt32(txtStock.Text);
                _currentProduct.Name = txtName.Text.Trim();
                _currentProduct.IssuedDate = dtPickerIssuedDate.Value;
                _currentProduct.Warranty = Convert.ToInt32(txtWarranty.Text);
                _currentProduct.Status = cbxStatus.SelectedValue.ToString();
                _currentProduct.IsActive = cbxIsActive.Checked;

                _productServices.UpdateProductByUniqueId(_mapper.Map<ProductModel>(_currentProduct));
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }
        }

        private void ResetFilter(object sender, EventArgs e)
        {
            txtProductNameSearch.Clear();
            LoadData();
            LoadProducts();
        }
    }
}
