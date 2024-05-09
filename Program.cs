
using ChatAppServer.WebAPI.Contex;
using ChatAppServer.WebAPI.Hubs;
using DefaultCorsPolicyNugetPackage;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDefaultCors();


        builder.Services.AddDbContext<ApplicationDbContex>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")).UseSnakeCaseNamingConvention();
        });

        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddSignalR();

        builder.Services.AddControllers();
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
        app.UseStaticFiles();
        app.UseCors();

        app.UseAuthorization();


        app.MapControllers();

        app.MapHub<ChatHub>("/chat-hub");


        app.Run();
    }
}
