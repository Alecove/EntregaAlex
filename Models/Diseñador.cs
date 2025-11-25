using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Diseñador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        public string Especialidad { get; set; } = "General";

        public int Edad { get; set; }

        public decimal SalarioAnual { get; set; }

        public bool EstaActivo { get; set; }

        public DateTime FechaContratacion { get; set; }

        // RELACIÓN CON MARCA
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        [JsonIgnore]
        public Marca? Marca { get; set; }

        // RELACIÓN HACIA ABAJO
        [JsonIgnore]
        public List<Coleccion>? Colecciones { get; set; }
    }
}