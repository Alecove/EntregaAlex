using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrendaController : ControllerBase
    {
        private readonly IPrendaService _prendaService;

        public PrendaController(IPrendaService prendaService)
        {
            _prendaService = prendaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrendaResponseDto>>> GetPrendas()
        {
            return Ok(await _prendaService.GetAllPrendasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrendaResponseDto>> GetPrenda(int id)
        {
            var prenda = await _prendaService.GetPrendaByIdAsync(id);
            if (prenda == null) return NotFound();
            return Ok(prenda);
        }

        [HttpPost]
        public async Task<ActionResult<PrendaResponseDto>> CreatePrenda(PrendaRequestDto prenda)
        {
            var created = await _prendaService.CreatePrendaAsync(prenda);
            return CreatedAtAction(nameof(GetPrenda), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrenda(int id, PrendaRequestDto updatedPrenda)
        {
            var result = await _prendaService.UpdatePrendaAsync(id, updatedPrenda);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrenda(int id)
        {
            var deleted = await _prendaService.DeletePrendaAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}