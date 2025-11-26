using Microsoft.AspNetCore.Mvc;

namespace SAConstruction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase {

        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        public TestController(IConfiguration config)
        {
            _config = config;
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("testConnection")]
        public object TestConnection()
        {

            DateTime sqlTime = _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            // return both so you can verify env vars work
            return new {
                SqlTime = sqlTime,
            };
        }

        [HttpGet("finna")]
        public string[] Finna()
        {

            // here print out a user secret...
            string finna = _config["finna"];


            string[] stringArray = {
                "Test 1",
                "Test 2",
                $"Finna secret : {finna}"
            };

            return stringArray;
        }
    }
}