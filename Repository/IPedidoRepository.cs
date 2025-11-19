using RESTAURANT_API_SERVICE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTAURANT_API_SERVICE.Repository
{
    // Interfaz específica para pedidos, hereda las funciones básicas del Genérico.
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        // Obtener pedidos por estado
        Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado);
        
        // Obtener el historial de pedidos de un usuario
        Task<IEnumerable<Pedido>> GetHistorialPedidosByUsuarioIdAsync(int usuarioId);
        
        // Marcar un pedido como preparado, incluyendo la lógica de actualización
        Task<bool> UpdateEstadoPedidoAsync(int pedidoId, string nuevoEstado);
    }
}