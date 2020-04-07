using System;
using System.Threading;
using System.Threading.Tasks;
using estiaApi.Entities;
using estiaApi.Models;
using estiaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace estiaApi.Controllers
{
    [ApiController]
    [Authorize]
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
            return Ok(new { count, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var data = await _buildingService.Get(id, cancellationToken);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Get(string id, Building data, CancellationToken cancellationToken)
        {
            var result = await _buildingService.Update(id, data, cancellationToken);
            return Ok(result);
        }
    }
}
