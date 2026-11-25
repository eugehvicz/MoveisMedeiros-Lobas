using MoveisMedeiros.Api.Models;

namespace MoveisMedeiros.Api.DTOs
{
    public class CriarChecklistDto
    {
        public string Chave { get; set; }
        public string Descricao { get; set; }
        public Checklist.ResultadoChecklist Resultado { get; set; }
        public string DescricaoProblema { get; set; }
        public string UrlImagem { get; set; }
        public int InspecaoId { get; set; }
    }
}