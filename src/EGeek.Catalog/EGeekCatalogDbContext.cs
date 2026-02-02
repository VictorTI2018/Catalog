using EGeek.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGeek.Catalog
{
    public sealed class EGeekCatalogDbContext
        : DbContext, IUnitOfWork
    {
        public EGeekCatalogDbContext(
            DbContextOptions<EGeekCatalogDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Catalog");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            try
            {
                var utcNow = DateTime.UtcNow;

                foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    if (entry.State == EntityState.Added)
                        entry.Property(nameof(AuditableEntity.Created)).CurrentValue = utcNow;

                    if (entry.State == EntityState.Modified)
                        entry.Property(nameof(AuditableEntity.Updated)).CurrentValue = utcNow;

                }
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
