using Microsoft.AspNetCore.Mvc;
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
    }
}
