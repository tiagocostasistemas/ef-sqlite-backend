using Backend.EF.Sqlite.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.EF.Sqlite.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet("{zipcode}")]
        public async Task<IActionResult> GetByZipcodeAsync([FromServices] IViaCepService service,
                                                           [FromRoute] int zipcode)
        {
            var addreess = await service.GetByZipcode(zipcode);
            return Ok(addreess);
        }
    }
}
