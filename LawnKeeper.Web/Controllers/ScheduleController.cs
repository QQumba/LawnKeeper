using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    [Authorize]
    public class ScheduleController : JwtController
    {
        private readonly ScheduleService _scheduleService;
        private readonly ILogger _logger;
        
        public ScheduleController(UserService userService, ScheduleService scheduleService, ILogger<ScheduleController> logger) : base(userService)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule is null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        [HttpGet]
        [Authorize(nameof(Role.Admin))]
        public async Task<IActionResult> GetAllSchedules()
        {
            return Ok(await _scheduleService.GetAllSchedulesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleViewModel model)
        {
            var schedule = await _scheduleService.CreateScheduleAsync(model);

            if (schedule is null)
            {
                return BadRequest();
            }
            return Ok(schedule);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchedule(ScheduleViewModel model)
        {
            var schedule = await _scheduleService.UpdateScheduleAsync(model);

            if (schedule is null)
            {
                return BadRequest();
            }
            return Ok(schedule);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleService.DeleteScheduleByIdAsync(id);
            return Ok();
        }
    }
}