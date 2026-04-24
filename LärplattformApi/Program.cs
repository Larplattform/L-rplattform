using Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<Business.Interfaces.ICourseInterface, Business.Services.CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.


    

app.MapOpenApi();
    

app.UseSwaggerUI(options =>
   options.SwaggerEndpoint("/openapi/v1.json", "Lärplattform Api"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"))
            .ExcludeFromDescription();


app.Run();
