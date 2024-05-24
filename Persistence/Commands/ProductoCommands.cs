using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IProductoCommands
    {
        Task<RespuestaDTO> crearProducto(ListaProductosDTOs listaProductosDTOs);
    }
    public class ProductoCommands: IProductoCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IProductoCommands> _logger;
        private readonly IConfiguration _configuration;

        public ProductoCommands(ILogger<ProductoCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

        #region CREAR PRODUCTO
        public async Task<RespuestaDTO> crearProducto(ListaProductosDTOs listaProductosDTOs)
        {
            _logger.LogTrace("Iniciando metodo ProductoCommands.crearProducto...");
            try
            {
                

                if (listaProductosDTOs.IdCategoria != 0)
                {
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el producto exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar el producto! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ProductoCommands.crearProducto...");
                throw;
            }
        }

        #endregion
    }
}
