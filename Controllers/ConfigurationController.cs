using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StudentMangementSystemC8.Settings;

namespace StudentMangementSystemC8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        public IConfiguration config;

        public UseAI useAi;

        public ConfigurationController(IConfiguration config, IOptionsMonitor<UseAI> optionsAI )
        {
            this.config = config;
            this.useAi = optionsAI.CurrentValue;
        }

        [HttpGet("useai")]
        public IActionResult FetchUseAI()
        {


            // return config["UseAI:name"];
            return Ok(useAi.APIKey);
        }
    }
}
