using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Marca : EntidadModa;
    {
        

        [Required(ErrorMessage = "El nombre de la marca es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty; // Ej: Gucci

        [Required]
        public string PaisOrigen { get; set; } = string.Empty; // Ej: Italia

        [Range(1800, 2025)]
        public int AnioFundacion { get; set; } // Atributo INT

        public decimal ValorMercadoMillones { get; set; } // Atributo DECIMAL

        public bool EsAltaCostura { get; set; } // Atributo BOOLEAN (Haute Couture?)

        public DateTime FechaAlianza { get; set; } // Atributo DATETIME (Cuándo firmaron con tu app)

        // Relación: Una Marca tiene una lista de Eventos
        [JsonIgnore] // Evita bucles infinitos al convertir a JSON
        public List<Evento>? Eventos { get; set; }
    }
}