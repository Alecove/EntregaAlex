using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Usuario : EntidadModa
    {
       

        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public int PuntosFidelidad { get; set; } = 0; // Gamificación (puntos acumulados)

        public decimal SaldoEnCartera { get; set; } // Para comprar entradas


        public DateTime FechaRegistro { get; set; }

        // Relación: Un Usuario compra muchas Entradas
        [JsonIgnore]
        public List<Entrada>? Entradas { get; set; }
    }
}