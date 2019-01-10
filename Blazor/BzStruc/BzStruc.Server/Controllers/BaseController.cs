﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BzStruc.Server.Controllers
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
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
    }
}