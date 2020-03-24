using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Services.MovieRentals.Dto;

namespace MovieStore.Services.MovieRentals
{
    public interface IMovieRentalService
    {
        Task<IEnumerable<MovieRentalWithRelationsDto>> GetAllByMovieId(int movieId);
        Task<IEnumerable<MovieRentalWithRelationsDto>> GetAllByRentalId(int rentalId);
    }
}