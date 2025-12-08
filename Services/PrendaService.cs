using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class PrendaService : IPrendaService
    {
        private readonly IPrendaRepository _repository;

        public PrendaService(IPrendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PrendaResponseDto>> GetAllPrendasAsync()
        {
            var prendas = await _repository.GetAllAsync();
            return prendas.Select(p => MapToDto(p)).ToList();
        }

        public async Task<PrendaResponseDto?> GetPrendaByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor que cero.");

            var p = await _repository.GetByIdAsync(id);
            return p == null ? null : MapToDto(p);
        }

        public async Task<PrendaResponseDto> CreatePrendaAsync(PrendaRequestDto request)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(request.Tipo))
                throw new ArgumentException("El tipo de prenda no puede estar vacío.");
            
            if (request.ColeccionId <= 0)
                throw new ArgumentException("Se requiere una Colección válida.");

            var nuevaPrenda = new Prenda
            {
                Tipo = request.Tipo,
                MaterialPrincipal = request.MaterialPrincipal,
                TallaNumerica = request.TallaNumerica,
                PrecioVenta = request.PrecioVenta,
                EnStock = request.EnStock,
                FechaFabricacion = request.FechaFabricacion,
                ColeccionId = request.ColeccionId
            };

            var creada = await _repository.CreateAsync(nuevaPrenda);
            return MapToDto(creada);
        }

        public async Task<PrendaResponseDto?> UpdatePrendaAsync(int id, PrendaRequestDto request)
        {
            if (id <= 0) throw new ArgumentException("El ID no es válido.");
            
            if (string.IsNullOrWhiteSpace(request.Tipo))
                throw new ArgumentException("El tipo de prenda no puede estar vacío.");

            var prenda = new Prenda
            {
                Id = id,
                Tipo = request.Tipo,
                MaterialPrincipal = request.MaterialPrincipal,
                TallaNumerica = request.TallaNumerica,
                PrecioVenta = request.PrecioVenta,
                EnStock = request.EnStock,
                FechaFabricacion = request.FechaFabricacion,
                ColeccionId = request.ColeccionId
            };

            var actualizada = await _repository.UpdateAsync(prenda);
            return actualizada == null ? null : MapToDto(actualizada);
        }

        public async Task<bool> DeletePrendaAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID no es válido.");
            return await _repository.DeleteAsync(id);
        }

        private PrendaResponseDto MapToDto(Prenda p)
        {
            return new PrendaResponseDto
            {
                Id = p.Id,
                Tipo = p.Tipo,
                MaterialPrincipal = p.MaterialPrincipal,
                TallaNumerica = p.TallaNumerica,
                PrecioVenta = p.PrecioVenta,
                EnStock = p.EnStock,
                FechaFabricacion = p.FechaFabricacion,
                ColeccionId = p.ColeccionId
            };
        }
    }
}