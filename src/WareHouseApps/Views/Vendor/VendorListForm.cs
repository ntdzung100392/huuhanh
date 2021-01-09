using AutoMapper;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WareHouseApps.Helper;
using WareHouseApps.Models;

namespace WareHouseApps
{
    public partial class VendorListForm : BaseMethod
    {
        private readonly IVendorServices _vendorServices;
        private IList<VendorViewModel> _vendorList;
        private IList<VendorViewModel> _filterList;
        private VendorViewModel _currentVendor;

        public VendorListForm(IVendorServices vendorServices, IMapper mapper) : base(mapper)
        {
            _vendorServices = vendorServices;
            InitializeComponent();
            CenterToParent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GetVendors();
        }

        private void UpdateVendor(object sender, EventArgs e)
        {
            if (YesNoDialog() != DialogResult.Yes)
                return;

            _currentVendor.Address = txtAddress.Text;
            _currentVendor.VendorName = txtCompany.Text;
            _currentVendor.Email = txtEmail.Text;
            _currentVendor.Fax = txtFax.Text;
            _currentVendor.Note = txtInformation.Text;
            _currentVendor.TaxCode = txtTaxCode.Text;
            _currentVendor.Phone = txtPhone.Text;
            _currentVendor.IsActive = cbxIsActive.Checked;

            try
            {
                _vendorServices.UpdateVendor(_mapper.Map<VendorModel>(_currentVendor));
                GetVendors();
                SuccessMessage();
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }
        }

        private void DeleteSupplier(object sender, EventArgs e)
        {
            if (YesNoDialog() != DialogResult.Yes)
                return;

            try
            {
                _vendorServices.DeleteVendorWithProducts(_mapper.Map<VendorModel>(_currentVendor));
                _currentVendor = null;
                SuccessMessage();
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }
            finally
            {
                GetVendors();
                ClearForm();
            }
        }

        private void LoadAddForm(object sender, EventArgs e)
        {
            var addSupplierForm = new NewVendor(_vendorServices, _mapper);
            addSupplierForm.FormClosed += NewVendorFormClosed;
            addSupplierForm.ShowDialog();
        }

        private void NewVendorFormClosed(object sender, FormClosedEventArgs e)
        {
            GetVendors();
        }

        private void GetVendors()
        {
            try
            {
                var result = _vendorServices.GetVendors();
                _vendorList = _mapper.Map<IEnumerable<VendorViewModel>>(result).ToList();

                vendorViewModelBindingSource.DataSource = _vendorList;
            }
            catch (Exception ex)
            {
                ErrorMessage();
            }
        }

        private void GetVendorDetails(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            _currentVendor = _vendorList[e.RowIndex];
            txtAddress.Text = _currentVendor.Address;
            txtCompany.Text = _currentVendor.VendorName;
            txtEmail.Text = _currentVendor.Email;
            txtFax.Text = _currentVendor.Fax;
            txtInformation.Text = _currentVendor.Note;
            txtTaxCode.Text = _currentVendor.TaxCode;
            txtPhone.Text = _currentVendor.Phone;
            cbxIsActive.Checked = _currentVendor.IsActive;

            txtAddress.Enabled = true;
            txtCompany.Enabled = true;
            txtEmail.Enabled = true;
            txtFax.Enabled = true;
            txtInformation.Enabled = true;
            txtTaxCode.Enabled = true;
            txtPhone.Enabled = true;
            cbxIsActive.Enabled = true;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void SearchSupplier(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                var filter = txtSearch.Text.Trim();
                _filterList = _vendorList.Where(s => s.VendorName.Contains(filter)).ToList();
                vendorViewModelBindingSource.DataSource = _filterList;
            }
            else
            {
                vendorViewModelBindingSource.DataSource = _vendorList;
            }
        }

        private void ClearForm()
        {
            txtAddress.Enabled = false;
            txtCompany.Enabled = false;
            txtEmail.Enabled = false;
            txtFax.Enabled = false;
            txtInformation.Enabled = false;
            txtTaxCode.Enabled = false;
            txtPhone.Enabled = false;
            cbxIsActive.Enabled = false;

            txtAddress.Clear();
            txtCompany.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtInformation.Clear();
            txtTaxCode.Clear();
            txtPhone.Clear();
            cbxIsActive.Checked = false;
        }
    }
}
