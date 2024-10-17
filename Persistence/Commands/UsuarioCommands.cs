using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Entities.UsuariosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;
using static TiendaUNAC.Domain.DTOs.UsuariosDTOs.UsuariosDTOs;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IUsuarioCommands
    {
        Task<RespuestaDTO> crearUsuario(UsuariosDTOs usuariosDTOs);
        Task<RespuestaDTO> actualizarUsuario(UsuariosDTOs usuariosDTOs, int Accion);
        Task<RespuestaDTO> actualizarContrasena(passwordDTOs passwordDTOs);
        Task<bool> insertarCodigo(CodigoDTOs codigoDTOs);
        Task<bool> EliminarCodigo(CodigoDTOs codigoDTOs);
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
                    IdPais = usuariosDTOs.IdPais,
                    IdDepartamento = usuariosDTOs.IdDepartamento,
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
                        orden = usuarioE.IdUsuario
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

        #region ACTUALIZAR CUPON
        public async Task<RespuestaDTO> actualizarCupon(CuponesDTOs cuponesDTOs)
        {
            _logger.LogTrace("Iniciando metodo GeneralesCommands.actualizarCupon...");
            try
            {
                var existeCupon = await _context.CuponesEs.FirstOrDefaultAsync(x => x.IdCupon == cuponesDTOs.IdCupon);
                if (existeCupon != null)
                {
                    existeCupon.TextoCupon = cuponesDTOs.TextoCupon;
                    existeCupon.ValorCupon = cuponesDTOs.ValorCupon;
                    existeCupon.FechaLimite = cuponesDTOs.FechaLimite;
                    existeCupon.Activo = cuponesDTOs.Activo;

                    _context.CuponesEs.Update(existeCupon);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado el cupón exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el cupón!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesCommands.actualizarCupon...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR USUARIO
        public async Task<RespuestaDTO> actualizarUsuario(UsuariosDTOs usuariosDTOs, int Accion)
        {
            _logger.LogTrace("Iniciando metodo UsuarioCommands.actualizarUsuario...");
            try
            {
                var existeUsuario = await _context.UsuariosEs.FirstOrDefaultAsync(x => x.IdUsuario == usuariosDTOs.IdUsuario);
                if (existeUsuario != null)
                {
                    if (Accion == 1)
                    {
                        existeUsuario.Celular = usuariosDTOs.Celular;
                        existeUsuario.Correo = usuariosDTOs.Correo;
                    }
                    else if(Accion == 2)
                    {
                        existeUsuario.IdDepartamento = usuariosDTOs.IdDepartamento;
                        existeUsuario.IdMunicipio = usuariosDTOs.IdMunicipio;
                        existeUsuario.Direccion = usuariosDTOs.Direccion;
                    }

                    _context.UsuariosEs.Update(existeUsuario);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado el usuario exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el usuario!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UsuarioCommands.actualizarUsuario...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR USUARIO
        public async Task<RespuestaDTO> actualizarContrasena(passwordDTOs passwordDTOs)
        {
            _logger.LogTrace("Iniciando metodo UsuarioCommands.actualizarContrasena...");
            try
            {
                var expresion = (Expression<Func<UsuariosE, bool>>)null;

                if (passwordDTOs.accion == 1)
                {
                    expresion = expresion = x => x.IdUsuario == passwordDTOs.idUsuario;
                }

                else if (passwordDTOs.accion == 2)
                {
                    expresion = expresion = x => x.Correo == passwordDTOs.correo.Trim();
                }

                var existeUsuario = await _context.UsuariosEs.FirstOrDefaultAsync(expresion);

                if (existeUsuario != null)
                {
                    var hashedPassword = await _password.GenerarPassword(passwordDTOs.password);

                    existeUsuario.Password = hashedPassword;

                    _context.UsuariosEs.Update(existeUsuario);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado la contraseña exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el usuario!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UsuarioCommands.actualizarContrasena...");
                throw;
            }
        }
        #endregion

        #region INSERTAR CÓDIGO
        public async Task<bool> insertarCodigo(CodigoDTOs codigoDTOs)
        {
            _logger.LogTrace("Iniciando metodo  UsuarioCommands.insertarCodigo...");
            try
            {
                var codigoE = CodigoDTOs.CrearE(codigoDTOs);
                await _context.CodigoEs.AddAsync(codigoE);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo  UsuarioCommands.insertarCodigo...");
                throw;
            }
        }
        #endregion

        #region ELIMINAR CÓDIGO
        public async Task<bool> EliminarCodigo(CodigoDTOs codigoDTOs)
        {
            _logger.LogTrace("Iniciando metodo UsuarioCommands.EliminarCodigo...");
            try
            {
                var codigoE = CodigoDTOs.CrearE(codigoDTOs);
                _context.CodigoEs.Remove(codigoE);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UsuarioCommands.EliminarCodigo......");
                throw;
            }
        }
        #endregion
    }
}
