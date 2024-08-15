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
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ComentarioE;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IComentarioCommands
    {
        Task<RespuestaDTO> agregarComentario(ComentarioDTOs comentarioDTOs);
    }
    public class ComentarioCommands: IComentarioCommands, IDisposable
    {

        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IComentarioCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IImagenes _imagenes;

        public ComentarioCommands(ILogger<ComentarioCommands> logger, IConfiguration configuration, IImagenes imagenes)
        {
            _logger = logger;
            _configuration = configuration;
            _imagenes = imagenes;
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
    }
}
