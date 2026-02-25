
using asp.net_api_teaching.Data;
using Microsoft.EntityFrameworkCore;

namespace asp.net_api_teaching
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // config database with sql server
            var connectionString = builder.Configuration.GetConnectionString("MyConnectionDb")
                ?? throw new InvalidOperationException("Connection string 'MyConnectionDb' not found.");

            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    )
                )
            );

            // Add CORS services and define a policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "FrontendReact",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("FrontendReact"); // Apply the named policy
            app.MapControllers();
            app.Run();
        }
    }
}
