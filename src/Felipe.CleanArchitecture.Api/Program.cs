using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Felipe.CleanArchitecture.Api;
using Felipe.CleanArchitecture.Api.Contracts.Trucks.Validators;
using Felipe.CleanArchitecture.Api.Filters;
using Felipe.CleanArchitecture.Api.Infrastructure.Factories;
using Felipe.CleanArchitecture.Api.Swagger;
using Felipe.CleanArchitecture.Application;
using Felipe.CleanArchitecture.Application.Common.Middlewares;
using Felipe.CleanArchitecture.Application.Interfaces;
using Felipe.CleanArchitecture.Infrastructure;
using Felipe.CleanArchitecture.Infrastructure.Data;
using Felipe.CleanArchitecture.Infrastructure.Services.Observability;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.IO;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.WebHost.UseUrls("https://*:80", "https://*:8080");

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add<GlobalExceptionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();
    options.DocumentFilter<FeatureGateDocumentFilter>();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token with Bearer in the field"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddTransient<CustomProblemDetailsFactory>();
builder.Services.AddApplicationInsightsTelemetry(config => config.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"]);
builder.Services.AddTransient(typeof(ICustomLogger<>), typeof(CustomLogger<>));
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields =
                HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestHeaders |
                HttpLoggingFields.ResponseStatusCode;
    logging.MediaTypeOptions.AddText("application/json");
    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.RequestBodyLogLimit = 4096;
});

var allowedOrigins = builder.Configuration["AllowedCORSOrigins"]?.Split(',') ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<RecyclableMemoryStreamManager>();

builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddAuthenticationConfig(builder.Configuration);

builder.Services.AddHttpClient();

builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

builder.Services.AddServices(builder.Configuration);
builder.Services.AddInfrastructureModule(builder.Configuration);
builder.Services.AddApplicationModule();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTruckRequestValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyAllowSpecificOrigins");
app.UseMiddleware<CustomLoggingMiddleware>();
app.UseMiddleware<RemoveServerHeaderMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

var env = app.Services.GetService<IWebHostEnvironment>();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!env!.IsEnvironment("SpecTests") && !env!.IsEnvironment("IntegrationTests") && !context.Database.IsInMemory())
    {
        await context.Database.MigrateAsync(CancellationToken.None);
    }
}

app.Run();
