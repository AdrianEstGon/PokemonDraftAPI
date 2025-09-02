using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PokemonDraftAPI.Database;
using PokemonDraftAPI.DTOs;

namespace PokemonDraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/counters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterDto>>> GetCounters()
        {
            var counters = await _context.Counters
                .Include(c => c.Pokemon)
                .Include(c => c.CounterPokemon)
                .Select(c => new CounterDto
                {
                    Id = c.Id,
                    PokemonId = c.PokemonId,
                    PokemonName = c.Pokemon!.Name,
                    CounterPokemonId = c.CounterPokemonId,
                    CounterPokemonName = c.CounterPokemon!.Name,
                    Value = c.Value
                })
                .ToListAsync();

            return Ok(counters);
        }



        [Authorize(Roles = "admin")]
        // POST: api/counters
        [HttpPost]
        public async Task<ActionResult<Counter>> PostCounter(Counter counter)
        {
            var pokemonExists = await _context.Pokemons.AnyAsync(p => p.Id == counter.PokemonId);
            var counterExists = await _context.Pokemons.AnyAsync(p => p.Id == counter.CounterPokemonId);

            if (!pokemonExists || !counterExists)
                return BadRequest("Pokémon or Counter Pokémon does not exist.");

            _context.Counters.Add(counter);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCounters), new { id = counter.Id }, counter);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/counters/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounter(int id, Counter counterDto)
        {
            var counter = await _context.Counters.FindAsync(id);
            if (counter == null) return NotFound();

            // Actualiza solo los campos necesarios
            counter.PokemonId = counterDto.PokemonId;
            counter.CounterPokemonId = counterDto.CounterPokemonId;
            counter.Value = counterDto.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/counters/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounter(int id)
        {
            var counter = await _context.Counters.FindAsync(id);
            if (counter == null) return NotFound();
            _context.Counters.Remove(counter);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
