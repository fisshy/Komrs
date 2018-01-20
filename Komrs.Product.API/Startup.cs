using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EventBus.RabbitMQ;
using RabbitMQ.Client;
using EventBus;
using Microsoft.AspNetCore.Http;
using MediatR;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.HealthChecks;
using Komrs.Product.Infrastructure;
using Storage;
using Storage.Azure;
using Swagger;

namespace Komrs.Product.API
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
            var connectionString = Configuration.GetValue<string>("ConnectionString");


            services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               })
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = Configuration["JwtIssuer"],
                       ValidAudience = Configuration["JwtIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            services.AddMvc();


            services.AddHealthChecks(checks =>
            {
                checks.AddSqlCheck("KomrsProductDB", Configuration["ConnectionString"]);
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Komrs Product", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

               // c.OperationFilter<FormFileOperationFilter>();

            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            ConfigureRabbitMQ(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IProductQueryRepository>((s) =>
            {
                var logger = s.GetService<ILogger<ProductQueryRepository>>();
                return new ProductQueryRepository(Configuration.GetValue<string>("ConnectionString"), logger);
            });

            services.AddSingleton<IProductRepository, ProductRepository>((s) =>
            {
                var logger = s.GetService<ILogger<ProductRepository>>();
                return new ProductRepository(Configuration.GetValue<string>("ConnectionString"), logger);
            });

            services.AddSingleton<IStorage, AzureStorage>((s) =>
            {
                return new AzureStorage(Configuration.GetValue<string>("StorageConnectionString"));
            });

            services.AddMvc();

            services.AddMediatR();

        }

        private void ConfigureRabbitMQ(IServiceCollection services)
        {
            services.AddSingleton<IPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<PersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                {
                    factory.UserName = Configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                {
                    factory.Password = Configuration["EventBusPassword"];
                }

                var retryCount = 5;

                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new PersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IBusClient, BusClient>(sp =>
            {
                var subscriptionClientName = Configuration["SubscriptionClientName"];

                var persitentConnection = sp.GetRequiredService<IPersistentConnection>();

                var logger = sp.GetRequiredService<ILogger<BusClient>>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new BusClient(persitentConnection, logger, subscriptionClientName, retryCount);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Komrs Product");
            });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
