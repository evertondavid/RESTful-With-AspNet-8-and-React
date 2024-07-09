using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Business.Implementations;
using RestfullWithAspNet.Configurations;
using RestfullWithAspNet.Hypermedia.Enricher;
using RestfullWithAspNet.Hypernedia.Enricher;
using RestfullWithAspNet.Hypernedia.Filters;
using RestfullWithAspNet.Model.Context;
using RestfullWithAspNet.Repository;
using RestfullWithAspNet.Repository.Generic;
using RestfullWithAspNet.Services;
using RestfullWithAspNet.Services.Implementations;
using Serilog;

// Application metadata
var appAspNetVersion = "7.0";
var appTitle = $"RESTful API to Azure with ASP.NET '{appAspNetVersion}'.";
var appVersion = "1.0.0";
var appDescription = $"RESTful API to Azure with ASP.NET '{appAspNetVersion}'.";

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("MySQLConnection");


// Service Configuration

// Configure Entity Framework with MySQL
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(5, 7, 44))));

// Add MVC services to the container
builder.Services.AddControllers();

// Configure CORS to allow any origin, method, and header
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Configure routing to use lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add API versioning support
builder.Services.AddApiVersioning();

// Dependency Injection Configuration

// Register business services
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Register HTTP context accessor
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>(); // Register file business service
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>(); // Register person business service
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>(); // Register book business service
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>(); // Register login business service
builder.Services.AddTransient<ITokenService, TokenService>(); // Register token service
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register user repository service
builder.Services.AddScoped<IPersonRepository, PersonRepository>(); // Register person repository service

// Register generic repository service
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// API Documentation Configuration

// Add API explorer and Swagger generator
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = appTitle,
        Version = appVersion,
        Description = $"{appDescription} - {appVersion}",
        Contact = new OpenApiContact
        {
            Name = "Everton David",
            Url = new Uri("https://github.com/evertondavid")
        }
    });
});

// Add MVC with support for XML and JSON formatters
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();

// HATEOAS Configuration

// Configure HyperMedia filters
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

// Configure token service from configuration file
// Configure the token configurations by retrieving them from the configuration file
var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("TokenConfigurations")
    ).Configure(tokenConfigurations);

// Register the token configurations as a singleton service
builder.Services.AddSingleton(tokenConfigurations);

// Configure authentication with JWT bearer scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    if (string.IsNullOrEmpty(tokenConfigurations.Secret))
    {
        throw new ArgumentNullException(nameof(tokenConfigurations.Secret), "The secret key cannot be null or empty.");
    }
    // Configure the token validation parameters
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

// Configure Serilog for logging
var app = builder.Build();

// Middleware Configuration

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    _ = connection ?? throw new ArgumentNullException(nameof(connection), "Connection string cannot be null.");
    MigrateDatabase(connection);

    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appDescription} - {appVersion}");
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

// Enable Authorization
app.UseAuthorization();

// Map controllers for MVC
app.MapControllers();

// Define default API route
app.MapControllerRoute("DefaultApi", "{controller=Values}/v{version:apiVersion}/{id?}");

app.Run();

/// <summary>
/// Migrates the database using Evolve.
/// </summary>
/// <param name="connection">Database connection string.</param>
void MigrateDatabase(string connection)
{
    try
    {
        using (var evolveConnection = new MySqlConnection(connection))
        {
            var evolve = new Evolve.Evolve(evolveConnection, Log.Information)
            {
                Locations = new List<string> { "db/migrations", "db/dataset" },
                IsEraseDisabled = true
            };
            evolve.Migrate();
        }
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed.", ex);
        throw;
    }
}
