using Microsoft.EntityFrameworkCore;

using LoanSaas.Backend.Data;
using LoanSaas.Backend.Services;
using LoanSaas.Backend.Workers;

var builder = WebApplication.CreateBuilder(args);

/// --------------------
/// DATABASE
/// --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

/// --------------------
/// CONTROLLERS
/// --------------------
builder.Services.AddControllers();
/// --------------------
/// SERVICES (CORE)
/// --------------------
builder.Services.AddScoped<ExotelService>();
builder.Services.AddScoped<ElevenLabsService>();
builder.Services.AddScoped<CallService>();
builder.Services.AddScoped<CostService>();
builder.Services.AddHostedService<CallWorker>();

/// --------------------
/// CORS (Angular frontend)
/// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

/// --------------------
/// PIPELINE
/// --------------------
app.UseCors("allowAll");

app.MapControllers();

await app.RunAsync();