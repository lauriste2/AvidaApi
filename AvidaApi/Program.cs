using AvidaApi.Data;
using AvidaApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//IConfiguration configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();


builder.Services.AddScoped<IDecisionRulesService, DecisionRulesService>();
builder.Services.AddScoped<IIndatavalidation, Indatavalidation>();

// Add services to the container.

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


