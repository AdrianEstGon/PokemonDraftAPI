using System.ComponentModel.DataAnnotations;

namespace PokemonDraftAPI.Database.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // 🔹 se guarda el hash, no la contraseña en texto plano

        [Required]
        public string Role { get; set; } = "user"; // valores: "admin", "user"
    }
}
