namespace RecipeBook.SharedKernel.SharedObjects
{
    public class Pagination
    {
        public int TotalItems { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int Page { get; }
        public int StartItem { get; }
        public int EndItem { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }

        public Pagination(int? pageSize, int? page, int itemsCount)
        {
            TotalItems = PaginationHelper.GetHandledTotalItems(itemsCount);
            PageSize = PaginationHelper.GetHandledPageSize(pageSize);
            TotalPages = PaginationHelper.GetHandledTotalPages(pageSize, itemsCount);
            Page = PaginationHelper.GetHandledPage(pageSize, page, itemsCount);

            HasNext = Page != TotalPages;
            HasPrevious = Page != 1;

            StartItem = TotalItems == 0 ? 0 : (PageSize * (Page - 1)) + 1;
            EndItem = (PageSize * Page) > TotalItems ? TotalItems : PageSize * Page;
        }

        public Pagination(PaginationFilter baseFilter, int itemsCount) :
                        this(baseFilter.PageSize, baseFilter.Page, itemsCount)
        {
        }
    }
}
