using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IMapper _mapper;

        public NavigationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public NavigationViewModel GetViewModel(NavigationAndBreadcrumbComponent componentModel, IPublishedContent currentModel, bool isLevel2)
        {
            if (currentModel == null)
                return new NavigationViewModel();

            var contentInfos = new NavigationViewModel();
            var activeNodes = GetActiveNodes(currentModel);
            contentInfos.BreadcrumbNodes = activeNodes;

            if (componentModel.StarterNode == null || !componentModel.StarterNode.Children.Any() || !componentModel.StarterNode.Children.Any(x => x.IsDisplayInNavigation()))
                return contentInfos;

            var navigationContents = new List<NavigationModel>();
            var nodeContents = componentModel.StarterNode.Children.Where(x => x.IsDisplayInNavigation());
            var navigationContentsModel = _mapper.Map<IEnumerable<NavigationModel>>(nodeContents);

            foreach (var navigationModel in navigationContentsModel)
            {
                navigationModel.ChildNodes.ToList().ForEach(ChildNode => ChildNode.IsActive = activeNodes.Any(x => x.Id == ChildNode.Id));
                navigationModel.IsActive = activeNodes.Any(x => x.Id == navigationModel.Id);
                navigationContents.Add(navigationModel);
            }

            contentInfos.Title = componentModel.StarterNode.GetProperty("pageTitle")?.Value().ToString();
            contentInfos.NavigationNodes = navigationContents;

            return contentInfos;
        }

        private IEnumerable<LinkItemModel> GetActiveNodes(IPublishedContent publishedContent)
        {
            var currentNodes = new List<LinkItemModel>();
            if (publishedContent == null)
                return currentNodes;

            var home = (Home)publishedContent.Root();
            if (home == null)
                return currentNodes;

            var rootNode = _mapper.Map<LinkItemModel>(home);
            while (publishedContent != null)
            {
                if (publishedContent is INavigationBase parentNode && (!parentNode.UmbracoNavihide || !parentNode.UmbracoBreadcrumHide))
                {
                    currentNodes.Add(_mapper.Map<LinkItemModel>(parentNode));
                }

                publishedContent = publishedContent.Parent;
            }

            currentNodes.Add(rootNode);

            return currentNodes;
        }
    }

    public interface INavigationService
    {
        NavigationViewModel GetViewModel(NavigationAndBreadcrumbComponent navigation, IPublishedContent publishedContent, bool isLevel2);
    }
}