using MovieStore.Core.Models.Movies;
using MovieStore.Core.Models.Rentals;

namespace MovieStore.Core.Models.MovieRentals
{
    public class MovieRental
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}