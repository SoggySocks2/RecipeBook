namespace RecipeBook.SharedKernel.Exceptions.Helpers
{
    public class Check : ICheckClause
    {
        private Check() { }

        public static ICheckClause For { get; } = new Check();
    }
}
