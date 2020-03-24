using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Models.Rentals;

namespace MovieStore.Core.Repositories.Rentals
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetAllByCustomerCpfAsync(string customerCpf);
        Task<IEnumerable<Rental>> GetAllWithMoviesByCustomerCpfAsync(string customerCpf);
        Task<IEnumerable<Rental>> GetAllWithMoviesAsync();
        Task<Rental> GetWithMoviesByIdAsync(int id);
        Task<IEnumerable<Rental>> GetAllWithMoviesByMovieIdAsync(int movieId);
    }
}