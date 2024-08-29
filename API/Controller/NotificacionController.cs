using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionCommads _notificacionCommads;
        private readonly INotificacionQueries _notificacionQueries;
        private readonly ILogger<NotificacionController> _logger;

        public NotificacionController(INotificacionCommads notificacionCommads, INotificacionQueries notificacionQueries, ILogger<NotificacionController> logger)
        {
            _notificacionCommads = notificacionCommads;
            _notificacionQueries = notificacionQueries;
             _logger = logger;
        }

        #region GET
        [HttpGet("Get_CantidadNotifiaciones")]
        public async Task<IActionResult> cantidadNotifiaciones(int idUsuario)
        {
            _logger.LogInformation("Iniciando NotificacionController.cantidadNotifiaciones...");
            try
            {
                var respuesta = await _notificacionQueries.cantidadNotifiaciones(idUsuario);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar NotificacionController.cantidadNotifiaciones");
                throw;
            }
        }

        [HttpGet("Get_Notifiaciones")]
        public async Task<IActionResult> ListarNotificaciones(int accion, int idUsuario)
        {
            _logger.LogInformation("Iniciando NotificacionController.ListarNotificaciones...");
            try
            {
                var respuesta = await _notificacionQueries.ListarNotificaciones(accion,idUsuario);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar NotificacionController.ListarNotificaciones");
                throw;
            }
        }
        #endregion

        #region PUT
        [HttpPut("Put_Actualizar_Estado")]
        public async Task<IActionResult> actualizarEstado([FromBody] NotificacionDTOs notificacionDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando NotificacionController.actualizarEstado...");
                var respuesta = await _notificacionCommads.actualizarEstado(notificacionDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar NotificacionController.actualizarEstado...");
                throw;
            }
        }
        #endregion
    }
}
