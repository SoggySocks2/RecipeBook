using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipeBook.SharedKernel.SharedObjects;
using System.Linq;

namespace RecipeBook.CoreApp.Infrastructure.Data.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static bool IsAdded(this EntityEntry entry) =>
            entry.State == EntityState.Added;

        /// <summary>
        /// Ensure entity state is modified if any child (or navigation) properties have been modified
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool IsModified(this EntityEntry entry) =>
            entry.State != EntityState.Added &&
            (entry.State == EntityState.Modified ||
            entry.References.Any(r => r.TargetEntry != null &&
                                        r.TargetEntry.Metadata.IsOwned() &&
                                        r.TargetEntry.Metadata.ClrType.BaseType == typeof(ValueObject) &&
                                        (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)));
    }
}
