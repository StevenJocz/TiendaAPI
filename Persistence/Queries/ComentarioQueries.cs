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
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ComentarioE;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IComentarioQueries
    {
        Task<List<ComentarioDTOs>> listarComentario(int IdProducto);

    }
    public class ComentarioQueries: IComentarioQueries,IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IComentarioQueries> _logger;
        private readonly IConfiguration _configuration;

        public ComentarioQueries(ILogger<ComentarioQueries> logger, IConfiguration configuration)
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

        #region LISTAR COMENTARIOS
        public async Task<List<ComentarioDTOs>> listarComentario(int IdProducto)
        {
            _logger.LogTrace("Iniciando metodo ComentarioQueries.listarComentario...");
            try
            {
                var comentarios = await _context.ComentarioEs.Where(x => x.IdProducto == IdProducto).ToListAsync();

                var listComentarios = new List<ComentarioDTOs>();
                foreach (var item in comentarios)
                {
                    var imagenesE = await _context.ComentarioImagenEs.Where(x => x.IdComentario == item.IdComentario).ToListAsync();

                    var ListImagenes = new List<ComentarioImagenDTOs>();

                    if (imagenesE.Count > 0)
                    {
                        foreach (var imagen in imagenesE)
                        {
                            var imagenes = new ComentarioImagenDTOs
                            {
                                IdComentarioImagen = imagen.IdComentarioImagen,
                                IdComentario = imagen.IdComentario,
                                Imagen = imagen.Imagen,
                            };

                            ListImagenes.Add(imagenes);
                        }
                    }
                   
                    var list = new ComentarioDTOs
                    {
                        IdComentario = item.IdComentario,
                        IdProducto = item.IdProducto,
                        IdUsuario = item.IdUsuario,
                        Comentario = item.Comentario,
                        Fecha = item.Fecha,
                        Calificacion = item.Calificacion,
                        imagenes = ListImagenes

                    };
                    listComentarios.Add(list);
                }

                return listComentarios;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ComentarioQueries.listarComentario...");
                throw;
            }
        }

        #endregion
    }
}
