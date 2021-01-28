using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudioGhibliAPI.Context;
using StudioGhibliAPI.Repository;
using StudioGhibliAPI.Repository.IRepository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudioGhibliAPI.GhibliMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StudioGhibliAPI
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
            services.AddMemoryCache();
            services.AddDbContext<GhibliDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<IGhibliRepository, GhibliRepository>();

            services.AddAutoMapper(typeof(GhibliMapping));

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddControllers();

           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudioGhibliAPI", Version = "v1" });
               
            });

            services.AddHttpClient();

            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
               builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudioGhibliAPI v1"));
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORSPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
