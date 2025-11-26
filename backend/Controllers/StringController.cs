using Microsoft.AspNetCore.Mvc;
using SAConstruction.Services;
using System.Text.Json;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StringController : ControllerBase
    {


        private readonly StringService _stringService = new();

        [HttpGet("random")]

        public string randomStringJoint()
        {
            return _stringService.GetRandomString();
        }


        [HttpPost("mixed")]
        public IActionResult MixedStringJoint([FromBody] JsonElement body)
        {
            Console.WriteLine("RAW BODY:");
            Console.WriteLine(body.ToString());

            var result = _stringService.GetMixedString(body);
            return Ok(result);
        }


    }
}