using AutoMapper;
using CardApplication.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Data;
using MySql.Data.MySqlClient;
using CardApplication.DataAccess.Interfaces;
using CardApplication.DataAccess.Repositories;
using CardApplication.Business.Interfaces;
using CardApplication.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using CardApplication.Api.Middlewares;
using FluentMigrator.Runner;
using CardApplication.DBMigrate.Migrations;

namespace CardApplication.Api.Extentions
{
    public static class ServiceConfigurationExtension
    {
        public static void AddAllServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("CardApplicationConnectionString");
            services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddMySql5()
                // Set the connection string
                .WithGlobalConnectionString(connString)
                // Define the assembly containing the migrations
                .ScanIn(typeof(M0001_CreateCardTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
            .BuildServiceProvider(true);

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication", null);
                
            services.AddControllers();

            /*  
                Singleton - only a single instance
                Transient - One per each call
                Scoped - One per each http call , same for internal calls
            */

            services.AddAutoMapper(typeof(MapperFunctions).Assembly);

            services.AddTransient<IDbConnection, MySqlConnection>(s => new MySqlConnection(connString));
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<ICardService, CardService>();

            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Card application api",
                    Version = "v1"
                });
            });

        }
    }
}
