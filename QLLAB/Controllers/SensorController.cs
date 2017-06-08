using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Controllers
{   
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        [HttpGet]
        [Route("set/{deviceId}/{sensorId}/{active}")]
        public IActionResult Set(string deviceId, string sensorId, bool active)
        {
            return Ok($"{deviceId}/{sensorId}/{active}");
        }
    }
}
