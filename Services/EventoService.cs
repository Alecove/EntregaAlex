using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _repository;

        public EventoService(IEventoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EventoResponseDto>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(e => new EventoResponseDto
            {
                Id = e.Id,
                Ciudad = e.Ciudad,
                UbicacionExacta = e.UbicacionExacta,
                CapacidadAsistentes = e.CapacidadAsistentes,
                CosteEntrada = e.CosteEntrada,
                EsBenefico = e.EsBenefico,
                FechaEvento = e.FechaEvento,
                ColeccionId = e.ColeccionId
            }).ToList();
        }

        public async Task<EventoResponseDto?> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;

            return new EventoResponseDto
            {
                Id = e.Id,
                Ciudad = e.Ciudad,
                UbicacionExacta = e.UbicacionExacta,
                CapacidadAsistentes = e.CapacidadAsistentes,
                CosteEntrada = e.CosteEntrada,
                EsBenefico = e.EsBenefico,
                FechaEvento = e.FechaEvento,
                ColeccionId = e.ColeccionId
            };
        }

        public async Task<EventoResponseDto> CreateAsync(EventoRequestDto request)
        {
            var nuevo = new Evento
            {
                Ciudad = request.Ciudad,
                UbicacionExacta = request.UbicacionExacta,
                CapacidadAsistentes = request.CapacidadAsistentes,
                CosteEntrada = request.CosteEntrada,
                EsBenefico = request.EsBenefico,
                ColeccionId = request.ColeccionId,
                FechaEvento = DateTime.Now // O lógica de fecha futura
            };

            var creado = await _repository.CreateAsync(nuevo);

            return new EventoResponseDto
            {
                Id = creado.Id,
                Ciudad = creado.Ciudad,
                UbicacionExacta = creado.UbicacionExacta,
                CapacidadAsistentes = creado.CapacidadAsistentes,
                CosteEntrada = creado.CosteEntrada,
                EsBenefico = creado.EsBenefico,
                FechaEvento = creado.FechaEvento,
                ColeccionId = creado.ColeccionId
            };
        }

        public async Task<EventoResponseDto?> UpdateAsync(int id, EventoRequestDto request)
        {
            var evento = new Evento
            {
                Id = id,
                Ciudad = request.Ciudad,
                UbicacionExacta = request.UbicacionExacta,
                CapacidadAsistentes = request.CapacidadAsistentes,
                CosteEntrada = request.CosteEntrada,
                EsBenefico = request.EsBenefico,
                ColeccionId = request.ColeccionId
            };

            var actualizado = await _repository.UpdateAsync(evento);
            if (actualizado == null) return null;

            return new EventoResponseDto
            {
                Id = actualizado.Id,
                Ciudad = actualizado.Ciudad,
                UbicacionExacta = actualizado.UbicacionExacta,
                CapacidadAsistentes = actualizado.CapacidadAsistentes,
                CosteEntrada = actualizado.CosteEntrada,
                EsBenefico = actualizado.EsBenefico,
                FechaEvento = actualizado.FechaEvento,
                ColeccionId = actualizado.ColeccionId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}