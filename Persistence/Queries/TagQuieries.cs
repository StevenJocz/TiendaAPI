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
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface ITagQuieries
    {
        Task<List<TagDTOs>> listaTag(int accion);
        Task<List<TagDTOs>> TagId(int idTag);
    }

    public class TagQuieries: ITagQuieries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<ITagQuieries> _logger;
        private readonly IConfiguration _configuration;

        public TagQuieries(ILogger<TagQuieries> logger, IConfiguration configuration)
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

        #region LISTAR TAG
        public async Task<List<TagDTOs>> listaTag(int accion)
        {
            _logger.LogTrace("Iniciando metodo TagQuieries.listaTag...");
            try
            {
                var expresion = (Expression<Func<TagE, bool>>)null;

                if (accion == 1)
                {
                    expresion = expresion = x => x.Activo == true || x.Activo == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.Activo == true;
                }

                var tag = await _context.TagEs.Where(expresion).ToListAsync();

                var ListTags = new List<TagDTOs>();

                foreach (var item in tag)
                {
                    var list = new TagDTOs
                    {
                        IdTag = item.IdTag,
                        Tag = item.Tag,
                        Activo = item.Activo,
                    };

                    ListTags.Add(list);
                }

                return ListTags;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagQuieries.listaTag.");
                throw;
            }
        }
        #endregion

        #region LISTAR TAG POR ID
        public async Task<List<TagDTOs>> TagId(int idTag)
        {
            _logger.LogTrace("Iniciando metodo TagQuieries.TagId....");
            try
            {
                var tag = await _context.TagEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdTag == idTag);

                var ListTags = new List<TagDTOs>();

                var list = new TagDTOs
                {
                    IdTag = tag.IdTag,
                    Tag = tag.Tag,
                    Activo = tag.Activo,
                };

                ListTags.Add(list);

                return ListTags;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar TagQuieries.TagId");
                throw;
            }
        }
        #endregion


    }
}
