using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankServer.Migrations
{
    public partial class BANK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumberId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_PhoneNumbers_PhoneNumberId",
                        column: x => x.PhoneNumberId,
                        principalTable: "PhoneNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumberId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_PhoneNumbers_PhoneNumberId",
                        column: x => x.PhoneNumberId,
                        principalTable: "PhoneNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Accounts_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
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
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber");
                    table.ForeignKey(
                        name: "FK_Transactions_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionsTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "ул. Московска № 19 София 1036" },
                    { 2, "пл. Света Неделя № 7 София 1000" },
                    { 3, "ул. Околовръстен път № 260 София 1766" },
                    { 4, "бул. Витоша, № 89 Б София 1463" },
                    { 5, "бул. България № 85 София 1404" },
                    { 6, "бул. Цариградско шосе № 111П София 1784" },
                    { 7, "бул. Тодор Александров № 117 София 1303" },
                    { 8, "бул. Цариградско шосе № 87 София 1086" },
                    { 9, "р-н Лозенец, ул. Сребърна № 16 София 1407" },
                    { 10, "ул. Славянска № 2 София 1000" },
                    { 11, "ул. Димитър Хаджикоцев № 52-54 София 1421" },
                    { 12, "бул. Тодор Александров № 26 София 1303" },
                    { 13, "бул. Тодор Александров № 81-83 София 1303" },
                    { 14, "бул. Генерал Тотлебен № 8 София 1606" },
                    { 15, "ул. Дякон Игнатий № 1 София 1000" },
                    { 16, "ул. Георг Вашингтон № 21 София 1000" },
                    { 17, "ул. Врабча № 6 София 1000" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Household & Services" },
                    { 2, "", "Home Improvements" },
                    { 3, "", "Food & Drinks" },
                    { 4, "", "Transport" },
                    { 5, "", "Shopping" },
                    { 6, "", "Leisure" },
                    { 7, "", "Health & Beauty" },
                    { 8, "", "Salary" },
                    { 9, "", "Pension" },
                    { 10, "", "Benefits" },
                    { 11, "", "Financial" },
                    { 12, "", "Other" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Phone" },
                values: new object[,]
                {
                    { 1, "+359 2 939 1220" },
                    { 2, "+359 2 923 2111" },
                    { 3, "+359 2 8166 000" },
                    { 4, "+359 2 811 2330; 811 2800; 811 2235" },
                    { 5, "+359 2 818 6123; 818 6124" },
                    { 6, "+359 2 91 001" },
                    { 7, "+359 2 903 5501/ 5505" },
                    { 8, "+359 2 926 62 66" },
                    { 9, "+359 2 9215 + в. ; 9215 404" },
                    { 10, "+359 2 9658 358; 9658 345" },
                    { 11, "+359 2 970 24 10; 8163 900" },
                    { 12, "+359 2 8135 100; 8135 808" },
                    { 13, "+359 2 8120 234; 9204 303" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Phone" },
                values: new object[,]
                {
                    { 14, "+359 2 935 7171; 464 1171" },
                    { 15, "+359 2 9 306 333" },
                    { 16, "+359 2 403 79 00; 02 40379 85" },
                    { 17, "+359 2 9300 111" }
                });

            migrationBuilder.InsertData(
                table: "TransactionsTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "External" },
                    { 2, "Internal" },
                    { 3, "Cash" },
                    { 4, "Non-cash" },
                    { 5, "Credit" },
                    { 6, "Personal" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Address", "Name", "PhoneNumberId" },
                values: new object[,]
                {
                    { 1, "ул. Московска № 19 София 1036", "Банка ДСК АД", 1 },
                    { 2, "пл. Света Неделя № 7 София 1000", "УниКредит Булбанк АД", 2 },
                    { 3, "ул. Околовръстен път № 260 София 1766", "Юробанк България АД", 3 },
                    { 4, "бул. Витоша, № 89 Б София 1463", "Обединена българска банка АД", 4 },
                    { 5, "бул. България № 85 София 1404", "Инвестбанк АД", 5 },
                    { 6, "бул. Цариградско шосе № 111П София 1784", "Първа инвестиционна банка АД", 6 },
                    { 7, "бул. Тодор Александров № 117 София 1303", "Тексим Банк АД", 7 },
                    { 8, "бул. Цариградско шосе № 87 София 1086", "Централна кооперативна банка АД", 8 },
                    { 9, "р-н Лозенец, ул. Сребърна № 16 София 1407", "Алианц Банк България АД", 9 },
                    { 10, "ул. Славянска № 2 София 1000", "Българо-американска кредитна банка АД", 10 },
                    { 11, "ул. Димитър Хаджикоцев № 52-54 София 1421", "ТИ БИ АЙ Банк EАД", 11 },
                    { 12, "бул. Тодор Александров № 26 София 1303", "ПроКредит Банк (България) EАД", 12 },
                    { 13, "бул. Тодор Александров № 81-83 София 1303", "Интернешънъл Асет Банк АД", 13 },
                    { 14, "бул. Генерал Тотлебен № 8 София 1606", "Търговска Банка Д АД", 14 },
                    { 15, "ул. Дякон Игнатий № 1 София 1000", "Българска банка за развитие ЕАД", 15 },
                    { 16, "ул. Георг Вашингтон № 21 София 1000", "Токуда Банк АД", 16 },
                    { 17, "ул. Врабча № 6 София 1000", "Общинска банка АД", 17 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "AddressId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 1, 41.947000000000003, 2.3843999999999999, "Vilanova de Sau" },
                    { 2, 2, 41.968589999999999, 20.928989999999999, "Dolno Palčište" },
                    { 3, 3, 31.75872, -106.48693, "El Paso" },
                    { 4, 4, -3.0714999999999999, 114.6604, "Belawang" },
                    { 5, 5, 45.101649999999999, 8.1019299999999994, "Robella" },
                    { 6, 6, 34.92597, 106.14722, "Huchuan" },
                    { 7, 7, -16.603290000000001, 59.65851, "Cargados Carajos" },
                    { 8, 8, 36.788310000000003, -3.4039999999999999, "Lújar" },
                    { 9, 9, 43.816180000000003, 10.676679999999999, "Altopascio" },
                    { 10, 10, 44.715820000000001, 22.593109999999999, "Jidoștița" },
                    { 11, 11, 24.957619999999999, 118.53364999999999, "Fengzhou" },
                    { 12, 12, 40.59064, 15.457599999999999, "Salvitelle" },
                    { 13, 13, 33.06221, 44.365639999999999, "Al Maḩmūdīyah" },
                    { 14, 14, 1.0000500000000001, 103.42186, "Tanjung Balai" },
                    { 15, 15, 46.04318, 27.58746, "Pochidia" },
                    { 16, 16, 16.594819999999999, -94.69256, "La Blanca" },
                    { 17, 17, -23.529170000000001, -47.135280000000002, "São Roque" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonId",
                table: "Accounts",
                column: "PersonId");

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumberId",
                table: "AspNetUsers",
                column: "PhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_PhoneNumberId",
                table: "Banks",
                column: "PhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankId",
                table: "Transactions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LocationId",
                table: "Transactions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");
        }

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
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TransactionsTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");
        }
    }
}
