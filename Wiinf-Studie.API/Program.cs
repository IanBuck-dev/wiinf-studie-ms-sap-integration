using Microsoft.Data.Sqlite;
using Wiinf_Studie.API.Data;
using Wiinf_Studie.API.Data.Migrations;
using Wiinf_Studie.API.Middlewares;

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
app.UseSwagger(options =>
{
    // Needed for PowerApps import of OpenAPI file (only supports v2, not v3)
    options.SerializeAsV2 = true;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// app.UseHttpsRedirection(); // Disabled for testing purposes

// Add authorization via API keys
app.UseMiddleware<ApiKeyAuthorizationMiddleware>();

app.MapControllers();

// Database migration/setup
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetService<CandidatesRepository>();

    if (!(await repo.GetDoublePaymentPairsIncludingCandidates()).Any())
    {
        // If no entries yet, seed database.
        await DatabaseSetup.SeedDatabaseWithDummyData(100);
    }
}

await app.RunAsync();
