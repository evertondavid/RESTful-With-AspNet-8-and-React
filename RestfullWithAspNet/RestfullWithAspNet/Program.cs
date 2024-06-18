using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Business.Implementations;
using RestfullWithAspNet.Hypernedia.Enricher;
using RestfullWithAspNet.Hypernedia.Filters;
using RestfullWithAspNet.Model.Context;
using RestfullWithAspNet.Repository;
using RestfullWithAspNet.Repository.Generic;
using Serilog;

var appTitle = "RESTful API to Azure with ASP.NET Core 5 and Docker"; // Set the title of the application
var appVersion = "1.0.0"; // Set the version of the application
var appDescription = "RESTful API to Azure with ASP.NET Core 5 and Docker"; // Set the name of the application
var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("MySQLConnection"); // Get the connection string from the appsettings.json file

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true); // Add services to the container for routing

builder.Services.AddControllers(); // Adiciona serviços MVC ao container (Controllers, Views, TagHelpers, etc.)

builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(5, 7, 44)))); // Add services to the container for Entity Framework Core, At version 3.2.0 we don't have to use MySqlServerVersion

builder.Services.AddApiVersioning(); // Add services to the container for API versioning

// Injection of dependencies
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>(); // Register the Rules of business in the container
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>(); // Register the Rules of business in the container
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>)); // Register the Rules of Repository (DataBase, Files, etc) in the container

// more about dependency injection: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
// Add services to the container for API Explorer (used by Swashbuckle)
builder.Services.AddEndpointsApiExplorer();

// Add services to the container for Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = appTitle, // Set the title of the API
        Version = appVersion, // Set the version of the API
        Description = $"'{appDescription}' - '{appVersion}'", // Set the description of the API
        Contact = new OpenApiContact // Set the contact information for the API
        {
            Name = "Everton David", // Set the name of the contact
            Url = new Uri("https://github.com/evertondavid") // Set the URL of the contact
        }
    });
});

// Add services to the container for MVC
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml"); // Add support for XML
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json"); // Add support for JSON
})
.AddXmlSerializerFormatters(); // Add support for XML serialization in the MVC middleware

//HATEOAS
var filterOptions = new HyperMediaFilterOptions(); // Create a new instance of the HyperMediaFilterOptions class
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher()); // Add a new instance of the PersonEnricher class to the ContentResponseEnricherList
filterOptions.ContentResponseEnricherList.Add(new BookEnricher()); // Add a new instance of the BookEnricher class to the ContentResponseEnricherList
builder.Services.AddSingleton(filterOptions); // Add the filterOptions instance to the container

var app = builder.Build(); // Create the application instance.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Verify if the environment is development
{
    _ = connection ?? throw new ArgumentNullException(nameof(connection), "Connection string cannot be null.");
    MigrateDatabase(connection); // Migrate the database

    app.UseDeveloperExceptionPage(); // Adds a developer exception page to the pipeline

    app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"'{appDescription}' - '{appVersion}'"); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
    }); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
    var option = new RewriteOptions(); // Create a new instance of the RewriteOptions class
    option.AddRedirect("^$", "swagger"); // Add a redirect rule to the RewriteOptions instance
    app.UseRewriter(option); // Enable the middleware to use the RewriteOptions instance
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Add the MVC middleware to the pipeline

//HATEOAS
app.MapControllerRoute("DefaultApi", "{controller=Values}/v{version:apiVersion}/{id?}"); // Add a route to the MVC middleware

app.Run(); // Execute the application

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
                //ValidateChecksums = false // Disable checksum validation
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
