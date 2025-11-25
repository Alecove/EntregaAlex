using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IColeccionRepository
    {
        Task<List<Coleccion>> GetAllAsync();
        Task<Coleccion?> GetByIdAsync(int id);
        Task<Coleccion> CreateAsync(Coleccion coleccion);
        Task<Coleccion?> UpdateAsync(Coleccion coleccion);
        Task<bool> DeleteAsync(int id);
    }
}