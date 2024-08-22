using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.UsuariosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using static TiendaUNAC.Domain.DTOs.UsuariosDTOs.UsuariosDTOs;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IUsuarioQueries
    {
        Task<RespuestaInicioSesion> InicioSesion(InicioSesionDTOs inicioSesionDTOs);
        Task<List<PermisosUsuarioDTOs>> permisosUsuario(int tipoUsuario);
        Task<List<InformacionUsuariosDTOS>> Usuario();
        Task<List<UsuariosDTOs>> UsuarioId(int IdUsuario);
        Task<List<InformacionUsuariosDTOS>> informacionUsuario(int IdUsuario);
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
                    var departamentoCiudad = (await _context.ubicacionEs
                                            .FromSqlInterpolated($"EXEC Ubicacion @Accion={5}, @Parametro={usuarioExite.IdMunicipio}")
                                            .ToListAsync())
                                            .FirstOrDefault();


                    if (verificarPassword)
                    {
                        var datosUsuario = new DatosUsuarioDTOs
                        {
                            idUsuario = usuarioExite.IdUsuario,
                            nombre = usuarioExite.Nombre,
                            correo = usuarioExite.Correo,
                            telefono = usuarioExite.Celular,
                            direccion = departamentoCiudad.Nombre + " - " + usuarioExite.Direccion,
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

        #region USUARIOS
        public async Task<List<InformacionUsuariosDTOS>> Usuario()
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.Usuario...");
            try
            {
                var usuarios = await _context.UsuariosEs.ToListAsync();

                var listaUsuarios = new List<InformacionUsuariosDTOS>();

                foreach (var item in usuarios)
                {

                    var tipoDocumento = (await _context.tipoDocumentosEs.FromSqlInterpolated($"EXEC Ubicacion @Accion={6}, @Parametro={item.IdTipoDocumento}").ToListAsync()).FirstOrDefault();

                    var ubicacion = (await _context.ubicacionEs.FromSqlInterpolated($"EXEC Ubicacion @Accion={5}, @Parametro={item.IdMunicipio}").ToListAsync()).FirstOrDefault();
                    
                    var lista = new InformacionUsuariosDTOS
                    {
                        IdUsuario = item.IdUsuario,
                        TipoUsuario = item.IdTipoUsuario == 1 ? "Administrador" : "Usuario",
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        TipoDocumento = tipoDocumento.Documento,
                        Documento = item.Documento,
                        FechaNacimiento = item.FechaNacimiento,
                        Celular = item.Celular,
                        Ubicacion = ubicacion.Nombre,
                        Direccion = item.Direccion,
                        Correo = item.Correo,
                        FechaRegistro = item.FechaRegistro
                    };

                    listaUsuarios.Add(lista);
                }

                return listaUsuarios;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.Usuario...");
                throw;
            }
        }
        #endregion

        #region USUARIO POR ID
        public async Task<List<UsuariosDTOs>> UsuarioId(int IdUsuario)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.UsuarioId...");
            try
            {
                var usuarios = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdUsuario == IdUsuario);

                var listaUsuarios = new List<UsuariosDTOs>();

                var lista = new UsuariosDTOs
                {
                    IdUsuario = usuarios.IdUsuario,
                    IdTipoUsuario = usuarios.IdTipoUsuario,
                    Nombre = usuarios.Nombre,
                    Apellido = usuarios.Apellido,
                    IdTipoDocumento = usuarios.IdTipoDocumento,
                    Documento = usuarios.Documento,
                    FechaNacimiento = usuarios.FechaNacimiento,
                    Celular = usuarios.Celular,
                    IdPais = usuarios.IdPais,
                    IdDepartamento = usuarios.IdDepartamento,
                    IdMunicipio = usuarios.IdMunicipio,
                    Direccion = usuarios.Direccion,
                    Correo = usuarios.Correo,
                    FechaRegistro = usuarios.FechaRegistro
                };

                listaUsuarios.Add(lista);

                return listaUsuarios;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.UsuarioId...");
                throw;
            }
        }
        #endregion

        #region INFORMACIÓN DE USUARIO
        public async Task<List<InformacionUsuariosDTOS>> informacionUsuario(int IdUsuario)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.informacionUsuario...");
            try
            {
                var usuarios = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdUsuario == IdUsuario);

                var listaUsuarios = new List<InformacionUsuariosDTOS>();

                var tipoDocumento = (await _context.tipoDocumentosEs.FromSqlInterpolated($"EXEC Ubicacion @Accion={6}, @Parametro={usuarios.IdTipoDocumento}").ToListAsync()).FirstOrDefault();

                var ubicacion = (await _context.ubicacionEs.FromSqlInterpolated($"EXEC Ubicacion @Accion={5}, @Parametro={usuarios.IdMunicipio}").ToListAsync()).FirstOrDefault();

                var lista = new InformacionUsuariosDTOS
                {
                    IdUsuario = usuarios.IdUsuario,
                    TipoUsuario = usuarios.IdTipoUsuario == 1 ? "Administrador" : "Usuario",
                    Nombre = usuarios.Nombre,
                    Apellido = usuarios.Apellido,
                    TipoDocumento = tipoDocumento.Documento,
                    Documento = usuarios.Documento,
                    FechaNacimiento = usuarios.FechaNacimiento,
                    Celular = usuarios.Celular,
                    Ubicacion = ubicacion.Nombre,
                    Direccion = usuarios.Direccion,
                    Correo = usuarios.Correo,
                    FechaRegistro = usuarios.FechaRegistro
                };

                listaUsuarios.Add(lista);

                return listaUsuarios;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.informacionUsuario...");
                throw;
            }
        }
        #endregion
    }
}
