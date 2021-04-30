using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/lawn")]
    public class LawnController : JwtController
    {
        private readonly ILogger _logger;
        private readonly LawnService _lawnService;

        public LawnController(ILogger<LawnController> logger, UserService userService, LawnService lawnService) : base(userService)
        {
            _logger = logger;
            _lawnService = lawnService;
        }

        [HttpGet]
        [Route("{lawnId}")]
        public async Task<IActionResult> GetLawn([FromRoute] int lawnId)
        {
            var lawn = await _lawnService.GetByIdAsync(lawnId);
            if (lawn is null)
            {
                return NotFound();
            }

            return Ok(lawn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLawn(LawnCreationViewModel model)
        {
            var lawnViewModel = model.Adapt<LawnViewModel>();
            lawnViewModel.UserEmail = Email;
            var lawn = await _lawnService.CreateAsync(lawnViewModel);
            if (lawn is null)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserLawns()
        {
            var lawns = await _lawnService.GetUserLawnsAsync(Email);
            return Ok(lawns);
        }

        [HttpGet]
        [Route("all")]
        [Authorize(nameof(Role.Admin))]
        public async Task<IActionResult> GetAllLawns()
        {
            var lawns = await _lawnService.GetUserLawnsAsync(Email);
            return Ok(lawns);
        }
        
        [HttpPut]
        [Route("on/{lawnId}")]
        public async Task<IActionResult> EnableWatering([FromRoute] int lawnId)
        {
            await _lawnService.EnableWateringAsync();
            _logger.Log(LogLevel.Information, $"Lawn watering turned on for. Lawn id: {lawnId}");
            
            return Ok();
        }
        
        [HttpPut]
        [Route("off/{lawnId}")]
        public async Task<IActionResult> DisableWatering([FromRoute] int lawnId)
        {
            await _lawnService.DisableWateringAsync();
            _logger.Log(LogLevel.Information, $"Lawn watering turned off for. Lawn id: {lawnId}");
            
            return Ok();
        }

        [HttpPost]
        [Route("schedule/{lawnId}")]
        public async Task<IActionResult> SetSchedule([FromRoute] int lawnId)
        {
            return Ok();
        }
    }
}