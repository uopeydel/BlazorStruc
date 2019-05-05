using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Web.Server.Controllers
{

    public class BaseController : Controller
    {
        protected int IdentityUser
        {
            get
            {
                if (User == null || User.Identity == null)
                {
                    return 0;
                }
                var x = User.Identity as ClaimsIdentity;
                var email = x.Claims.Where(w => w.Type == ClaimTypes.Email).Select(s => s.Value).FirstOrDefault();
                var userId = x.Claims.Where(w => w.Type == ClaimTypes.NameIdentifier).Select(s => s.Value).FirstOrDefault();
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(userId);
            }
        }


        protected string BaseApiUrl
        {
            get
            {
                var origin = Request.Headers.Where(w => w.Key.Equals("Origin")).Select(s => s.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(origin)/* && ConfigurationManager.AppSettings["corsOrigins"].Contains(origin)*/)
                {
                    return origin;
                }
                return string.Empty;
            }
        }

        public string GetIp()
        {
            return GetClientIp();
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            var remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }

    }
}
