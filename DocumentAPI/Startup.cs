using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditorAPI.Config;
using DocumentAPI.Factories;
using DocumentAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DocumentService
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
            services.AddSwaggerGen(
                x => {
                    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "IdentityService", Version = "v1" });
                    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

                });
            services.AddSingleton<IStorageConnectionFactory, StorageConnectionFactory>(sp =>
            {
                CloudStorageOptionsDTO cloudStorageOptionsDTO = new CloudStorageOptionsDTO();
                cloudStorageOptionsDTO.ConnectionString = Configuration["AzureBlobStorage:ConnectionString"];
                cloudStorageOptionsDTO.DocumentsContainer = Configuration["AzureBlobStorage:BlobContainer"];
                return new StorageConnectionFactory(cloudStorageOptionsDTO);

            });
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200").AllowAnyHeader());
            });
            services.AddScoped<DocumentAPI.Services.IDocumentUpload, CloudUploadService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            SwaggerSettings swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            app.UseSwagger(setting => { setting.RouteTemplate = swaggerSettings.JsonRoute; });
            app.UseSwaggerUI(setting => { setting.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description); });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyHeader());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
