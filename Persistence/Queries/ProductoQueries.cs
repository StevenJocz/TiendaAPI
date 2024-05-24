using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IProductoQueries
    {
        Task<List<InventarioSionDTOs>> inventariosSION();
    }

    public class ProductoQueries: IProductoQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IProductoQueries> _logger;
        private readonly IConfiguration _configuration;

        public ProductoQueries(ILogger<ProductoQueries> logger, IConfiguration configuration)
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

        #region INVENTARIO SION
        public async Task<List<InventarioSionDTOs>> inventariosSION()
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.inventariosSION...");
            try
            {
                var inventario = await _context.InventarioSionEs.FromSqlInterpolated($"EXEC InventarioSION").ToListAsync();

                var listInventario = new List<InventarioSionDTOs>();
                foreach (var item in inventario)
                {
                    var list = new InventarioSionDTOs
                    {
                        idInventario = item.idInventario,
                        codigo = item.codigo,
                        nombre = item.nombre,
                        precio = item.precio,

                    };
                    listInventario.Add(list);
                }
                return listInventario;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CursoQueries.ConsultarMaterias...");
                throw;
            }
        }
        #endregion

    }
}
