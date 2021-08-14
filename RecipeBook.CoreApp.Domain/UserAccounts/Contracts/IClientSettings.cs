namespace RecipeBook.CoreApp.Domain.UserAccounts.Contracts
{
    public interface IClientSettings
    {
        string Name { get; set; }
        string DatabaseSecretName { get; set; }
    }
}
