using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core;
using MovieStore.Services.MovieRentals.Dto;

namespace MovieStore.Services.MovieRentals
{
    public class MovieRentalService : IMovieRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieRentalService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MovieRentalWithRelationsDto>> GetAllByMovieId(int movieId)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MovieRentalWithRelationsDto>>(await _unitOfWork.MovieRentals.GetAllWithMovieAndRentalByMovieId(movieId));
        }

        public async Task<IEnumerable<MovieRentalWithRelationsDto>> GetAllByRentalId(int rentalId)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MovieRentalWithRelationsDto>>(await _unitOfWork.MovieRentals.GetAllWithMovieAndRentalByRentalId(rentalId));
        }
    }
}