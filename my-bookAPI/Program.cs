using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using my_bookAPI.Data;
using my_bookAPI.Data.Services;
using my_bookAPI.Exceptions;
using my_books.Data.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Configure DBContext with SQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Configure the Services
builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<AuthorsService>();
builder.Services.AddTransient<PublishersService>();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(2, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;

    //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
    config.ApiVersionReader = new MediaTypeApiVersionReader();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Exception Handling
app.ConfigureBuiltInExceptionHandler();
//app.ConfigureCustomExceptionHandler();

app.MapControllers();

//AppDbInitializer.Seed(app);

app.Run();

