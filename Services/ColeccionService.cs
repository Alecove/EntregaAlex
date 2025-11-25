using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class ColeccionService : IColeccionService
    {
        private readonly IColeccionRepository _repository;

        public ColeccionService(IColeccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ColeccionResponseDto>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(c => new ColeccionResponseDto
            {
                Id = c.Id,
                NombreColeccion = c.NombreColeccion,
                Temporada = c.Temporada,
                NumeroPiezas = c.NumeroPiezas,
                PresupuestoInversion = c.PresupuestoInversion,
                EsLimitada = c.EsLimitada,
                FechaLanzamiento = c.FechaLanzamiento,
                DiseñadorId = c.DiseñadorId
            }).ToList();
        }

        public async Task<ColeccionResponseDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return null;

            return new ColeccionResponseDto
            {
                Id = c.Id,
                NombreColeccion = c.NombreColeccion,
                Temporada = c.Temporada,
                NumeroPiezas = c.NumeroPiezas,
                PresupuestoInversion = c.PresupuestoInversion,
                EsLimitada = c.EsLimitada,
                FechaLanzamiento = c.FechaLanzamiento,
                DiseñadorId = c.DiseñadorId
            };
        }

        public async Task<ColeccionResponseDto> CreateAsync(ColeccionRequestDto request)
        {
            var nueva = new Coleccion
            {
                NombreColeccion = request.NombreColeccion,
                Temporada = request.Temporada,
                NumeroPiezas = request.NumeroPiezas,
                PresupuestoInversion = request.PresupuestoInversion,
                EsLimitada = request.EsLimitada,
                DiseñadorId = request.DiseñadorId,
                FechaLanzamiento = DateTime.Now
            };

            var creada = await _repository.CreateAsync(nueva);

            return new ColeccionResponseDto
            {
                Id = creada.Id,
                NombreColeccion = creada.NombreColeccion,
                Temporada = creada.Temporada,
                NumeroPiezas = creada.NumeroPiezas,
                PresupuestoInversion = creada.PresupuestoInversion,
                EsLimitada = creada.EsLimitada,
                FechaLanzamiento = creada.FechaLanzamiento,
                DiseñadorId = creada.DiseñadorId
            };
        }

        public async Task<ColeccionResponseDto?> UpdateAsync(int id, ColeccionRequestDto request)
        {
            var coleccion = new Coleccion
            {
                Id = id,
                NombreColeccion = request.NombreColeccion,
                Temporada = request.Temporada,
                NumeroPiezas = request.NumeroPiezas,
                PresupuestoInversion = request.PresupuestoInversion,
                EsLimitada = request.EsLimitada,
                DiseñadorId = request.DiseñadorId
            };

            var actualizada = await _repository.UpdateAsync(coleccion);
            if (actualizada == null) return null;

            return new ColeccionResponseDto
            {
                Id = actualizada.Id,
                NombreColeccion = actualizada.NombreColeccion,
                Temporada = actualizada.Temporada,
                NumeroPiezas = actualizada.NumeroPiezas,
                PresupuestoInversion = actualizada.PresupuestoInversion,
                EsLimitada = actualizada.EsLimitada,
                FechaLanzamiento = actualizada.FechaLanzamiento,
                DiseñadorId = actualizada.DiseñadorId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}