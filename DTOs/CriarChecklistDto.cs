namespace MoveisMedeiros.Api.DTOs
{
    public class CriarChecklistDto
    {
        public string Descricao { get; set; }
        public string Resultado { get; set; }
        public string Observacao { get; set; }
        public string UrlImagem { get; set; }
        public int InspecaoId { get; set; }
    }
}
