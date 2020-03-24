using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core;
using MovieStore.Core.Models.Genres;
using MovieStore.Services.Genres.Dto;

namespace MovieStore.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<GenreDto> CreateGenre(GenreDto genre)
        {
            var entityGenre = ObjectMapper.Mapper.Map<Genre>(genre);
            await _unitOfWork.Genres.AddAsync(entityGenre);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<GenreDto>(entityGenre);
        }

        public async Task DeleteGenre(GenreDto genre)
        {
            var entityGenre = await _unitOfWork.Genres.GetByIdAsync(genre.Id);
            _unitOfWork.Genres.Remove(entityGenre);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteGenres(IEnumerable<int> genreIds)
        {
            var genres = _unitOfWork.Genres.Find(x => genreIds.Any(i => i == x.Id));
            _unitOfWork.Genres.RemoveRange(genres);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenres()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<GenreDto>>(await _unitOfWork.Genres.GetAllAsync());
        }

        public async Task<GenreDto> GetGenreById(int id)
        {
            return ObjectMapper.Mapper.Map<GenreDto>(await _unitOfWork.Genres.GetByIdAsync(id));
        }

        public async Task<GenreDto> UpdateGenre(GenreDto genre)
        {
            var entityGenre = await _unitOfWork.Movies.GetByIdAsync(genre.Id);
            entityGenre = ObjectMapper.Mapper.Map(genre, entityGenre);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<GenreDto>(entityGenre);
        }
    }
}