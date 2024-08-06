using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Entities.Identity;

namespace RealEstate.API.Context
{
    public class RealEstateContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
         
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {
        }

        
        public DbSet<Entities.Property> Properties { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<Entities.Status> Statuses { get; set; }
        public DbSet<Entities.Currency> Currencies { get; set; }
        public DbSet<Entities.Photo> Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Additional configurations
            builder.Entity<Entities.Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId);
        }

    }
}
