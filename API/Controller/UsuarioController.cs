using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;
using static TiendaUNAC.Domain.DTOs.UsuariosDTOs.UsuariosDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioQueries _usuariosQueries;
        private readonly IUsuarioCommands _usuarioCommands;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioQueries usuariosQueries, IUsuarioCommands usuarioCommands, ILogger<UsuarioController> logger)
        {
            _usuariosQueries = usuariosQueries;
            _usuarioCommands = usuarioCommands;
            _logger = logger;
        }

        [HttpPost("Post_Crear_Usuario")]
        public async Task<IActionResult> crearUsuario([FromBody] UsuariosDTOs usuariosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando UsuarioController.crearUsuario...");
                var respuesta = await _usuarioCommands.crearUsuario(usuariosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.crearUsuario...");
                throw;
            }
        }

        [HttpPost("Post_InicioSesion")]
        public async Task<IActionResult> InicioSesion([FromBody] InicioSesionDTOs inicioSesionDTOs)
        {
            _logger.LogInformation("Iniciando UsuarioController.inicioSesion...");
            try
            {
                var resultado = await _usuariosQueries.InicioSesion(inicioSesionDTOs);
                if (resultado == null)
                    return Unauthorized();
                return Ok(resultado);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.inicioSesion...");
                throw;
            }
        }

    }
}
