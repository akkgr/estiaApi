using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estiaApi.Entities;
using estiaApi.Models;
using estiaApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace estiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingsController : ControllerBase
    {
        private readonly ILogger<BuildingsController> _logger;
        private readonly BuildingService _buildingService;

        public BuildingsController(ILogger<BuildingsController> logger, BuildingService buildingService)
        {
            _logger = logger;
            _buildingService = buildingService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ListRequest param)
        {
            var (count, data) = _buildingService.Get(param);
            Response.Headers.Add("Content-Range", $"{count}");
            return Ok(data);
        }
    }
}
