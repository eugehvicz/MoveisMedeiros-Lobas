namespace MoveisMedeiros.Api.DTOs
{
    public class CriarInspecaoDto
    {
        public DateTime DataInspecao { get; set; }
        public string Descricao { get; set; }
        public int ObraId { get; set; }
        public int UsuarioId { get; set; }
    }
}
