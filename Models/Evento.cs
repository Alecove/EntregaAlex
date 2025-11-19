using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Evento : EntidadModa
    {
        

        [Required]
        public string Titulo { get; set; } = string.Empty; // Ej: Presentación Otoño/Invierno

        public string? DescripcionActividades { get; set; } // "Meet & greet, pasarela..."

        public int EntradasDisponibles { get; set; }

        [Range(0, 10000)]
        public decimal PrecioEntradaBase { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; } // Para cumplir el requisito de rango de fechas


        [JsonIgnore]
        public List<Entrada>? EntradasVendidas { get; set; }
    }
}