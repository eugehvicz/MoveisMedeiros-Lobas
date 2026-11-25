namespace MoveisMedeiros.Api.Models
{
    public class Inspecao
    {
        public int Id { get; set; }

        public string InspecaoKey { get; set; }

        public string Status { get; set; }

        public string Observacoes { get; set; }

        public int ObraId { get; set; }
        public Obra? Obra { get; set; }

        public string FurnitureId { get; set; }

        public List<Checklist> Checklist { get; set; } = new List<Checklist>();

        
    }
}
