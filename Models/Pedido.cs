using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RESTAURANT_API_SERVICE.Models
{
    // Entidad Pedido, representa la orden de un cliente.
    public class Pedido : EntidadModa
    {
        
        
        // Clave foránea al usuario que realiza el pedido (si aplica)
        public int? UsuarioId { get; set; } 
        
        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        
        [Required]
        public string Estado { get; set; } = "PENDIENTE"; // PENDIENTE, EN PREPARACIÓN, ENTREGADO, CANCELADO
        
        // Total del pedido
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        
        // Relación uno a muchos: un Pedido tiene muchos DetallesPedido
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }

    // Entidad para manejar los detalles del pedido (productos, cantidad)
    public class DetallePedido
    {
        [Key]
        public int Id { get; set; }
        
        // Clave foránea al Pedido
        public int PedidoId { get; set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; } 

        // Clave foránea al Producto
        public int ProductoId { get; set; }
        
        public string NombreProducto { get; set; } = string.Empty;
        
        public int Cantidad { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
    }
}