using ApolloBank.Data;
using ApolloBank.Repositories;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.SampleScheduler.Extensions;
using ApolloBank.SampleScheduler.Factories;
using ApolloBank.SampleScheduler.TimerSchedulers;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddCors(options =>
{

    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = configuration["jwt:issuer"],
        ValidAudience = configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHashService, HashService>(); //estava apenas com o "HashService", ent�o n�o conseguia fazer a inje��o.
builder.Services.AddScoped<HashService>();
builder.Services.AddScoped<RandomNumberService>();



//SampleScheduler
builder.Services.AddCronJob<TimerCheckDatabase>(c => c.CronExpression = @"0 */1 * * * *");
//builder.Services.AddCronJob<TimerCheckDatabase>(c => c.CronExpression = "0 0 * * * *");
builder.Services.AddTransient<IServiceScopeFactory, DefaultServiceScopeFactory>();



//DI de Services
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
/*builder.Services.AddTransient<ICreditCardsService, CreditCardsService>();
builder.Services.AddTransient<IInvoiceService, InvoiceService>();*/


//DI de Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
/*builder.Services.AddScoped<ICreditCardsRepository, CreditCardsRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();*/
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>();

/*options => options.UseSqlite(builder.Configuration.GetConnectionString("ApolloBankContext"))*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AnotherPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
