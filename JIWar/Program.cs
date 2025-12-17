using AutoMapper;
using GEWAR;
using GEWAR.Configurations;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Account;
using Jiwar.Account.DTOs;
using Jiwar.Account.Services;
using Jiwar.Controllers;
using Jiwar.Mappings;
using Jiwar.Models;
using Jiwar.Models.Offers;
using Jiwar.Repositories;
using Jiwar.Repositories;
using Jiwar.Repositories.DistrictAnalyticService;
using Jiwar.Repositories.Interfaces;
using Jiwar.Repositories.Valuation;
using Jiwar.Service;
using Jiwar.Services;
using Jiwar.Services.DesignerProposalService;
using Jiwar.Services.GoogleService;
using Jiwar.Services.ValuationService;
using JIWAR.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Jiwar
{
    public class Program
    {

        //public static async Task SeedRolesAsync(IApplicationBuilder app)
        //{
        //    using var scope = app.ApplicationServices.CreateScope();
        //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    string[] roles = { "Customer", "PropertyOwner", "InteriorDesigner", "Admin" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //        }
        //    }
        //}

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });



            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            // Database
            builder.Services.AddDbContext<GiwarContext>(options =>
                 options.UseLazyLoadingProxies().UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                   ));

            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<GiwarContext>()
            .AddDefaultTokenProviders();
            // Authentication & Identity
            var key = builder.Configuration["Jwt:Key"];
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = secKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            


//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //Dependency Injection for Repositories and Services
            // Generic
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Repositories
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportOrderRepository, ReportOrderRepository>();
            builder.Services.AddScoped<IBookingPaymentRepository, BookingPaymentRepository>();
            builder.Services.AddScoped<IValuationHistoryRepository, ValuationHistoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IAdminAnalyticsService, AdminAnalyticsService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IValuationHistoryRepository, ValuationHistoryRepository>();
            builder.Services.AddScoped<IBookingPaymentRepository, BookingPaymentRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IReportOrderRepository, ReportOrderRepository>();


            // Services
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPaymentService, PaymobPaymentService>();
            builder.Services.AddScoped<IBookingService, BookingService>();  // ← المهم
                                                                            // Replace this line:
                                                                            // builder.Services.AddScoped<DesignerService, IDesignerService>();  

            // With this corrected line:
            builder.Services.AddScoped<IDesignerProposalService, DesignerProposalService>();
           
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<GoogleAuthService>();

            builder.Services.AddScoped<TokenService>();


            builder.Services.AddScoped<IPropertyAnalyticsService, PropertyAnalyticsService>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAdminAnalyticsService, AdminAnalyticsService>();
            builder.Services.AddScoped<IValuationHistoryService, ValuationHistoryService>();
            builder.Services.AddAutoMapper(cfg => {  cfg.AddProfile<MappingProfile>();});




            // Build App
            var app = builder.Build();
            //await SeedRolesAsync(app);
            // Middleware Pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {              
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //Authorization: Bearer <token>
            app.MapControllers();
            app.Run();
        }
    }

}
