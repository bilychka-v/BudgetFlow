using Db;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.Repositories;
using Application.Interfaces;

namespace BudgetFlow;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // builder.WebHost.UseUrls("http://localhost:5236");

        
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectBd")));


        // Add services to the container.
        builder.Services.AddAuthorization();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddControllers();
        
        
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        

        app.Run();
    }
}

// 6. Корисні ресурси
// Офіційний гайд Microsoft по Exception Handling Middleware:
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0
// Приклад реалізації кастомних помилок в Clean Architecture:
// https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Application/Common/Exceptions
// Стаття: "Best practices for error handling in ASP.NET Core"
// https://exceptionnotfound.net/error-handling-in-asp-net-core-web-api/

// “gRPC in .NET 6 Tutorial” — IAmTimCorey
// https://www.youtube.com/watch?v=F82Jt3g3-8c
// (дуже зрозуміло, крок за кроком)
// “ASP.NET Core gRPC Tutorial” — Microsoft Developer
// https://www.youtube.com/watch?v=0gxr1B-p2N0
// (офіційне відео від Microsoft)
// “gRPC with .NET Core” — Nick Chapsas
// https://www.youtube.com/watch?v=KjBF8D5vM0s
// (глибший погляд із прикладами)
