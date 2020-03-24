namespace MovieStore.Services.Users.Dto
{
    public class LoginResultDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}