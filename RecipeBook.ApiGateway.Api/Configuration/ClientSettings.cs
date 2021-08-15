using RecipeBook.CoreApp.Domain.UserAccounts.Contracts;

namespace RecipeBook.ApiGateway.Api.Configuration
{
    /// <summary>
    /// Represents client configuration settings from appSettings
    /// </summary>
    public class ClientSettings : IClientSettings
    {
        /* configuration section */
        public const string CONFIG_NAME = "ClientSettings";

        public static ClientSettings Instance { get; } = new ClientSettings();
        private ClientSettings() { }

        /// <summary>
        /// Client name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default paged response page size
        /// </summary>
        public int DefaultPageSize { get; set; }

        /// <summary>
        /// Default pages response size limit
        /// </summary>
        public int DefaultPageSizeLimit { get; set; }

        /// <summary>
        /// Azure key vault name for the database connection secrect
        /// </summary>
        public string DatabaseSecretName { get; set; }
    }
}
