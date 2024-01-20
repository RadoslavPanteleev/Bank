using BankServer.Entities;
using BankServer.Entities.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankServer
{
    public partial class AppDbContext : IdentityDbContext<Entities.Person>
    {
        private void Seed(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Household & Services", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 2, Name = "Home Improvements", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 3, Name = "Food & Drinks", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 4, Name = "Transport", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 5, Name = "Shopping", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 6, Name = "Leisure", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 7, Name = "Health & Beauty", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 8, Name = "Salary", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 9, Name = "Pension", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 10, Name = "Benefits", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 11, Name = "Financial", Description = "" });
            builder.Entity<Category>().HasData(new Category { Id = 12, Name = "Other", Description = "" });

            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 1, Type = "External" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 2, Type = "Internal" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 3, Type = "Cash" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 4, Type = "Non-cash" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 5, Type = "Credit" });
            builder.Entity<TransactionType>().HasData(new TransactionType { Id = 6, Type = "Personal" });

            builder.Entity<Bank>().HasData(new Bank { Id = 1, Name = "Банка ДСК АД", Address = "ул. Московска № 19 София 1036"                              , Phone = "+359 2 939 1220" });
            builder.Entity<Bank>().HasData(new Bank { Id = 2, Name = "УниКредит Булбанк АД", Address = "пл. Света Неделя № 7 София 1000"                    , Phone = "+359 2 923 2111" });
            builder.Entity<Bank>().HasData(new Bank { Id = 3, Name = "Юробанк България АД", Address = "ул. Околовръстен път № 260 София 1766"               , Phone = "+359 2 8166 000" });
            builder.Entity<Bank>().HasData(new Bank { Id = 4, Name = "Обединена българска банка АД", Address = "бул. Витоша, № 89 Б София 1463"             , Phone = "+359 2 811 2330; 811 2800; 811 2235" });
            builder.Entity<Bank>().HasData(new Bank { Id = 5, Name = "Инвестбанк АД", Address = "бул. България № 85 София 1404"                             , Phone = "+359 2 818 6123; 818 6124" });
            builder.Entity<Bank>().HasData(new Bank { Id = 6, Name = "Първа инвестиционна банка АД", Address = "бул. Цариградско шосе № 111П София 1784"    , Phone = "+359 2 91 001" });
            builder.Entity<Bank>().HasData(new Bank { Id = 7, Name = "Тексим Банк АД", Address = "бул. Тодор Александров № 117 София 1303"                  , Phone = "+359 2 903 5501/ 5505" });
            builder.Entity<Bank>().HasData(new Bank { Id = 8, Name = "Централна кооперативна банка АД", Address = "бул. Цариградско шосе № 87 София 1086"   , Phone = "+359 2 926 62 66" });
            builder.Entity<Bank>().HasData(new Bank { Id = 9, Name = "Алианц Банк България АД", Address = "р-н Лозенец, ул. Сребърна № 16 София 1407"       , Phone = "+359 2 9215 + в. ; 9215 404" });
            builder.Entity<Bank>().HasData(new Bank { Id = 10, Name = "Българо-американска кредитна банка АД", Address = "ул. Славянска № 2 София 1000"     , Phone = "+359 2 9658 358; 9658 345" });
            builder.Entity<Bank>().HasData(new Bank { Id = 11, Name = "ТИ БИ АЙ Банк EАД", Address = "ул. Димитър Хаджикоцев № 52-54 София 1421"            , Phone = "+359 2 970 24 10; 8163 900" });
            builder.Entity<Bank>().HasData(new Bank { Id = 12, Name = "ПроКредит Банк (България) EАД", Address = "бул. Тодор Александров № 26 София 1303"   , Phone = "+359 2 8135 100; 8135 808" });
            builder.Entity<Bank>().HasData(new Bank { Id = 13, Name = "Интернешънъл Асет Банк АД", Address = "бул. Тодор Александров № 81-83 София 1303"    , Phone = "+359 2 8120 234; 9204 303" });
            builder.Entity<Bank>().HasData(new Bank { Id = 14, Name = "Търговска Банка Д АД", Address = "бул. Генерал Тотлебен № 8 София 1606"              , Phone = "+359 2 935 7171; 464 1171" });
            builder.Entity<Bank>().HasData(new Bank { Id = 15, Name = "Българска банка за развитие ЕАД", Address = "ул. Дякон Игнатий № 1 София 1000"       , Phone = "+359 2 9 306 333" });
            builder.Entity<Bank>().HasData(new Bank { Id = 16, Name = "Токуда Банк АД", Address = "ул. Георг Вашингтон № 21 София 1000"                     , Phone = "+359 2 403 79 00; 02 40379 85" });
            builder.Entity<Bank>().HasData(new Bank { Id = 17, Name = "Общинска банка АД", Address = "ул. Врабча № 6 София 1000"                            , Phone = "+359 2 9300 111" });

            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 1, Description = "ул. Московска № 19 София 1036" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 2, Description = "пл. Света Неделя № 7 София 1000" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 3, Description = "ул. Околовръстен път № 260 София 1766" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 4, Description = "бул. Витоша, № 89 Б София 1463" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 5, Description = "бул. България № 85 София 1404" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 6, Description = "бул. Цариградско шосе № 111П София 1784" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 7, Description = "бул. Тодор Александров № 117 София 1303" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 8, Description = "бул. Цариградско шосе № 87 София 1086" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 9, Description = "р-н Лозенец, ул. Сребърна № 16 София 1407" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 10, Description = "ул. Славянска № 2 София 1000" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 11, Description = "ул. Димитър Хаджикоцев № 52-54 София 1421" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 12, Description = "бул. Тодор Александров № 26 София 1303" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 13, Description = "бул. Тодор Александров № 81-83 София 1303" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 14, Description = "бул. Генерал Тотлебен № 8 София 1606" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 15, Description = "ул. Дякон Игнатий № 1 София 1000" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 16, Description = "ул. Георг Вашингтон № 21 София 1000" });
            builder.Entity<Entities.Address>().HasData(new Entities.Address { Id = 17, Description = "ул. Врабча № 6 София 1000" });

            builder.Entity<Location>().HasData(new Location { Id = 1, Name = "Vilanova de Sau", AddressId = 1, Latitude = 41.947, Longitude = 2.3844 });
            builder.Entity<Location>().HasData(new Location { Id = 2, Name = "Dolno Palčište", AddressId = 2, Latitude = 41.96859, Longitude = 20.92899 });
            builder.Entity<Location>().HasData(new Location { Id = 3, Name = "El Paso", AddressId = 3, Latitude = 31.75872, Longitude = -106.48693 });
            builder.Entity<Location>().HasData(new Location { Id = 4, Name = "Belawang", AddressId = 4, Latitude = -3.0715, Longitude = 114.6604 });
            builder.Entity<Location>().HasData(new Location { Id = 5, Name = "Robella", AddressId = 5, Latitude = 45.10165, Longitude = 8.10193 });
            builder.Entity<Location>().HasData(new Location { Id = 6, Name = "Huchuan", AddressId = 6, Latitude = 34.92597, Longitude = 106.14722 });
            builder.Entity<Location>().HasData(new Location { Id = 7, Name = "Cargados Carajos", AddressId = 7, Latitude = -16.60329, Longitude = 59.65851 });
            builder.Entity<Location>().HasData(new Location { Id = 8, Name = "Lújar", AddressId = 8, Latitude = 36.78831, Longitude = -3.404 });
            builder.Entity<Location>().HasData(new Location { Id = 9, Name = "Altopascio", AddressId = 9, Latitude = 43.81618, Longitude = 10.67668 });
            builder.Entity<Location>().HasData(new Location { Id = 10, Name = "Jidoștița", AddressId = 10, Latitude = 44.71582, Longitude = 22.59311 });
            builder.Entity<Location>().HasData(new Location { Id = 11, Name = "Fengzhou", AddressId = 11, Latitude = 24.95762, Longitude = 118.53365 });
            builder.Entity<Location>().HasData(new Location { Id = 12, Name = "Salvitelle", AddressId = 12, Latitude = 40.59064, Longitude = 15.4576 });
            builder.Entity<Location>().HasData(new Location { Id = 13, Name = "Al Maḩmūdīyah", AddressId = 13, Latitude = 33.06221, Longitude = 44.36564 });
            builder.Entity<Location>().HasData(new Location { Id = 14, Name = "Tanjung Balai", AddressId = 14, Latitude = 1.00005, Longitude = 103.42186 });
            builder.Entity<Location>().HasData(new Location { Id = 15, Name = "Pochidia", AddressId = 15, Latitude = 46.04318, Longitude = 27.58746 });
            builder.Entity<Location>().HasData(new Location { Id = 16, Name = "La Blanca", AddressId = 16, Latitude = 16.59482, Longitude = -94.69256 });
            builder.Entity<Location>().HasData(new Location { Id = 17, Name = "São Roque", AddressId = 17, Latitude = -23.52917, Longitude = -47.13528 });

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new UsersWithRolesConfiguration());
        }
    }
}
