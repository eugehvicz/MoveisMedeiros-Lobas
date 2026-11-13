namespace MoveisMedeiros.Api.DTOs
{
    public class CriarDocumentoDto
    {
        public string Tipo { get; set; }
        public string Arquivo { get; set; }
        public DateTime Data { get; set; }
        public int ObraId { get; set; }
    }
}
