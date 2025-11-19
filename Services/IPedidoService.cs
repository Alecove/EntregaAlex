using RESTAURANT_API_SERVICE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTAURANT_API_SERVICE.Services
{
    // Interfaz para la lógica de negocio de Pedidos.
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetPedidosPendientesAsync();
        Task<Pedido> CreatePedidoAsync(Pedido nuevoPedido, int usuarioId);
        Task<bool> FinalizarPedidoAsync(int pedidoId);
        Task<Pedido?> GetPedidoByIdAsync(int pedidoId);
    }
}