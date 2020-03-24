using System.Threading.Tasks;
using MovieStore.Services.Users.Dto;

namespace MovieStore.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> Login(LoginDto loginData);
        Task<UserDto> Register(RegisterDto registerData);
    }
}