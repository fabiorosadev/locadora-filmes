using AutoMapper;
using MovieStore.Core.Models.Movies;

namespace MovieStore.Services.Movies.Dto
{
    public class MovieMapProfile : Profile
    {
        public MovieMapProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
        }
    }
}