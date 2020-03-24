using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Models.MovieRentals;
using MovieStore.Core.Repositories.MovieRentals;

namespace MovieStore.Data.Repositories
{
    public class MovieRentalRepository : Repository<MovieRental>, IMovieRentalRepository
    {
        public MovieRentalRepository(DbContext context) : base(context)
        {
        }

        private MovieStoreDbContext MovieStoreDbContext
        {
            get { return _context as MovieStoreDbContext; }
        }

        public async Task<IEnumerable<MovieRental>> GetAllWithMovieAndRental()
        {
            return await MovieStoreDbContext.MovieRentals.Include(mr => mr.Movie).Include(mr => mr.Rental).ToListAsync();
        }

        public async Task<IEnumerable<MovieRental>> GetAllWithMovieAndRentalByMovieId(int movieId)
        {
            return await MovieStoreDbContext.MovieRentals.Include(mr => mr.Movie).Include(mr => mr.Rental).Where(mr => mr.MovieId == movieId).ToListAsync();
        }

        public async Task<IEnumerable<MovieRental>> GetAllWithMovieAndRentalByRentalId(int rentalId)
        {
            return await MovieStoreDbContext.MovieRentals.Include(mr => mr.Movie).Include(mr => mr.Rental).Where(mr => mr.RentalId == rentalId).ToListAsync();
        }
    }
}