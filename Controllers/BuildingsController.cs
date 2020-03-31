using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public async Task<IActionResult> Get([FromQuery] ListRequest param, CancellationToken cancellationToken)
        {
            var (count, data) = await _buildingService.Get(param, cancellationToken);
            // Response.Headers.Add("Content-Range", $"{count}");
            return Ok(new { count, data });
        }
    }
}
