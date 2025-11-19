using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Sede : EntidadModa
    {
        

        [Required]
        public string NombreRecinto { get; set; } = string.Empty; // Ej: Palacio de Congresos

        [Required]
        public string Ciudad { get; set; } = string.Empty; 

        [Range(10, 500)]
        public int AforoMaximo { get; set; } // Atributo INT

        public decimal CosteAlquilerPorHora { get; set; } // Atributo DECIMAL

        public bool TieneZonaVip { get; set; } // Atributo BOOLEAN

    

        // Relación: En una Sede se celebran muchos Eventos
        [JsonIgnore]
        public List<Evento>? Eventos { get; set; }
    }
}