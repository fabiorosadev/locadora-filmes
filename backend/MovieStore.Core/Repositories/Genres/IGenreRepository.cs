using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.Genres;

namespace MovieStore.Core.Repositories.Genres
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetAllWithMoviesAsync();
        Task<Genre> GetWithMoviesByIdAsync(int id);
    }
}