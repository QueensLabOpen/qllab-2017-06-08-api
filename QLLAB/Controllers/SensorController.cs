using Microsoft.AspNetCore.Mvc;

namespace QLLAB.Controllers
{   
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        [HttpGet]
        [Route("set/{deviceId}/{sensorId}/{active}")]
        public IActionResult Set(string deviceId, string sensorId, string value)
        {
            return Ok($"{deviceId}/{sensorId}/{value}");
        }
    }
}
