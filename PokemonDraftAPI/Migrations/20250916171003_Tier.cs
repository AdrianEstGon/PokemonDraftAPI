using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonDraftAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tier",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tier",
                table: "Pokemons");
        }
    }
}
