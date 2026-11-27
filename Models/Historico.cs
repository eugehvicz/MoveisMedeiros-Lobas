using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoveisMedeiros.Api.Models
{
    public class Historico
    {
        public int Id { get; set; }

        [Required]
        public string Acao { get; set; }

        public string Descricao { get; set; }

        public DateTime DataHora { get; set; }

        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        public int? ObraId { get; set; }

        [JsonIgnore]
        public Obra? Obra { get; set; }
    }
}
