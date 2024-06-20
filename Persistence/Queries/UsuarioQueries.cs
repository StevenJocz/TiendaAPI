using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IUsuarioQueries
    {
        
    }

    public class UsuarioQueries : IUsuarioQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IUsuarioQueries> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioQueries(ILogger<UsuarioQueries> logger, IConfiguration configuration)
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


       
    }
}
