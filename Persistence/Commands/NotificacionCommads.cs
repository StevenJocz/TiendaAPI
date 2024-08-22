using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Domain.Entities.NotificacionE;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.Persistence.Commands
{
    public interface INotificacionCommads
    {
        Task<bool> agregarNotificacion(NotificacionCrear notificacionCrear);
    }
    public class NotificacionCommads: INotificacionCommads, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<INotificacionCommads> _logger;
        private readonly IConfiguration _configuration;

        public NotificacionCommads(ILogger<NotificacionCommads> logger, IConfiguration configuration)
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

        #region AGREGAR NOTIFICACION
        public async Task <bool> agregarNotificacion(NotificacionCrear notificacionCrear)
        {
            _logger.LogTrace("Iniciando metodo NotificacionCommads.agregarNotificacion...");
            try
            {
                var newNotificacion = new NotificacionDTOs
                {
                    IdTipoNotificacion = notificacionCrear.IdTipoNotificacion,
                    DeIdUsuario = notificacionCrear.DeIdUsuario,
                    ParaIdUsuario = notificacionCrear.ParaIdUsuario,
                    Fecha = (DateTime.UtcNow).ToLocalTime(),
                    Leida = false
                };

                var notificacion = NotificacionDTOs.CrearE(newNotificacion);
                await _context.NotificacionEs.AddAsync(notificacion);
                await _context.SaveChangesAsync();

                if (notificacion.IdNotificacion != 0)
                {
                    var newNotificacionRelacion = new NotificacionRelacionDTOs
                    {
                        IdNotificaciones = notificacion.IdNotificacion,
                        IdTipoRelacionNotificaciones = notificacionCrear.IdTipoRelacion,
                        IdRelacion = notificacionCrear.IdRelacion
                    };

                    var notificacionRelacion = NotificacionRelacionDTOs.CrearE(newNotificacionRelacion);
                    await _context.NotificacionRelacionEs.AddAsync(notificacionRelacion);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo NotificacionCommads.agregarNotificacion...");
                throw;
            }
        }
        #endregion

    }
}
