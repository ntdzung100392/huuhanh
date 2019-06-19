using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class ContentInfoService : IContentInfoService
    {
        private readonly IMapper _mapper;
        private readonly UmbracoHelper _umbracoHelper;

        public ContentInfoService(IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
        }

        public ContentInfoModel GetNodeContentInfo(IPublishedContent nodeContent)
        {
            if (nodeContent == null)
                return new ContentInfoModel();

            if (nodeContent is Home)
            {
                var content = (NavigationManagement)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.NavigationManagementContent);
                return new ContentInfoModel
                {
                    Title = content?.SiteName ?? string.Empty,
                    ImageUrl = content.Logo?.Url(mode: UrlMode.Absolute) ?? string.Empty,
                    ImageAlt = content.SiteName ?? string.Empty,
                    Caption = content.SiteDescription ?? string.Empty
                };
            }

            if (nodeContent is IContentBase)
            {
                var content = (IContentBase)nodeContent;
                return _mapper.Map<ContentInfoModel>(content);
            }

            return new ContentInfoModel();
        }
    }

    public interface IContentInfoService
    {
        ContentInfoModel GetNodeContentInfo(IPublishedContent nodeContent);
    }
}