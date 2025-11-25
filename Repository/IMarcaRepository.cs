using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAllAsync();
        Task<Marca?> GetByIdAsync(int id);
        Task<Marca> CreateAsync(Marca marca);
        Task<Marca?> UpdateAsync(Marca marca);
        Task<bool> DeleteAsync(int id);
    }
}