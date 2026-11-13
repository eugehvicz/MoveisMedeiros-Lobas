using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class Documento
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Caminho { get; set; }

        public DateTime DataEnvio { get; set; }

        public int ObraId { get; set; }
        public Obra? Obra { get; set; }
    }
}
