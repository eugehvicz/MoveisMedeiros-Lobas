using System.ComponentModel.DataAnnotations;

namespace MoveisMedeiros.Api.Models
{
    public class Checklist
    {
        public enum ResultadoChecklist
        {
            OK,
            NOK,
            NOA
        }

        public int Id { get; set; }

        public string Chave { get; set; }

        [Required]
        public string Descricao { get; set; }

        public ResultadoChecklist Resultado { get; set; }

        public string DescricaoProblema { get; set; }

        public string UrlImagem { get; set; }

        public int InspecaoId { get; set; }
        public Inspecao? Inspecao { get; set; }
    }
}
