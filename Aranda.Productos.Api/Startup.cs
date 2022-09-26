using Aranda.Productos.Api.Exceptions;
using Aranda.Productos.Apì.Interfaces;
using Aranda.Productos.Api.Mapper;
using Aranda.Productos.Api.Utilities;
using Aranda.Productos.BusinessLogic.Dependency;
using Aranda.Productos.BusinessLogic.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Productos.Api
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
            
            services.AddSingleton(GetMapper());

            AdditionalConfigureServices(services);

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aranda Producto API",
                    Version = "v1",
                    Description = "Evaluacion Tecnica Aranda",
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo Carrillo Blanbquicett",
                        Email = "jsp.tavo@hotmail.com"
                    }
                });
                
            });

          

            services.AddControllers();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aranda Producto API"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ApplyDbMigrationsLogic.Apply(app.ApplicationServices.CreateScope().ServiceProvider);
        }

        public virtual void AdditionalConfigureServices(IServiceCollection services)
        {
            // Inject BusinessLogic & Repository
            InjectBusinessLogic.Inject(services, Configuration);

            services.AddSingleton<IGuardarArchivo, GuardarArchivo>();
            services.AddHttpContextAccessor();
        }

        public virtual IMapper GetMapper()
        {
            var map = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMapper());
                mc.AddProfile(new CoreMapper());
            });
            return map.CreateMapper();
        }


    }
}
