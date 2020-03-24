using System;
using System.Linq;
using MovieStore.Core.Models.Genres;
using MovieStore.Core.Models.Movies;
using MovieStore.Data;

namespace MovieStore.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(MovieStoreDbContext dbContext)
        {
            // Gêneros
            var genre1 = new Genre() { Name = "Ação", Status = GenreStatusEnum.Active };
            var genre2 = new Genre() { Name = "Aventura", Status = GenreStatusEnum.Active };
            var genre3 = new Genre() { Name = "Comédia", Status = GenreStatusEnum.Active };
            var genre4 = new Genre() { Name = "Ficção Científica", Status = GenreStatusEnum.Active };
            var genre5 = new Genre() { Name = "Musical", Status = GenreStatusEnum.Active };


            dbContext.Genres.Add(genre1);
            dbContext.Genres.Add(genre2);
            dbContext.Genres.Add(genre3);
            dbContext.Genres.Add(genre4);
            dbContext.Genres.Add(genre5);

            // Filmes
            dbContext.Movies.Add(new Movie() { Name = "Vingadores Ultimato", Status = MovieStatusEnum.Active, GenreId = genre1.Id });
            dbContext.Movies.Add(new Movie() { Name = "John Wick 3", Status = MovieStatusEnum.Active, GenreId = genre1.Id });
            dbContext.Movies.Add(new Movie() { Name = "Toy Story 4", Status = MovieStatusEnum.Active, GenreId = genre2.Id });
            dbContext.Movies.Add(new Movie() { Name = "Aladdin", Status = MovieStatusEnum.Active, GenreId = genre2.Id });
            dbContext.Movies.Add(new Movie() { Name = "Parasita", Status = MovieStatusEnum.Active, GenreId = genre3.Id });
            dbContext.Movies.Add(new Movie() { Name = "Star Wars: Episódio IX", Status = MovieStatusEnum.Active, GenreId = genre4.Id });
            dbContext.Movies.Add(new Movie() { Name = "O Rei Leão", Status = MovieStatusEnum.Active, GenreId = genre5.Id });

            dbContext.SaveChanges();
        }
    }
}