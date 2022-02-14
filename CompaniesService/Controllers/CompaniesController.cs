using System;
using System.Net.Http;
using System.Threading.Tasks;
using CompaniesService.Data;
using CompaniesService.Models;
using CompaniesService.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CompaniesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly CompaniesContext _context;
        private readonly IRabbitMqService _rabbitMqService;


        public CompaniesController(CompaniesContext context, IRabbitMqService rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _context.Companies.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            return Ok(await _context.Companies.FirstOrDefaultAsync(c => c.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyEntity company)
        {
            var createdCompany = await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            Console.WriteLine(DateTime.Now + ": new company created.");
            Console.WriteLine(DateTime.Now + ": notification was sent to RabbitMq.");

            //send notification using rabbit mq to all employees
            var companyModel = new CompanyModel()
            {
                Id = createdCompany.Entity.Id,
                Title = createdCompany.Entity.Title,
                FoundationYear = createdCompany.Entity.FoundationYear
            };

            _rabbitMqService.SendMessage(companyModel);

            return Ok("Company created!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyEntity company)
        {
            var companyToUpdate = await _context.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);

            if (companyToUpdate == null)
            {
                throw new Exception("Bad request, can't update!");
            }

            companyToUpdate.Title = company.Title;
            companyToUpdate.FoundationYear = company.FoundationYear;
            _context.Update(companyToUpdate);
            await _context.SaveChangesAsync();

            Console.WriteLine(DateTime.Now + ": company updated.");
            Console.WriteLine(DateTime.Now + ": notification was sent by HTTP directly.");

            //send notification using rabbit mq employees of current company
            var companyModel = new CompanyModel
            {
                Id = companyToUpdate.Id,
                Title = companyToUpdate.Title,
                FoundationYear = companyToUpdate.FoundationYear
            };

            string jsonCompany = JsonConvert.SerializeObject(companyModel);

            var httpClient = new HttpClient();
            var result = await httpClient.PostAsync("http://localhost:6000/api/companies/companyUpdated", new StringContent(jsonCompany));
            
            Console.WriteLine(DateTime.Now + ": company updated!");
            return Ok("Company updated!");
        }
    }
}