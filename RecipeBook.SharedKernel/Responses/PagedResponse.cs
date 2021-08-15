using RecipeBook.SharedKernel.SharedObjects;

namespace RecipeBook.SharedKernel.Responses
{
    /// <summary>
    /// A page of data with pagination details used to determine which page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        public Pagination Pagination { get; }
        public T Data { get; }

        public PagedResponse(T data, Pagination pagination)
        {
            Data = data;
            Pagination = pagination;
        }
    }
}
