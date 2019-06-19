using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class FilterCategoryService : IFilterCategoryService
    {
        private readonly IDataTypeService _dataTypeService;

        public FilterCategoryService(IDataTypeService dataTypeService)
        {
            _dataTypeService = dataTypeService;
        }

        public IEnumerable<FilterCategoryModel> GetFilterCategories(IEnumerable<string> filterCategoryNames)
        {
            var result = new List<FilterCategoryModel>();
            foreach (var filterCategoryName in filterCategoryNames)
            {
                var filterCategory = FilterCategory.FromDisplayName(filterCategoryName);
                if (filterCategory == null)
                    continue;

                var dataType = _dataTypeService.GetDataType(filterCategory.DataTypeName);
                var configuration = (DropDownFlexibleConfiguration)dataType.Configuration;

                result.Add(new FilterCategoryModel
                {
                    Key = filterCategory.Value,
                    Name = filterCategory.DisplayName,
                    IsPrimaryFilter = false,
                    Items = configuration.Items.Select(x => new FilterItemModel
                    {
                        Key = $"{filterCategory.Value}:{x.Id}",
                        Name = x.Value,
                        IsPrimarySubFilter = false
                    })
                });
            }

            return result;
        }

        public IEnumerable<FilterCriterionModel> GetDefaultFilterCategories(IEnumerable<FilterTypeValueOptions> defaultFilters)
        {
            if (defaultFilters == null || !defaultFilters.Any())
                return Enumerable.Empty<FilterCriterionModel>();

            var criteria = new List<FilterCriterionModel>();
            foreach (var filterTypeValueOptions in defaultFilters)
            {
                var filterCategory = FilterCategory.FromDisplayName(filterTypeValueOptions.FilterType);
                if (filterCategory == null)
                    continue;

                var filterValues = filterTypeValueOptions.FilterValues.Split(',');

                criteria.AddRange(filterValues.Select(x => new FilterCriterionModel
                {
                    FilterCategoryKey = filterCategory.Value,
                    FilterValue = x.Trim()
                }));
            }

            return criteria;
        }

        public void UpdateSearchRequestCriteriaFilterKey(IEnumerable<FilterCriterion> criteria)
        {
            foreach (var filterCriterion in criteria)
            {
                if (string.IsNullOrEmpty(filterCriterion.FilterCategoryKey))
                {
                    var filterCategory = FilterCategory.FromDisplayName(filterCriterion.FilterCategoryDisplayName);
                    if (filterCategory == null) 
                        throw new ArgumentException($"Filter category {filterCriterion.FilterCategoryDisplayName} is not valid!");

                    filterCriterion.FilterCategoryKey = filterCategory.Value;
                }
            }
        }
    }

    public interface IFilterCategoryService
    {
        IEnumerable<FilterCategoryModel> GetFilterCategories(IEnumerable<string> filterCategoryNames);
        IEnumerable<FilterCriterionModel> GetDefaultFilterCategories(IEnumerable<FilterTypeValueOptions> defaultFilters);
        void UpdateSearchRequestCriteriaFilterKey(IEnumerable<FilterCriterion> criteria);
    }
}