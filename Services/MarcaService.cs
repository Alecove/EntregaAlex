using EntregaAlex.Models;
using EntregaAlex.Repository;
using EntregaAlex.Dtos;

namespace EntregaAlex.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;

        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MarcaResponseDto>> GetAllMarcasAsync()
        {
            var marcas = await _repository.GetAllAsync();
            
            // Convertimos de Modelo (BBDD) a DTO (Respuesta)
            return marcas.Select(m => new MarcaResponseDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                PaisOrigen = m.PaisOrigen,
                AnioFundacion = m.AnioFundacion,
               
                EsAltaCostura = m.EsAltaCostura
            }).ToList();
        }

        public async Task<MarcaResponseDto?> GetMarcaByIdAsync(int id)
        {
            var m = await _repository.GetByIdAsync(id);
            if (m == null) return null;

            return new MarcaResponseDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                PaisOrigen = m.PaisOrigen,
                AnioFundacion = m.AnioFundacion,
                
                EsAltaCostura = m.EsAltaCostura
            };
        }

        public async Task<MarcaResponseDto> CreateMarcaAsync(MarcaRequestDto request)
        {
            // Convertimos DTO (Petición) a Modelo (BBDD)
            var nuevaMarca = new Marca
            {
                Nombre = request.Nombre,
                PaisOrigen = request.PaisOrigen,
                AnioFundacion = request.AnioFundacion,
                EsAltaCostura = request.EsAltaCostura,
            };

            // Guardamos en BBDD
            var creada = await _repository.CreateAsync(nuevaMarca);

            // Devolvemos el resultado convertido a DTO
            return new MarcaResponseDto
            {
                Id = creada.Id,
                Nombre = creada.Nombre,
                PaisOrigen = creada.PaisOrigen,
                AnioFundacion = creada.AnioFundacion,
                EsAltaCostura = creada.EsAltaCostura
            };
        }

        public async Task<MarcaResponseDto?> UpdateMarcaAsync(int id, MarcaRequestDto request)
        {
            var marca = new Marca
            {
                Id = id,
                Nombre = request.Nombre,
                PaisOrigen = request.PaisOrigen,
                AnioFundacion = request.AnioFundacion,
                EsAltaCostura = request.EsAltaCostura
            };

            var actualizada = await _repository.UpdateAsync(marca);
            if (actualizada == null) return null;

            return new MarcaResponseDto
            {
                Id = actualizada.Id,
                Nombre = actualizada.Nombre,
                PaisOrigen = actualizada.PaisOrigen,
                AnioFundacion = actualizada.AnioFundacion,
                EsAltaCostura = actualizada.EsAltaCostura
            };
        }

        public async Task<bool> DeleteMarcaAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}