namespace MoveisMedeiros.Api.DTOs
{
    public class ChecklistDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Resultado { get; set; }
        public string Observacao { get; set; }
        public string UrlImagem { get; set; }
        public int InspecaoId { get; set; }
    }
}
