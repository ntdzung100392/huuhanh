using AutoMapper;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;
using System.Windows.Forms;
using Ninject;
using WareHouseApps.Helper;
using WareHouseApps.Models;

namespace WareHouseApps
{
    public partial class NewVendor : BaseMethod
    {
        [Inject]
        private readonly IVendorServices _vendorServices;

        public NewVendor(IVendorServices vendorServices, IMapper mapper) : base(mapper)
        {
            InitializeComponent();
            CenterToParent();
            _vendorServices = vendorServices;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtCompany.Validating += TextBoxValidateNotEmpty;

            txtAddress.Validating += TextBoxValidateNotEmpty;

            txtTaxCode.Validating += TextBoxValidateNotEmpty;
            txtTaxCode.KeyPress += NumericOnly;

            txtPhone.Validating += TextBoxValidateNotEmpty;
            txtPhone.KeyPress += NumericOnly;

            txtFax.Validating += TextBoxValidateNotEmpty;
            txtFax.KeyPress += NumericOnly;

            FormClosing += PreventClosingFormWithErrorProvider;
        }

        private void InsertNewVendor(object sender, EventArgs e)
        {
            if (FormValid(out var errorMessage))
            {
                try
                {
                    var viewModel = new VendorViewModel
                    {
                        VendorName = txtCompany.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Fax = txtFax.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Note = txtInformation.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        TaxCode = txtTaxCode.Text.Trim()
                    };

                    _vendorServices.InsertVendor(_mapper.Map<VendorModel>(viewModel));
                    if (YesNoDialog("Thành Công!", "Bạn có muốn tiếp tục không ?") == DialogResult.Yes)
                    {
                        ClearForm();
                    }

                    Close();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    ErrorMessage();
                }
            }
            else
            {
                ErrorMessage(errorMessage);
            }
        }

        private bool FormValid(out string errorMessage)
        {
            errorMessage = string.Empty;
            return ValidEmailAddress(txtEmail.Text.Trim(), out errorMessage);
        }

        private void ClearForm()
        {
            txtAddress.Clear();
            txtCompany.Clear();
            txtFax.Clear();
            txtEmail.Clear();
            txtInformation.Clear();
            txtPhone.Clear();
            txtTaxCode.Clear();
        }
    }
}
