using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Workstation.Context;
using Workstation.Utils;

namespace Workstation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AddServices(services);
            services.AddCustomJwtAuth();

            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseNpgsql(Configuration["DbConnectionString"],
                    c => c.MigrationsAssembly("Workstation"));
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("project", new OpenApiInfo {Title = "Курсовой проект", Version = "v1"});
                opt.AddDefaultOptions();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(opt => { opt.RouteTemplate = "api/{documentName}/swagger/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/project/swagger/swagger.json", "Курсовой проект");
                options.RoutePrefix = "api/help";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddServices(IServiceCollection collection)
        {
        }
    }
}