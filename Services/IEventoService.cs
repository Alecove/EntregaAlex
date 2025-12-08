using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IEventoService
    {
        Task<List<EventoResponseDto>> GetAllAsync();
        Task<EventoResponseDto?> GetByIdAsync(int id);
        Task<EventoResponseDto> CreateAsync(EventoRequestDto request);
        Task<EventoResponseDto?> UpdateAsync(int id, EventoRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}