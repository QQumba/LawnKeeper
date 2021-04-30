using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace LawnKeeper.Web.JwtUtils
{
    public abstract class JwtController : ControllerBase
    {
        private readonly UserService _userService;
        
        protected JwtController(UserService userService)
        {
            _userService = userService;
        }
        
        protected UserService UserService => _userService;
        protected string Email => User.Identity?.Name;
        
        protected async Task<UserViewModel> GetCurrentUserAsync(CancellationToken ct = default)
        {
            return await _userService.GetByEmailAsync(Email, ct);
        }
    }
}