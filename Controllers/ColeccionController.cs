using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColeccionController : ControllerBase
    {
        private readonly IColeccionService _service;

        public ColeccionController(IColeccionService service)
        {
            _service = service;
        }

        // 1. GET ALL
        [HttpGet]
        public async Task<ActionResult<List<Coleccion>>> GetAll()
        {
            return Ok(await _service.GetAllColeccionesAsync());
        }

        // 2. GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Coleccion>> GetById(int id)
        {
            var resultado = await _service.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        // 3. CREATE (POST)
        [HttpPost]
        public async Task<ActionResult<Coleccion>> Create(Coleccion coleccion)
        {
            // El servicio ya se encarga de guardar y devolver el objeto con ID
            var resultado = await _service.CreateAsync(coleccion);
            
            // Devuelve un 201 Created y la ruta para consultar el objeto creado
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        // 4. UPDATE (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Coleccion coleccion)
        {
            if (id != coleccion.Id) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var resultado = await _service.UpdateAsync(id, coleccion);
            
            if (resultado == null) return NotFound();
            
            return NoContent(); // 204 No Content (Estándar para Updates exitosos)
        }

        // 5. DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrado = await _service.DeleteAsync(id);
            
            if (!borrado) return NotFound();
            
            return NoContent();
        }
    }
}