using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MovieStore.Core.Models.Genres;
using MovieStore.Core.Models.MovieRentals;

namespace MovieStore.Core.Models.Movies
{
    public class Movie
    {
        public Movie()
        {
            MovieRentals = new Collection<MovieRental>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public MovieStatusEnum Status { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MovieRental> MovieRentals { get; set; }
    }
}