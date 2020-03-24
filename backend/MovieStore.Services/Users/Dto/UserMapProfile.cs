using AutoMapper;
using MovieStore.Core.Models.Users;

namespace MovieStore.Services.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
        }
    }
}