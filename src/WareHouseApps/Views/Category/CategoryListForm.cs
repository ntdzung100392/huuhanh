using AutoMapper;
using HHCoApps.Libs;
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
    public partial class CategoryList : BaseMethod
    {
        private readonly ICategoryServices _categoryServices;
        private IList<CategoryViewModel> _categoryList;
        private CategoryViewModel _selectedCategory;
        public CategoryList(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
            InitializeComponent();
            CenterToParent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private void LoadData()
        {
            var result = _categoryServices.GetCategories();
            if (result.Any())
            {
                _categoryList = result.Select(Mapper.Map<CategoryViewModel>).OrderBy(c => c.Code).ToList();
            }
            categoryViewModelBindingSource.DataSource = _categoryList;
        }

        private void ClearForm()
        {
            txtCode.Clear();
            txtName.Clear();
        }

        private void InsertNewCategory(object sender, EventArgs e)
        {
            if (YesNoDialog() != DialogResult.Yes)
                return;

            var model = new CategoryModel
            {
                Code = txtCode.Text.Trim(),
                Name = txtName.Text.Trim()
            };

            try
            {
                _categoryServices.InsertNewCategory(model);
                LoadData();
                SuccessMessage();
                ClearForm();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                ErrorMessage();
            }
        }

        private void UpdateCategoryContent(object sender, EventArgs e)
        {
            if (YesNoDialog() != DialogResult.Yes)
                return;

            var model = _categoryServices.GetCategoryById(_selectedCategory.Id);
            if (model != null)
            {
                model.Name = txtName.Text.Trim();
                model.Code = txtCode.Text.Trim();

                try
                {
                    _categoryServices.UpdateCategory(model);
                    LoadData();
                    SuccessMessage();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    ErrorMessage();
                }
            }
            else
            {
                ErrorMessage();
            }
        }

        private void GeneratedCategoryCode(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
                return;

            var categoryName = string.Empty;
            var splitNameResult = txtName.Text.Trim().Split();

            var length = splitNameResult.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                categoryName += splitNameResult[i][0];
            }
            txtCode.Text = StringHelper.RemoveDiacritics(categoryName);
        }

        private void GetCategoryDetails(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            _selectedCategory = _categoryList[e.RowIndex];
            txtName.Text = _selectedCategory.Name;
            txtCode.Text = _selectedCategory.Code;
        }
    }
}
