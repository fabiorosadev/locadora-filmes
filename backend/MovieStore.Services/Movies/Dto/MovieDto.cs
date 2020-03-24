using System;
using MovieStore.Core.Models.Movies;
using MovieStore.Services.Genres.Dto;

namespace MovieStore.Services.Movies.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public MovieStatusEnum Status { get; set; }
        public int GenreId { get; set; }
        public GenreDto Genre { get; set; }
    }
}