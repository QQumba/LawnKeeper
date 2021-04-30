using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Contract.ViewModels.Auth;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : JwtController
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, UserService userService, IConfiguration configuration) : base(userService)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupViewModel model, CancellationToken ct)
        {
            model.Role = Role.User.ToString();
            
            var user = await UserService.CreateAsync(model, ct);
            if (user is null)
            {
                return BadRequest("User with specified email already exists.");
            }
            
            _logger.Log(LogLevel.Information,$"New user created. User email: {model.Email}");
            
            var token = await AuthenticateAsync(new LoginViewModel()
            {
                Email = model.Email,
                Password = model.Password
            }, ct);

            if (token is null)
            {
                _logger.Log(LogLevel.Information, $"User can not be authenticated. User email: {model.Email}");
                return BadRequest();
            }
            
            _logger.Log(LogLevel.Information, $"User authenticated. User email: {model.Email}");
            return Ok(token);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken ct)
        {
            var token = await AuthenticateAsync(model, ct);

            if (token is null)
            {
                _logger.Log(LogLevel.Information, $"User can not be authenticated. User email: {model.Email}");
                return BadRequest();
            }

            _logger.Log(LogLevel.Information, $"User authenticated. User email: {model.Email}");

            return Ok(token);
        }

        [HttpPost("login-as-admin")]
        public async Task<IActionResult> AdminLogin(string login, string password, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }
            if (!login.Equals(_configuration["Administration:Login"]) ||
                !password.Equals(_configuration["Administration:Password"]))
            {
                return BadRequest();
            }
            
            var user = await UserService.GetByEmailAsync(login, ct);
            if (user is null)
            {
                await UserService.CreateAsync(new SignupViewModel
                {
                    Email = login,
                    Name = login,
                    Role = Role.Admin.ToString(),
                    Password = password,
                    PasswordConfirmation = password
                }, ct);
            }
            var token = await AuthenticateAsync(new LoginViewModel
            {
                Email = login,
                Password = password
            }, ct);

            return Ok(token);
        }
        
        private async Task<Jwt> AuthenticateAsync(LoginViewModel model, CancellationToken ct = default)
        {
            var user = await UserService.GetVerifiedUserAsync(model, ct);

            if (user is null)
            {
                _logger.Log(LogLevel.Information, $"User not found. User email: {model.Email}");
                return null;
            }

            _logger.Log(LogLevel.Information, $"User found. User email: {user.Email}");
            
            var provider = new JwtProvider(_configuration);
            return provider.GenerateJwtToken(user);
        }
    }
}