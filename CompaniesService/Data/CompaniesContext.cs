using Microsoft.EntityFrameworkCore;

namespace CompaniesService.Data
{
    public class CompaniesContext : DbContext
    {
        public CompaniesContext(DbContextOptions<CompaniesContext> options) 
            : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.Entity<CompanyEntity>().HasData(new CompanyEntity[]
            {
                new CompanyEntity
                {
                    Id = 1,
                    Title = "Adidas",
                    FoundationYear = 1967
                },
                new CompanyEntity
                {
                    Id = 2,
                    Title = "Nike",
                    FoundationYear = 1980
                },
                new CompanyEntity
                {
                    Id = 3,
                    Title = "Google",
                    FoundationYear = 1993
                },
                new CompanyEntity
                {
                    Id = 4,
                    Title = "Microsoft",
                    FoundationYear = 1983
                }
            });
        }
    }
}