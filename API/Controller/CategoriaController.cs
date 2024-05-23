using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaCommands _categoriaCommands;
        private readonly ICategoriaQueries _categoriaQueries;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaCommands categoriaCommands, ICategoriaQueries categoriaQueries, ILogger<CategoriaController> logger)
        {
            _categoriaCommands = categoriaCommands;
            _categoriaQueries = categoriaQueries;
            _logger = logger;
        }

        [HttpPost("Post_Crear_Categoria")]
        public async Task<IActionResult> crearCategoria([FromBody] CategoriaDTOs categoriaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CategoriaController.crearCategoria...");
                var respuesta = await _categoriaCommands.crearCategoria(categoriaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaController.crearCategoria...");
                throw;
            }
        }


        [HttpPut("Put_Actualizar_Categoria")]
        public async Task<IActionResult> actualizarCategoria([FromBody] CategoriaDTOs categoriaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CategoriaController.actualizarCategoria...");
                var respuesta = await _categoriaCommands.actualizarCategoria(categoriaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaController.actualizarCategoria...");
                throw;
            }
        }


        [HttpGet("Get_Categoria")]
        public async Task<IActionResult> listaCategorias(int accion)
        {
            _logger.LogInformation("Iniciando CategoriaController.listaCategorias...");
            try
            {
                var respuesta = await _categoriaQueries.listaCategorias(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron categorías registradass. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaController.listaCategorias");
                throw;
            }
        }


        [HttpGet("Get_Id_Categoria")]
        public async Task<IActionResult> categoriasId(int idCategoria)
        {
            _logger.LogInformation("Iniciando CategoriaController.CategoriastId...");
            try
            {
                var respuesta = await _categoriaQueries.categoriasId(idCategoria);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron categoria registrada. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaController.ListTypeDocument");
                throw;
            }
        }

    }
}
