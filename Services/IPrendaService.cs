using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IPrendaService
    {
        Task<List<PrendaResponseDto>> GetAllPrendasAsync();
        Task<PrendaResponseDto?> GetPrendaByIdAsync(int id);
        Task<PrendaResponseDto> CreatePrendaAsync(PrendaRequestDto request);
        Task<PrendaResponseDto?> UpdatePrendaAsync(int id, PrendaRequestDto request);
        Task<bool> DeletePrendaAsync(int id);
    }
}