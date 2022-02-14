using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeesService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesContext _context;

        public EmployeesController(EmployeesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeEntity employee)
        {
            return Ok(await _context.Employees.AddAsync(employee));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await _context.Employees.FirstOrDefaultAsync(e => e.Id == id));
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string name, [FromQuery] string surname)
        {
            var list = await _context.Employees.Where(e =>
                (name != "" && e.Name.Contains(name) || name == "") &&
                (surname != "" && e.Surname.Contains(surname) || surname == "")).ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        [Route("notify")]
        public async Task<IActionResult> SendSmsToEmployees([FromQuery] int companyId, [FromQuery] string message)
        {
            var employees = await _context.Employees.Where(e => e.CompanyId == companyId).ToListAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name + ' ' + employee.Surname + " : " + message);
            }

            return Ok();
        }
    }
}