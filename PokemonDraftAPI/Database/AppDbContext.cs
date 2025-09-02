using Microsoft.EntityFrameworkCore;

using PokemonDraftAPI.Database.Entities;

namespace PokemonDraftAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Counter>()
                .HasOne(c => c.Pokemon)
                .WithMany(p => p.Counters)
                .HasForeignKey(c => c.PokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Counter>()
                .HasOne(c => c.CounterPokemon)
                .WithMany()
                .HasForeignKey(c => c.CounterPokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Opcional: índice único para no repetir el mismo counter
            modelBuilder.Entity<Counter>()
                .HasIndex(c => new { c.PokemonId, c.CounterPokemonId })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }

}
