using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
       
        private readonly IPedidoCommands _pedidoCommands;
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(IPedidoCommands pedidoCommands, ILogger<PedidoController> logger)
        {
            _pedidoCommands = pedidoCommands;
            _logger = logger;
        }


        [HttpPost("Post_Registrar_Pedido")]
        public async Task<IActionResult> registrarPedido([FromBody] RegistrarPedido registrarPedido)
        {
            try
            {
                _logger.LogInformation("Iniciando PedidoController.registrarPedido...");
                var respuesta = await _pedidoCommands.registrarPedido(registrarPedido);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.registrarPedido...");
                throw;
            }
        }
    }
}
