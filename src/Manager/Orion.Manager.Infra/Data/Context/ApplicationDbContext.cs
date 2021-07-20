using Microsoft.EntityFrameworkCore;

namespace Orion.Manager.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureMapping(GetType());
            builder.ConfigureSoftDelete();
            base.OnModelCreating(builder);
        }
    }
}