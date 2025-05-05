using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentMangementSystemC8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("print-token")]
        public IActionResult PrintToken()
        {
            // Extract token from the Authorization header
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                Console.WriteLine($"Received Token: {token}");
                return Ok($"Token logged: {token}");
            }

            return BadRequest("No valid token found.");
        }
    }
}
