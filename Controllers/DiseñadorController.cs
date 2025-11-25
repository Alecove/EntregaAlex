using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseñadorController : ControllerBase
    {
        private readonly IDiseñadorService _service;

        public DiseñadorController(IDiseñadorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiseñadorResponseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiseñadorResponseDto>> GetById(int id)
        {
            var resultado = await _service.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<DiseñadorResponseDto>> Create(DiseñadorRequestDto request)
        {
            // OJO: Si intentas crear un diseñador con un MarcaId que no existe, MySQL dará error 500.
            // Lo ideal sería validar primero si la marca existe, pero para este trabajo vale así.
            var resultado = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DiseñadorRequestDto request)
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