using RestfullWithAspNet.Services;
using RestfullWithAspNet.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Adiciona serviços MVC ao container (Controllers, Views, TagHelpers, etc.)

// Injection of dependencies
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>(); // Register the service in the container

// more about dependency injection: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
builder.Services.AddEndpointsApiExplorer(); // Add services to the container for API Explorer (used by Swashbuckle)
builder.Services.AddSwaggerGen(); // Add services to the container for Swagger

var app = builder.Build(); // Create the application instance.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Verify if the environment is development
{
    app.UseDeveloperExceptionPage(); // Adds a developer exception page to the pipeline
    app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwaggerUI(); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
}
{
    app.UseSwagger(); //Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwaggerUI(); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Add the MVC middleware to the pipeline

app.Run(); // Execute the application
