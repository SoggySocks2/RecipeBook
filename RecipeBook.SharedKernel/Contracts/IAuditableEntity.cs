using System;

namespace RecipeBook.SharedKernel.Contracts
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// When the data was created
        /// </summary>
        DateTime Created { get; set; }

        /// <summary>
        /// Who created the data
        /// </summary>
        Guid CreatedBy { get; set; }

        /// <summary>
        /// When the data was modified
        /// </summary>
        DateTime Modified { get; set; }

        /// <summary>
        /// Who modified the data
        /// </summary>
        Guid ModifiedBy { get; set; }
    }
}
