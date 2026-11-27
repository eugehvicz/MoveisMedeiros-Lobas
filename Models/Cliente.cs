using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoveisMedeiros.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public List<Obra> Obras { get; set; } = new List<Obra>();
    }
}
