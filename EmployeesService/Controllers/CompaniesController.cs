using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeesService.Data;
using EmployeesService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly EmployeesContext _context;

        public CompaniesController(EmployeesContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("companyUpdated")]
        public async Task<IActionResult> NotifyAboutCompanyUpdated([FromBody] CompanyModel company)
        {
            Console.WriteLine("companyUpdated");

            var employees = await _context.Employees.Where(e => e.CompanyId == company.Id).ToListAsync();
            foreach (var employee in employees)
            {
                Console.Write($"{employee.Name} {employee.Surname} was notified about {company.Title} company was updated!");
            }

            return Ok();
        }
    }
}