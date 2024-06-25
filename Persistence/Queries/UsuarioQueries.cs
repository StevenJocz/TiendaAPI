using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using static TiendaUNAC.Domain.DTOs.UsuariosDTOs.UsuariosDTOs;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IUsuarioQueries
    {
        Task<RespuestaInicioSesion> InicioSesion(InicioSesionDTOs inicioSesionDTOs);
        Task<List<PermisosUsuarioDTOs>> permisosUsuario(int tipoUsuario);
    }

    public class UsuarioQueries : IUsuarioQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IUsuarioQueries> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPassword _password;
        private readonly IGenerarToken _generarToken;

        public UsuarioQueries(ILogger<UsuarioQueries> logger, IPassword password, IGenerarToken generarToken, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _password = password;
            _generarToken = generarToken;
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

        #region INICIO DE SESIÓN
        public async Task<RespuestaInicioSesion> InicioSesion(InicioSesionDTOs inicioSesionDTOs)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.InicioSesion...");
            try
            {
                string correo = inicioSesionDTOs.correo.Trim();
                string password = inicioSesionDTOs.password.Trim();

                var usuarioExite = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.Correo == correo);

                if (usuarioExite != null)
                {
                    var verificarPassword = await _password.VerificarPassword(password, usuarioExite.Password);

                    if (verificarPassword)
                    {
                        var datosUsuario = new DatosUsuarioDTOs
                        {
                            idUsuario = usuarioExite.IdUsuario,
                            nombre = usuarioExite.Nombre,
                            correo = usuarioExite.Correo,
                            tipoUsuario = usuarioExite.IdTipoUsuario
                        };

                        var totken = await _generarToken.Token(datosUsuario);

                        return new RespuestaInicioSesion
                        {
                            resultado = true,
                            mensaje = "¡Inicio de sesión exitos!",
                            token = totken
                        };
                    }
                    else
                    {
                        return new RespuestaInicioSesion
                        {
                            resultado = false,
                            mensaje = "Las credenciales de correo electrónico o contraseña proporcionadas son inválidas.Por favor, verifica la información ingresada e intenta nuevamente.",
                            token = ""
                        };
                    }
                }
                else
                {
                    return new RespuestaInicioSesion
                    {
                        resultado = false,
                        mensaje = "Las credenciales de correo electrónico o contraseña proporcionadas son inválidas.Por favor, verifica la información ingresada e intenta nuevamente.",
                        token = ""
                    };

                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.InicioSesion...");
                throw;
            }
        }
        #endregion

        #region PERMISOS USUARIOS
        public async Task<List<PermisosUsuarioDTOs>> permisosUsuario(int tipoUsuario)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.permisosUsuario...");
            try
            {
                var permisos = await _context.PermisosUsuarioEs.Where(x => x.IdTipoUsuario == tipoUsuario).ToListAsync();

                var listaPermisos = new List<PermisosUsuarioDTOs>();

                foreach(var permiso in permisos)
                {
                    var lista = new PermisosUsuarioDTOs
                    {
                        IdTipoUsuariosPermiso = permiso.IdTipoUsuariosPermiso,
                        IdTipoUsuario = permiso.IdTipoUsuario,
                        Path = permiso.Path,
                        Icono = permiso.Icono,
                        Texto = permiso.Texto
                    };

                    listaPermisos.Add(lista);
                }

                return listaPermisos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.permisosUsuario...");
                throw;
            }
        }
        #endregion
    }
}
