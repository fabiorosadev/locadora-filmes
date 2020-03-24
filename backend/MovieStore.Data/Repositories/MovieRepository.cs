using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieStore.Core.Models.Genres;
using MovieStore.Core.Models.Movies;
using MovieStore.Core.Repositories.Movies;

namespace MovieStore.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly IConfiguration _configuration;
        public MovieRepository(MovieStoreDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        private MovieStoreDbContext MovieStoreDbContext
        {
            get { return _context as MovieStoreDbContext; }
        }

        public async Task<IEnumerable<Movie>> GetAllByGenreId(int genreId)
        {
            return await MovieStoreDbContext.Movies.Where(m => m.GenreId == genreId).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllWithGenreAsync()
        {
            // var query = @"SELECT * FROM Movies AS A INNER JOIN Genres AS B ON A.GenreId = B.Id;";
            // using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            // {
            //     IEnumerable<Movie> moviesWithGenre = await connection.QueryAsync<Movie, Genre, Movie>(query, (movie, genre) =>
            //     {
            //         movie.Genre = genre;
            //         return movie;
            //     },
            //     splitOn: "GenreId");


            //     return moviesWithGenre.ToList();
            // }
            // Consulta padrão com EntityFramework
            return await MovieStoreDbContext.Movies.Include(m => m.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllWithGenreByGenreId(int genreId)
        {
            return await MovieStoreDbContext.Movies.Include(m => m.Genre).Where(m => m.GenreId == genreId).ToListAsync();
        }

        public async Task<Movie> GetWithGenreByIdAsync(int id)
        {
            return await MovieStoreDbContext.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}