using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IDiseñadorService
    {
        Task<List<DiseñadorResponseDto>> GetAllAsync();
        Task<DiseñadorResponseDto?> GetByIdAsync(int id);
        Task<DiseñadorResponseDto> CreateAsync(DiseñadorRequestDto request);
        Task<DiseñadorResponseDto?> UpdateAsync(int id, DiseñadorRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}