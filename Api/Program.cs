using Db;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.Repositories;
using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

namespace BudgetFlow;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.WebHost.UseUrls("http://localhost:5236");
        
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
        
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
        
        builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
        
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        // builder.Services.AddScoped<JwtOptions>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<ExceptionMiddleware>();

        // app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();
        app.MapControllers();
        
        app.Run();
    }
}

