using BzStruc.Facade.Implement;
using BzStruc.Facade.Interface;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Service.Implement;
using BzStruc.Repository.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.Service
{
    public static class ServiceExtensions
    {
        public static void AddIoc(this IServiceCollection services/*, IConfiguration configuration, IHostingEnvironment hostingEnvironment*/)
        {
            #region Transient
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            //services.AddTransient<IEmailSender, EmailSender>();

            #endregion


            #region Scoped

            services.AddScoped(typeof(IGenericEFRepository<>), typeof(GenericEFRepository<>));
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IConversationFacade, ConversationFacade>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountFacade, AccountFacade>();
            //services.AddScoped(typeof(SignInManager<>));
            //services.AddScoped(typeof(UserManager<>));
            #endregion


            #region Singleton

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton<UrlRedirectRule>();

            #endregion
        }

    }

}
