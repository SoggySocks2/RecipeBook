namespace RecipeBook.SharedKernel.SharedObjects
{
    /// <summary>
    /// Filters used when paginating data
    /// </summary>
    public class PaginationFilter
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}
