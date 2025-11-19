using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAURANT_API_SERVICE.Models
{
    // Entidad Producto, de la cual podrían heredar otras (Bebida, Postre, etc.)
    public class Producto : EntidadModa
    {
        
        
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        public string Descripcion { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        
        public string Categoria { get; set; } = string.Empty; // Ej: Bebida, Entrante, Principal
        
        public bool EstaDisponible { get; set; } = true;
    }
}