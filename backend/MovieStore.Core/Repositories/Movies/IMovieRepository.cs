using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.Movies;

namespace MovieStore.Core.Repositories.Movies
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAllWithGenreAsync();
        Task<Movie> GetWithGenreByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllByGenreId(int genreId);
        Task<IEnumerable<Movie>> GetAllWithGenreByGenreId(int genreId);
    }
}