using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IronShop.Api.Core;
using IronShop.Api.Core.IServices;
using IronShop.Api.Core.Services;
using IronShop.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IronShop.Api
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
