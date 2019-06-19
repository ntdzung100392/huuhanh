using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace HHCoApps.CMSWeb.Models
{
    public class QuestionSearchResult : PagingResult<ContentInfoModel>
    {
        public QuestionSearchResult()
        {
        }

        public QuestionSearchResult(IPagedList<string> pagedListIds) : base(pagedListIds)
        {
        }

        public IEnumerable<IssueTypeModel> IssueTypes { get; set; } = Enumerable.Empty<IssueTypeModel>();
    }
}