using AutoMapper;
using HHCoApps.Repository;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.Core;

namespace HHCoApps.Services.Implementation
{
    internal class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories.Any() ? Mapper.Map<IEnumerable<CategoryModel>>(categories) : Enumerable.Empty<CategoryModel>();
        }

        public CategoryModel GetCategoryById(int categoryId)
        {
            return Mapper.Map<CategoryModel>(_categoryRepository.GetCategoryById(categoryId));
        }

        public void InsertNewCategory(CategoryModel model)
        {
            var entity = Mapper.Map<Category>(model);
            _categoryRepository.InsertCategory(entity);
        }

        public void UpdateCategory(CategoryModel model)
        {
            var entity = Mapper.Map<Category>(model);
            _categoryRepository.UpdateCategoryByUniqueId(entity);
        }
    }
}
