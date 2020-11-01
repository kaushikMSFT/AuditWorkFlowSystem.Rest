using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Client.Services;
using ClientAPI.BuildingBlocks.Handlers;
using ClientAPI.BuildingBlocks.Queries;
using ClientAPI.Config;
using ClientAPI.Contracts;
using ClientAPI.Domain;
using ClientAPI.EventSubscriptions;
using ClientAPI.MessageProcessors;
using ClientAPI.Persistence;
using ClientAPI.Persistence.UnitOfWork;
using ClientAPI.Services;
using ClientAPI.UnitOfWork;
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

namespace ClientService
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
            services.AddDbContext<ClientDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

            services.AddSingleton<IClientUnitOfWork, ClientAPI.UnitOfWork.UnitOfWork>();
            services.AddSingleton<IRepository<ClientProfile>, ClientProfileRepository>();
            services.AddSingleton<IProcessMessage, MessageProcessor>();
           services.AddSingleton<IMessageBusSubscription, ServiceBusTopicSubscription>();

            services.AddSingleton<IProcessMessage, MessageProcessor>();
            services.AddHostedService<ClientSubscription>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
          
            services.AddTransient<IRequestHandler<GetAllAuditPortfoliosQuery, List<AuditPortfolioCreationResponse>>, GetAllAuditPortfoliosQueryHandler>();
            services.AddScoped<IAuditPortfolioService, AuditPortfolioService>();
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
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4201").AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.WithOrigins("http://localhost:4201").AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            //var busSubscription =   app.ApplicationServices.GetService<IMessageBusSubscription>();
           // busSubscription.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
