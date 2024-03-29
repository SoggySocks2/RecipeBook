﻿using RecipeBook.ApiGateway.Api.Features.Identity;
using System;

namespace RecipeBook.CoreAppTests.Shared.UserAccounts.Builders
{
    public class AuthenticatedUserBuilder
    {
        private Guid Id { get; set; }
        private string Name { get; set; }

        public AuthenticatedUserBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Name = "Test Name";
            return this;
        }

        public AuthenticatedUserBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public AuthenticatedUserBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public AuthenticatedUser Build()
        {
            return new AuthenticatedUser(Id, Name);
        }
    }
}
