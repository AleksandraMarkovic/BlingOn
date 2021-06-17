using AutoMapper;
using blingOn.Api.Core;
using blingOn.Application;
using blingOn.Application.Email;
using blingOn.Application.Interfaces;
using blingOn.DataAccess;
using blingOn.Implementation;
using blingOn.Implementation.Commands;
using blingOn.Implementation.Email;
using blingOn.Implementation.Logging;
using blingOn.Implementation.Queries;
using blingOn.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Api
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

            var appSettings = new AppSettings();

            Configuration.Bind(appSettings);

            services.AddControllers();
            services.AddTransient<UseCaseExecutor>();
            services.AddDbContext<BlingOnContext>();
            services.AddTransient<GetProductsQuery>();
            services.AddTransient<GetBrandsQuery>();
            services.AddTransient<GetOrdersQuery>();
            services.AddTransient<GetUsersQuery>();
            services.AddTransient<GetLogsQuery>();
            services.AddTransient<CreateUserCommand>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserCommand>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<DeleteUserCommand>();
            services.AddTransient<CreateBrandCommand>();
            services.AddTransient<CreateBrandValidator>();
            services.AddTransient<UpdateBrandCommand>();
            services.AddTransient<UpdateBrandValidator>();
            services.AddTransient<DeleteBrandCommand>();
            services.AddTransient<CreateSizeCommand>();
            services.AddTransient<CreateSizeValidator>();
            services.AddTransient<DeleteSizeCommand>();
            services.AddTransient<CreateProductCommand>();
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductCommand>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<DeleteProductCommand>();
            services.AddTransient<CreateOrderCommand>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<UpdateOrderCommand>();
            services.AddTransient<UpdateOrderValidator>();
            services.AddTransient<DeleteOrderCommand>();
            services.AddTransient<RateCommand>();
            services.AddTransient<RateValidator>();
            services.AddHttpContextAccessor();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<IEmailSender>(x => new SmtpEmailSender(appSettings.EmailFrom, appSettings.EmailPassword));
            services.AddAutoMapper(typeof(GetProductsQuery).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });

            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;

                if (user.FindFirst("UserData") == null)
                {
                    return new UnregisteredUser();
                }

                var userString = user.FindFirst("UserData").Value;

                var userJwt = JsonConvert.DeserializeObject<JwtUser>(userString);

                return userJwt;
            });

            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<BlingOnContext>();

                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseMiddleware<GlobalException>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
