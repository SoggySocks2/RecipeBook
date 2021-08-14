using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;

namespace RecipeBook.ApiGateway.Api.Configuration
{
    public class ClientSettings : IClientSettings
    {
        public const string CONFIG_NAME = "ClientSettings";

        public static ClientSettings Instance { get; } = new ClientSettings();
        private ClientSettings() { }

        public string Name { get; set; }
        public int DefaultPageSize { get; set; }
        public int DefaultPageSizeLimit { get; set; }
        public string DatabaseSecretName { get; set; }
    }
}
