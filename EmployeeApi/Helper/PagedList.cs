using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApi.Helper
{
    public class PagedList<T> : List<T>
    {

        public int PageSize { get; private set; }

        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);

        public bool HasNext => (CurrentPage < TotalPages);


        public PagedList(List<T> items, int Count, int PageNumber, int pageSize)
        {
            TotalCount = Count;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(Count / (double)PageSize);
            CurrentPage = PageNumber;
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int PageNumber, int PageSize)
        {
            var Count = source.Count();
            var items = source
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize)
                .ToList();

            return new PagedList<T>(items, Count, PageNumber, PageSize);
        }



    }
}
