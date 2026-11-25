using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class Documento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Arquivo { get; set; }
        public DateTime Data { get; set; }
        public int ObraId { get; set; }
        public Obra Obra { get; set; }
    }
}
