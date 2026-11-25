namespace MoveisMedeiros.Api.DTOs
{
    public class CriarObraDto
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataPrevisao { get; set; }
        public string Status { get; set; }
        public int ClienteId { get; set; }
    }
}
