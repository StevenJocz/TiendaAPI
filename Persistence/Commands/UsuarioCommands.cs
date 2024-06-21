using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.UsuariosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IUsuarioCommands
    {
        Task<RespuestaDTO> crearUsuario(UsuariosDTOs usuariosDTOs);
    }

    public class UsuarioCommands: IUsuarioCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IUsuarioCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPassword _password;

        public UsuarioCommands(ILogger<IUsuarioCommands> logger, IConfiguration configuration, IPassword password)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);
            _password = password;
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
        public async Task<RespuestaDTO> crearUsuario(UsuariosDTOs usuariosDTOs)
        {
            _logger.LogInformation("Iniciando metodo UsuarioCommands.crearUsuario...");
            try
            {
                var hashedPassword = await _password.GenerarPassword(usuariosDTOs.Password);
                var usuario = new UsuariosDTOs
                {
                    IdTipoUsuario = usuariosDTOs.IdTipoUsuario,
                    Nombre = usuariosDTOs.Nombre,
                    Apellido = usuariosDTOs.Apellido,
                    IdTipoDocumento = usuariosDTOs.IdTipoDocumento,
                    Documento = usuariosDTOs.Documento,
                    FechaNacimiento = usuariosDTOs.FechaNacimiento,
                    Celular = usuariosDTOs.Celular,
                    IdMunicipio = usuariosDTOs.IdMunicipio,
                    Direccion = usuariosDTOs.Direccion,
                    Correo = usuariosDTOs.Correo,
                    Password = hashedPassword,
                    FechaRegistro = usuariosDTOs.FechaRegistro
                };

                var usuarioE = UsuariosDTOs.CrearE(usuario);
                await _context.UsuariosEs.AddAsync(usuarioE);
                await _context.SaveChangesAsync();

                if (usuarioE.IdUsuario != 0)
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
                _logger.LogError("Error en el metodo UsuarioCommands.crearUsuario...");
                throw;
            }
        } 

        #endregion


    }
}
