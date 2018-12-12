using BzStruc.Facade.Implement;
using BzStruc.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzStruc.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IInterlocutorFacade _interlocutorFacade;
        public SampleDataController(IInterlocutorFacade interlocutorFacade)
        {
            _interlocutorFacade = interlocutorFacade;
        }

        private static string[] Summaries = new[]
        {
            "Freezing a", "Bracing a", "Chilly b", "Cool b", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = _interlocutorFacade.AddA(Summaries[rng.Next(Summaries.Length)]).Result
            });
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(await _interlocutorFacade.AddA("ss"));

        }

        [AllowAnonymous]
        [HttpGet("testtask")]
        public async Task<IActionResult> testtask([FromQuery]int time, [FromQuery]int value)
        {
            await _interlocutorFacade.AddCTask(time, value);
            return Ok();
        }
    }
}
