using RecipeBook.SharedKernel.Contracts;
using System;

namespace RecipeBook.SharedKernel.BaseClasses
{
    /// <summary>
    /// Represents who and when created or modified data
    /// </summary>
    public class AuditableEntity : IAuditableEntity
    {
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
