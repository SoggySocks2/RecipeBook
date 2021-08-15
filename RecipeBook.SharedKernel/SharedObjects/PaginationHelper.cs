using System;

namespace RecipeBook.SharedKernel.SharedObjects
{
    /// <summary>
    /// Various calculations for paged responses
    /// </summary>
    public static class PaginationHelper
    {
        private static readonly object _lock = new();

        public static int DefaultPage { get; private set; } = 1;
        public static int DefaultPageSize { get; private set; } = 10;
        public static int DefaultPageSizeLimit { get; private set; } = 50;

        public static void SetDefaults(int defaultPageSize, int defaultPageSizeLimit)
        {
            lock (_lock)
            {
                DefaultPageSize = defaultPageSize;
                DefaultPageSizeLimit = defaultPageSizeLimit;
            }
        }

        public static int GetHandledTotalItems(int itemsCount)
        {
            return itemsCount < 0 ? 0 : itemsCount;
        }

        public static int GetHandledPageSize(int? pageSize)
        {
            if (!pageSize.HasValue || pageSize <= 0) return DefaultPageSize;

            if (pageSize > DefaultPageSizeLimit) return DefaultPageSizeLimit;

            return pageSize.Value;
        }

        public static int GetHandledTotalPages(int? pageSize, int itemsCount)
        {
            var totalItems = GetHandledTotalItems(itemsCount);

            return totalItems == 0 ? 1 : (int)Math.Ceiling((decimal)totalItems / GetHandledPageSize(pageSize));
        }

        public static int GetHandledPage(int? pageSize, int? page, int itemsCount)
        {
            if (!page.HasValue || page <= 0) return DefaultPage;

            var totalPages = GetHandledTotalPages(pageSize, itemsCount);

            if (page.Value > totalPages) return totalPages;

            return page.Value;
        }

        public static int CalculateTake(int? pageSize)
        {
            return GetHandledPageSize(pageSize);
        }
        public static int CalculateSkip(int? pageSize, int? page, int itemsCount)
        {
            return CalculateTake(pageSize) * (GetHandledPage(pageSize, page, itemsCount) - 1);
        }

        public static int CalculateTake(PaginationFilter baseFilter)
        {
            return CalculateTake(baseFilter.PageSize);
        }
        public static int CalculateSkip(PaginationFilter baseFilter, int itemsCount)
        {
            return CalculateSkip(baseFilter.PageSize, baseFilter.Page, itemsCount);
        }
    }
}
