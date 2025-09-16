using PokemonDraftAPI.Database.Entities;

namespace PokemonDraftAPI.Database
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? Tier { get; set; }

        public ICollection<Counter>? Counters { get; set; }

        public ICollection<UserPokemon> UserPokemons { get; set; } = new List<UserPokemon>();
    }
}
