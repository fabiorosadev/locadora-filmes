using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.MovieRentals;

namespace MovieStore.Core.Repositories.MovieRentals
{
    public interface IMovieRentalRepository : IRepository<MovieRental>
    {
        Task<IEnumerable<MovieRental>> GetAllWithMovieAndRental();
        Task<IEnumerable<MovieRental>> GetAllWithMovieAndRentalByMovieId(int movieId);
        Task<IEnumerable<MovieRental>> GetAllWithMovieAndRentalByRentalId(int rentalId);
    }
}