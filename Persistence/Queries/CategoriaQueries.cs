using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface ICategoriaQueries
    {
        Task<List<CategoriaDTOs>> listaCategorias(int accion);
        Task<List<CategoriaDTOs>> categoriasId(int idCategoria);
    }

    public class CategoriaQueries: ICategoriaQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<ICategoriaQueries> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaQueries(ILogger<CategoriaQueries> logger, IConfiguration configuration)
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

        #region LISTAR CATEGORIA
        public async Task<List<CategoriaDTOs>> listaCategorias(int accion)
        {
            _logger.LogTrace("Iniciando metodo CategoriaQueries.listaCategorias...");
            try
            {
                var expresion = (Expression<Func<CategoriaE, bool>>)null;

                if (accion == 1)
                {
                    expresion = expresion = x => x.Activo == true || x.Activo == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.Activo == true;
                }

                var categorias = await _context.CategoriaEs.Where(expresion).ToListAsync();

                var ListCategorias = new List<CategoriaDTOs>();

                foreach (var item in categorias)
                {
                    var list = new CategoriaDTOs
                    {
                        IdCategoria = item.IdCategoria,
                        Titulo = item.Titulo,
                        Descripcion = item.Descripcion,
                        Nombre = item.Nombre,
                        Imagen = item.Imagen,
                        Activo = item.Activo,
                        IdTercero = item.IdTercero,
                        FechaCreacion = item.FechaCreacion
                    };

                    ListCategorias.Add(list);
                }

                return ListCategorias;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaQueries.listaCategorias");
                throw;
            }
        }
        #endregion

        #region LISTAR CATEGORIA POR ID
        public async Task<List<CategoriaDTOs>> categoriasId(int idCategoria)
        {
            _logger.LogTrace("Iniciando metodo CategoriaQueries.categoriasId...");
            try
            {
                var categorias = await _context.CategoriaEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdCategoria == idCategoria);

                var ListCategorias = new List<CategoriaDTOs>();

                var list = new CategoriaDTOs
                {
                    IdCategoria = categorias.IdCategoria,
                    Titulo = categorias.Titulo,
                    Descripcion = categorias.Descripcion,
                    Nombre = categorias.Nombre,
                    Imagen = categorias.Imagen,
                    Activo = categorias.Activo,
                    IdTercero = categorias.IdTercero,
                    FechaCreacion = categorias.FechaCreacion
                };

                ListCategorias.Add(list);

                return ListCategorias;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CategoriaQueries.categoriasId");
                throw;
            }
        }
        #endregion

    }
}
