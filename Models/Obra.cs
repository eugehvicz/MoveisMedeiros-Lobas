using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class Obra
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Status { get; set; }

        public string NomeCliente { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public List<Usuario> Equipe { get; set; } = new List<Usuario>();

        public List<ItemObra> Itens { get; set; } = new List<ItemObra>();

        public List<Inspecao> Inspecoes { get; set; } = new List<Inspecao>();

        public List<Documento> Documentos { get; set; } = new List<Documento>();

        public List<Historico> Historicos { get; set; } = new List<Historico>();
    }
}
