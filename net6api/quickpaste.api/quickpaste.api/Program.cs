using quickpaste.api.Extensions;
using quickpaste.api.Interfaces;
using quickpaste.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDataService, AzureCosmosDataService>();

string dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? string.Empty;
string dbName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? string.Empty;

builder.Services.RegisterCosmosDbClient(dbUrl, dbName);

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
