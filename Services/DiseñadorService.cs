using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class DiseñadorService : IDiseñadorService
    {
        private readonly IDiseñadorRepository _repository;

        public DiseñadorService(IDiseñadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DiseñadorResponseDto>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(d => new DiseñadorResponseDto
            {
                Id = d.Id,
                NombreCompleto = d.NombreCompleto,
                Especialidad = d.Especialidad,
                Edad = d.Edad,
                SalarioAnual = d.SalarioAnual,
                EstaActivo = d.EstaActivo,
                MarcaId = d.MarcaId
            }).ToList();
        }

        public async Task<DiseñadorResponseDto?> GetByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            if (d == null) return null;

            return new DiseñadorResponseDto
            {
                Id = d.Id,
                NombreCompleto = d.NombreCompleto,
                Especialidad = d.Especialidad,
                Edad = d.Edad,
                SalarioAnual = d.SalarioAnual,
                EstaActivo = d.EstaActivo,
                MarcaId = d.MarcaId
            };
        }

        public async Task<DiseñadorResponseDto> CreateAsync(DiseñadorRequestDto request)
        {
            var nuevo = new Diseñador
            {
                NombreCompleto = request.NombreCompleto,
                Especialidad = request.Especialidad,
                Edad = request.Edad,
                SalarioAnual = request.SalarioAnual,
                EstaActivo = request.EstaActivo,
                MarcaId = request.MarcaId,
                FechaContratacion = DateTime.Now 
            };

            var creado = await _repository.CreateAsync(nuevo);

            return new DiseñadorResponseDto
            {
                Id = creado.Id,
                NombreCompleto = creado.NombreCompleto,
                Especialidad = creado.Especialidad,
                Edad = creado.Edad,
                SalarioAnual = creado.SalarioAnual,
                EstaActivo = creado.EstaActivo,
                MarcaId = creado.MarcaId
            };
        }

        public async Task<DiseñadorResponseDto?> UpdateAsync(int id, DiseñadorRequestDto request)
        {
            var diseñador = new Diseñador
            {
                Id = id,
                NombreCompleto = request.NombreCompleto,
                Especialidad = request.Especialidad,
                Edad = request.Edad,
                SalarioAnual = request.SalarioAnual,
                EstaActivo = request.EstaActivo,
                MarcaId = request.MarcaId
            };

            var actualizado = await _repository.UpdateAsync(diseñador);
            if (actualizado == null) return null;

            return new DiseñadorResponseDto
            {
                Id = actualizado.Id,
                NombreCompleto = actualizado.NombreCompleto,
                Especialidad = actualizado.Especialidad,
                Edad = actualizado.Edad,
                SalarioAnual = actualizado.SalarioAnual,
                EstaActivo = actualizado.EstaActivo,
                MarcaId = actualizado.MarcaId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}