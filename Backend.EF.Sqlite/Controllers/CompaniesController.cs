using Backend.EF.Sqlite.Data;
using Backend.EF.Sqlite.Helpers.Mappers;
using Backend.EF.Sqlite.Models;
using Backend.EF.Sqlite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.EF.Sqlite.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var companies = await context.Companies.Include(company => company.Employees).ToListAsync();
            var companiesResponse = CompanyMapper.Map(companies);

            return Ok(companiesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context,
                                                      [FromRoute] int id)
        {
            var company = await context.Companies.Include(company => company.Employees)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (company is null) return NotFound();

            var companyResponse = CompanyMapper.Map(company);

            return companyResponse is null ? NotFound() : Ok(companyResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
                                                   [FromBody] CreateCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var company = new Company()
            {
                Name = model.Name,
                Street = model.Street,
                Number = model.Number,
                Complement = model.Complement,
                District = model.District,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                Phone = model.Phone,
            };

            try
            {
                await context.Companies.AddAsync(company);
                await context.SaveChangesAsync();
                return Created($"v1/companies/{company.Id}", company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
                                                  [FromBody] UpdateCompanyViewModel model,
                                                  [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var company = await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (company is null)
                return NotFound();

            company.Name = model.Name;
            company.Street = model.Street;
            company.Number = model.Number;
            company.Complement = model.Complement;
            company.District = model.District;
            company.City = model.City;
            company.State = model.State;
            company.ZipCode = model.ZipCode;
            company.Phone = model.Phone;

            try
            {
                context.Companies.Update(company);
                await context.SaveChangesAsync();
                return Ok(company);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context,                                                    
                                                     [FromRoute] int id)
        {
            var company = await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (company is null)
                return NotFound();

            try
            {
                context.Companies.Remove(company);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
