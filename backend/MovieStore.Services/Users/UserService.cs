using System.Threading.Tasks;
using MovieStore.Core;
using MovieStore.Core.Models.Users;
using RockLib.Encryption;

namespace MovieStore.Services.Users.Dto
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Login(LoginDto loginData)
        {
            var user = await _unitOfWork.Users.GetByUserName(loginData.UserName);
            if (user != null)
            {
                var decryptedPassword = Crypto.Decrypt(user.Password);
                if (decryptedPassword == loginData.Password)
                {
                    return ObjectMapper.Mapper.Map<UserDto>(user);
                }
            }

            return null;
        }

        public async Task<UserDto> Register(RegisterDto registerData)
        {
            var user = ObjectMapper.Mapper.Map<User>(registerData);
            user.Password = Crypto.Encrypt(registerData.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<UserDto>(user);
        }
    }
}