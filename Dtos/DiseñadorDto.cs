using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    // LO QUE RECIBES (POST/PUT)
    public class DiseñadorRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; } = string.Empty;

        public string Especialidad { get; set; } = "General";

        [Range(18, 100, ErrorMessage = "La edad debe estar entre 18 y 100")]
        public int Edad { get; set; }

        public decimal SalarioAnual { get; set; }

        public bool EstaActivo { get; set; }

        [Required(ErrorMessage = "Debes indicar a qué Marca pertenece")]
        public int MarcaId { get; set; }
    }

    // LO QUE DEVUELVES (GET)
    public class DiseñadorResponseDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal SalarioAnual { get; set; }
        public bool EstaActivo { get; set; }
        public int MarcaId { get; set; }
    }
}