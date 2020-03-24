using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieStore.Api;
using MovieStore.Data;

namespace MovieStore.Tests
{
    public class MovieStoreApiFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {

                // Adiciona um novo service provider para os testes
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();


                // IMPORTANTE: No .net Core 3.0 e acima, o InMemory do DbContext não está funcionando no ServiceProvider.
                // Para contornar este problema, pesquisando na Web encontrei este Workaround: https://stackoverflow.com/questions/58375527/override-ef-core-dbcontext-in-asp-net-core-webapplicationfactory
                // O qual implementei abaixo:                
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MovieStoreDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<MovieStoreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Compilando o Service Provider
                var sp = services.BuildServiceProvider();


                // Criando o escopo para obter a referência do contexto da base de dados
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var movieStoreDb = scopedServices.GetRequiredService<MovieStoreDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();

                    // Cria a base de dados
                    movieStoreDb.Database.EnsureCreated();

                    try
                    {
                        // Popula a base de dados para testes
                        SeedData.PopulateTestData(movieStoreDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Ocorreu um erro ao popular a base de dados para testes. Erro: {ex.Message}");
                    }
                }

            });
        }
    }
}