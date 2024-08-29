using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ComentarioDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ComentarioE;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.NotificacionE;
using TiendaUNAC.Domain.Entities.PedidosE;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IComentarioCommands
    {
        Task<RespuestaDTO> agregarComentario(ComentarioDTOs comentarioDTOs);
        Task<RespuestaDTO> EliminarComentario(int idComentario);
        Task<RespuestaDTO> actualizareEstadoComentario(ComentarioDTOs comentarioDTOs);
    }
    public class ComentarioCommands: IComentarioCommands, IDisposable
    {

        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IComentarioCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IImagenes _imagenes;
        private readonly INotificacionCommads _notificacion;

        public ComentarioCommands(ILogger<ComentarioCommands> logger, IConfiguration configuration, IImagenes imagenes, INotificacionCommads notificacion)
        {
            _logger = logger;
            _configuration = configuration;
            _imagenes = imagenes;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);
            _notificacion = notificacion;
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

        #region AGREGAR COMENTARIO
        public async Task<RespuestaDTO> agregarComentario(ComentarioDTOs comentarioDTOs)
        {
            _logger.LogTrace("Iniciando metodo ComentarioCommands.agregarComentario...");
            try
            {
                var newComentario = new ComentarioDTOs
                {
                    IdProducto = comentarioDTOs.IdProducto,
                    IdUsuario = comentarioDTOs.IdUsuario,
                    Comentario = string.IsNullOrEmpty(comentarioDTOs.Comentario) ? null : comentarioDTOs.Comentario,
                    Fecha = (DateTime.UtcNow).ToLocalTime(),
                    Calificacion = comentarioDTOs.Calificacion,
                    VistoAdmin = false,
                };

                var comentario = ComentarioDTOs.CrearE(newComentario);
                await _context.ComentarioEs.AddAsync(comentario);
                await _context.SaveChangesAsync();

                if (comentario.IdComentario != 0)
                {
                    if (comentarioDTOs.imagenes != null || comentarioDTOs.imagenes.Count > 0)
                    {
                        foreach (var imagen in comentarioDTOs.imagenes)
                        {
                            var ruta = "";
                            string rutaImagen = "wwwroot/Comentarios";
                            ruta = await _imagenes.guardarImage(imagen.Imagen, rutaImagen);

                            var imagenes = new ComentarioImagenDTOs
                            {
                                IdComentario = comentario.IdComentario,
                                Imagen = ruta,
                            };

                            var imagenE = ComentarioImagenDTOs.CrearE(imagenes);
                            await _context.ComentarioImagenEs.AddAsync(imagenE);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var notificacionEnvio = new NotificacionCrear
                    {
                        IdTipoNotificacion = 14,
                        DeIdUsuario = comentarioDTOs.IdUsuario,
                        ParaIdUsuario = 2,
                        IdTipoRelacion = 3,
                        IdRelacion = comentario.IdComentario
                    };

                    await _notificacion.agregarNotificacion(notificacionEnvio);

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el comentario exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar el comentario! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ComentarioCommands.agregarComentario...");
                throw;
            }
        }

        #endregion

        #region ELIMINAR COMENTARIO
        public async Task<RespuestaDTO> EliminarComentario(int idComentario)
        {

            _logger.LogTrace("Iniciando metodo ComentarioCommands.cambiarEstadoComentario...");
            try
            {
                var EliminarComentario = await _context.ComentarioEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdComentario == idComentario);

                if (EliminarComentario.IdComentario != 0)
                {
                    var EliminarImagenes = _context.ComentarioImagenEs.AsNoTracking().Where(x => x.IdComentario == idComentario).ToList();


                    if (EliminarImagenes.Count > 0)
                    {
                        foreach (var imagen in EliminarImagenes)
                        {
                            var location = await _imagenes.eliminarImage(imagen.Imagen);
                            _context.ComentarioImagenEs.Remove(imagen);
                            _context.SaveChanges();
                        }
                    }

                    _context.ComentarioEs.Remove(EliminarComentario);
                    _context.SaveChanges();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Comentario eliminado exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡Problemas al eliminar el comentario!",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ComentarioCommands.cambiarEstadoComentario...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR ESTADO COMENTARIO
        public async Task<RespuestaDTO> actualizareEstadoComentario(ComentarioDTOs comentarioDTOs)
        {
            _logger.LogTrace("Iniciando metodo actualizareEstadoComentario.actualizarCupon...");
            try
            {
                var existeComentario = await _context.ComentarioEs.FirstOrDefaultAsync(x => x.IdComentario == comentarioDTOs.IdComentario);
                if (existeComentario != null)
                {
                    existeComentario.VistoAdmin = true;

                    _context.ComentarioEs.Update(existeComentario);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado el estado del comentario exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el comentario!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo actualizareEstadoComentario.actualizarCupon...");
                throw;
            }
        }
        #endregion

    }
}
