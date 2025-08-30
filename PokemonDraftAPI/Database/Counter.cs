namespace PokemonDraftAPI.Database
{
    public class Counter
    {
        public int Id { get; set; }

        public int PokemonId { get; set; }
        public virtual Pokemon? Pokemon { get; set; }  // Ahora opcional

        public int CounterPokemonId { get; set; }
        public virtual Pokemon? CounterPokemon { get; set; } // Ahora opcional

        public int Value { get; set; }
    }

}
