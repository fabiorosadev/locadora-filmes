using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.Movies;
using MovieStore.Services.Movies.Dto;

namespace MovieStore.Services.Movies
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllWithGenre();
        Task<MovieDto> GetMovieById(int id);
        Task<IEnumerable<MovieDto>> GetMoviesByGenreId(int genreId);
        Task<bool> ExistMoviesWithGenreIds(IEnumerable<int> genreIds);
        Task<MovieDto> CreateMovie(MovieDto newMovie);
        Task<MovieDto> UpdateMovie(MovieDto movie);
        Task DeleteMovie(MovieDto movie);
        Task DeleteMovies(IEnumerable<int> movieIds);

    }
}