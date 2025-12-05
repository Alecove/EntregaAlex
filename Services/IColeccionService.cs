using System.Collections.Generic;
using System.Threading.Tasks;
using EntregaAlex.Models;

namespace EntregaAlex.Services
{
    // IMPORTANTE: 'public' para que Program.cs no de error
    public interface IColeccionService
    {
        Task<List<Coleccion>> GetAllColeccionesAsync();
        Task<Coleccion?> GetByIdAsync(int id);
        Task<Coleccion> CreateAsync(Coleccion coleccion);
        Task<Coleccion?> UpdateAsync(int id, Coleccion coleccion);
        Task<bool> DeleteAsync(int id);
    }
}