namespace MoveisMedeiros.Api.DTOs
{
    public class InspecaoDto
    {
        public int Id { get; set; }
        public string InspecaoKey { get; set; }
        public string Status { get; set; }
        public string Observacoes { get; set; }
        public int ObraId { get; set; }
        public string NomeObra { get; set; }
        public string FurnitureId { get; set; }
    }
}