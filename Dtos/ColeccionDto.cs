using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    public class ColeccionRequestDto
    {
        [Required]
        public string NombreColeccion { get; set; } = string.Empty;

        public string Temporada { get; set; } = "Otoño-Invierno";

        [Range(0, 1000)]
        public int NumeroPiezas { get; set; }

        public decimal PresupuestoInversion { get; set; }

        public bool EsLimitada { get; set; }

        public DateTime FechaLanzamiento { get; set; }

        [Required]
        public int DiseñadorId { get; set; }
    }

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