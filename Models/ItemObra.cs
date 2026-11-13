using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class ItemObra
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public double LarguraCm { get; set; }

        public double AlturaCm { get; set; }

        public string Tipo { get; set; }

        public string Ambiente { get; set; }

        public string Material { get; set; }

        public int ObraId { get; set; }
        public Obra? Obra { get; set; }
    }
}
