using RecipeBook.SharedKernel.SharedObjects;

namespace RecipeBook.SharedKernel.Responses
{
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
