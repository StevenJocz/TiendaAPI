using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IUsuarioCommands
    {

    }

    public class UsuarioCommands: IUsuarioCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IUsuarioCommands> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioCommands(TiendaUNACContext context, ILogger<IUsuarioCommands> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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

        #region CREA USUARIO
        public async Task<RespuestaDTO> crearUsuario()
        {
            _logger.LogInformation("Iniciando metodo UsuarioCommands.crearUsuario...");
            try
            {

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UsuarioCommands.crearUsuario...");
                throw;
            }
        } 

        #endregion


    }
}
