using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesService.Data
{
    public class PerpDB
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<EmployeesContext>());
        }

        private static void SeedData(EmployeesContext context)
        {
            if (!context.Employees.Any())
            {
                Console.WriteLine("No data");
                context.Employees.AddRange(new EmployeeEntity[]
                {
                    new EmployeeEntity()
                    {
                        Id = 1,
                        Name = "Dima",
                        Surname = "Karkanitsa",
                        CompanyId = 2
                    },
                    new EmployeeEntity()
                    {
                        Id = 2,
                        Name = "Vova",
                        Surname = "Gulenkov",
                        CompanyId = 2
                    },
                    new EmployeeEntity()
                    {
                        Id = 3,
                        Name = "Kirill",
                        Surname = "Petrov",
                        CompanyId = 1
                    },
                    new EmployeeEntity()
                    {
                        Id = 4,
                        Name = "Anna",
                        Surname = "Vasil",
                        CompanyId = 5
                    },
                    new EmployeeEntity()
                    {
                        Id = 5,
                        Name = "Sasha",
                        Surname = "Block",
                        CompanyId = 6
                    },
                    new EmployeeEntity()
                    {
                        Id = 6,
                        Name = "Misha",
                        Surname = "Samoilenko",
                        CompanyId = 1
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