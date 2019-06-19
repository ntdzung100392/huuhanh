using System.Collections.Generic;
using HHCoApps.Core;

namespace HHCoApps.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        void InsertCategory(Category entity);
        void UpdateCategoryByUniqueId(Category entity);
    }
}