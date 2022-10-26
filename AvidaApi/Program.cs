using AvidaApi.Data;
using AvidaApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// OBS Not Working
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<IDecisionRulesService, DecisionRulesService>();
builder.Services.AddScoped<IIndatavalidation, Indatavalidation>();
builder.Services.AddScoped<IRepositoryLoanApplication, RepositoryLoanApplication>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ILoanService, LoanService>();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();


Log.Information("Starting web host");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LoanDBContext>(
    x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


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




