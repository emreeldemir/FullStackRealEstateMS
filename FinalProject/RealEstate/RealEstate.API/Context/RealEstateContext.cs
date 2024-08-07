using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Entities.Identity;
using System.Reflection.Emit;

namespace RealEstate.API.Context
{
    public class RealEstateContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
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

            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "user" });
            base.OnModelCreating(builder);

            // Seed roles with unique integer IDs as strings and ConcurrencyStamp
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new ApplicationRole { Id = 2, Name = "user", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );

            // Additional configurations
            builder.Entity<Entities.Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId);
        }

    }
}
