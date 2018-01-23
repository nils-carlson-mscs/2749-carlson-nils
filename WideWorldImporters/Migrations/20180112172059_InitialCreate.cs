using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WideWorldImporters.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.EnsureSchema(
                name: "Sales");

            migrationBuilder.CreateTable(
                name: "RawSqlReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawSqlReturn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "Application",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Continent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CountryType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FormalName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsoAlpha3Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsoNumericCode = table.Column<int>(type: "int", nullable: true),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LatestRecordedPopulation = table.Column<long>(type: "bigint", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Subregion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "Application",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomFields = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false),
                    IsExternalLogonProvider = table.Column<bool>(type: "bit", nullable: false),
                    IsPermittedToLogon = table.Column<bool>(type: "bit", nullable: false),
                    IsSalesperson = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemUser = table.Column<bool>(type: "bit", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LogonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Photo = table.Column<string>(type: "varbinary(max)", nullable: true),
                    PreferredName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPreferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCategories",
                schema: "Sales",
                columns: table => new
                {
                    CustomerCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategories", x => x.CustomerCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "StateProvinces",
                schema: "Application",
                columns: table => new
                {
                    StateProvinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LatestRecordedPopulation = table.Column<long>(type: "bigint", nullable: true),
                    SalesTerritory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateProvinceCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StateProvinceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvinces", x => x.StateProvinceID);
                    table.ForeignKey(
                        name: "FK_Application_StateProvinces_CountryID_Application_Countries",
                        column: x => x.CountryID,
                        principalSchema: "Application",
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "Application",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    LatestRecordedPopulation = table.Column<long>(type: "bigint", nullable: true),
                    StateProvinceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Application_Cities_StateProvinceID_Application_StateProvinces",
                        column: x => x.StateProvinceID,
                        principalSchema: "Application",
                        principalTable: "StateProvinces",
                        principalColumn: "StateProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Sales",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountOpenedDate = table.Column<DateTime>(type: "date", nullable: false),
                    AlternateContactPersonID = table.Column<int>(type: "int", nullable: false),
                    BillToCustomerID = table.Column<int>(type: "int", nullable: false),
                    BuyingGroupID = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerCategoryID = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryAddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DeliveryAddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DeliveryCityID = table.Column<int>(type: "int", nullable: false),
                    DeliveryLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryMethodID = table.Column<int>(type: "int", nullable: false),
                    DeliveryPostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DeliveryRun = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsOnCreditHold = table.Column<bool>(type: "bit", nullable: false),
                    IsStatementSent = table.Column<bool>(type: "bit", nullable: false),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    PaymentDays = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PostalAddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PostalAddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    PostalCityID = table.Column<int>(type: "int", nullable: false),
                    PostalPostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrimaryContactPersonID = table.Column<int>(type: "int", nullable: false),
                    RunPosition = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    StandardDiscountPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_AppPeople_SalesCustomers_AlternateContact",
                        column: x => x.AlternateContactPersonID,
                        principalSchema: "Application",
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerCategories_CustomerCategoryID",
                        column: x => x.CustomerCategoryID,
                        principalSchema: "Sales",
                        principalTable: "CustomerCategories",
                        principalColumn: "CustomerCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Cities_Sales_Customers_Delivery",
                        column: x => x.DeliveryCityID,
                        principalSchema: "Application",
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Cities_Sales_Customers",
                        column: x => x.PostalCityID,
                        principalSchema: "Application",
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppPeople_SalesCustomers_PrimaryContact",
                        column: x => x.PrimaryContactPersonID,
                        principalSchema: "Application",
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_Application_Cities_StateProvinceID",
                schema: "Application",
                table: "Cities",
                column: "StateProvinceID");

            migrationBuilder.CreateIndex(
                name: "UQ_Application_Countries_CountryName",
                schema: "Application",
                table: "Countries",
                column: "CountryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Application_Countries_FormalName",
                schema: "Application",
                table: "Countries",
                column: "FormalName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Application_StateProvinces_CountryID",
                schema: "Application",
                table: "StateProvinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Application_StateProvinces_SalesTerritory",
                schema: "Application",
                table: "StateProvinces",
                column: "SalesTerritory");

            migrationBuilder.CreateIndex(
                name: "UQ_Application_StateProvinces_StateProvinceName",
                schema: "Application",
                table: "StateProvinces",
                column: "StateProvinceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AlternateContactPersonID",
                schema: "Sales",
                table: "Customers",
                column: "AlternateContactPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCategoryID",
                schema: "Sales",
                table: "Customers",
                column: "CustomerCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryCityID",
                schema: "Sales",
                table: "Customers",
                column: "DeliveryCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PostalCityID",
                schema: "Sales",
                table: "Customers",
                column: "PostalCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PrimaryContactPersonID",
                schema: "Sales",
                table: "Customers",
                column: "PrimaryContactPersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawSqlReturn");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "People",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CustomerCategories",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "StateProvinces",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "Application");
        }
    }
}
