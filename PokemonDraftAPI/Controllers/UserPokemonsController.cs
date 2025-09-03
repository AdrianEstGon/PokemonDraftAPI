using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PokemonDraftAPI.Database;
using PokemonDraftAPI.Database.Entities;
using PokemonDraftAPI.DTOs;

namespace PokemonDraftAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserPokemonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPokemonsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Obtener la mochila de un usuario
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserPokemons(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserPokemons)
                .ThenInclude(up => up.Pokemon)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found");

            var pokemons = user.UserPokemons.Select(up => new
            {
                up.PokemonId,
                up.Pokemon.Name,
                up.Pokemon.ImageUrl,
                up.Pokemon.Role,
            });

            return Ok(pokemons);
        }

        // 🔹 Agregar un pokemon a la mochila
        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddPokemonToUser(int userId, AddPokemonToUserDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var pokemon = await _context.Pokemons.FindAsync(dto.PokemonId);
            if (pokemon == null)
                return NotFound("Pokemon not found");

            var alreadyExists = await _context.UserPokemons.FindAsync(userId, dto.PokemonId);
            if (alreadyExists != null)
                return BadRequest("User already has this Pokemon");

            var userPokemon = new UserPokemon
            {
                UserId = userId,
                PokemonId = dto.PokemonId,
            };

            _context.UserPokemons.Add(userPokemon);
            await _context.SaveChangesAsync();

            return Ok(new { userPokemon.UserId, userPokemon.PokemonId, pokemon.Name });
        }

        // 🔹 Eliminar un pokemon de la mochila
        [HttpDelete("{userId}/remove/{pokemonId}")]
        public async Task<IActionResult> RemovePokemonFromUser(int userId, int pokemonId)
        {
            var userPokemon = await _context.UserPokemons.FindAsync(userId, pokemonId);
            if (userPokemon == null)
                return NotFound("Pokemon not found in user’s bag");

            _context.UserPokemons.Remove(userPokemon);
            await _context.SaveChangesAsync();

            return Ok("Pokemon removed from bag");
        }
    }
}
