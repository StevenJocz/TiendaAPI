using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ComentarioDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioCommands _comentarioCommands;
        private readonly IComentarioQueries _queriesCommands;
        private readonly ILogger<ComentarioController> _logger;

        public ComentarioController(IComentarioCommands comentarioCommands, IComentarioQueries queriesCommands, ILogger<ComentarioController> logger)
        {
            _comentarioCommands = comentarioCommands;
            _queriesCommands = queriesCommands;
            _logger = logger;
        }

        [HttpPost("Post_Agregar_Comentario")]
        public async Task<IActionResult> agregarComentario([FromBody] ComentarioDTOs comentarioDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ComentarioController.agregarComentario...");
                var respuesta = await _comentarioCommands.agregarComentario(comentarioDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ComentarioController.agregarComentario...");
                throw;
            }
        }


        [HttpGet("Get_Comentario")]
        public async Task<IActionResult> listarComentario(int IdProducto)
        {
            _logger.LogInformation("Iniciando ComentarioController.listarComentario...");
            try
            {
                var respuesta = await _queriesCommands.listarComentario(IdProducto);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontron comentarios registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ComentarioController.listarComentario");
                throw;
            }
        }
    }
}
