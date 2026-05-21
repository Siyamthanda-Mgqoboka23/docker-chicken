using ChickenAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ChickenAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString =
                builder.Configuration.GetConnectionString("SQL_Connection_String") ??
                builder.Configuration["SQL_CONNECTION_STRING"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                // Local default for development when env/config is not set.
                connectionString = "Server=localhost,1433;Database=Farm;User Id=sa;Password=Password1!;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true";
            }

            builder.Services.AddDbContext<FarmDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Chicken API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}