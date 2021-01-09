using AutoMapper;
using System;
using System.Linq;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class FooterNavigationService : IFooterNavigationService
    {
        private readonly IMapper _mapper;

        public FooterNavigationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FooterNavigationViewModel GetViewModel(FooterNavigationComponent componentModel, IPublishedContent assignedContentItem)
        {
            var result = new FooterNavigationViewModel();

            if (assignedContentItem.Parent != null)
            {
                var nodes = assignedContentItem.Parent.Children.ToArray();
                if (nodes.Length == 0)
                    return new FooterNavigationViewModel();

                var currentNodeIndex = Array.IndexOf(nodes, assignedContentItem);
                if (currentNodeIndex >= 0)
                {
                    var previousNodeIndex = currentNodeIndex == 0 ? nodes.Length - 1 : currentNodeIndex - 1;
                    var nextNodeIndex = currentNodeIndex == nodes.Length - 1 ? 0 : currentNodeIndex + 1;

                    result = new FooterNavigationViewModel
                    {
                        PreviousUrl = nodes[previousNodeIndex].Url,
                        NextUrl = nodes[nextNodeIndex].Url
                    };
                }
            }

            return result;
        }
    }

    public interface IFooterNavigationService
    {
        FooterNavigationViewModel GetViewModel(FooterNavigationComponent navigation, IPublishedContent publishedContent);
    }
}