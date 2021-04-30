using System.Threading.Tasks;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Services;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawnKeeper.Web.Controllers
{
    
    [ApiController]
    [Authorize(nameof(Role.Admin))] //TODO: add role "admin"
    [Route("api/admin")]
    public class AdministrationController : JwtController
    {
        private readonly DatabaseBackupService _backupService;
        
        public AdministrationController(UserService userService, DatabaseBackupService backupService) : base(userService)
        {
            _backupService = backupService;
        }

        [HttpPut]
        [Route("backup")]
        public IActionResult Backup()
        {
            _backupService.Backup();
            return Ok();
        }

        [HttpPut]
        [Route("restore")]
        public IActionResult RestoreLatest()
        {
            var restored = _backupService.RestoreLatest();
            if (!restored)
            {
                return Forbid();
            }

            return Ok();
        }
        
        
    }
}