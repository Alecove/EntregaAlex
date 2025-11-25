using System.ComponentModel.DataAnnotations;

namespace EntregaAlex.Dtos
{
    // 1. LO QUE PIDES (Para Crear/Editar)
    // Aquí definimos qué datos tiene que enviarte el usuario desde Swagger
    public class MarcaRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país de origen es obligatorio")]
        public string PaisOrigen { get; set; } = string.Empty;

        [Range(1800, 2025, ErrorMessage = "El año debe estar entre 1800 y 2025")]
        public int AnioFundacion { get; set; }

        

        public bool EsAltaCostura { get; set; }
    }

    // 2. LO QUE DEVUELVES (Para Leer)
    // Aquí definimos qué datos ve el usuario (incluyendo el ID generado)
    public class MarcaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public int AnioFundacion { get; set; }
        public decimal ValorMercadoMillones { get; set; }
        public bool EsAltaCostura { get; set; }
        // Nota: Omitimos 'FechaAlianza' o 'Eventos' si no queremos mostrarlos aquí
    }
}