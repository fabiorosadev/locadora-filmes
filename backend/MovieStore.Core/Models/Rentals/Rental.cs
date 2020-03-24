using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MovieStore.Core.Models.MovieRentals;

namespace MovieStore.Core.Models.Rentals
{
    public class Rental
    {
        public Rental()
        {
            MovieRentals = new Collection<MovieRental>();
        }

        public int Id { get; set; }
        public string CustomerCpf { get; set; }
        public DateTime RentalDate { get; set; }
        public ICollection<MovieRental> MovieRentals { get; set; }
    }
}