using EGeek.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EGeek.Identity
{
    public sealed class EGeekIdentityDbContext(DbContextOptions<EGeekIdentityDbContext> options)
                : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("EGeekIdentity");
        }
    }
}
