using Microsoft.Data.Sqlite;
using Wiinf_Studie.API.Data;
using Wiinf_Studie.API.Data.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Settings
DatabaseConfiguration.DatabaseName = builder.Configuration.GetValue(DatabaseConfiguration.DatabaseNameKey, "Data Source=candidates.db");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom classes for DI
builder.Services.AddScoped<CandidatesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

// Database migration/setup
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetService<CandidatesRepository>();

    if (!(await repo.GetDoublePaymentPairs()).Any())
    {
        // If no entries yet, seed database.
        await DatabaseSetup.SeedDatabaseWithDummyData(100);
    }
}

await app.RunAsync();
