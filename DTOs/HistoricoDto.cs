namespace MoveisMedeiros.Api.DTOs
{
    public class HistoricoDto
    {
        public int Id { get; set; }
        public string NomeObra { get; set; }
        public string NomeUsuario { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
    }
}
