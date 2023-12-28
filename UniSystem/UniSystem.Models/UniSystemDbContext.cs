using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UniSystem.Models.Common;
using UniSystem.Models.Entities.User;

namespace UniSystem.Models
{
    public class UniSystemDbContext : DbContext
    {
        public UniSystemDbContext(DbContextOptions<UniSystemDbContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure name mappings
                entity.SetTableName(entity.ClrType.Name.ToLower());
                if (typeof(IEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .HasKey(nameof(IEntity.Id));
                }

                entity.GetProperties()
                    .ToList()
                    .ForEach(e => e.SetColumnName(e.Name.ToLower()));
                entity.GetForeignKeys()
                    .Where(e => !e.IsOwnership && e.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniSystemDbContext).Assembly);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(IAuditable).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Added)
                {
                    var entity = entry.Entity as IAuditable;
                    entity.CreateDate = DateTime.Now;
                    // entity.CreatorUserId = this.userContext.UserId; !!!!!!!!
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}