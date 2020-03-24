using AutoMapper;
using MovieStore.Core.Models.Genres;

namespace MovieStore.Services.Genres.Dto
{
    public class GenreMapProfile : Profile
    {
        public GenreMapProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>()
                .ForMember(g => g.Movies, options => options.Ignore());
        }
    }
}