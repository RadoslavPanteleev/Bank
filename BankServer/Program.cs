using BankServer;
using BankServer.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var connection = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContextPool<BankContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<AccountsService>();
builder.Services.AddTransient<AddressesService>();
builder.Services.AddTransient<BanksService>();
builder.Services.AddTransient<CategoriesService>();
builder.Services.AddTransient<LocationsService>();
builder.Services.AddTransient<PhoneNumbersService>();
builder.Services.AddTransient<TransactionTypesService>();

builder.Services.AddScoped<UsersService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<BankContext>())
    try
    {
        context?.Database.EnsureCreated();
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API V1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
