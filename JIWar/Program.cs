using AutoMapper;
using GEWAR;
using GEWAR.Models;
using Jiwar.Account;
using Jiwar.Account.Services;
using Jiwar.Controllers;
using Jiwar.Hubs;
using Jiwar.Hubs;
using Jiwar.Mappings;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.Repositories.ChatAi;
using Jiwar.Repositories.DistrictAnalyticService;
using Jiwar.Repositories.Interfaces;
using Jiwar.Repositories.SimulationChatAI;
using Jiwar.Repositories.Valuation;
using Jiwar.Service;
using Jiwar.Services;
using Jiwar.Services.AI;
using Jiwar.Services.AI.Chat;
using Jiwar.Services.AI.Comparison;
using Jiwar.Services.DesignerProposalService;
using Jiwar.Services.DesignRequestService;
using Jiwar.Services.DesignService;
using Jiwar.Services.GoogleService;
using Jiwar.Services.MailService;
using Jiwar.Services.NotificationService; // Ensure namespace is available
using Jiwar.Services.ProposalService;
using Jiwar.Services.RequestService;
using Jiwar.Services.ValuationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using static Jiwar.Services.AI.Comparison.IPropertyComparisonAiService;
using IPropertyComparisonAiService = Jiwar.Services.AI.Comparison.IPropertyComparisonAiService;
using Microsoft.AspNetCore.SignalR;

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

            // Controllers & Swagger
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
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
                    Description = "Enter 'Bearer' followed by your token in the text box below."
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


            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                          .AllowAnyHeader()
                     .AllowCredentials();
                });
            });

            // Database
            builder.Services.AddDbContext<GiwarContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GiwarContext>()
                .AddDefaultTokenProviders();

            // Authentication

            var key = builder.Configuration["Jwt:Key"] ?? "vY7fG9pQ2zR5xW8mK3nB1vC4xZ6mN9bV"; // Default fallback for development
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/notificationHub", StringComparison.OrdinalIgnoreCase) ||
                                 path.StartsWithSegments("/chathub", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingPaymentRepository, BookingPaymentRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportOrderRepository, ReportOrderRepository>();
            builder.Services.AddScoped<IValuationHistoryRepository, ValuationHistoryRepository>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IRenovationSimulationRepository, RenovationSimulationRepository>();
            builder.Services.AddScoped<ISimulationChatRepository, SimulationChatRepository>();
            builder.Services.AddScoped<IQuotaRepository, QuotaRepository>();



            // Services
            builder.Services.AddHttpClient<IAiService, OpenAiService>();
            builder.Services.AddScoped<IAiService, OpenAiService>();
            builder.Services.AddHttpClient(); // Registers IHttpClientFactory
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IImgService, ImgService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IPaymentService, PaymobPaymentService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IAdminAnalyticsService, AdminAnalyticsService>();
            builder.Services.AddScoped<IValuationHistoryService, ValuationHistoryService>();
            builder.Services.AddScoped<IPropertyAnalyticsService, PropertyAnalyticsService>();
            builder.Services.AddScoped<IDesignerProposalService, DesignerProposalService>();
            //builder.Services.AddScoped<IRenovationSimulationService, RenovationSimulationService>();
            // Add this line in your Program.cs
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped<IDesignRequestService, DesignRequestService>();
            builder.Services.AddScoped<IDesignService, DesignService>();
            builder.Services.AddScoped<IProposalService, ProposalService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<Jiwar.Services.ReviewService.IReviewService, Jiwar.Services.ReviewService.ReviewService>();


            builder.Services.AddScoped<IRenovationSimulationService, RenovationSimulationService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IImgService, ImgService>();
            builder.Services.AddScoped<IAiChatService, AiChatService>();
            builder.Services.AddScoped<IPropertyComparisonAiService, PropertyComparisonAiService>();



            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });

            // Other Services
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<GoogleAuthService>();
            builder.Services.AddScoped<INotificationService, NotificationService>(); // Register service
            
            // SignalR
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Build App
            var app = builder.Build();

            //await SeedRolesAsync(app);
            // Apply pending EF Core migrations at startup to ensure database schema is up-to-date
            //using (var scope = app.Services.CreateScope())
            //{
            //    try
            //    {
            //        var context = scope.ServiceProvider.GetRequiredService<GiwarContext>();
            //        context.Database.Migrate();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log or handle migration failures as needed. For brevity we rethrow here.
            //        throw;
            //    }
            //}

            // Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
           

                        app.UseEndpoints(endpoints =>
                        { 
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
            //app.MapControllers();
            app.Run();
        }
    }
}