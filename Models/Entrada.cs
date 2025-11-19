using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Models
{
    public class Entrada : EntidadModa
    {
        

        [Required]
        public string CodigoQr { get; set; } = string.Empty; // Identificador único de entrada

        public string TipoAsiento { get; set; } = "General"; // "Pista", "Grada", "Front Row"

        public decimal PrecioFinalPagado { get; set; } // Puede variar por descuentos

        public int NumeroAsiento { get; set; }

        public bool IncluyeMeetAndGreet { get; set; } // Tu requisito de "ventaja exclusiva"

        public DateTime FechaCompra { get; set; }

        // CLAVES FORÁNEAS
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}