using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieStore.Core.Models.Genres;
using MovieStore.Core.Repositories.Genres;

namespace MovieStore.Data.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly IConfiguration _configuration;
        public GenreRepository(DbContext context, IConfiguration configuration) : base(context)
        {
            this._configuration = configuration;
        }

        private MovieStoreDbContext MovieStoreDbContext
        {
            get { return _context as MovieStoreDbContext; }
        }

        public override async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var query = @"SELECT * FROM Genres;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                IEnumerable<Genre> genres = await connection.QueryAsync<Genre>(query);
                return genres.ToList();
            }
        }

        public async Task<IEnumerable<Genre>> GetAllWithMoviesAsync()
        {
            return await MovieStoreDbContext.Genres.Include(g => g.Movies).ToListAsync();
        }

        public async Task<Genre> GetWithMoviesByIdAsync(int id)
        {
            return await MovieStoreDbContext.Genres.Include(g => g.Movies).SingleOrDefaultAsync(g => g.Id == id);
        }
    }
}