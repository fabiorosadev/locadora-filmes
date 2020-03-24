using System;
using MovieStore.Core.Models.Genres;

namespace MovieStore.Services.Genres.Dto
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public GenreStatusEnum Status { get; set; }
    }
}