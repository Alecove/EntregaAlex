using Microsoft.EntityFrameworkCore;
using RESTAURANT_API_SERVICE.Models;
using RESTAURANT_API_SERVICE.Data; // Asumo que tienes un Contexto llamado RestaurantContext
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAURANT_API_SERVICE.Repository
{
    // Implementación del repositorio de Pedidos
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(RestaurantContext context) : base(context) { }

        // Sobrescribe GetByIdAsync para incluir los detalles del pedido
        public override async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado)
        {
            return await _dbSet
                .Include(p => p.Detalles)
                .Where(p => p.Estado == estado)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Pedido>> GetHistorialPedidosByUsuarioIdAsync(int usuarioId)
        {
            return await _dbSet
                .Include(p => p.Detalles)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();
        }

        public async Task<bool> UpdateEstadoPedidoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await GetByIdAsync(pedidoId);
            if (pedido == null) return false;

            // Lógica de validación de estados aquí si fuera necesario

            pedido.Estado = nuevoEstado;
            _dbSet.Update(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}