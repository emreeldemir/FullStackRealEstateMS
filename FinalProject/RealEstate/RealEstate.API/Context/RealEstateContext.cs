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

            // Create a password hasher to hash the passwords
            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed admin and user
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "AdminPassword123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "UserPassword123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            // Seed user roles by adding entries to AspNetUserRoles table
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 }, // admin user with admin role
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 }  // normal user with user role
            );

            // Additional configurations
            builder.Entity<Entities.Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId);
        }

    }
}
