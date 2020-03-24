using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MovieStore.Core.Models.Movies;

namespace MovieStore.Core.Models.Genres
{
    public class Genre
    {
        public Genre()
        {
            Movies = new Collection<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public GenreStatusEnum Status { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}