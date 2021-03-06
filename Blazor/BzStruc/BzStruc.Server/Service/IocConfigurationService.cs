﻿using BzStruc.Facade.Implement;
using BzStruc.Facade.Interface;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Service.Implement;
using BzStruc.Repository.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BzStruc.Server.Service
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
            #endregion


            #region Singleton

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton<UrlRedirectRule>();

            #endregion
        }

    }

}
