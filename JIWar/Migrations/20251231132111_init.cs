using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Transactions");

            migrationBuilder.EnsureSchema(
                name: "Ratings");

            migrationBuilder.EnsureSchema(
                name: "Messaging");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Design");

            migrationBuilder.EnsureSchema(
                name: "RealEstate");

            migrationBuilder.EnsureSchema(
                name: "Analytics");

            migrationBuilder.EnsureSchema(
                name: "Property");

            migrationBuilder.EnsureSchema(
                name: "Requests");

            migrationBuilder.EnsureSchema(
                name: "Management");

            migrationBuilder.EnsureSchema(
                name: "Complaints");

            migrationBuilder.EnsureSchema(
                name: "Media");

            migrationBuilder.EnsureSchema(
                name: "UserData");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignRequests",
                schema: "Design",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    PreferredStyle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImageURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsForSaleEnhancement = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictPriceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgPricePerMeter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictPriceHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfilePicURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteriorDesigners",
                columns: table => new
                {
                    InteriorDesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    PortfolioURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteriorDesigners", x => x.InteriorDesignerID);
                    table.ForeignKey(
                        name: "FK_InteriorDesigners_Users_InteriorDesignerID",
                        column: x => x.InteriorDesignerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentPortfolio",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentPortfolio", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_InvestmentPortfolio_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notification_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatedType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paymentMethod = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payment_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    planTypeEnum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    statusEnum2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_PropertyOwners_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    planTypeEnum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    statusEnum2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    FinishType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    View = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAge = table.Column<int>(type: "int", nullable: false),
                    MostLikelyPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    FactorBreakdownJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuationHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignerProposals",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignRequestID = table.Column<int>(type: "int", nullable: false),
                    DesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstimatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDays = table.Column<int>(type: "int", nullable: false),
                    ProposalDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    SampleDesignURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignerProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignerProposals_DesignRequests_DesignRequestID",
                        column: x => x.DesignRequestID,
                        principalSchema: "Design",
                        principalTable: "DesignRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignerProposals_InteriorDesigners_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "InteriorDesigners",
                        principalColumn: "InteriorDesignerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "RealEstate",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LocationLat = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    LocationLang = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area_sqm = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    NumBedrooms = table.Column<int>(type: "int", nullable: true),
                    NumBathrooms = table.Column<int>(type: "int", nullable: true),
                    Tour360Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsAvaliable = table.Column<bool>(type: "bit", nullable: true),
                    statusEnum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingType = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyOwners_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "PropertyOwners",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                schema: "Messaging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    OfferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferID);
                    table.ForeignKey(
                        name: "FK_Offers_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Users_BuyerID",
                        column: x => x.BuyerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioProperties",
                columns: table => new
                {
                    PortfolioPropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    OriginalPurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedROI = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProperties", x => x.PortfolioPropertyID);
                    table.ForeignKey(
                        name: "FK_PortfolioProperties_InvestmentPortfolio_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "InvestmentPortfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioProperties_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAnalytics",
                schema: "Analytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    FairValue_Estimate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price_Influence_Factors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalysisDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAnalytics_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeature",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeature", x => new { x.PropertyId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PropertyFeature_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFeature_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMedia",
                schema: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    MediaURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    mediaTypeEnum = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyMedia_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID");
                });

            migrationBuilder.CreateTable(
                name: "PropertyPriceHistories",
                columns: table => new
                {
                    PropertyPriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPriceHistories", x => x.PropertyPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_PropertyPriceHistories_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenovationProjects",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    EstimatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectStatusEnum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationProjects_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RenovationSimulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BudgetMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RenovationGoalsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationSimulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationSimulations_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    StatusEnumRequest = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VirtualTour",
                schema: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TourURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TourTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualTour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualTour_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VirtualTour_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                schema: "UserData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                schema: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferID = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Offers_OfferID",
                        column: x => x.OfferID,
                        principalTable: "Offers",
                        principalColumn: "OfferID");
                    table.ForeignKey(
                        name: "FK_Booking_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Users_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenovationCost",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationProjectID = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationCost_RenovationProjects_RenovationProjectID",
                        column: x => x.RenovationProjectID,
                        principalSchema: "Management",
                        principalTable: "RenovationProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearBuilt = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationDetails_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationMedias_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAIGenerated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationRecommendations_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designs",
                schema: "Design",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteriorDesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ProposalID = table.Column<int>(type: "int", nullable: true),
                    ImageURLs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AI_Generated = table.Column<bool>(type: "bit", nullable: false),
                    SelectedStyle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designs_DesignerProposals_ProposalID",
                        column: x => x.ProposalID,
                        principalSchema: "dbo",
                        principalTable: "DesignerProposals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Designs_InteriorDesigners_InteriorDesignerID",
                        column: x => x.InteriorDesignerID,
                        principalTable: "InteriorDesigners",
                        principalColumn: "InteriorDesignerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Designs_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Designs_Requests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "Transactions",
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                schema: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    OfferDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PriceEstimate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    InteriorDesignerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusEnumReq = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_InteriorDesigners_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "InteriorDesigners",
                        principalColumn: "InteriorDesignerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposals_Requests_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "Transactions",
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRatings",
                schema: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRatings_InteriorDesigners_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "InteriorDesigners",
                        principalColumn: "InteriorDesignerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestRatings_Requests_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "Transactions",
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRatings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingPayments_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Transactions",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPayments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingRatings",
                schema: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRatings_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Transactions",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRatings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesignerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BookingID = table.Column<int>(type: "int", nullable: true),
                    ComplaintText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaint_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Transactions",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaint_InteriorDesigners_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "InteriorDesigners",
                        principalColumn: "InteriorDesignerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaint_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: true),
                    ReportType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Transactions",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportOrders_Reports_ReportID",
                        column: x => x.ReportID,
                        principalSchema: "Complaints",
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportOrders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CustomerID",
                schema: "Transactions",
                table: "Booking",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_OfferID",
                schema: "Transactions",
                table: "Booking",
                column: "OfferID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PropertyID",
                schema: "Transactions",
                table: "Booking",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPayments_BookingID",
                table: "BookingPayments",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPayments_UserID",
                table: "BookingPayments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRatings_BookingID",
                schema: "Ratings",
                table: "BookingRatings",
                column: "BookingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRatings_UserID",
                schema: "Ratings",
                table: "BookingRatings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_PropertyID",
                schema: "Messaging",
                table: "Chat",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ReceiverID",
                schema: "Messaging",
                table: "Chat",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_SenderID",
                schema: "Messaging",
                table: "Chat",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_BookingID",
                table: "Complaint",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_DesignerID",
                table: "Complaint",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_UserID",
                table: "Complaint",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerProposals_DesignerID",
                schema: "dbo",
                table: "DesignerProposals",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerProposals_DesignRequestID",
                schema: "dbo",
                table: "DesignerProposals",
                column: "DesignRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_InteriorDesignerID",
                schema: "Design",
                table: "Designs",
                column: "InteriorDesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_PropertyID",
                schema: "Design",
                table: "Designs",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_ProposalID",
                schema: "Design",
                table: "Designs",
                column: "ProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_RequestId",
                schema: "Design",
                table: "Designs",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentPortfolio_UserID",
                table: "InvestmentPortfolio",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BuyerID",
                table: "Offers",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PropertyID",
                table: "Offers",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserID",
                table: "Payment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProperties_PortfolioID",
                table: "PortfolioProperties",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProperties_PropertyID",
                table: "PortfolioProperties",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerID",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerUserId",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAnalytics_AnalysisDate",
                schema: "Analytics",
                table: "PropertyAnalytics",
                column: "AnalysisDate");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAnalytics_PropertyID",
                schema: "Analytics",
                table: "PropertyAnalytics",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_FeatureId",
                table: "PropertyFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMedia_PropertyID",
                schema: "Property",
                table: "PropertyMedia",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceHistories_PropertyID",
                table: "PropertyPriceHistories",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_DesignerID",
                schema: "Requests",
                table: "Proposals",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_RequestID",
                schema: "Requests",
                table: "Proposals",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationCost_RenovationProjectID",
                schema: "Management",
                table: "RenovationCost",
                column: "RenovationProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationProjects_PropertyID",
                schema: "Management",
                table: "RenovationProjects",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationSimulations_PropertyID",
                table: "RenovationSimulations",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOrders_ReportID",
                table: "ReportOrders",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOrders_UserID",
                table: "ReportOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_BookingID",
                schema: "Complaints",
                table: "Reports",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserID",
                schema: "Complaints",
                table: "Reports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRatings_DesignerID",
                schema: "Ratings",
                table: "RequestRatings",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRatings_RequestID",
                schema: "Ratings",
                table: "RequestRatings",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRatings_UserID",
                schema: "Ratings",
                table: "RequestRatings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerID",
                schema: "Transactions",
                table: "Requests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_PropertyID",
                schema: "Transactions",
                table: "Requests",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails",
                column: "RenovationSimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationMedias_RenovationSimulationID",
                table: "SimulationMedias",
                column: "RenovationSimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRecommendations_RenovationSimulationID",
                table: "SimulationRecommendations",
                column: "RenovationSimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserID",
                table: "Subscriptions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ValuationHistories_UserId",
                table: "ValuationHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTour_PropertyID",
                schema: "Media",
                table: "VirtualTour",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTour_UserID",
                schema: "Media",
                table: "VirtualTour",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_PropertyID",
                schema: "UserData",
                table: "WishList",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserID",
                schema: "UserData",
                table: "WishList",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingPayments");

            migrationBuilder.DropTable(
                name: "BookingRatings",
                schema: "Ratings");

            migrationBuilder.DropTable(
                name: "Chat",
                schema: "Messaging");

            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "Designs",
                schema: "Design");

            migrationBuilder.DropTable(
                name: "DistrictPriceHistories");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PortfolioProperties");

            migrationBuilder.DropTable(
                name: "PropertyAnalytics",
                schema: "Analytics");

            migrationBuilder.DropTable(
                name: "PropertyFeature");

            migrationBuilder.DropTable(
                name: "PropertyMedia",
                schema: "Property");

            migrationBuilder.DropTable(
                name: "PropertyPriceHistories");

            migrationBuilder.DropTable(
                name: "Proposals",
                schema: "Requests");

            migrationBuilder.DropTable(
                name: "RenovationCost",
                schema: "Management");

            migrationBuilder.DropTable(
                name: "ReportOrders");

            migrationBuilder.DropTable(
                name: "RequestRatings",
                schema: "Ratings");

            migrationBuilder.DropTable(
                name: "SimulationDetails");

            migrationBuilder.DropTable(
                name: "SimulationMedias");

            migrationBuilder.DropTable(
                name: "SimulationRecommendations");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "ValuationHistories");

            migrationBuilder.DropTable(
                name: "VirtualTour",
                schema: "Media");

            migrationBuilder.DropTable(
                name: "WishList",
                schema: "UserData");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DesignerProposals",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InvestmentPortfolio");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "RenovationProjects",
                schema: "Management");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Complaints");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "Transactions");

            migrationBuilder.DropTable(
                name: "RenovationSimulations");

            migrationBuilder.DropTable(
                name: "DesignRequests",
                schema: "Design");

            migrationBuilder.DropTable(
                name: "InteriorDesigners");

            migrationBuilder.DropTable(
                name: "Booking",
                schema: "Transactions");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "RealEstate");

            migrationBuilder.DropTable(
                name: "PropertyOwners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
