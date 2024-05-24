using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagCommands _tagCommands;
        private readonly ITagQuieries _tagQueries;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagCommands tagCommands, ITagQuieries tagQueries, ILogger<TagController> logger)
        {
            _tagCommands = tagCommands;
            _tagQueries = tagQueries;
            _logger = logger;
        }

        [HttpPost("Post_Crear_Tag")]
        public async Task<IActionResult> crearTag([FromBody] TagDTOs tagDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TagController.crearTag...");
                var respuesta = await _tagCommands.crearTag(tagDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagController.crearTag...");
                throw;
            }
        }


        [HttpPut("Put_Actualizar_Tag")]
        public async Task<IActionResult> actualizarTag([FromBody] TagDTOs tagDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando TagController.actualizarTag...");
                var respuesta = await _tagCommands.actualizarTag(tagDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagController.actualizarTag...");
                throw;
            }
        }


        [HttpGet("Get_Tag")]
        public async Task<IActionResult> listaTag(int accion)
        {
            _logger.LogInformation("Iniciando TagController.listaTag...");
            try
            {
                var respuesta = await _tagQueries.listaTag(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el tag registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagController.listaTag");
                throw;
            }
        }


        [HttpGet("Get_Id_Tag")]
        public async Task<IActionResult> TagId(int idTag)
        {
            _logger.LogInformation("Iniciando TagController.TagId...");
            try
            {
                var respuesta = await _tagQueries.TagId(idTag);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el tag registrado. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagController.TagId");
                throw;
            }
        }
    }
}
