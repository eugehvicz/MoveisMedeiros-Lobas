namespace MoveisMedeiros.Api.DTOs
{
    public class InspecaoDto
    {
        public int Id { get; set; }
        public DateTime DataInspecao { get; set; }
        public string Descricao { get; set; }
        public int ObraId { get; set; }
        public string NomeObra { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
    }
}
