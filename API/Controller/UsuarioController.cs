using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioQueries _usuariosQueries;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioQueries usuariosQueries, ILogger<UsuarioController> logger)
        {
            _usuariosQueries = usuariosQueries;
            _logger = logger;
        }

      
    }
}
