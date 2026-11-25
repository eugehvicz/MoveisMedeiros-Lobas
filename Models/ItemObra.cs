using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class ItemObra
    {
        public int Id { get; set; }
        public string NomeMovel { get; set; }
        public string Tipo { get; set; }
        public string Ambiente { get; set; }
        public string Material { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
