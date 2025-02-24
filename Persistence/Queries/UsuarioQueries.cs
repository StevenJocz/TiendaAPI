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
using System.Xml.Linq;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.UsuariosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Infrastructure.Email;
using TiendaUNAC.Persistence.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
        Task<List<UsuariosDTOs>> UsuarioDocumento(string documento);
        Task<RespuestaDTO> UsuarioCorreo(string correo);
        Task<RespuestaDTO> verificarCodigo(string correo, int codigo);
    }

    public class UsuarioQueries : IUsuarioQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IUsuarioQueries> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPassword _password;
        private readonly IGenerarToken _generarToken;
        private readonly IUsuarioCommands _usuarioCommands;
        private readonly IGenerarCodigo _generarCodigo;
        private readonly IEmailService _emailService;


        public UsuarioQueries(ILogger<UsuarioQueries> logger, IPassword password, IGenerarToken generarToken, IUsuarioCommands usuarioCommands, IGenerarCodigo generarCodigo, IEmailService emailService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _password = password;
            _generarToken = generarToken;
            _usuarioCommands = usuarioCommands;
            _generarCodigo = generarCodigo;
            _emailService = emailService;
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
                        var departamentoCiudad = (await _context.ubicacionEs
                                           .FromSqlInterpolated($"EXEC Ubicacion @Accion={5}, @Parametro={usuarioExite.IdMunicipio}")
                                           .ToListAsync())
                                           .FirstOrDefault();


                        var tipoDocumento = (await _context.tipoDocumentosEs.FromSqlInterpolated($"EXEC TipoDocumentos @Accion={2}, @IdTipo={usuarioExite.IdTipoDocumento}").ToListAsync())
                                                 .FirstOrDefault();

                        var datosUsuario = new DatosUsuarioDTOs
                        {
                            idUsuario = usuarioExite.IdUsuario,
                            nombre = usuarioExite.Nombre,
                            apellido = usuarioExite.Apellido,
                            tipoDocumento = usuarioExite.IdTipoDocumento,
                            documento = usuarioExite.Documento,
                            correo = usuarioExite.Correo,
                            telefono = usuarioExite.Celular,
                            tipoUsuario = usuarioExite.IdTipoUsuario,
                            genero = usuarioExite.IdGenero,
                            pais = usuarioExite.IdPais,
                            departamento = usuarioExite.IdDepartamento,
                            ciudad = usuarioExite.IdMunicipio,
                            tipoVia = usuarioExite.TipoVia,
                            Numero1 = usuarioExite.Numero1,
                            Numero2 = usuarioExite.Numero2,
                            Numero3 = usuarioExite.Numero3,


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
                        Celular = item.Celular,
                        Ubicacion = ubicacion.Nombre,
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
                    Celular = usuarios.Celular,
                    IdPais = usuarios.IdPais,
                    IdDepartamento = usuarios.IdDepartamento,
                    IdMunicipio = usuarios.IdMunicipio,
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
                    Celular = usuarios.Celular,
                    Ubicacion = ubicacion.Nombre,
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

        #region INFORMACIÓN DE USUARIO CON NUMERO DE DOCUMENTO
        public async Task<List<UsuariosDTOs>> UsuarioDocumento(string documento)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.UsuarioId...");
            try
            {
                documento = documento.Trim();
                var usuarios = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.Documento == documento);
                var listaUsuarios = new List<UsuariosDTOs>();
                if (usuarios != null)
                {
                    var lista = new UsuariosDTOs
                    {
                        IdUsuario = usuarios.IdUsuario,
                        IdTipoUsuario = usuarios.IdTipoUsuario,
                        Nombre = usuarios.Nombre,
                        Apellido = usuarios.Apellido,
                        IdTipoDocumento = usuarios.IdTipoDocumento,
                        Documento = usuarios.Documento,
                        Celular = usuarios.Celular,
                        IdPais = usuarios.IdPais,
                        IdDepartamento = usuarios.IdDepartamento,
                        IdMunicipio = usuarios.IdMunicipio,
                        Correo = usuarios.Correo,
                        FechaRegistro = usuarios.FechaRegistro
                    };

                    listaUsuarios.Add(lista);
                }

                return listaUsuarios;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.UsuarioId...");
                throw;
            }
        }
        #endregion

        #region USUARIO POR CORREO
        public async Task<RespuestaDTO> UsuarioCorreo(string correo)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.UsuarioCorreo...");
            try
            {
                correo = correo.Trim();
                var usuarios = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.Correo == correo);

                if (usuarios.IdUsuario != null)
                {
                    string codigo = await _generarCodigo.generarCodigo();

                    var codigoDTOs = new CodigoDTOs
                    {
                        Codigo = int.Parse(codigo),
                        Correo = correo
                    };

                    bool insertarCodigo = await _usuarioCommands.insertarCodigo(codigoDTOs);

                    if (insertarCodigo)
                    {
                        var contentEmail = await _context.EmailEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdTemplateCorreo == 1);

                        var objectoEmail = new EmailEnviar
                        {
                            Para = correo,
                            Asunto = "Restablecimiento de contraseña",
                            Contenido = contentEmail.Contenido,
                            Codigo = codigo,
                            Nombre = usuarios.Nombre + " " + usuarios.Apellido
                        };

                        bool enviarEmail = await _emailService.EnviarEmailCodigo(objectoEmail); 

                        if (enviarEmail)
                        {
                            return new RespuestaDTO
                            {
                                resultado = true,
                                mensaje = "¡Las instrucciones se han enviado correctamente. Por favor, revisa tu bandeja de entrada.!"
                            };
                        }
                        else
                        {
                            return new RespuestaDTO
                            {
                                resultado = false,
                                mensaje = "¡Hubo un problema al enviar el correo. Por favor, inténtalo más tarde!"
                            };
                        }
                    }
                    else
                    {
                        return new RespuestaDTO
                        {
                            resultado = false,
                            mensaje = "¡Hubo un problema al enviar el correo. Por favor, inténtalo más tarde!"
                        };
                    }
                } 
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se encontró ningun usuario con el correo electrónico proporcionado. Por favor, verifica y vuelve a intentarlo.!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.UsuarioCorreo...");
                throw;
            }
        }
        #endregion

        #region VERIFICAR CÓDIGO
        public async Task<RespuestaDTO> verificarCodigo(string correo, int codigo)
        {
            _logger.LogInformation("Iniciando metodo UsuarioQueries.verificarCodigo...");
            try
            {
                correo = correo.Trim();
                var existeCodigo = await _context.CodigoEs.AsNoTracking().FirstOrDefaultAsync(x => x.Correo == correo && x.Codigo == codigo);

                if (existeCodigo != null)
                {
                    var codigoDTOs = CodigoDTOs.CrearDTOs(existeCodigo);

                    bool codigoEliminado = await _usuarioCommands.EliminarCodigo(codigoDTOs);

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡El código proporcionado es correcto!"
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡El código proporcionado es incorrecto. Por favor, verifica y vuelve a intentarlo!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UsuarioQueries.verificarCodigo...");
                throw;
            }
        }
        #endregion
    }
}
