using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Web.Server.Service;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        //[Route("env")]
        public IActionResult getEnviroment()
        { 
            bool debug = false;
#if DEBUG
            debug = true;
#endif
            return Ok(new
            {
                siteUrl = BaseApiUrl,
                debug,
                ip = GetIp(),
                buildAt = Singleton.Instance.GetStartupDateTime(),
                HttpContext.Request.Path,
                HttpContext.Request.Host
            });
        }
    }
}
