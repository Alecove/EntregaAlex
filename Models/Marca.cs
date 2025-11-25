using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string PaisOrigen { get; set; } = string.Empty;

        [Range(1800, 2025)]
        public int AnioFundacion { get; set; }

        public bool EsAltaCostura { get; set; }

    

        // RELACIONES
        [JsonIgnore]
        public List<Diseñador>? Diseñadores { get; set; }
    }
}