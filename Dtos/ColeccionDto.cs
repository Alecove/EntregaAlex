using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    // LO QUE PIDES (POST/PUT)
    public class ColeccionRequestDto
    {
        [Required(ErrorMessage = "El nombre de la colección es obligatorio")]
        public string NombreColeccion { get; set; } = string.Empty;

        public string Temporada { get; set; } = "Otoño-Invierno";

        [Range(1, 1000, ErrorMessage = "Debe haber al menos 1 pieza")]
        public int NumeroPiezas { get; set; }

        public decimal PresupuestoInversion { get; set; }

        public bool EsLimitada { get; set; }

        // El ID del Diseñador que creó la colección es OBLIGATORIO
        [Required]
        public int DiseñadorId { get; set; }
    }

    // LO QUE DEVUELVES (GET)
    public class ColeccionResponseDto
    {
        public int Id { get; set; }
        public string NombreColeccion { get; set; } = string.Empty;
        public string Temporada { get; set; } = string.Empty;
        public int NumeroPiezas { get; set; }
        public decimal PresupuestoInversion { get; set; }
        public bool EsLimitada { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int DiseñadorId { get; set; }
    }
}