using AutoMapper;
using MovieStore.Core.Models.Rentals;

namespace MovieStore.Services.Rentals.Dto
{
    public class RentalMapProfile : Profile
    {
        public RentalMapProfile()
        {
            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();
            //.ForMember(x => x.MovieRentals, options => options.Ignore());
        }
    }
}