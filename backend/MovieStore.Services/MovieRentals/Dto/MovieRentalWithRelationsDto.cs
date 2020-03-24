using MovieStore.Services.Movies.Dto;
using MovieStore.Services.Rentals.Dto;

namespace MovieStore.Services.MovieRentals.Dto
{
    public class MovieRentalWithRelationsDto : MovieRentalDto
    {
        public RentalDto Rental { get; set; }
        public MovieDto Movie { get; set; }
    }
}