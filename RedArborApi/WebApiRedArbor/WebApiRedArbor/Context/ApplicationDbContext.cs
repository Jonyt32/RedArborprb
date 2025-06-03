using Microsoft.EntityFrameworkCore;
using WebApiRedArbor.Entities;

namespace WebApiRedArbor.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void SeedRoles()
        {
            if (!Roles.Any())
            {
                Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Desarrollador" },
                    new Role { RoleId = 2, RoleName = "TeamLeader" },
                    new Role { RoleId = 3, RoleName = "CTO" }
                );
                SaveChanges();
            }
        }
    }
}
