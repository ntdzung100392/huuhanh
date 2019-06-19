using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace HHCoApps.CMSWeb.Models
{
    public class PagingResult<TItem>
    {
        public PagingResult()
        {
            Items = Enumerable.Empty<TItem>();
        }

        public PagingResult(IPagedList<string> pagedListIds)
        {
            IsLastPage = pagedListIds.IsLastPage;
            TotalItemCount = pagedListIds.TotalItemCount;
            PageNumber = pagedListIds.PageNumber;
            PageSize = pagedListIds.PageSize;
        }

        public IEnumerable<TItem> Items { get; set; }
        public bool IsLastPage { get; set; }
        public long TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}