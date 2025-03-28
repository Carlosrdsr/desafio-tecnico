using Questao5.Infrastructure.Sqlite;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog;
using System.Globalization;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

//Log
var loggerConfiguration = new LoggerConfiguration()
    .Enrich.WithClientIp()
    .Enrich.WithMachineName()
    .Enrich.WithCorrelationId()
    .Enrich.WithEnvironmentName()
    .Enrich.FromLogContext()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Is(builder.Environment.IsDevelopment() ?
        LogEventLevel.Debug :
        LogEventLevel.Information);

loggerConfiguration.WriteTo.Console(new JsonFormatter(renderMessage: true, formatProvider: new CultureInfo("en-US")));

Log.Logger = loggerConfiguration.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Host.UseSerilog(Log.Logger);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

//Repositories
builder.Services.AddScoped<ICurrentAccountRepository, CurrentAccountRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();
builder.Services.AddScoped<IIdempotencyRepository, IdempotencyRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


