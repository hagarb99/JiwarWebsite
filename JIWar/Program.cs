using GEWAR;
using GEWAR.Configurations;
using GEWAR.Models;
using GEWAR.Models.Configurations;
using Jiwar.Account;
using Jiwar.Account.DTOs;
using Jiwar.Account.Services;
using Jiwar.Models;
using Jiwar.Models.Offers;
using Jiwar.Repositories;
using JIWAR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



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

            builder.Services.AddDbContext<GiwarContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<GiwarContext>()
             .AddDefaultTokenProviders();
           builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            // Create roles once at startup
            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    foreach (var role in System.Enum.GetNames(typeof(UserTypeEnum)))
            //    {
            //        if (!await roleManager.RoleExistsAsync(role))
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //        }
            //    }
            //}
            //await CreateRoles();

            app.Run();
        }
    }
}
