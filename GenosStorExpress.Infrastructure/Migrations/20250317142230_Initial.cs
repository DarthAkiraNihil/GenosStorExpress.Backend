using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GenosStorExpress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "ActiveDiscounts",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankSystems",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates80Plus",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates80Plus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerCaseTypesizes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerCaseTypesizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoolerMaterials",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolerMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPUSocket",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUSocket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPIModes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DPI = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPIModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardTypesizes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardTypesizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatrixTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrixTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardFormFactors",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardFormFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PCIEVersions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCIEVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAMTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleComputerComponentTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleComputerComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Underlights",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Underlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VesaSizes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesaSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoPorts",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_AspNetUsers_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ImageBase64 = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ActiveDiscountId = table.Column<int>(type: "integer", nullable: true),
                    ItemTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ActiveDiscounts_ActiveDiscountId",
                        column: x => x.ActiveDiscountId,
                        principalSchema: "public",
                        principalTable: "ActiveDiscounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "public",
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimpleComputerComponents",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleComputerComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimpleComputerComponents_SimpleComputerComponentTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "public",
                        principalTable: "SimpleComputerComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUCores",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUCores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUCores_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "public",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUs_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "public",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "public",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Carts_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputerComponents",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TDP = table.Column<double>(type: "double precision", nullable: false),
                    VendorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerComponents_Items_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerComponents_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "public",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rating = table.Column<byte>(type: "smallint", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioChipsets",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioChipsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioChipsets_SimpleComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SimpleComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardChipsets",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardChipsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherboardChipsets_SimpleComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SimpleComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkAdapters",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkAdapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkAdapters_SimpleComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SimpleComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSDControllers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SSDControllers_SimpleComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SimpleComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "public",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.CartId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "public",
                        principalTable: "Carts",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CartId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "public",
                        principalTable: "Carts",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerCases",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TypesizeId = table.Column<long>(type: "bigint", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    HasARGBLighting = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerCases_ComputerCaseTypesizes_TypesizeId",
                        column: x => x.TypesizeId,
                        principalSchema: "public",
                        principalTable: "ComputerCaseTypesizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerCases_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUCoolers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    MaxFanRPM = table.Column<long>(type: "bigint", nullable: false),
                    TubesCount = table.Column<byte>(type: "smallint", nullable: false),
                    TubesDiameter = table.Column<float>(type: "real", nullable: false),
                    FanCount = table.Column<byte>(type: "smallint", nullable: false),
                    FoundationMaterialId = table.Column<long>(type: "bigint", nullable: false),
                    RadiatorMaterialId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUCoolers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUCoolers_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUCoolers_CoolerMaterials_FoundationMaterialId",
                        column: x => x.FoundationMaterialId,
                        principalSchema: "public",
                        principalTable: "CoolerMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUCoolers_CoolerMaterials_RadiatorMaterialId",
                        column: x => x.RadiatorMaterialId,
                        principalSchema: "public",
                        principalTable: "CoolerMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CoreId = table.Column<int>(type: "integer", nullable: false),
                    SocketId = table.Column<long>(type: "bigint", nullable: false),
                    CoresCount = table.Column<int>(type: "integer", nullable: false),
                    ThreadsCount = table.Column<int>(type: "integer", nullable: false),
                    L2CahceSize = table.Column<float>(type: "real", nullable: false),
                    L3CacheSize = table.Column<float>(type: "real", nullable: false),
                    TechnicalProcess = table.Column<float>(type: "real", nullable: false),
                    BaseFrequency = table.Column<float>(type: "real", nullable: false),
                    SupportedRAMSize = table.Column<int>(type: "integer", nullable: false),
                    HasIntegratedGraphics = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUs_CPUCores_CoreId",
                        column: x => x.CoreId,
                        principalSchema: "public",
                        principalTable: "CPUCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUs_CPUSocket_SocketId",
                        column: x => x.SocketId,
                        principalSchema: "public",
                        principalTable: "CPUSocket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUs_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiskDrives",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    ReadSpeed = table.Column<long>(type: "bigint", nullable: false),
                    WriteSpeed = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiskDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiskDrives_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Displays",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    MaxUpdateFrequency = table.Column<int>(type: "integer", nullable: false),
                    ScreenDiagonal = table.Column<double>(type: "double precision", nullable: false),
                    DefinitionId = table.Column<int>(type: "integer", nullable: false),
                    MatrixTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UnderlightId = table.Column<long>(type: "bigint", nullable: false),
                    VesaSizeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Displays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Displays_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Displays_Definitions_DefinitionId",
                        column: x => x.DefinitionId,
                        principalSchema: "public",
                        principalTable: "Definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Displays_MatrixTypes_MatrixTypeId",
                        column: x => x.MatrixTypeId,
                        principalSchema: "public",
                        principalTable: "MatrixTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Displays_Underlights_UnderlightId",
                        column: x => x.UnderlightId,
                        principalSchema: "public",
                        principalTable: "Underlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Displays_VesaSizes_VesaSizeId",
                        column: x => x.VesaSizeId,
                        principalSchema: "public",
                        principalTable: "VesaSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    VideoRAM = table.Column<int>(type: "integer", nullable: false),
                    MaxDisplaysSupported = table.Column<byte>(type: "smallint", nullable: false),
                    UsedSlots = table.Column<byte>(type: "smallint", nullable: false),
                    GPUId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraphicsCards_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCards_GPUs_GPUId",
                        column: x => x.GPUId,
                        principalSchema: "public",
                        principalTable: "GPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Keyboards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    HasRGBLighting = table.Column<bool>(type: "boolean", nullable: false),
                    IsWireless = table.Column<bool>(type: "boolean", nullable: false),
                    TypesizeId = table.Column<long>(type: "bigint", nullable: false),
                    KeyboardTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyboards_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keyboards_KeyboardType_KeyboardTypeId",
                        column: x => x.KeyboardTypeId,
                        principalSchema: "public",
                        principalTable: "KeyboardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keyboards_KeyboardTypesizes_TypesizeId",
                        column: x => x.TypesizeId,
                        principalSchema: "public",
                        principalTable: "KeyboardTypesizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ButtonsCount = table.Column<byte>(type: "smallint", nullable: false),
                    HasProgrammableButtons = table.Column<bool>(type: "boolean", nullable: false),
                    IsWireless = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mouses_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplies",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SataPorts = table.Column<byte>(type: "smallint", nullable: false),
                    MolexPorts = table.Column<byte>(type: "smallint", nullable: false),
                    PowerOutput = table.Column<int>(type: "integer", nullable: false),
                    Certificate80PlusId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_Certificates80Plus_Certificate80PlusId",
                        column: x => x.Certificate80PlusId,
                        principalSchema: "public",
                        principalTable: "Certificates80Plus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TotalSize = table.Column<int>(type: "integer", nullable: false),
                    ModuleSize = table.Column<int>(type: "integer", nullable: false),
                    ModulesCount = table.Column<byte>(type: "smallint", nullable: false),
                    Frequency = table.Column<int>(type: "integer", nullable: false),
                    CL = table.Column<byte>(type: "smallint", nullable: false),
                    tRCD = table.Column<byte>(type: "smallint", nullable: false),
                    tRP = table.Column<byte>(type: "smallint", nullable: false),
                    tRAS = table.Column<byte>(type: "smallint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMs_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RAMs_RAMTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "public",
                        principalTable: "RAMTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    RAMSlots = table.Column<byte>(type: "smallint", nullable: false),
                    RAMChannels = table.Column<byte>(type: "smallint", nullable: false),
                    MaxRAMFrequency = table.Column<int>(type: "integer", nullable: false),
                    PCIESlotsCount = table.Column<byte>(type: "smallint", nullable: false),
                    PCIEVersionId = table.Column<int>(type: "integer", nullable: false),
                    HasNVMeSupport = table.Column<bool>(type: "boolean", nullable: false),
                    M2SlotsCount = table.Column<byte>(type: "smallint", nullable: false),
                    SataPortsCount = table.Column<byte>(type: "smallint", nullable: false),
                    USBPortsCount = table.Column<byte>(type: "smallint", nullable: false),
                    RJ45PortsCount = table.Column<byte>(type: "smallint", nullable: false),
                    DigitalAudioPortsCount = table.Column<byte>(type: "smallint", nullable: false),
                    NetworkAdapterSpeed = table.Column<float>(type: "real", nullable: false),
                    FormFactorId = table.Column<long>(type: "bigint", nullable: false),
                    CPUSocketId = table.Column<long>(type: "bigint", nullable: false),
                    PCIEVersionId1 = table.Column<long>(type: "bigint", nullable: false),
                    MotherboardChipsetId = table.Column<long>(type: "bigint", nullable: false),
                    AudioChipsetId = table.Column<long>(type: "bigint", nullable: false),
                    NetworkAdapterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motherboards_AudioChipsets_AudioChipsetId",
                        column: x => x.AudioChipsetId,
                        principalSchema: "public",
                        principalTable: "AudioChipsets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_CPUSocket_CPUSocketId",
                        column: x => x.CPUSocketId,
                        principalSchema: "public",
                        principalTable: "CPUSocket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_ComputerComponents_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "ComputerComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_MotherboardChipsets_MotherboardChipsetId",
                        column: x => x.MotherboardChipsetId,
                        principalSchema: "public",
                        principalTable: "MotherboardChipsets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_MotherboardFormFactors_FormFactorId",
                        column: x => x.FormFactorId,
                        principalSchema: "public",
                        principalTable: "MotherboardFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_NetworkAdapters_NetworkAdapterId",
                        column: x => x.NetworkAdapterId,
                        principalSchema: "public",
                        principalTable: "NetworkAdapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_PCIEVersions_PCIEVersionId1",
                        column: x => x.PCIEVersionId1,
                        principalSchema: "public",
                        principalTable: "PCIEVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankCards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    ValidThruMonth = table.Column<byte>(type: "smallint", nullable: false),
                    ValidThruYear = table.Column<byte>(type: "smallint", nullable: false),
                    CVC = table.Column<byte>(type: "smallint", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    BankSystemId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCards_BankSystems_BankSystemId",
                        column: x => x.BankSystemId,
                        principalSchema: "public",
                        principalTable: "BankSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankCards_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "public",
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndividualEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualEntities_Customer_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    INN = table.Column<long>(type: "bigint", nullable: false),
                    KPP = table.Column<long>(type: "bigint", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LegalAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalEntities_Customer_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    OrderStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "public",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalSchema: "public",
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerCaseMotherboardFormFactor",
                schema: "public",
                columns: table => new
                {
                    ComputerCasesId = table.Column<int>(type: "integer", nullable: false),
                    SupportedMotherboardFormFactorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerCaseMotherboardFormFactor", x => new { x.ComputerCasesId, x.SupportedMotherboardFormFactorsId });
                    table.ForeignKey(
                        name: "FK_ComputerCaseMotherboardFormFactor_ComputerCases_ComputerCas~",
                        column: x => x.ComputerCasesId,
                        principalSchema: "public",
                        principalTable: "ComputerCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerCaseMotherboardFormFactor_MotherboardFormFactors_Su~",
                        column: x => x.SupportedMotherboardFormFactorsId,
                        principalSchema: "public",
                        principalTable: "MotherboardFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPURAMType",
                schema: "public",
                columns: table => new
                {
                    CPUsId = table.Column<int>(type: "integer", nullable: false),
                    SupportedRamTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPURAMType", x => new { x.CPUsId, x.SupportedRamTypeId });
                    table.ForeignKey(
                        name: "FK_CPURAMType_CPUs_CPUsId",
                        column: x => x.CPUsId,
                        principalSchema: "public",
                        principalTable: "CPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPURAMType_RAMTypes_SupportedRamTypeId",
                        column: x => x.SupportedRamTypeId,
                        principalSchema: "public",
                        principalTable: "RAMTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HDDs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    RPM = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HDDs_DiskDrives_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "DiskDrives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSDs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TBW = table.Column<int>(type: "integer", nullable: false),
                    DWPD = table.Column<float>(type: "real", nullable: false),
                    BitsForCell = table.Column<byte>(type: "smallint", nullable: false),
                    ControllerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SSDs_DiskDrives_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "DiskDrives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSDs_SSDControllers_ControllerId",
                        column: x => x.ControllerId,
                        principalSchema: "public",
                        principalTable: "SSDControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardVideoPort",
                schema: "public",
                columns: table => new
                {
                    GraphicsCardsId = table.Column<int>(type: "integer", nullable: false),
                    VideoPortsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardVideoPort", x => new { x.GraphicsCardsId, x.VideoPortsId });
                    table.ForeignKey(
                        name: "FK_GraphicsCardVideoPort_GraphicsCards_GraphicsCardsId",
                        column: x => x.GraphicsCardsId,
                        principalSchema: "public",
                        principalTable: "GraphicsCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardVideoPort_VideoPorts_VideoPortsId",
                        column: x => x.VideoPortsId,
                        principalSchema: "public",
                        principalTable: "VideoPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DPIModeMouse",
                schema: "public",
                columns: table => new
                {
                    DPIModesId = table.Column<int>(type: "integer", nullable: false),
                    MousesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPIModeMouse", x => new { x.DPIModesId, x.MousesId });
                    table.ForeignKey(
                        name: "FK_DPIModeMouse_DPIModes_DPIModesId",
                        column: x => x.DPIModesId,
                        principalSchema: "public",
                        principalTable: "DPIModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DPIModeMouse_Mouses_MousesId",
                        column: x => x.MousesId,
                        principalSchema: "public",
                        principalTable: "Mouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUCoreMotherboard",
                schema: "public",
                columns: table => new
                {
                    MotherboardsId = table.Column<int>(type: "integer", nullable: false),
                    SupportedCPUCoresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUCoreMotherboard", x => new { x.MotherboardsId, x.SupportedCPUCoresId });
                    table.ForeignKey(
                        name: "FK_CPUCoreMotherboard_CPUCores_SupportedCPUCoresId",
                        column: x => x.SupportedCPUCoresId,
                        principalSchema: "public",
                        principalTable: "CPUCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPUCoreMotherboard_Motherboards_MotherboardsId",
                        column: x => x.MotherboardsId,
                        principalSchema: "public",
                        principalTable: "Motherboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardRAMType",
                schema: "public",
                columns: table => new
                {
                    MotherboardsId = table.Column<int>(type: "integer", nullable: false),
                    SupportedRAMTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardRAMType", x => new { x.MotherboardsId, x.SupportedRAMTypesId });
                    table.ForeignKey(
                        name: "FK_MotherboardRAMType_Motherboards_MotherboardsId",
                        column: x => x.MotherboardsId,
                        principalSchema: "public",
                        principalTable: "Motherboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardRAMType_RAMTypes_SupportedRAMTypesId",
                        column: x => x.SupportedRAMTypesId,
                        principalSchema: "public",
                        principalTable: "RAMTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardVideoPort",
                schema: "public",
                columns: table => new
                {
                    MotherboardsId = table.Column<int>(type: "integer", nullable: false),
                    VideoPortsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardVideoPort", x => new { x.MotherboardsId, x.VideoPortsId });
                    table.ForeignKey(
                        name: "FK_MotherboardVideoPort_Motherboards_MotherboardsId",
                        column: x => x.MotherboardsId,
                        principalSchema: "public",
                        principalTable: "Motherboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherboardVideoPort_VideoPorts_VideoPortsId",
                        column: x => x.VideoPortsId,
                        principalSchema: "public",
                        principalTable: "VideoPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreparedAssemblies",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CPUId = table.Column<int>(type: "integer", nullable: false),
                    MotherboardId = table.Column<int>(type: "integer", nullable: false),
                    GraphicsCardId = table.Column<int>(type: "integer", nullable: false),
                    PowerSupplyId = table.Column<int>(type: "integer", nullable: false),
                    DisplayId = table.Column<int>(type: "integer", nullable: true),
                    ComputerCaseId = table.Column<int>(type: "integer", nullable: false),
                    KeyboardId = table.Column<int>(type: "integer", nullable: true),
                    MouseId = table.Column<int>(type: "integer", nullable: true),
                    CPUCoolerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparedAssemblies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_CPUCoolers_CPUCoolerId",
                        column: x => x.CPUCoolerId,
                        principalSchema: "public",
                        principalTable: "CPUCoolers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_CPUs_CPUId",
                        column: x => x.CPUId,
                        principalSchema: "public",
                        principalTable: "CPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_ComputerCases_ComputerCaseId",
                        column: x => x.ComputerCaseId,
                        principalSchema: "public",
                        principalTable: "ComputerCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_Displays_DisplayId",
                        column: x => x.DisplayId,
                        principalSchema: "public",
                        principalTable: "Displays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_GraphicsCards_GraphicsCardId",
                        column: x => x.GraphicsCardId,
                        principalSchema: "public",
                        principalTable: "GraphicsCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_Items_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_Keyboards_KeyboardId",
                        column: x => x.KeyboardId,
                        principalSchema: "public",
                        principalTable: "Keyboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_Motherboards_MotherboardId",
                        column: x => x.MotherboardId,
                        principalSchema: "public",
                        principalTable: "Motherboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_Mouses_MouseId",
                        column: x => x.MouseId,
                        principalSchema: "public",
                        principalTable: "Mouses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreparedAssemblies_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalSchema: "public",
                        principalTable: "PowerSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "public",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    BoughtFor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "public",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "public",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NVMeSSDs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NVMeSSDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NVMeSSDs_SSDs_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SSDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SataSSDs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SataSSDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SataSSDs_SSDs_Id",
                        column: x => x.Id,
                        principalSchema: "public",
                        principalTable: "SSDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiskDrivePreparedAssembly",
                schema: "public",
                columns: table => new
                {
                    DisksId = table.Column<int>(type: "integer", nullable: false),
                    PreparedAssembliesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiskDrivePreparedAssembly", x => new { x.DisksId, x.PreparedAssembliesId });
                    table.ForeignKey(
                        name: "FK_DiskDrivePreparedAssembly_DiskDrives_DisksId",
                        column: x => x.DisksId,
                        principalSchema: "public",
                        principalTable: "DiskDrives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiskDrivePreparedAssembly_PreparedAssemblies_PreparedAssemb~",
                        column: x => x.PreparedAssembliesId,
                        principalSchema: "public",
                        principalTable: "PreparedAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreparedAssemblyRAM",
                schema: "public",
                columns: table => new
                {
                    PreparedAssembliesId = table.Column<int>(type: "integer", nullable: false),
                    RAMId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparedAssemblyRAM", x => new { x.PreparedAssembliesId, x.RAMId });
                    table.ForeignKey(
                        name: "FK_PreparedAssemblyRAM_PreparedAssemblies_PreparedAssembliesId",
                        column: x => x.PreparedAssembliesId,
                        principalSchema: "public",
                        principalTable: "PreparedAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparedAssemblyRAM_RAMs_RAMId",
                        column: x => x.RAMId,
                        principalSchema: "public",
                        principalTable: "RAMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankSystemId",
                schema: "public",
                table: "BankCards",
                column: "BankSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_CustomerId",
                schema: "public",
                table: "BankCards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ItemId",
                schema: "public",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ItemId",
                schema: "public",
                table: "Carts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerCaseMotherboardFormFactor_SupportedMotherboardFormF~",
                schema: "public",
                table: "ComputerCaseMotherboardFormFactor",
                column: "SupportedMotherboardFormFactorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerCases_TypesizeId",
                schema: "public",
                table: "ComputerCases",
                column: "TypesizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerComponents_VendorId",
                schema: "public",
                table: "ComputerComponents",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUCoolers_FoundationMaterialId",
                schema: "public",
                table: "CPUCoolers",
                column: "FoundationMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUCoolers_RadiatorMaterialId",
                schema: "public",
                table: "CPUCoolers",
                column: "RadiatorMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUCoreMotherboard_SupportedCPUCoresId",
                schema: "public",
                table: "CPUCoreMotherboard",
                column: "SupportedCPUCoresId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUCores_VendorId",
                schema: "public",
                table: "CPUCores",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_CPURAMType_SupportedRamTypeId",
                schema: "public",
                table: "CPURAMType",
                column: "SupportedRamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_CoreId",
                schema: "public",
                table: "CPUs",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_SocketId",
                schema: "public",
                table: "CPUs",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CartId",
                schema: "public",
                table: "Customer",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiskDrivePreparedAssembly_PreparedAssembliesId",
                schema: "public",
                table: "DiskDrivePreparedAssembly",
                column: "PreparedAssembliesId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_DefinitionId",
                schema: "public",
                table: "Displays",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_MatrixTypeId",
                schema: "public",
                table: "Displays",
                column: "MatrixTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_UnderlightId",
                schema: "public",
                table: "Displays",
                column: "UnderlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_VesaSizeId",
                schema: "public",
                table: "Displays",
                column: "VesaSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_DPIModeMouse_MousesId",
                schema: "public",
                table: "DPIModeMouse",
                column: "MousesId");

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_VendorId",
                schema: "public",
                table: "GPUs",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCards_GPUId",
                schema: "public",
                table: "GraphicsCards",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardVideoPort_VideoPortsId",
                schema: "public",
                table: "GraphicsCardVideoPort",
                column: "VideoPortsId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ActiveDiscountId",
                schema: "public",
                table: "Items",
                column: "ActiveDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                schema: "public",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_KeyboardTypeId",
                schema: "public",
                table: "Keyboards",
                column: "KeyboardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_TypesizeId",
                schema: "public",
                table: "Keyboards",
                column: "TypesizeId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardRAMType_SupportedRAMTypesId",
                schema: "public",
                table: "MotherboardRAMType",
                column: "SupportedRAMTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_AudioChipsetId",
                schema: "public",
                table: "Motherboards",
                column: "AudioChipsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_CPUSocketId",
                schema: "public",
                table: "Motherboards",
                column: "CPUSocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_FormFactorId",
                schema: "public",
                table: "Motherboards",
                column: "FormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_MotherboardChipsetId",
                schema: "public",
                table: "Motherboards",
                column: "MotherboardChipsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_NetworkAdapterId",
                schema: "public",
                table: "Motherboards",
                column: "NetworkAdapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_PCIEVersionId1",
                schema: "public",
                table: "Motherboards",
                column: "PCIEVersionId1");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardVideoPort_VideoPortsId",
                schema: "public",
                table: "MotherboardVideoPort",
                column: "VideoPortsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                schema: "public",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "public",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "public",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_Certificate80PlusId",
                schema: "public",
                table: "PowerSupplies",
                column: "Certificate80PlusId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_ComputerCaseId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "ComputerCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_CPUCoolerId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "CPUCoolerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_CPUId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_DisplayId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "DisplayId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_GraphicsCardId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "GraphicsCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_KeyboardId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "KeyboardId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_MotherboardId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_MouseId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "MouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblies_PowerSupplyId",
                schema: "public",
                table: "PreparedAssemblies",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedAssemblyRAM_RAMId",
                schema: "public",
                table: "PreparedAssemblyRAM",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_RAMs_TypeId",
                schema: "public",
                table: "RAMs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ItemId",
                schema: "public",
                table: "Review",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleComputerComponents_TypeId",
                schema: "public",
                table: "SimpleComputerComponents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SSDs_ControllerId",
                schema: "public",
                table: "SSDs",
                column: "ControllerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BankCards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ComputerCaseMotherboardFormFactor",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPUCoreMotherboard",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPURAMType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DiskDrivePreparedAssembly",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DPIModeMouse",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GraphicsCardVideoPort",
                schema: "public");

            migrationBuilder.DropTable(
                name: "HDDs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "IndividualEntities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LegalEntities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MotherboardRAMType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MotherboardVideoPort",
                schema: "public");

            migrationBuilder.DropTable(
                name: "NVMeSSDs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PreparedAssemblyRAM",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Review",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SataSSDs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BankSystems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DPIModes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "VideoPorts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PreparedAssemblies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RAMs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SSDs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OrderStatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPUCoolers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPUs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ComputerCases",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Displays",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GraphicsCards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Keyboards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Motherboards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Mouses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PowerSupplies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RAMTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DiskDrives",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SSDControllers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CoolerMaterials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPUCores",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ComputerCaseTypesizes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Definitions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MatrixTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Underlights",
                schema: "public");

            migrationBuilder.DropTable(
                name: "VesaSizes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GPUs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KeyboardType",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KeyboardTypesizes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AudioChipsets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CPUSocket",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MotherboardChipsets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MotherboardFormFactors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "NetworkAdapters",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PCIEVersions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Certificates80Plus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ComputerComponents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SimpleComputerComponents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Vendors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SimpleComputerComponentTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ActiveDiscounts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ItemTypes",
                schema: "public");
        }
    }
}
