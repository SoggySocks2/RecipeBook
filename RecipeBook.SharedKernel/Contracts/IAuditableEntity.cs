using System;

namespace RecipeBook.SharedKernel.Contracts
{
    public interface IAuditableEntity
    {
        DateTime Created { get; set; }
        Guid CreatedBy { get; set; }
        DateTime Modified { get; set; }
        Guid ModifiedBy { get; set; }
    }
}
