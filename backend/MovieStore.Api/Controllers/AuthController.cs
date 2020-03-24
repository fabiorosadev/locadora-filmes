using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Api.Authentication;
using MovieStore.Services.Users;
using MovieStore.Services.Users.Dto;
//using MovieStore.Api.Models;

namespace MovieStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            this._userService = userService;
        }

        // POST api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto loginData)
        {
            try
            {
                var user = await _userService.Login(loginData);
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                return new LoginResultDto()
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<LoginResultDto>> Register(RegisterDto registerData)
        {
            try
            {
                var user = await _userService.Register(registerData);
                // Gera o Token
                var token = TokenService.GenerateToken(user);

                return new LoginResultDto()
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}