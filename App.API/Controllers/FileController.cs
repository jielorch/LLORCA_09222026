using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("Hello");
        }
    }
}
