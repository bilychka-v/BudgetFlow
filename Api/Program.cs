using Db;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.Repositories;
using Application.Interfaces;
using Microsoft.AspNetCore.Diagnostics;

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
        
        builder.Services.AddExceptionHandler(options =>
        {
            options.ExceptionHandlingPath = "/error";
        });
        

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
        
        
        app.UseMiddleware<ExceptionMiddleware>();
        

        app.Run();
    }
}




// “gRPC in .NET 6 Tutorial” — IAmTimCorey
// https://www.youtube.com/watch?v=F82Jt3g3-8c
// (дуже зрозуміло, крок за кроком)
// “ASP.NET Core gRPC Tutorial” — Microsoft Developer
// https://www.youtube.com/watch?v=0gxr1B-p2N0
// (офіційне відео від Microsoft)
// “gRPC with .NET Core” — Nick Chapsas
// https://www.youtube.com/watch?v=KjBF8D5vM0s
// (глибший погляд із прикладами)
