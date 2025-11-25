using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IDiseñadorRepository
    {
        Task<List<Diseñador>> GetAllAsync();
        Task<Diseñador?> GetByIdAsync(int id);
        Task<Diseñador> CreateAsync(Diseñador diseñador);
        Task<Diseñador?> UpdateAsync(Diseñador diseñador);
        Task<bool> DeleteAsync(int id);
    }
}