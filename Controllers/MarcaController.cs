using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MarcaResponseDto>>> GetMarcas()
        {
            var marcas = await _marcaService.GetAllMarcasAsync();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaResponseDto>> GetMarca(int id)
        {
            var marca = await _marcaService.GetMarcaByIdAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaResponseDto>> CreateMarca(MarcaRequestDto marca)
        {
            var createdMarca = await _marcaService.CreateMarcaAsync(marca);
            return CreatedAtAction(nameof(GetMarca), new { id = createdMarca.Id }, createdMarca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarca(int id, MarcaRequestDto updatedMarca)
        {
            // En nuestro diseño, el servicio se encarga de buscar y actualizar.
            // Si devuelve null, significa que no encontró la marca (NotFound).
            var result = await _marcaService.UpdateMarcaAsync(id, updatedMarca);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            // El servicio intenta borrar. Si devuelve false, es que no existía.
            var deleted = await _marcaService.DeleteMarcaAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}