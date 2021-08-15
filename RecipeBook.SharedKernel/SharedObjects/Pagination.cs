namespace RecipeBook.SharedKernel.SharedObjects
{
    /// <summary>
    /// Paged data pagination properties
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Count if records that meet the search criteria
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// Total pages available at the current page size
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Number for records per page
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// First item in the current page
        /// </summary>
        public int StartItem { get; }

        /// <summary>
        /// Last item in the current page
        /// </summary>
        public int EndItem { get; }

        /// <summary>
        /// Is there a page available before this one
        /// </summary>
        public bool HasPrevious { get; }

        /// <summary>
        /// Is there a page available after this one
        /// </summary>
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
