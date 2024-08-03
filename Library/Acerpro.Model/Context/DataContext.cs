using Acerpro.Common.Enums;
using Acerpro.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acerpro.Model.Context
{
    public class DataContext : DbContext
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options/*, IHttpContextAccessor httpContextAccessor*/)
            : base(options)
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Active,
                    Email = "admin@admin.com",
                    PhoneNumber = "123",
                    Password = "123",
                    FirstName = "Admin",
                    LastName = "ADMIN",
                    Title = "Admin",
                    LastLogin = DateTime.Now,
                });
        }
    }
}
