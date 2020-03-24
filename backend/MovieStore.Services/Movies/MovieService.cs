using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core;
using MovieStore.Core.Models.Movies;
using MovieStore.Services.Movies.Dto;

namespace MovieStore.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<MovieDto> CreateMovie(MovieDto newMovie)
        {
            var entityMovie = ObjectMapper.Mapper.Map<Movie>(newMovie);
            await _unitOfWork.Movies.AddAsync(entityMovie);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<MovieDto>(entityMovie);
        }

        public async Task DeleteMovie(MovieDto movie)
        {
            var entityMovie = await _unitOfWork.Movies.GetByIdAsync(movie.Id);
            _unitOfWork.Movies.Remove(entityMovie);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMovies(IEnumerable<int> movieIds)
        {
            var movies = _unitOfWork.Movies.Find(m => movieIds.Any(i => i == m.Id));
            _unitOfWork.Movies.RemoveRange(movies);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistMoviesWithGenreIds(IEnumerable<int> genreIds)
        {
            return await Task.FromResult(_unitOfWork.Movies.Find(m => genreIds.Any(i => i == m.GenreId)).Any());
        }

        public async Task<IEnumerable<MovieDto>> GetAllWithGenre()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MovieDto>>(await _unitOfWork.Movies.GetAllWithGenreAsync());
        }

        public async Task<MovieDto> GetMovieById(int id)
        {
            return ObjectMapper.Mapper.Map<MovieDto>(await _unitOfWork.Movies.GetWithGenreByIdAsync(id));
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesByGenreId(int genreId)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MovieDto>>(await _unitOfWork.Movies.GetAllWithGenreByGenreId(genreId));
        }

        public async Task<MovieDto> UpdateMovie(MovieDto movie)
        {
            var entityMovie = await _unitOfWork.Movies.GetByIdAsync(movie.Id);
            entityMovie = ObjectMapper.Mapper.Map(movie, entityMovie);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<MovieDto>(entityMovie);
        }
    }
}