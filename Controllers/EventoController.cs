using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _service;

        public EventoController(IEventoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventoResponseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoResponseDto>> GetById(int id)
        {
            var resultado = await _service.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<EventoResponseDto>> Create(EventoRequestDto request)
        {
            var resultado = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EventoRequestDto request)
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