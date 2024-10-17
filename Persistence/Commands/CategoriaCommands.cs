using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaUNAC.Persistence.Commands
{
    public interface ICategoriaCommands
    {
        Task<RespuestaDTO> crearCategoria(CategoriaDTOs categoriaDTOs);
        Task<RespuestaDTO> actualizarCategoria(CategoriaDTOs categoriaDTOs);
    }

    public class CategoriaCommands: ICategoriaCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<ICategoriaCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IImagenes _imagenes;

        public CategoriaCommands(ILogger<CategoriaCommands> logger, IConfiguration configuration, IImagenes imagenes)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);
            _imagenes = imagenes;
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

        #region CREAR CATEGORIA
        public async Task<RespuestaDTO> crearCategoria(CategoriaDTOs categoriaDTOs)
        {
            _logger.LogTrace("Iniciando metodo CategoriaCommands.crearCategoria...");
            try
            {
                var ruta = "";
                if (categoriaDTOs.Imagen != null)
                {
                    string rutaImagen = "wwwroot/Categoria";
                    ruta = await _imagenes.guardarImage(categoriaDTOs.Imagen, rutaImagen);
                }

                var newCategoria = new CategoriaDTOs
                {
                    IdCategoria = categoriaDTOs.IdCategoria,
                    Titulo = categoriaDTOs.Titulo,
                    Descripcion = categoriaDTOs.Descripcion,
                    Nombre = categoriaDTOs.Nombre,
                    Imagen = ruta,
                    Activo = categoriaDTOs.Activo,
                    IdTercero = categoriaDTOs.IdTercero,
                    FechaCreacion = DateTime.UtcNow
                };

                var categoria = CategoriaDTOs.CrearE(newCategoria);
                await _context.CategoriaEs.AddAsync(categoria);
                await _context.SaveChangesAsync();

                if (categoria.IdCategoria != 0)
                {
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido la categoría exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar la categoría! Por favor, inténtalo de nuevo más tarde.cc",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CategoriaCommands.crearCategoria...");
                throw;
            }
        }

        #endregion

        #region ACTUALIZAR CATEGORIA

        public async Task<RespuestaDTO> actualizarCategoria(CategoriaDTOs categoriaDTOs)
        {
            _logger.LogTrace("Iniciando metodo CategoriaCommands.actualizarCategoria...");
            try
            {
                var existeCategoria = await _context.CategoriaEs.FirstOrDefaultAsync(x => x.IdCategoria == categoriaDTOs.IdCategoria);
                if (existeCategoria != null)
                {
                    var ruta = "";
                    if (categoriaDTOs.Imagen != "")
                    {
                        await _imagenes.eliminarImage(categoriaDTOs.Imagen);

                        string rutaImagen = "wwwroot/Categoria";
                        ruta = await _imagenes.guardarImage(categoriaDTOs.Imagen, rutaImagen);
                        existeCategoria.Imagen = ruta;
                    }

                    existeCategoria.Titulo = categoriaDTOs.Titulo;
                    existeCategoria.Descripcion = categoriaDTOs.Descripcion;
                    existeCategoria.Nombre = categoriaDTOs.Nombre;
                    existeCategoria.Activo = categoriaDTOs.Activo;
                    existeCategoria.IdTercero = categoriaDTOs.IdTercero;
                    existeCategoria.FechaCreacion = DateTime.UtcNow;

                     _context.CategoriaEs.Update(existeCategoria);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado la categoría exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar la categoría!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CategoriaCommands.actualizarCategoria...");
                throw;
            }
        }
        #endregion
    }
}
