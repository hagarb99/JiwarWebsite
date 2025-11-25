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
using Jiwar.Repositories.Interfaces;
using Jiwar.Service;
using Jiwar.Services;
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
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<GiwarContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));//aya
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();//aya


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
           builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();

         //i ADD IT ZEINAB SHAHAT (TO TRY TO RESOLVE IPropertyService)
         builder.Services.AddScoped<IPropertyService, IPropertyService>();

         builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
         builder.Services.AddScoped<ISubscriptionService, SubscriptionService>(); // commented to try to resolve IPropertyService




            builder.Services.AddScoped<TokenService>();
            var app = builder.Build();
 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                   

            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            //Authorization: Bearer <token>



            app.MapControllers();
            app.Run();
        }
    }

}
