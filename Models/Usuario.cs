using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        public string Perfil { get; set; }

        public bool Ativo { get; set; } = true;

        public List<Obra> Obras { get; set; } = new List<Obra>();

        public List<Historico> Historicos { get; set; } = new List<Historico>();
    }
}
