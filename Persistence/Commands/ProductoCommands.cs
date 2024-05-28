using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IProductoCommands
    {
        Task<RespuestaDTO> crearProducto(ListaProductosDTOs listaProductosDTOs);
    }
    public class ProductoCommands: IProductoCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IProductoCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IImagenes _imagenes;

        public ProductoCommands(ILogger<ProductoCommands> logger, IConfiguration configuration, IImagenes imagenes)
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

        #region CREAR PRODUCTO
        public async Task<RespuestaDTO> crearProducto(ListaProductosDTOs listaProductosDTOs)
        {
            _logger.LogTrace("Iniciando metodo ProductoCommands.crearProducto...");
            try
            {
                var producto = new ProductoDTOs
                {
                    IdInventario = listaProductosDTOs.IdInventario,
                    IdCategoria = listaProductosDTOs.IdCategoria,
                    Nombre = listaProductosDTOs.Nombre,
                    Descripcion = listaProductosDTOs.Descripcion,
                    Informacion = listaProductosDTOs.Informacion,
                    Tags = listaProductosDTOs.Tags,
                    Descuento = listaProductosDTOs.Descuento,
                    FechaFinDescuento = listaProductosDTOs.FechaFinDescuento,
                    Activo = listaProductosDTOs.Activo,
                    IdTercero = listaProductosDTOs.IdTercero,
                    FechaCreado = DateTime.UtcNow
                };

                var productoE = ProductoDTOs.CrearE(producto);
                await _context.ProductoEs.AddAsync(productoE);
                await _context.SaveChangesAsync();

                if (productoE.IdProducto != 0)
                {
                    if (listaProductosDTOs.Imagenes != null || listaProductosDTOs.Imagenes.Count > 0)
                    {
                        foreach (var imagen in listaProductosDTOs.Imagenes)
                        {
                            var ruta = "";
                            string rutaImagen = "wwwroot/Productos";
                            ruta = await _imagenes.guardarImage(imagen.imagen, rutaImagen);

                            var imagenes = new ImagenProductoDTOs
                            {
                                IdProducto = productoE.IdProducto,
                                Imagen = ruta,
                                Nombre = imagen.nombreImagen,
                                NombreColor = imagen.nombreColor,
                                Color = imagen.color,
                                PorcentajeValor = int.Parse(imagen.porcentajeValor)
                            };

                            var imagenE = ImagenProductoDTOs.CrearE(imagenes);
                            await _context.ImagenProductoEs.AddAsync(imagenE);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (listaProductosDTOs.Tallas != null || listaProductosDTOs.Tallas.Count > 0)
                    {
                        foreach (var talla in listaProductosDTOs.Tallas)
                        {
                            var tallas = new TallaProductoDTOs
                            {
                                IdProducto = productoE.IdProducto,
                                Talla = talla.nombre,
                                PorcentajeValor = int.Parse(talla.porcentaje)
                            };

                            var tallaE = TallaProductoDTOs.CrearE(tallas);
                            await _context.TallaProductoEs.AddAsync(tallaE);
                            await _context.SaveChangesAsync();
                        }
                    }
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
                _logger.LogError("Error en el metodo ProductoCommands.crearProducto...");
                throw;
            }
        }

        #endregion
    }
}
