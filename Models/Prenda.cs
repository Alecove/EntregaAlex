using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Prenda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; } = string.Empty; // Ej: Camisa, Falda

        public string MaterialPrincipal { get; set; } = string.Empty;

        public int TallaNumerica { get; set; }

        public decimal PrecioVenta { get; set; }

        public bool EnStock { get; set; }

        public DateTime FechaFabricacion { get; set; }

        // RELACIÓN CON COLECCIÓN
        public int ColeccionId { get; set; }
        [ForeignKey("ColeccionId")]
        [JsonIgnore]
        public Coleccion? Coleccion { get; set; }
    }
}