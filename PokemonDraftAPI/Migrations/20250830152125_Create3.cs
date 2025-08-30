using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonDraftAPI.Migrations
{
    /// <inheritdoc />
    public partial class Create3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Pokemons");
        }
    }
}
