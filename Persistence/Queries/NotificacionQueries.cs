using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Commands;

namespace TiendaUNAC.Persistence.Queries
{
    public interface INotificacionQueries
    {

    }

    public class NotificacionQueries: INotificacionQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<INotificacionQueries> _logger;
        private readonly IConfiguration _configuration;

        public NotificacionQueries(ILogger<NotificacionQueries> logger, IConfiguration configuration)
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
