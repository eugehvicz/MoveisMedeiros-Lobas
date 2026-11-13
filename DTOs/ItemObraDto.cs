namespace MoveisMedeiros.Api.DTOs
{
    public class ItemObraDto
    {
        public int Id { get; set; }
        public string NomeMovel { get; set; }
        public string Tipo { get; set; }
        public string Ambiente { get; set; }
        public string Material { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public int ObraId { get; set; }
    }
}
