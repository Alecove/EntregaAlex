using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ciudad { get; set; } = string.Empty;

        public string UbicacionExacta { get; set; } = string.Empty;

        public int CapacidadAsistentes { get; set; }

        public decimal CosteEntrada { get; set; }

        public bool EsBenefico { get; set; }

        public DateTime FechaEvento { get; set; }

        // RELACIÓN CON COLECCIÓN (Presentan una colección en el evento)
        public int ColeccionId { get; set; }
        [ForeignKey("ColeccionId")]
        [JsonIgnore]
        public Coleccion? Coleccion { get; set; }
    }
}