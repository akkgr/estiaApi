using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using estiaApi.Entities;
using estiaApi.Models;
using estiaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace estiaApi.Controllers {
    [ApiController]
    [Authorize]
    [Route ("api/[controller]")]
    public class BuildingsController : ControllerBase {
        private readonly ILogger<BuildingsController> _logger;
        private readonly BuildingService _buildingService;
        private readonly ApartmentService _apartmentService;

        public BuildingsController (ILogger<BuildingsController> logger, BuildingService buildingService, ApartmentService apartmentService) {
            _logger = logger;
            _buildingService = buildingService;
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] ListRequest param, CancellationToken cancellationToken) {
            var (count, data) = await _buildingService.Get (param, cancellationToken);
            return Ok (new { count, data });
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (string id, CancellationToken cancellationToken) {
            var data = await _buildingService.Get (id, cancellationToken);
            return Ok (data);
        }

        [HttpPost ("")]
        public async Task<IActionResult> Post (Building data, CancellationToken cancellationToken) {
            var result = await _buildingService.Create (data, cancellationToken);
            return Ok (result);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (string id, Building data, CancellationToken cancellationToken) {
            var result = await _buildingService.Update (id, data, cancellationToken);
            return Ok (result);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (string id, CancellationToken cancellationToken) {
            await _buildingService.Remove (id, cancellationToken);
            return Ok ();
        }

        [HttpGet ("{id}/apartments")]
        public async Task<IActionResult> GetApartments (string id, [FromQuery] ListRequest param, CancellationToken cancellationToken) {
            var (count, data) = await _apartmentService.GetByBuilding (id, param, cancellationToken);
            return Ok (new { count, data });
        }
    }
}