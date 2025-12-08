using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IPrendaRepository
    {
        Task<List<Prenda>> GetAllAsync();
        Task<Prenda?> GetByIdAsync(int id);
        Task<Prenda> CreateAsync(Prenda prenda);
        Task<Prenda?> UpdateAsync(Prenda prenda);
        Task<bool> DeleteAsync(int id);
    }
}