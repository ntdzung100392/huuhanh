using HHCoApps.Services.Interfaces;
using System;
using System.Windows.Forms;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class MainMenu : BaseMethod
    {
        private readonly IVendorServices _vendorServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;
        public MainMenu(IVendorServices vendorServices, ICategoryServices categoryServices, IProductServices productServices)
        {
            InitializeComponent();
            CenterToScreen();
            _vendorServices = vendorServices;
            _categoryServices = categoryServices;
            _productServices = productServices;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (YesNoDialog("Xác Nhận!", "Bạn có muốn thoát ?") == DialogResult.Yes)
            {
                Close();
            }
        }

        private void LoadReportForm(object sender, EventArgs e)
        {
            var reportForm = new Report();
            reportForm.ShowDialog();
        }

        private void LoadCategoryForm(object sender, EventArgs e)
        {
            var categoryForm = new CategoryList(_categoryServices);
            categoryForm.ShowDialog();
        }

        private void LoadProductForm(object sender, EventArgs e)
        {
            var productForm = new ProductList(_categoryServices, _vendorServices, _productServices);
            productForm.ShowDialog();
        }

        private void LoadImportExportForm(object sender, EventArgs e)
        {
            var noteForm = new NoteList();
            noteForm.ShowDialog();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            var supplierForm = new VendorListForm(_vendorServices);
            supplierForm.ShowDialog();
        }

        private void ImportProduct(object sender, EventArgs e)
        {
            var importForm = new ImportProduct();
            importForm.ShowDialog();
        }

        private void ExportProduct(object sender, EventArgs e)
        {
            var exportForm = new ExportProduct();
            exportForm.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var timer = new Timer
            {
                Interval = 100,
                Enabled = true
            };
            timer.Tick += TimerSetDate;
            timer.Start();
            lblVersion.Text = "Version: 1.0.0";
        }

        private void TimerSetDate(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy");
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var addProductForm = new NewProduct(_productServices, _categoryServices, _vendorServices);
            addProductForm.ShowDialog();
        }
    }
}
