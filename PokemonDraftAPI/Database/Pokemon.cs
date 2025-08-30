namespace PokemonDraftAPI.Database
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Role { get; set; } = null!; 

        public ICollection<Counter>? Counters { get; set; }
    }
}
