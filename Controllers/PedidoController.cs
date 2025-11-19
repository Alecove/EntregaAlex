using Microsoft.AspNetCore.Mvc;
using RESTAURANT_API_SERVICE.Models;
using RESTAURANT_API_SERVICE.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RESTAURANT_API_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        // Asumo que tienes un servicio de logging, sino puedes usar ILogger<PedidosController>

        // Inyección de dependencia del servicio
        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // --- MÉTODOS DE LECTURA (GET) ---

        // Endpoint para obtener todos los pedidos pendientes (para la cocina o gestión)
        // GET: api/pedidos/pendientes
        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosPendientes()
        {
            var pedidos = await _pedidoService.GetPedidosPendientesAsync();
            if (pedidos == null)
            {
                return NotFound("No se encontraron pedidos pendientes.");
            }
            return Ok(pedidos);
        }

        // Endpoint para obtener un pedido por su ID
        // GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            if (pedido == null)
            {
                return NotFound($"Pedido con ID {id} no encontrado.");
            }
            return Ok(pedido);
        }

        // --- MÉTODOS DE ESCRITURA (POST/PUT) ---
        
        // Endpoint para crear un nuevo pedido
        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> CrearPedido([FromBody] Pedido nuevoPedido)
        {
            // Nota: Aquí se simula la obtención del ID de usuario, en una app real vendría del JWT o de la sesión.
            int usuarioIdSimulado = 1; 

            try
            {
                var createdPedido = await _pedidoService.CreatePedidoAsync(nuevoPedido, usuarioIdSimulado);
                // Retorna 201 Created con la ubicación del nuevo recurso.
                return CreatedAtAction(nameof(GetPedidoById), new { id = createdPedido.Id }, createdPedido);
            }
            catch (Exception ex)
            {
                // Manejo general de errores, como datos inválidos o problemas de cálculo
                return BadRequest($"Error al crear el pedido: {ex.Message}");
            }
        }

        // Endpoint para marcar un pedido como finalizado/entregado
        // PUT: api/pedidos/finalizar/5
        [HttpPut("finalizar/{id}")]
        public async Task<IActionResult> FinalizarPedido(int id)
        {
            var success = await _pedidoService.FinalizarPedidoAsync(id);
            if (!success)
            {
                return NotFound($"No se pudo finalizar el pedido con ID {id}. Podría no existir.");
            }
            // Retorna 204 No Content para una actualización exitosa sin cuerpo de respuesta.
            return NoContent(); 
        }
    }
}