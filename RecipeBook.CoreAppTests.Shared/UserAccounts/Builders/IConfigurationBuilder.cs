using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class IConfigurationBuilder
    {
        private IReadOnlyDictionary<string, string> DefaultConfigurationStrings { get; set; }

        public IConfigurationBuilder WithTestValues()
        {
            DefaultConfigurationStrings =
                new Dictionary<string, string>()
                {
                    //[$"AppConfiguration:Salt"] = "MDxNyrhRlHee7I0CTW9fzVk=",
                    [$"Salt"] = "MDxNyrhRlHee7I0CTW9fzVk=",
                    [$"JWTEncryptionKey"] = "57FAF374-B579-4828-ACCD-A7E43C559AAB",
                };

            return this;
        }

        public IConfiguration Build()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddInMemoryCollection(DefaultConfigurationStrings);
            return configurationBuilder.Build();
        }
    }
}
