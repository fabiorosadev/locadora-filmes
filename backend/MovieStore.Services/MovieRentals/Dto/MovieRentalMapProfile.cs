using AutoMapper;
using MovieStore.Core.Models.MovieRentals;

namespace MovieStore.Services.MovieRentals.Dto
{
    public class MovieRentalMapProfile : Profile
    {
        public MovieRentalMapProfile()
        {
            CreateMap<MovieRental, MovieRentalDto>();
            CreateMap<MovieRentalDto, MovieRental>();
            CreateMap<MovieRentalWithMovieDto, MovieRental>()
                .ForMember(x => x.Movie, options => options.Ignore());
            CreateMap<MovieRental, MovieRentalWithRelationsDto>();
            CreateMap<MovieRental, MovieRentalWithMovieDto>();
        }
    }
}