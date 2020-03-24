using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Models.Rentals;
using MovieStore.Core.Repositories.Rentals;

namespace MovieStore.Data.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }
        private MovieStoreDbContext MovieStoreDbContext
        {
            get { return _context as MovieStoreDbContext; }
        }

        public async Task<IEnumerable<Rental>> GetAllByCustomerCpfAsync(string customerCpf)
        {
            return await MovieStoreDbContext.Rentals.Where(r => r.CustomerCpf == customerCpf).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllWithMoviesAsync()
        {
            return await MovieStoreDbContext.Rentals.Include(r => r.MovieRentals).ThenInclude(mr => mr.Movie).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllWithMoviesByCustomerCpfAsync(string customerCpf)
        {
            return await MovieStoreDbContext.Rentals.Include(r => r.MovieRentals).ThenInclude(mr => mr.Movie).Where(r => r.CustomerCpf == customerCpf).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllWithMoviesByMovieIdAsync(int movieId)
        {
            return await MovieStoreDbContext.Rentals.Include(r => r.MovieRentals).ThenInclude(mr => mr.Movie).Where(r => r.MovieRentals.Any(mr => mr.MovieId == movieId)).ToListAsync();
        }

        public async Task<Rental> GetWithMoviesByIdAsync(int id)
        {
            return await MovieStoreDbContext.Rentals.Include(r => r.MovieRentals).ThenInclude(mr => mr.Movie).SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}