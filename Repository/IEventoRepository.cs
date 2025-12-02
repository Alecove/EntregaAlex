using EntregaAlex.Models;

namespace EntregaAlex.Repository
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetAllAsync();
        Task<Evento?> GetByIdAsync(int id);
        Task<Evento> CreateAsync(Evento evento);
        Task<Evento?> UpdateAsync(Evento evento);
        Task<bool> DeleteAsync(int id);
    }
}