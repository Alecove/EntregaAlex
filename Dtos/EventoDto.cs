using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    // LO QUE ENVÍAS (POST)
    public class EventoRequestDto
    {
        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string Ciudad { get; set; } = string.Empty;

        public string UbicacionExacta { get; set; } = string.Empty;

        [Range(10, 100000, ErrorMessage = "La capacidad debe ser positiva")]
        public int CapacidadAsistentes { get; set; }

        public decimal CosteEntrada { get; set; }

        public bool EsBenefico { get; set; }

        // OBLIGATORIO: Qué colección se presenta en este evento
        [Required]
        public int ColeccionId { get; set; }
    }

    // LO QUE RECIBES (GET)
    public class EventoResponseDto
    {
        public int Id { get; set; }
        public string Ciudad { get; set; } = string.Empty;
        public string UbicacionExacta { get; set; } = string.Empty;
        public int CapacidadAsistentes { get; set; }
        public decimal CosteEntrada { get; set; }
        public bool EsBenefico { get; set; }
        public DateTime FechaEvento { get; set; }
        public int ColeccionId { get; set; }
    }
}