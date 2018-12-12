using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Server.Service;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;

namespace BzStruc.Server
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services/*, IConfiguration configuration, IHostingEnvironment hostingEnvironment*/)
        {
            services.AddDbContext<MsSql1DbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("MsSql1DbConnection")));

            services.AddIdentity<GenericUser, GenericRole>()
                .AddEntityFrameworkStores<MsSql1DbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddIoc(/*configuration, hostingEnvironment*/);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
