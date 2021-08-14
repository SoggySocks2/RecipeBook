using RecipeBook.SharedKernel.Contracts;
using System;

namespace RecipeBook.SharedKernel.BaseClasses
{
    /// <summary>
    /// Properties and methods to be shared amongst all sub classes
    /// </summary>
    public abstract class BaseEntity : IAuditableEntity, ISoftDelete
    {
        public Guid Id { get; protected set; }
        public bool IsDeleted { get; private set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
