using System;
using System.Threading.Tasks;
using MovieStore.Core.Repositories.Genres;
using MovieStore.Core.Repositories.MovieRentals;
using MovieStore.Core.Repositories.Movies;
using MovieStore.Core.Repositories.Rentals;
using MovieStore.Core.Repositories.Users;

namespace MovieStore.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IRentalRepository Rentals { get; }
        IMovieRentalRepository MovieRentals { get; }
        Task<int> CommitAsync();
    }
}