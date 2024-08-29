using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        
        private readonly IProductoQueries _productoQueries;
        private readonly IProductoCommands _productoCommands;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductoQueries productoQueries, IProductoCommands productoCommands, ILogger<ProductoController> logger)
        {
            _productoQueries = productoQueries;
            _productoCommands = productoCommands;
            _logger = logger;
        }


        #region POST
        [HttpPost("Post_Agregar_Favoritos")]
        public async Task<IActionResult> agregarFavoritos([FromBody] FavoritosDTOs agregarFavoritos)
        {
            try
            {
                _logger.LogInformation("Iniciando ProductoController.agregarFavoritos...");
                var respuesta = await _productoCommands.agregarFavoritos(agregarFavoritos);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.agregarFavoritos...");
                throw;
            }
        }

        [HttpPost("Post_Crear_Producto")]
        public async Task<IActionResult> crearProducto([FromBody] ListaProductosDTOs listaProductosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProductoController.crearProducto...");
                var respuesta = await _productoCommands.crearProducto(listaProductosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.crearProducto...");
                throw;
            }
        }
        #endregion

        #region GET
        [HttpGet("Get_Favoritos_Usuario")]
        public async Task<IActionResult> listaFavoritosUsuarios(int idUsuario)
        {
            _logger.LogInformation("Iniciando ProductoController.listaFavoritosUsuarios...");
            try
            {
                var respuesta = await _productoQueries.listaFavoritosUsuarios(idUsuario);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontro favoritos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.listaFavoritosUsuarios");
                throw;
            }
        }

        [HttpGet("Get_Inventario")]
        public async Task<IActionResult> inventariosSION()
        {
            _logger.LogInformation("Iniciando ProductoController.inventariosSION...");
            try
            {
                var respuesta = await _productoQueries.inventariosSION();

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
                _logger.LogError("Error al iniciar ProductoController.inventariosSION");
                throw;
            }
        }

        [HttpGet("Get_Id_Inventario")]
        public async Task<IActionResult> inventariosSIONId(int idInventario)
        {
            _logger.LogInformation("Iniciando ProductoController.inventariosSIONId...");
            try
            {
                var respuesta = await _productoQueries.inventariosSIONId(idInventario);

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
                _logger.LogError("Error al iniciar ProductoController.inventariosSIONId");
                throw;
            }
        }

        [HttpGet("Get_Productos")]
        public async Task<IActionResult> listaProductos(int accion)
        {
            _logger.LogInformation("Iniciando ProductoController.listaProductos...");
            try
            {
                var respuesta = await _productoQueries.listaProductos(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontro productos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.listaProductos");
                throw;
            }
        }

        [HttpGet("Get_Id_Producto")]
        public async Task<IActionResult> ProductosId(int idProducto)
        {
            _logger.LogInformation("Iniciando CategoriaController.ProductosId...");
            try
            {
                var respuesta = await _productoQueries.ProductosId(idProducto);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el producto registrado. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.ProductosId");
                throw;
            }
        }

       
        [HttpGet("Get_Favoritos")]
        public async Task<IActionResult> listaFavoritos(int idUsuario)
        {
            _logger.LogInformation("Iniciando ProductoController.listaFavoritos...");
            try
            {
                var respuesta = await _productoQueries.listaFavoritos(idUsuario);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontro favoritos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.listaFavoritos");
                throw;
            }
        }

        [HttpGet("Get_Productos_Favoritos")]
        public async Task<IActionResult> listaProductosFavoritos([FromQuery]  List<int> IdProductos)
        {
            _logger.LogInformation("Iniciando ProductoController.listaProductosFavoritos...");
            try
            {
                var respuesta = await _productoQueries.listaProductosFavoritos(IdProductos);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontro productos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.listaProductosFavoritos");
                throw;
            }
        }
        #endregion

        #region PUT
        [HttpPut("Put_Actualizar_Producto")]
        public async Task<IActionResult> actualizarProducto([FromBody] ListaProductosDTOs listaProductosDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ProductoController.actualizarProducto...");
                var respuesta = await _productoCommands.actualizarProducto(listaProductosDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.actualizarProducto...");
                throw;
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete_Eliminar_Favoritos")]
        public async Task<IActionResult> eliminarFavoritos(int idProducto, int idUsuario)
        {
            try
            {
                _logger.LogInformation("Iniciando ProductoController.eliminarFavoritos...");
                var respuesta = await _productoCommands.eliminarFavoritos(idProducto, idUsuario);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoController.eliminarFavoritos...");
                throw;
            }
        }
        #endregion

    }
}
