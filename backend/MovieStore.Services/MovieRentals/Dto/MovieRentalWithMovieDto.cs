using MovieStore.Services.Movies.Dto;

namespace MovieStore.Services.MovieRentals.Dto
{
    public class MovieRentalWithMovieDto : MovieRentalDto
    {
        public MovieDto Movie { get; set; }
    }
}