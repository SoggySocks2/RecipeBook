using System;

namespace RecipeBook.SharedKernel.Contracts
{
    public interface IAuthenticatedUser
    {
        /// <summary>
        /// User account id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// User account first name & last name
        /// </summary>
        string Name { get; }
    }
}
