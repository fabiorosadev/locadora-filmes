using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.Genres;
using MovieStore.Services.Genres.Dto;

namespace MovieStore.Services.Genres
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenres();
        Task<GenreDto> GetGenreById(int id);
        Task<GenreDto> CreateGenre(GenreDto genre);
        Task<GenreDto> UpdateGenre(GenreDto genre);
        Task DeleteGenre(GenreDto genre);
        Task DeleteGenres(IEnumerable<int> genreIds);
    }
}