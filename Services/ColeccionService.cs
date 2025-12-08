using System.Collections.Generic;
using System.Threading.Tasks;
using EntregaAlex.Models;
using EntregaAlex.Repository;

namespace EntregaAlex.Services
{
    public class ColeccionService : IColeccionService
    {
        private readonly IColeccionRepository _repository;

        public ColeccionService(IColeccionRepository repository)
        {
            _repository = repository;
        }

        // 1. GET ALL
        public async Task<List<Coleccion>> GetAllColeccionesAsync()
        {
            return await _repository.GetAllColeccionesAsync();
        }

        // 2. GET BY ID
        public async Task<Coleccion?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // 3. CREATE
        public async Task<Coleccion> CreateAsync(Coleccion coleccion)
        {
            return await _repository.CreateAsync(coleccion);
        }

        // 4. UPDATE
        public async Task<Coleccion?> UpdateAsync(int id, Coleccion coleccion)
        {
            // Aseguramos que el ID del objeto coincida con el de la URL
            coleccion.Id = id;
            return await _repository.UpdateAsync(coleccion);
        }

        // 5. DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}