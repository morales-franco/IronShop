﻿using AutoMapper;
using IronShop.Api.Core;
using IronShop.Api.Core.Common;
using IronShop.Api.Core.IServices;
using IronShop.Api.Core.Services;
using IronShop.Api.Data;
using IronShop.Api.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IronShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container., IHostingEnvironment env
        public void ConfigureServices(IServiceCollection services)
        {
            //enable cors
            services.AddCors();

            //Configuramos Json Web tokens

            /*
             * Register our Authentication Schema --> We use JWT: JwtBearerDefaults.AuthenticationScheme
             * Configurer our Authentication Schema --> We specify needed parameters for consider a token valid
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, //validate the server that created that token
                        ValidateAudience = true, //ensure that the recipient of the token is authorized to receive it
                        ValidateLifetime = true, //check that the token is not expired
                        ValidateIssuerSigningKey = true, //verify that the key used to sign the incoming token is part of a list of trusted keys
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProductModule_AllowedRoles",
                    policy => policy.RequireRole(((int)eRole.Admin).ToString(), ((int)eRole.ProductManager).ToString()));

                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole(((int)eRole.Admin).ToString()));

                options.AddPolicy("ElevatedRightsOfStore",
                    policy => policy.RequireRole(((int)eRole.Admin).ToString(),
                                                 ((int)eRole.SalesManager).ToString()));

                options.AddPolicy("LowRightsOfStore",
                   policy => policy.RequireRole(((int)eRole.Employee).ToString()));


            });

            services.AddAutoMapper();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DbConnection")));

            /*
             * https://stackoverflow.com/questions/38138100/what-is-the-difference-between-services-addtransient-service-addscoped-and-serv
             * Transient objects are always different; a new instance is provided to every controller and every service.
             * Scoped objects are the same within a request, but different across different requests
             * Singleton objects are the same for every object and every request (regardless of whether an instance is provided in ConfigureServices)
             */

            //Enable access Http Context out controller (in our case UserService)
            services.AddHttpContextAccessor();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddTransient<IronSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Custom middleware por exceptions
            app.UseIronExceptionHandler();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //Habilitamos Auth --> Before MVC!
            app.UseAuthentication();

            //Confiramos Cors
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
            );

            app.UseMvc();
        }
    }
}
