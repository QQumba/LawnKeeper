using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUaCities()
        {
            var path = @"C:/city.list.json";
            var jsonString = await System.IO.File.ReadAllTextAsync(path);
            var cities = JsonConvert.DeserializeObject<List<LocationViewModel>>(jsonString);
            if (cities is null)
            {
                return BadRequest("error");
            }
            return Ok(cities);
        }
    }
}