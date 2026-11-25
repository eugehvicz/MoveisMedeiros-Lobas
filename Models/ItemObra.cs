using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public List<Inspecao> Inspecoes { get; set; } = new List<Inspecao>();

        public int ObraId { get; set; }

        [JsonIgnore]
        public Obra? Obra { get; set; }
    }
}
