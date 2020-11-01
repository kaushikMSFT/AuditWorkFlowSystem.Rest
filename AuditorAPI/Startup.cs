using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AuditAPI.BuildingBlocks.Handlers;
using AuditorAPI.BuildingBlocks.Commands;
using AuditorAPI.BuildingBlocks.Handlers;
using AuditorAPI.BuildingBlocks.Queries;
using AuditorAPI.Config;
using AuditorAPI.Contracts;
using AuditorAPI.Domain;
using AuditorAPI.EventConsumers;
using AuditorAPI.Persistence;
using AuditorAPI.Services;
using AuditorAPI.UnitOfWork;
using MassTransit;
using MediatR;
using Messaging.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AuditorAPI
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
            services.AddDbContext<AuditDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AuditorProfileEventConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host("rabbitmq://localhost");
                    config.ReceiveEndpoint("Auditor-Topic", endpoint =>
                    {
                        endpoint.PrefetchCount = 10;
                        endpoint.ConfigureConsumer<AuditorProfileEventConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
            JWTSettings jwtSettings = new JWTSettings();
            Configuration.Bind(nameof(JWTSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });



            services.AddSwaggerGen(
                x => {
                    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "AuditorAPI", Version = "v1" });
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
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200").AllowAnyHeader());
            });
            services.AddScoped<IAuditUnitOfWork, AuditorAPI.UnitOfWork.UnitOfWork>();
            services.AddScoped<IRepository<AuditPortfolio>, AuditPortfolioRepository>();
            services.AddScoped<IAuditPortfolioService, AuditPortfolioService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<CreateAuditPortfolioCommand, AuditPortfolioCreationResponse>, CreateAuditPortfolioCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllAuditPortfoliosQuery, List<AuditPortfolioCreationResponse>>, GetAllAuditPortfoliosQueryHandler>();
            services.AddScoped<MessageProducer>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
