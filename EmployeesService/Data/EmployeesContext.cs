using Microsoft.EntityFrameworkCore;

namespace EmployeesService.Data
{
    public class EmployeesContext : DbContext
    {
        protected EmployeesContext()
        {
        }

        public EmployeesContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}