using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7Proj1.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //SeedRoles (USER,ADMIN,SUPERADMIN)

            var adminRoleId = "cdffbe66-07e0-4d1f-b84b-22acd08ed0b6";
            var superAdminRoleId = "3102965b-e4a2-4b48-81c5-f83ff23fedd8";
            var userRoleId = "90a81c72-ed06-44c9-854c-3d442061438e";

            var Roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId

                },
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName="User",
                    Id= userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(Roles);

            //Seed SuperAdmin User

            var superAdminId = "ac7cbd6d-69e7-4cf0-960f-fd24ad941496";
            var superAdminUser = new IdentityUser
            {
                UserName = "satyadalayi@bloggieDeveloper.com",
                Email = "satyadalayi@bloggieDeveloper.com",
                NormalizedEmail = "satyadalayi@bloggieDeveloper.com".ToUpper(),
                NormalizedUserName = "satyadalayi@bloggieDeveloper.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Satyadalayi@Developer123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser

            var SuperAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                     RoleId = adminRoleId,
                     UserId = superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId = superAdminRoleId,
                     UserId = superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }


            };

            builder.Entity<IdentityUserRole<string>>().HasData(SuperAdminRoles);

        }

    }
}
