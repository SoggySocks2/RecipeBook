namespace RecipeBook.CoreApp.Domain.UserAccounts.Contracts
{
    /// <summary>
    /// Client configuration settings
    /// </summary>
    public interface IClientSettings
    {
        string Name { get; set; }
        string DatabaseSecretName { get; set; }
    }
}
