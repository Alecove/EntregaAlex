using RESTAURANT_API_SERVICE.Repository;
using RESTAURANT_API_SERVICE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RESTAURANT_API_SERVICE.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosPendientesAsync()
        {
            // Lógica para obtener solo los pedidos en estado PENDIENTE o EN PREPARACIÓN
            return await _pedidoRepository.GetPedidosByEstadoAsync("PENDIENTE");
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido nuevoPedido, int usuarioId)
        {
            // Asignar el ID de usuario y la fecha
            nuevoPedido.UsuarioId = usuarioId;
            nuevoPedido.FechaPedido = DateTime.Now;
            nuevoPedido.Estado = "PENDIENTE";

            // Lógica de negocio para calcular el Total (debería ir aquí)
            nuevoPedido.Total = nuevoPedido.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

            await _pedidoRepository.AddAsync(nuevoPedido);
            await _pedidoRepository.SaveAsync();
            return nuevoPedido;
        }

        public async Task<bool> FinalizarPedidoAsync(int pedidoId)
        {
            // Lógica de negocio: actualizar inventario, generar factura, etc.
            var success = await _pedidoRepository.UpdateEstadoPedidoAsync(pedidoId, "ENTREGADO");
            return success;
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int pedidoId)
        {
            return await _pedidoRepository.GetByIdAsync(pedidoId);
        }
    }
}