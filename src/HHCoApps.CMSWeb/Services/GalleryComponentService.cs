using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Services.Models;
using PagedList;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class GalleryComponentService : IGalleryComponentService
    {
        private readonly IMapper _mapper;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IContentInfoService _contentInfoService;

        public GalleryComponentService(IMapper mapper, UmbracoHelper umbracoHelper, IContentInfoService contentInfoService)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
            _contentInfoService = contentInfoService;
        }

        public GalleryComponentViewModel GetViewModel(GalleryComponent model)
        {
            if (string.IsNullOrEmpty(model.ContentSourceOption))
                return new GalleryComponentViewModel();

            var viewModel = _mapper.Map<GalleryComponentViewModel>(model);
            viewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? "Get Inspired" : viewModel.Title;
            viewModel.ViewMoreLabel = string.IsNullOrEmpty(viewModel.ViewMoreLabel) ? "View more" : viewModel.ViewMoreLabel;

            var contentInfos = model.ContentSourceOption.Equals(ContentSource.FromChildPages.DisplayName, StringComparison.OrdinalIgnoreCase) ? 
                GetContentInfoModelsFromStarterNode(model) : _mapper.Map<IEnumerable<ContentInfoModel>>(model.FixedItems);

            var contentInfoModels = contentInfos.ToList();
            var cmsContents = contentInfoModels.Where(x => !string.IsNullOrEmpty(x.Udi)).ToList();
            var nodeContents = _umbracoHelper.Content(cmsContents.Select(x => Udi.Parse(x.Udi))).ToArray();

            foreach (var content in contentInfoModels)
            {
                if (string.IsNullOrEmpty(content.Udi))
                    continue;

                var nodeContent = nodeContents.FirstOrDefault(x => x.GetUDI() == content.Udi);
                if (nodeContent != null)
                {
                    var nodeContentInfoModel = _contentInfoService.GetNodeContentInfo(nodeContent);
                    content.Title = string.IsNullOrEmpty(content.Title) ? nodeContentInfoModel.Title : content.Title;
                    content.ImageUrl ??= nodeContentInfoModel.ImageUrl;
                    content.ImageWidth = nodeContentInfoModel.ImageWidth;
                    content.ImageHeight = nodeContentInfoModel.ImageHeight;
                    content.ImageAlt ??= nodeContentInfoModel.Title;
                    content.Caption = string.IsNullOrEmpty(content.Caption) ? nodeContentInfoModel.Caption : content.Caption;
                }
            }

            var numberOfRows = model.NumberOfRows == default ? 1 : model.NumberOfRows;
            numberOfRows = Math.Min(numberOfRows, contentInfoModels.Count);

            viewModel.Rows = contentInfoModels.Split(numberOfRows);

            return viewModel;
        }

        private IEnumerable<ContentInfoModel> GetContentInfoModelsFromStarterNode(GalleryComponent model)
        {
            if (model.StarterNode == null)
                return Enumerable.Empty<ContentInfoModel>();
            var childrenIds = model.StarterNode.Children.Select(x => x.Id);

            var contents = _umbracoHelper.Content(childrenIds);

            return _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
        }
    }

    public interface IGalleryComponentService
    {
        GalleryComponentViewModel GetViewModel(GalleryComponent model);
    }
}