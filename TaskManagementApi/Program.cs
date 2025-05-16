// Program.cs
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;

var builder = WebApplication.CreateBuilder(args); //Creates a WebApplicationBuilder instance, which is used to configure the application and its services.

// Add services to the container
//Add Services to the Dependency Injection Container
builder.Services.AddControllers();

// Add DbContext using SQL Server
//Registers the controllers in the application, enabling the use of MVC or Web API patterns.
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Registers the TaskDbContext with the dependency injection container.
//Configures it to use SQL Server with a connection string named DefaultConnection from the appsettings.json fil
builder.Services.AddEndpointsApiExplorer();
//Adds support for generating Swagger/OpenAPI documentation for the API.
builder.Services.AddSwaggerGen();

//Build the app

var app = builder.Build();

// Configure the HTTP request pipeline (middleware pipeline)
//Enables Swagger and Swagger UI for API documentation when the application is running in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Redirects HTTP requests to HTTPS for secure communication.

app.UseHttpsRedirection();
//Adds authorization middleware to the pipeline (though no specific policies are defined in this code).

app.UseAuthorization();

//Maps controller endpoints to handle incoming HTTP requests.

app.MapControllers();

// Create and seed the database if it doesn't exist
// Creates a scoped service provider to resolve services.
// Retrieves the TaskDbContext from the service provider.
// Calls EnsureCreated() to create the database if it doesn't already exist.
// This ensures that the database is ready and seeded with initial data (as defined in TaskDbContext).
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskDbContext>();
    context.Database.EnsureCreated();
}
//Starts the application and begins listening for incoming HTTP requests.

app.Run();

// Summary
// Dependency Injection: Configures services like TaskDbContext, controllers, and Swagger.
// Database Setup: Ensures the database is created and seeded with initial data.
// Middleware Pipeline: Configures middleware for HTTPS redirection, Swagger, and controller routing.
// Application Startup: Starts the application and listens for requests.