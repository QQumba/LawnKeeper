using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : JwtController
    {
        private readonly ILogger _logger;

        public UserController(ILogger<UserController> logger, UserService userService) : base(userService)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(CancellationToken ct)
        {
            var user = await GetCurrentUserAsync(ct);
            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            if (!model.Email.Equals(Email))
            {
                return BadRequest();
            }
            
            var user = await UserService.UpdateAsync(model);
            if (user is null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            await UserService.DeleteAsync(Email);
            return Ok();
        }
    }
}