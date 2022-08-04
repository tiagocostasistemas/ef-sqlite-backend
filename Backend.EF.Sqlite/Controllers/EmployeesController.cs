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
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var employees = await context.Employees.Include(employee => employee.Company).ToListAsync();

            var employeesDTO = EmployeeMapper.Map(employees);

            return Ok(employeesDTO);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context,
                                                      [FromRoute] int id)
        {
            var employee = await context.Employees.Include(employee => employee.Company)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null) return NotFound();

            var employeeDTO = EmployeeMapper.Map(employee);

            return employeeDTO is null ? NotFound() : Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
                                                   [FromBody] CreateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var company = await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.CompanyId);

            if (company is null)
                return BadRequest();    

            var employee = new Employee()
            {
                Name = model.Name,
                Position = model.Position,
                Salary = model.Salary,
                CompanyId = company.Id
            };

            try
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return Created($"v1/employees/{employee.Id}", null);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
                                                  [FromBody] UpdateEmployeeViewModel model,
                                                  [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var employee = await context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
                return NotFound();

            employee.Name = model.Name;
            employee.Position = model.Position;
            employee.Salary = model.Salary;

            try
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
                return Ok(employee);
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
            var employee = await context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
                return NotFound();

            try
            {
                context.Employees.Remove(employee);
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
