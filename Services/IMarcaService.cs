using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public interface IMarcaService
    {
        Task<List<MarcaResponseDto>> GetAllMarcasAsync();
        Task<MarcaResponseDto?> GetMarcaByIdAsync(int id);
        Task<MarcaResponseDto> CreateMarcaAsync(MarcaRequestDto request);
        Task<MarcaResponseDto?> UpdateMarcaAsync(int id, MarcaRequestDto request);
        Task<bool> DeleteMarcaAsync(int id);
    }
}