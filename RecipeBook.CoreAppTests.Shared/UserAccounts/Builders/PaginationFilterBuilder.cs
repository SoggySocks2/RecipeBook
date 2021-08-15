using RecipeBook.SharedKernel.SharedObjects;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class PaginationFilterBuilder
    {
        private int? Page { get; set; }
        private int? PageSize { get; set; }
        private string SortBy { get; set; }
        private string OrderBy { get; set; }

        public PaginationFilterBuilder WithTestValues()
        {
            Page = 2;
            PageSize = 10;
            SortBy = "Id";
            OrderBy = "Id";
            return this;
        }
        public PaginationFilterBuilder WithPage(int? page)
        {
            Page = page;
            return this;
        }
        public PaginationFilterBuilder WithPageSize(int? pageSize)
        {
            PageSize = pageSize;
            return this;
        }
        public PaginationFilterBuilder WithPageSortBy(string sortBy)
        {
            SortBy = sortBy;
            return this;
        }
        public PaginationFilterBuilder WithPageOrderBy(string orderBy)
        {
            OrderBy = orderBy;
            return this;
        }
        public PaginationFilter Build()
        {
            var paginationFilter = new PaginationFilter
            {
                Page = Page,
                PageSize = PageSize,
                SortBy = SortBy,
                OrderBy = OrderBy
            };
            return paginationFilter;
        }
    }
}
