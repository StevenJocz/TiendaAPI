using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralesController : ControllerBase
    {
        private readonly IGeneralesQueries _generalesQueries;
        private readonly ILogger<GeneralesController> _logger;

        public GeneralesController(IGeneralesQueries generalesQueries, ILogger<GeneralesController> logger)
        {
            _generalesQueries = generalesQueries;
            _logger = logger;
        }

        [HttpGet("Get_TipoDocumento")]
        public async Task<IActionResult> tipoDocumentos()
        {
            _logger.LogInformation("Iniciando GeneralesController.tipoDocumentos...");
            try
            {
                var respuesta = await _generalesQueries.TiposDocumentos();
                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontro inventario registrado. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.tipoDocumentos");
                throw;
            }
        }

        [HttpGet("Get_Generos")]
        public async Task<IActionResult> generos()
        {
            _logger.LogInformation("Iniciando GeneralesController.generos...");
            try
            {
                var respuesta = await _generalesQueries.generos();
                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron generos registrados. Por favorm intenta nuevamente más tarde");
                }
                else
                {
                    return Ok(respuesta);
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.generos");
                throw;
            }
        }

        [HttpGet("Get_Ubicacion")]
        public async Task<IActionResult> ubicacion(int Accion, int Parametro)
        {
            _logger.LogInformation("Iniciando GeneralesController.Ubicacion...");
            try
            {
                var respuesta = await _generalesQueries.ubicacion(Accion, Parametro);
                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron registros. Por favorm intenta nuevamente más tarde");
                }
                else
                {
                    return Ok(respuesta);
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.Ubicacion");
                throw;
            }
        }
    }
}
