using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.UserAccounts;
using RecipeBook.CoreApp.Infrastructure.Data.Extensions;
using RecipeBook.CoreApp.Infrastructure.Data.UserAccounts.Configuration;
using RecipeBook.SharedKernel.BaseClasses;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data
{
    public class CoreDbContext : DbContext
    {
        private readonly IAuthenticatedUser AuthenticatedUser;

        public DbSet<UserAccount> UserAccounts { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options, IAuthenticatedUser authenticatedUser) : base(options)
        {
            AuthenticatedUser = authenticatedUser;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccountConfiguration).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // The order is important. Apply soft delete then auditing.
            ApplySoftDelete();
            ApplyAuditing();

            return await base.SaveChangesAsync(cancellationToken);
        }


        /// <summary>
        /// Change state of deleted entities and set IsDeleted = true
        /// </summary>
        private void ApplySoftDelete()
        {
            var entries = ChangeTracker.Entries<ISoftDelete>().Where(x => x.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
                entry.CurrentValues[nameof(ISoftDelete.IsDeleted)] = true;

                var referenceEntries = entry.References.Where(x => x.TargetEntry != null &&
                                                                    x.TargetEntry.Metadata.IsOwned() &&
                                                                    x.TargetEntry.Metadata.ClrType.BaseType == typeof(ValueObject));

                foreach (var referenceEntry in referenceEntries)
                {
                    referenceEntry.TargetEntry.CurrentValues.SetValues(referenceEntry.TargetEntry.OriginalValues);
                    referenceEntry.TargetEntry.State = EntityState.Unchanged;
                }
            }
        }

        /// <summary>
        /// Set created and modified properties
        /// </summary>
        private void ApplyAuditing()
        {
            var addedEntries = ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsAdded());
            var modifiedEntries = ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsModified());

            var now = DateTime.Now;
            foreach (var entry in addedEntries)
            {
                entry.CurrentValues[nameof(BaseEntity.Created)] = now;
                entry.CurrentValues[nameof(BaseEntity.CreatedBy)] = AuthenticatedUser?.Id;
                entry.CurrentValues[nameof(BaseEntity.Modified)] = now;
                entry.CurrentValues[nameof(BaseEntity.ModifiedBy)] = AuthenticatedUser?.Id;
            }

            foreach (var entry in modifiedEntries)
            {
                entry.CurrentValues[nameof(BaseEntity.Modified)] = now;
                entry.CurrentValues[nameof(BaseEntity.ModifiedBy)] = AuthenticatedUser?.Id;
            }
        }
    }
}
