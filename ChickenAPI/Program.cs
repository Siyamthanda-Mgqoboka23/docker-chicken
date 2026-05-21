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

            SeedDatabase(app);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseStartup");
            var context = scope.ServiceProvider.GetRequiredService<FarmDbContext>();

            const int maxAttempts = 10;

            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    context.Database.EnsureCreated();

                    if (!context.Chickens.Any())
                    {
                        context.Chickens.AddRange(
                            new Chicken
                            {
                                Name = "Clucky",
                                Breed = "Leghorn",
                                Age = 2,
                                EggProduction = 0.75m,
                                IsPregnant = false,
                                LastVetCheck = new DateTime(2024, 1, 15)
                            },
                            new Chicken
                            {
                                Name = "Feathers",
                                Breed = "Rhode Island Red",
                                Age = 3,
                                EggProduction = 0.60m,
                                IsPregnant = true,
                                LastVetCheck = new DateTime(2024, 2, 20)
                            },
                            new Chicken
                            {
                                Name = "Pecky",
                                Breed = "Plymouth Rock",
                                Age = 1,
                                EggProduction = 0.80m,
                                IsPregnant = false,
                                LastVetCheck = new DateTime(2024, 3, 10)
                            },
                            new Chicken
                            {
                                Name = "Eggy",
                                Breed = "Sussex",
                                Age = 4,
                                EggProduction = 0.50m,
                                IsPregnant = true,
                                LastVetCheck = new DateTime(2024, 1, 30)
                            });

                        context.SaveChanges();
                    }

                    return;
                }
                catch (Exception ex) when (attempt < maxAttempts)
                {
                    logger.LogWarning(ex, "Database was not ready. Retry {Attempt}/{MaxAttempts}", attempt, maxAttempts);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }

            context.Database.EnsureCreated();
        }
    }
}
