namespace MoveisMedeiros.Api.DTOs
{
    public class DocumentoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Arquivo { get; set; }
        public DateTime Data { get; set; }
        public int ObraId { get; set; }
        public string NomeObra { get; set; }
    }
}
