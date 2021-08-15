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
                };

            return this;
        }

        public IConfiguration Build()
        {
            Microsoft.Extensions.Configuration.ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddInMemoryCollection(DefaultConfigurationStrings);
            return configurationBuilder.Build();
        }
    }
}
