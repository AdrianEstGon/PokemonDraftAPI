namespace PokemonDraftAPI.DTOs
{
    public class CounterDto
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public string PokemonName { get; set; } = string.Empty;
        public int CounterPokemonId { get; set; }
        public string CounterPokemonName { get; set; } = string.Empty;
        public int Value { get; set; }
    }

}
