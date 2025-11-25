using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

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

        [HttpGet]
        public async Task<ActionResult<List<ColeccionResponseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ColeccionResponseDto>> GetById(int id)
        {
            var resultado = await _service.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<ColeccionResponseDto>> Create(ColeccionRequestDto request)
        {
            // Recuerda poner un ID de Diseñador que exista, o fallará la Foreign Key
            var resultado = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ColeccionRequestDto request)
        {
            var resultado = await _service.UpdateAsync(id, request);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrado = await _service.DeleteAsync(id);
            if (!borrado) return NotFound();
            return NoContent();
        }
    }
}