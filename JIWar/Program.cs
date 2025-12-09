using GEWAR;
using GEWAR.Configurations;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Account;
using Jiwar.Account.DTOs;
using Jiwar.Account.Services;
using Jiwar.Controllers;
using Jiwar.Models;
using Jiwar.Models.Offers;
using Jiwar.Repositories;
using Jiwar.Repositories.DistrictAnalyticService;
using Jiwar.Repositories.Interfaces;
using Jiwar.Repositories.Valuation;
using Jiwar.Service;
using Jiwar.Services;
using Jiwar.Services.ValuationService;
using JIWAR.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace Jiwar
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            // Database
            builder.Services.AddDbContext<GiwarContext>(options =>
                 options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()
                   ));
            // Authentication & Identity
            var key = builder.Configuration["Jwt:Key"];
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddIdentity<User, IdentityRole>()
           .AddEntityFrameworkStores<GiwarContext>()
            .AddDefaultTokenProviders();

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

            // Services
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<TokenService>();


            builder.Services.AddScoped<IPropertyAnalyticsService, PropertyAnalyticsService>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAdminAnalyticsService, AdminAnalyticsService>();
            builder.Services.AddScoped<IValuationHistoryService, ValuationHistoryService>();




            // Build App
            var app = builder.Build();
            // Middleware Pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {              
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            //Authorization: Bearer <token>
            app.MapControllers();
            app.Run();
        }
    }

}
