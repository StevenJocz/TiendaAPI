using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralesController : ControllerBase
    {
        private readonly IGeneralesQueries _generalesQueries;
        private readonly IGeneralesCommands _generalesCommands;
        private readonly ILogger<GeneralesController> _logger;

        public GeneralesController(IGeneralesQueries generalesQueries, IGeneralesCommands generalesCommands, ILogger<GeneralesController> logger)
        {
            _generalesQueries = generalesQueries;
            _generalesCommands = generalesCommands;
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

        [HttpPost("Post_Crear_Cupon")]
        public async Task<IActionResult> crearCupon([FromBody] CuponesDTOs cuponesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CategoriaController.crearCategoria...");
                var respuesta = await _generalesCommands.crearCupon(cuponesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaController.crearCategoria...");
                throw;
            }
        }

        [HttpGet("Get_Cupones")]
        public async Task<IActionResult> Cupones(int Accion, string Cupon)
        {
            _logger.LogInformation("Iniciando GeneralesController.Cupones...");
            try
            {
                var respuesta = await _generalesQueries.cupones(Accion, Cupon);
                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron registros. Por favor intenta nuevamente más tarde");
                }
                else
                {
                    return Ok(respuesta);
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.Cupones");
                throw;
            }
        }
    }
}
