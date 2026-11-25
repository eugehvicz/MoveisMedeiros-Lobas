using System.ComponentModel.DataAnnotations;

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
        public Usuario? Usuario { get; set; }

        public int? ObraId { get; set; }
        public Obra? Obra { get; set; }
    }
}
