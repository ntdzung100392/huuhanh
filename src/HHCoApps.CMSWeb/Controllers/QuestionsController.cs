using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services;
using HHCoApps.CMSWeb.Services.Models;
using PagedList;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class QuestionsController : UmbracoApiController
    {
        private const int DefaultPageSize = 10;

        private readonly IContentIndexQueryService _queryService;
        private readonly IMapper _mapper;

        public QuestionsController(IContentIndexQueryService queryService, IMapper mapper)
        {
            _queryService = queryService;
            _mapper = mapper;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public QuestionsViewModel GetQuestions(string id)
        {
            var viewModel = new QuestionsViewModel();
            var model = (Questions)Umbraco.Content(id);
            var content = model.Children.Where(c => c.ContentType.Alias.Equals("questions")).ToList();

            var topics = content.Select(x => new TopicModel
            {
                Name = ((Questions)Umbraco.Content(x.Id)).PageTitle,
                Path = x.Path
            });

            viewModel.Topics = topics;
            viewModel.NoResultsFoundMessage = model.NoResultsFoundMessage;
            return viewModel;
        }

        [HttpPost]
        public QuestionSearchResult SearchQuestions([FromBody] SearchRequest searchRequest, [FromUri]int pageNumber = 1, [FromUri]int pageSize = DefaultPageSize)
        {
            var searchResults = _queryService.GetContentsBySearchRequestModel(
                IndexConstants.QuestionIndex, new[] { IndexConstants.QuestionNodeTypeAlias }, _mapper.Map<SearchRequestModel>(searchRequest));

            var questionIds = searchResults.Select(x => x.Id).ToArray();
            var issueTypeIds = searchResults.Select(x => x.Values["parentID"].ToString()).Distinct();
            var issueTypeNodes = Umbraco.Content(issueTypeIds);
            var pagedListIds = questionIds.ToPagedList(pageNumber, pageSize);
            var questions = Umbraco.Content(pagedListIds);

            var result = new QuestionSearchResult(pagedListIds)
            {
                Items = _mapper.Map<IEnumerable<ContentInfoModel>>(questions),
                IssueTypes = issueTypeNodes.Select(issueTypeNode => new IssueTypeModel
                {
                    Name = ((Questions)issueTypeNode).PageTitle,
                    Paths = new List<string>
                    {
                        issueTypeNode.Path
                    }
                })
            };

            result.IssueTypes = result.IssueTypes.GroupBy(x => x.Name).Select(issueTypesGroup => new IssueTypeModel
            {
                Name = issueTypesGroup.Key.Trim(),
                Paths = issueTypesGroup.SelectMany(z => z.Paths).ToList()
            }).ToList();

            return result;
        }
    }
}
