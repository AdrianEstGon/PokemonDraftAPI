using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PokemonDraftAPI.Database;

namespace PokemonDraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PokemonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pokemons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }

        // GET: api/pokemons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return pokemon;
        }

        [Authorize(Roles = "admin")]
        // POST: api/pokemons
        [HttpPost]
        public async Task<ActionResult<Pokemon>> PostPokemon(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon(int id, Pokemon pokemon)
        {
            var existingPokemon = await _context.Pokemons.FindAsync(id);

            if (existingPokemon == null)
            {
                return NotFound();
            }

            // ✅ Copiar propiedades (puedes usar AutoMapper si quieres algo más automático)
            existingPokemon.Name = pokemon.Name;
            existingPokemon.ImageUrl = pokemon.ImageUrl;
            existingPokemon.Role = pokemon.Role;
            existingPokemon.Tier = pokemon.Tier;
            // agrega aquí el resto de propiedades que quieras actualizar

            await _context.SaveChangesAsync();

            return Ok(existingPokemon); // 👈 ahora devuelve el objeto actualizado y trackeado
        }


        [Authorize(Roles = "admin")]
        // DELETE: api/pokemons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
