using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using WebApplication1.API.Mapping;
using WebApplication1.API.RequestFilters;
using WebApplication1.API.Services;
using WebApplication1.Common;
using WebApplication1.EntityFramework;
using WebApplication1.IdentityServices;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Middleware;
using WebApplication1.Models.Common;
using WebApplication1.RequestFilters;
using WebApplication1.Services;
using WebApplication1.SignalR;

namespace WebApplication1.API
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
            services.AddCors();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddControllers((options) =>
            {
                options.Filters.Add<GlobalRequestFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WEB API",
                    Version = "v1",
                    Description = "Web API .net 6",
                    Contact = new OpenApiContact
                    {
                        Name = "Auhor",
                        Email = "email@email.com"
                    },
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                          },
                          new string[] {}

                    }
                });
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        serverOptions => serverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                        ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddEFServices();
            services.AddIdentityAndAdminServices();
            services.AddAutoMapperProfiles();
            services.AddSignalRServices();
            services.AddCommonServices();

            services.AddSingleton<IJwtUtils, JwtUtils>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IOperationContextFactory, OperationContextFactory>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return InvalidModelStateProcessor.ReturnBadRequestWithModelStateErrors(actionContext);
                };
            });

            services.AddSignalR();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IIdentitySeeder identitySeeder)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseRouting();


            // custom jwt auth middleware
            app.UseMiddleware<TokenMiddleware>();

            identitySeeder.SeedIdentity();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;    // do not hide jwt token errors
            }

            app.UseHttpsRedirection();


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.API v1"));


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                endpoints.MapHub<ConnectionHub>("/connectionhub");
            });
        }
    }
}
