using Microsoft.AspNetCore.Mvc;

namespace PokemonDraftAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        // Permite tanto GET como HEAD
        [HttpGet, HttpHead]
        public IActionResult Get()
        {
            return Ok(new { status = "ok", timestamp = DateTime.UtcNow });
        }
    }
}
