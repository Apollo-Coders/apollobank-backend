using ApolloBank.Data;
using ApolloBank.Enums;
using ApolloBank.Repositories;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services;
using ApolloBank.Services.Interfaces;
using ApolloBank.Services;
using Microsoft.EntityFrameworkCore;
using ApolloBank.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI de Services
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

//DI de Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("ApolloBankContext"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
