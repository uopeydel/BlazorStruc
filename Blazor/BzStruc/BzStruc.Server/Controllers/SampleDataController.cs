using BzStruc.Facade.Implement;
using BzStruc.Facade.Interface;
using BzStruc.Repository.DAL;
using BzStruc.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BzStruc.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : BaseController
    {
        private readonly IConversationFacade _conversationFacade;
        public SampleDataController(IConversationFacade conversationFacade)
        {
            _conversationFacade = conversationFacade;
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
                Summary = ""
            });
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> GetConversationPreviewList([FromQuery]PagingParameters paging)
        {
             
            var result = await _conversationFacade.GetPaging(IdentityUser,paging);
            return Ok(result);

        }
         
    }
}
