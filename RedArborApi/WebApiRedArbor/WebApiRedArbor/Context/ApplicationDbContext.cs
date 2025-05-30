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
    }
}
