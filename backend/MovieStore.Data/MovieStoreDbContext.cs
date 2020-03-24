using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Models.Genres;
using MovieStore.Core.Models.MovieRentals;
using MovieStore.Core.Models.Movies;
using MovieStore.Core.Models.Rentals;
using MovieStore.Core.Models.Users;
using MovieStore.Data.Configurations;

namespace MovieStore.Data
{
    public class MovieStoreDbContext : DbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<MovieRental> MovieRentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new MovieConfiguration());

            modelBuilder
                .ApplyConfiguration(new GenreConfiguration());

            modelBuilder
                .ApplyConfiguration(new RentalConfiguration());

            modelBuilder
                .ApplyConfiguration(new MovieRentalConfiguration());
        }

    }
}