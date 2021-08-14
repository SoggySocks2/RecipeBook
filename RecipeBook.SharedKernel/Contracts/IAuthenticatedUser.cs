using System;

namespace RecipeBook.SharedKernel.Contracts
{
    public interface IAuthenticatedUser
    {
        Guid Id { get; }
        string Name { get; }
    }
}
