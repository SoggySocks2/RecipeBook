using RecipeBook.SharedKernel.Contracts;
using System;

namespace RecipeBook.SharedKernel.BaseClasses
{
    public class AuditableEntity : IAuditableEntity
    {
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
