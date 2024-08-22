using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
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
        public async Task<IActionResult> Cupones()
        {
            _logger.LogInformation("Iniciando GeneralesController.Cupones...");
            try
            {
                var respuesta = await _generalesQueries.cupones();
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.Cupones");
                throw;
            }
        }


        [HttpGet("Get_Consultar_Cupones")]
        public async Task<IActionResult> ConsultarCupon(string cupon, int idUsuario)
        {
            _logger.LogInformation("Iniciando GeneralesController.CconsultarCupon...");
            try
            {
                var respuesta = await _generalesQueries.consultarCupon(cupon, idUsuario);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.CconsultarCupon");
                throw;
            }
        }

        [HttpGet("Get_Id_Cupones")]
        public async Task<IActionResult> CuponesId(int IdCupon)
        {
            _logger.LogInformation("Iniciando GeneralesController.CuponesId...");
            try
            {
                var respuesta = await _generalesQueries.cuponesId(IdCupon);
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
                _logger.LogError("Error al iniciar GeneralesController.CuponesId");
                throw;
            }
        }

        [HttpPut("Put_Actualizar_Cupones")]
        public async Task<IActionResult> actualizarCupones([FromBody] CuponesDTOs cuponesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GeneralesController.actualizarCupones...");
                var respuesta = await _generalesCommands.actualizarCupon(cuponesDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.actualizarCupones...");
                throw;
            }
        }


        [HttpGet("Get_Monto")]
        public async Task<IActionResult> Monto(int IdMonto)
        {
            _logger.LogInformation("Iniciando GeneralesController.Monto...");
            try
            {
                var respuesta = await _generalesQueries.listarMonto(IdMonto);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.Monto");
                throw;
            }
        }

        [HttpPut("Put_Actualizar_Monto")]
        public async Task<IActionResult> actualizarMonto([FromBody] MontoEnvioDTOs montoEnvioDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando GeneralesController.actualizarMonto...");
                var respuesta = await _generalesCommands.actualizarMonto(montoEnvioDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.actualizarMonto...");
                throw;
            }
        }

        [HttpGet("Get_Estados")]
        public async Task<IActionResult> listarEstados(int Accion)
        {
            _logger.LogInformation("Iniciando GeneralesController.listarEstados...");
            try
            {
                var respuesta = await _generalesQueries.listarEstados(Accion);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.listarEstados");
                throw;
            }
        }

        [HttpGet("Get_ItemDashboard")]
        public async Task<IActionResult> listarItemDashboard()
        {
            _logger.LogInformation("Iniciando GeneralesController.Ubicacion...");
            try
            {
                var respuesta = await _generalesQueries.listarItemDashboard();
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
                _logger.LogError("Error al iniciar GeneralesController.listarItemDashboard");
                throw;
            }
        }

        [HttpGet("Get_VentasPorMesYAnio")]
        public async Task<IActionResult> ObtenerVentasPorMesYAnio()
        {
            _logger.LogInformation("Iniciando GeneralesController.ObtenerVentasPorMesYAnio...");
            try
            {
                var respuesta = await _generalesQueries.ObtenerVentasPorMesYAnio();
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
                _logger.LogError("Error al iniciar GeneralesController.ObtenerVentasPorMesYAnio");
                throw;
            }
        }

    }
}
