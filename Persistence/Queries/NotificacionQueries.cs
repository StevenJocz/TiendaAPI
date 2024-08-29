using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaUNAC.Persistence.Queries
{
    public interface INotificacionQueries
    {
        Task<List<ListarNotificacionesDTOs>> ListarNotificaciones(int accion, int idUsuario);
        Task<int> cantidadNotifiaciones(int idUsuario);
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

        #region LISTAR NOTIFICACIONES
        public async Task<int> cantidadNotifiaciones(int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo NotificacionQueries.cantidadNotifiaciones...");
            try
            {
                var notficaciones =  _context.CountNotificacionEs.FromSqlInterpolated($"EXEC Listar_Notificaciones @Accion={1}, @IdUsuario={idUsuario}").AsEnumerable()
                       .FirstOrDefault();

                int CantidadNotficaciones = notficaciones.cantidad;
              
                return CantidadNotficaciones;

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo NotificacionQueries.cantidadNotifiaciones...");
                throw;
            }
        }
        #endregion

        #region LISTAR NOTIFICACIONES
        public async Task<List<ListarNotificacionesDTOs>> ListarNotificaciones(int accion, int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo NotificacionQueries.ListarNotificaciones...");
            try
            {
                var notficaciones = await _context.ListarNotificacionEs.FromSqlInterpolated($"EXEC Listar_Notificaciones @Accion={accion}, @IdUsuario={idUsuario}").ToListAsync();

                var listNotficaciones = new List<ListarNotificacionesDTOs>();
                foreach (var item in notficaciones)
                {
                    var list = new ListarNotificacionesDTOs
                    {
                        IdNotificacion = item.IdNotificacion,
                        IdTipoNotificacion = item.IdTipoNotificacion,
                        DeIdUsuario = item.DeIdUsuario,
                        ParaIdUsuario = item.ParaIdUsuario,
                        Leida = item.Leida,
                        Icono = item.Icono,
                        Notificacion = item.Notificacion,
                        IdRelacion = item.IdRelacion,
                        Orden = item.Orden,
                        Fecha = item.Fecha,
                        CategoriaFecha = item.CategoriaFecha,
                    };
                    listNotficaciones.Add(list);
                }
                return listNotficaciones;

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo NotificacionQueries.ListarNotificaciones...");
                throw;
            }
        }
        #endregion
    }
}
