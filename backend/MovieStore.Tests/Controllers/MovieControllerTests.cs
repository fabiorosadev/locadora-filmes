using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Api;
using MovieStore.Core.Models.Movies;
using MovieStore.Services.Movies.Dto;
using MovieStore.Services.Users.Dto;
using Newtonsoft.Json;
using Xunit;

namespace MovieStore.Tests.Controllers
{
    public class MovieControllerTests : IClassFixture<MovieStoreApiFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly MovieStoreApiFactory<Startup> _factory;

        public MovieControllerTests(MovieStoreApiFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private async Task<string> GetToken()
        {
            var userData = new
            {
                username = "moviestoretests",
                password = "MovieTest#123",
                role = "Admin"
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var registerJsonContent = JsonConvert.SerializeObject(userData);

            var contentString = new StringContent(registerJsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse;

            httpResponse = await _client.PostAsync("/api/auth/login", contentString);

            if (!httpResponse.IsSuccessStatusCode)
                httpResponse = await _client.PostAsync("/api/auth/register", contentString);

            if (httpResponse.IsSuccessStatusCode)
            {
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResultDto>(stringResponse);
                return result.Token;
            }

            return string.Empty;

        }

        [Fact]
        public async Task CanGetMovies()
        {
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Chama o endpoint
            var httpResponse = await _client.GetAsync("/api/Movies");

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

            // Faz leitura do retorno e verifica alguns filmes que devem ter retornado.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieDto>>(stringResponse);
            Assert.Contains(movies, m => m.Name == "O Rei Leão");
            Assert.Contains(movies, m => m.Name == "Aladdin");
        }

        [Fact]
        public async Task CanGetMovieById()
        {
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Chama o endpoint
            var httpResponse = await _client.GetAsync("/api/Movies/1");

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

            // Faz leitura do retorno e verifica algumas propriedades
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<MovieDto>(stringResponse);

            Assert.Equal(1, movie.Id);
            Assert.Equal("Vingadores Ultimato", movie.Name);
        }

        [Fact]
        public async Task CanInsertMovie()
        {

            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var newMovie = new MovieDto()
            {
                Name = "Filme Teste",
                Status = MovieStatusEnum.Active,
                GenreId = 1,
                CreationDate = DateTime.UtcNow
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var movieJsonContent = JsonConvert.SerializeObject(newMovie);

            var contentString = new StringContent(movieJsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Chama o endpoint
            var httpResponse = await _client.PostAsync("/api/Movies", contentString);

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task CanUpdateMovie()
        {
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var newMovie = new MovieDto()
            {
                Id = 2,
                Name = "Filme Teste",
                Status = MovieStatusEnum.Active,
                GenreId = 1,
                CreationDate = DateTime.UtcNow
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var movieJsonContent = JsonConvert.SerializeObject(newMovie);

            var contentString = new StringContent(movieJsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Chama o endpoint
            var httpResponse = await _client.PutAsync("/api/Movies", contentString);

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

            var httpResponseUpdated = await _client.GetAsync("/api/Movies/2");

            // Faz leitura do retorno e verifica algumas propriedades
            var stringResponse = await httpResponseUpdated.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<MovieDto>(stringResponse);

            Assert.Equal(2, movie.Id);
            Assert.Equal("Filme Teste", movie.Name);
        }

        [Fact]
        public async Task CanDeleteMovie()
        {
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.DeleteAsync("/api/Movies/3");

            httpResponse.EnsureSuccessStatusCode();

            httpResponse = await _client.GetAsync("/api/Movies");

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

            // Faz leitura do retorno e verifica alguns filmes que devem ter retornado.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieDto>>(stringResponse);
            Assert.DoesNotContain(movies, m => m.Name == "Toy Story 4");
            Assert.DoesNotContain(movies, m => m.Id == 3);

        }

        [Fact]
        public async Task CanDeleteMultipleMovies()
        {
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var idsToDelete = new int[2] { 5, 6 };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var movieJsonContent = JsonConvert.SerializeObject(idsToDelete);

            var contentString = new StringContent(movieJsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var httpResponse = await _client.PostAsync("/api/Movies/RemoveMultiple", contentString);

            httpResponse.EnsureSuccessStatusCode();

            httpResponse = await _client.GetAsync("/api/Movies");

            // Verifica se o retorno é OK
            httpResponse.EnsureSuccessStatusCode();

            // Faz leitura do retorno e verifica alguns filmes que devem ter retornado.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieDto>>(stringResponse);
            Assert.DoesNotContain(movies, m => m.Id == 5);
            Assert.DoesNotContain(movies, m => m.Id == 6);
        }
    }
}