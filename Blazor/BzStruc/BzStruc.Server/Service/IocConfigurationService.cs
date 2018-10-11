using BzStruc.Facade.Implement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzStruc.Server.Service
{
    public static class ServiceExtensions
    {
        public static void AddIoc(this IServiceCollection services/*, IConfiguration configuration, IHostingEnvironment hostingEnvironment*/)
        { 
            #region Transient

            //services.AddTransient<IEmailSender, EmailSender>();

            #endregion


            #region Scoped

            //services.AddScoped(typeof(IEntityFrameworkRepository<,>), typeof(EntityFrameworkRepository<,>));
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInterlocutorFacade, InterlocutorFacade>();
            #endregion


            #region Singleton

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

            //services.AddSingleton<UrlRedirectRule>();

            #endregion
        }

    }
 
}
