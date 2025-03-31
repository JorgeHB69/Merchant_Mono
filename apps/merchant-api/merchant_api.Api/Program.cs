using DotNetEnv;
using merchant_api.Business;
using merchant_api.Business.ValidatorSettings;
using merchant_api.Business.ValidatorSettings.Inventory;
using merchant_api.Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policyBuilder => policyBuilder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("validationSettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ValidationSettings>(builder.Configuration);

string connectionString = Env.GetString("POSTGRES_SQL_CONNECTION")?? throw new ArgumentNullException("POSTGRES_SQL_CONNECTION environment variable is not set.");

builder.Services.AddDbContext<DbContext, PostgresContext>(options =>
    options.UseNpgsql(connectionString,
            b => b.MigrationsAssembly("merchant_api.Api"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddConfigurations();  
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();