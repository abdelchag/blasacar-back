using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using blasa.tarvel.DependencyInjectionContainer;
//using blasa.tarvel.DependencyInjectionContainer;
using blasa.travel.web.Mapping;
using blasa.travel.web.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Tools.Email;

namespace blasa.travel.web
{
    public class Startup
    {
        private string message;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddMvc(options =>
            //{
            //  //  options.Filters.Add(typeof(DomainExceptionFilter));
            //    options.Filters.Add(typeof(ValidateModelAttribute));
            //});
            services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Logger From Invalid Model");
            var allErrors = context.ModelState.Values.SelectMany(
                v => v.Errors.Select(b => new Error { message = b.ErrorMessage }))
                             .ToList<Error>();
            logger.LogError(JsonConvert.SerializeObject(allErrors));
            var result = new BadRequestObjectResult(allErrors);
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            return result;
        };

    });
            #region Services application

            //services.Add(new ServiceDescriptor(typeof(IResponse<User>), new Response<User>()));
            //services.Add(new ServiceDescriptor(typeof(IGenericCommandAsync), new Token()));
            //services.Add(new ServiceDescriptor(typeof(IGenericCommandAsync<Travel>), new GenericCommandAsync<Travel>(GenericRepositoryAsync<Travel>)));
            services.AddTransient<IEmailSender, Models.EmailSender>();

            //dependency  injection of Infrastructure
            //services.AddScoped<IGenericCommandAsync<Travel>, GenericCommandAsync<Travel>>();
            //services.AddScoped<IUserCommandAsync, UserCommandAsync>();
            AddPersistence(services, Configuration);



            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region AutoMapper           

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            #endregion


            #region Swagger

            //services.AddSwaggerGen(options =>
            //{
            //    services.AddSwaggerGen(c =>
            //{
            //    c.IncludeXmlComments(string.Format(@"{0}\blasa-travel.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "blasa-travel",
            //    });             
            //});

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\blasa-travel.xml", System.AppDomain.CurrentDomain.BaseDirectory));

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "blasa-travel",
                });
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
            #endregion

            #region DBcontext

            //services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion



            #region API Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
        {
            // Specify the default API Version as 1.0
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // If the client hasn't specified the API version in the request, use the default API version number 
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });
            #endregion


            ////dependency  injection of Application layer
            //services.AddApplication();
            // For Identity  

            //services.AddIdentity<User, IdentityRole>(opt =>
            //{
            //    opt.Password.RequiredLength = 8;
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    //opt.User.RequireUniqueEmail = true;
            //    opt.User.RequireUniqueEmail = false;



            //    //opt.SignIn.RequireConfirmedEmail = false;
            //})
            //  //services.AddIdentity<ApplicationUser, IdentityRole>()

            //  .AddEntityFrameworkStores<ApplicationDbContext>()
            // .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidAudience = Configuration["JWT:ValidAudience"],
                    //ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            // It should be one of your very first registrations
            //app.UseExceptionHandler("/error"); // Add this
            // Hook in the global error-handling middleware

            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //    var exception = exceptionHandlerPathFeature.Error;

            //    var result = JsonConvert.SerializeObject(new { error = exception.Message });
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(result);
            //}));
            app.UseSerilogRequestLogging();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();




            // Register any middleware to report exceptions to a third-party service *after* our ErrorHandlingMiddleware
            //app.UseExcepticon();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "blasa-travel");
            });
            #endregion
        }
        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjectionContainer.RegisterServices(services, configuration);
        }
    }
}
