using System.Collections.Generic;
using System.Threading.Tasks;
using EntregaAlex.Models; // Asegúrate de que este using apunta a donde tienes la clase Coleccion

namespace EntregaAlex.Repository
{
    // TIENE QUE SER 'public' PARA QUE PROGRAM.CS LA VEA
    public interface IColeccionRepository
    {
        Task<List<Coleccion>> GetAllColeccionesAsync();
        Task<Coleccion?> GetByIdAsync(int id);
        Task<Coleccion> CreateAsync(Coleccion coleccion);
        Task<Coleccion?> UpdateAsync(Coleccion coleccion);
        Task<bool> DeleteAsync(int id);
    }
}