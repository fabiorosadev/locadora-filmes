using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MovieStore.Services.MovieRentals.Dto;

namespace MovieStore.Services.Rentals.Dto
{
    public class RentalDto
    {
        public RentalDto()
        {
            MovieRentals = new Collection<MovieRentalWithMovieDto>();
        }

        public int Id { get; set; }
        public string CustomerCpf { get; set; }
        public DateTime RentalDate { get; set; }
        public ICollection<MovieRentalWithMovieDto> MovieRentals { get; set; }
    }
}