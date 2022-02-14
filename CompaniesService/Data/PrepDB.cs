using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CompaniesService.Data
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<CompaniesContext>());
        }

        private static void SeedData(CompaniesContext context)
        {
            if (!context.Companies.Any())
            {
                Console.WriteLine("No data");
                context.Companies.AddRange(new CompanyEntity[]
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
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Date already exists");
            }
        }
    }
}