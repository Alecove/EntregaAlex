using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IColeccionService
    {
        Task<List<ColeccionResponseDto>> GetAllAsync();
        Task<ColeccionResponseDto?> GetByIdAsync(int id);
        Task<ColeccionResponseDto> CreateAsync(ColeccionRequestDto request);
        Task<ColeccionResponseDto?> UpdateAsync(int id, ColeccionRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}