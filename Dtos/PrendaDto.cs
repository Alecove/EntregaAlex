using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    public class PrendaRequestDto
    {
        [Required(ErrorMessage = "El tipo de prenda es obligatorio")]
        public string Tipo { get; set; } = string.Empty;

        public string MaterialPrincipal { get; set; } = string.Empty;

        [Range(0, 100)] 
        public int TallaNumerica { get; set; }

        public decimal PrecioVenta { get; set; }

        public bool EnStock { get; set; }

        public DateTime FechaFabricacion { get; set; }

        [Required]
        public int ColeccionId { get; set; }
    }

    public class PrendaResponseDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string MaterialPrincipal { get; set; } = string.Empty;
        public int TallaNumerica { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool EnStock { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public int ColeccionId { get; set; }
    }
}