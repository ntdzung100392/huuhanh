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
        private readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories.Any() ? _mapper.Map<IEnumerable<CategoryModel>>(categories) : Enumerable.Empty<CategoryModel>();
        }

        public CategoryModel GetCategoryById(int categoryId)
        {
            return _mapper.Map<CategoryModel>(_categoryRepository.GetCategoryById(categoryId));
        }

        public void InsertNewCategory(CategoryModel model)
        {
            var entity = _mapper.Map<Category>(model);
            _categoryRepository.InsertCategory(entity);
        }

        public void UpdateCategory(CategoryModel model)
        {
            var entity = _mapper.Map<Category>(model);
            _categoryRepository.UpdateCategoryByUniqueId(entity);
        }
    }
}
