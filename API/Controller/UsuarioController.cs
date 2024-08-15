using Microsoft.AspNetCore.Mvc;
using System;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
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

        [HttpGet("Get_Permisos")]
        public async Task<IActionResult> permisosUsuario(int tipoUsuario)
        {
            _logger.LogInformation("Iniciando UsuarioController.permisosUsuario...");
            try
            {
                var respuesta = await _usuariosQueries.permisosUsuario(tipoUsuario);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron permisos registradass. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.permisosUsuario");
                throw;
            }
        }


        [HttpGet("Get_Usuario")]
        public async Task<IActionResult> Usuario()
        {
            _logger.LogInformation("Iniciando UsuarioController.Usuario...");
            try
            {
                var respuesta = await _usuariosQueries.Usuario();
                    return Ok(respuesta);
                
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.Usuario");
                throw;
            }
        }

        [HttpGet("Get_UsuarioID")]
        public async Task<IActionResult> UsuarioId(int IdUsuario)
        {
            _logger.LogInformation("Iniciando UsuarioController.UsuarioId...");
            try
            {
                var respuesta = await _usuariosQueries.UsuarioId(IdUsuario);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.UsuarioId");
                throw;
            }
        }

        [HttpGet("Get_Informacion_Usuario")]
        public async Task<IActionResult> informacionUsuario(int IdUsuario)
        {
            _logger.LogInformation("Iniciando UsuarioController.informacionUsuario...");
            try
            {
                var respuesta = await _usuariosQueries.informacionUsuario(IdUsuario);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.informacionUsuario");
                throw;
            }
        }


        [HttpPut("Put_Actualizar_Usuario")]
        public async Task<IActionResult> actualizarUsuario([FromBody] UsuariosDTOs usuariosDTOs, int Accion)
        {
            try
            {
                _logger.LogInformation("Iniciando UsuarioController.actualizarUsuario...");
                var respuesta = await _usuarioCommands.actualizarUsuario(usuariosDTOs, Accion);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.actualizarUsuario...");
                throw;
            }
        }

        [HttpPut("Put_Actualizar_contrasena")]
        public async Task<IActionResult> actualizarContrasena([FromBody] passwordDTOs passwordDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando UsuarioController.actualizarContrasena...");
                var respuesta = await _usuarioCommands.actualizarContrasena(passwordDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioController.actualizarContrasena...");
                throw;
            }
        }
    }
}
