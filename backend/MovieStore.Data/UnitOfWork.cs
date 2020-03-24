using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieStore.Core;
using MovieStore.Core.Repositories.Genres;
using MovieStore.Core.Repositories.MovieRentals;
using MovieStore.Core.Repositories.Movies;
using MovieStore.Core.Repositories.Rentals;
using MovieStore.Core.Repositories.Users;
using MovieStore.Data.Repositories;

namespace MovieStore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieStoreDbContext _context;

        private UserRepository _userRepository;
        private MovieRepository _movieRepository;
        private GenreRepository _genreRepository;
        private RentalRepository _rentalRepository;
        private MovieRentalRepository _movieRentalRepository;
        private readonly IConfiguration _configuration;

        public UnitOfWork(MovieStoreDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public IMovieRepository Movies => _movieRepository = _movieRepository ?? new MovieRepository(_context, _configuration);

        public IGenreRepository Genres => _genreRepository = _genreRepository ?? new GenreRepository(_context, _configuration);

        public IRentalRepository Rentals => _rentalRepository = _rentalRepository ?? new RentalRepository(_context);

        public IMovieRentalRepository MovieRentals => _movieRentalRepository = _movieRentalRepository ?? new MovieRentalRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}