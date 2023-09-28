using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.DBOperations;
using WebApi.Middleware;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IBookStoreDbContext, BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Veritaban� ba�lant�s� ve verileri ba�latma i�lemi
    DataGenerator.Initialize(services);
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.UseCustomExceptionMiddleware();//Authorization �al��t�ktan sonra endpointe gitmeden �nce(request endpointe d��meden �nce)

app.MapControllers();

app.Run();
