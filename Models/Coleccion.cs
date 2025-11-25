using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntregaAlex.Models
{
    public class Coleccion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreColeccion { get; set; } = string.Empty;

        public string Temporada { get; set; } = "Otoño-Invierno";

        public int NumeroPiezas { get; set; }

        public decimal PresupuestoInversion { get; set; }

        public bool EsLimitada { get; set; }

        public DateTime FechaLanzamiento { get; set; }

        // RELACIÓN CON DISEÑADOR
        public int DiseñadorId { get; set; }
        [ForeignKey("DiseñadorId")]
        [JsonIgnore]
        public Diseñador? Diseñador { get; set; }

        // RELACIONES HACIA ABAJO
        [JsonIgnore]
        public List<Prenda>? Prendas { get; set; }
        
        [JsonIgnore]
        public List<Evento>? Eventos { get; set; }
    }
}