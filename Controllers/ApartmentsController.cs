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

namespace estiaApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly ILogger<BuildingsController> _logger;
        private readonly ApartmentService _apartmentService;

        public ApartmentsController(ILogger<BuildingsController> logger, ApartmentService apartmentService)
        {
            _logger = logger;
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ListRequest param, CancellationToken cancellationToken)
        {
            var (count, data) = await _apartmentService.Get(param, cancellationToken);
            return Ok(new { count, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var data = await _apartmentService.Get(id, cancellationToken);
            return Ok(data);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(Apartment data, CancellationToken cancellationToken)
        {
            var result = await _apartmentService.Create(data, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Apartment data, CancellationToken cancellationToken)
        {
            var result = await _apartmentService.Update(id, data, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _apartmentService.Remove(id, cancellationToken);
            return Ok();
        }
    }
}
