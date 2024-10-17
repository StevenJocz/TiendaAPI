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
        Task<RespuestaDTO> actualizarProducto(ListaProductosDTOs listaProductosDTOs);
        Task<RespuestaDTO> agregarFavoritos(FavoritosDTOs favoritosDTOs);
        Task<RespuestaDTO> eliminarFavoritos(int idProducto, int idUsuario);
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

        #region ACTUALIZAR PRODUCTO
        public async Task<RespuestaDTO> actualizarProducto(ListaProductosDTOs listaProductosDTOs)
        {
            _logger.LogTrace("Iniciando metodo ProductoCommands.actualizarProducto...");
            try
            {
                var existeProducto = await _context.ProductoEs.FirstOrDefaultAsync(x => x.IdProducto == listaProductosDTOs.Id);
                if (existeProducto != null)
                {

                    var tallaToDelete = await _context.TallaProductoEs.Where(x => x.IdProducto == listaProductosDTOs.Id).ToListAsync();

                    if (tallaToDelete.Any())
                    {
                        _context.TallaProductoEs.RemoveRange(tallaToDelete);
                        await _context.SaveChangesAsync();
                    }

                    if (listaProductosDTOs.Tallas != null || listaProductosDTOs.Tallas.Count > 0)
                    {
                        foreach (var talla in listaProductosDTOs.Tallas)
                        {
                            var tallas = new TallaProductoDTOs
                            {
                                IdProducto = listaProductosDTOs.Id,
                                Talla = talla.nombre,
                                PorcentajeValor = int.Parse(talla.porcentaje)
                            };

                            var tallaE = TallaProductoDTOs.CrearE(tallas);
                            await _context.TallaProductoEs.AddAsync(tallaE);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (listaProductosDTOs.Imagenes != null || listaProductosDTOs.Imagenes.Count > 0)
                    {
                        foreach (var imagen in listaProductosDTOs.Imagenes)
                        {
                            if (imagen.actualizar == false)
                            {
                                var ruta = "";
                                string rutaImagen = "wwwroot/Productos";
                                ruta = await _imagenes.guardarImage(imagen.imagen, rutaImagen);

                                var imagenes = new ImagenProductoDTOs
                                {
                                    IdProducto = listaProductosDTOs.Id,
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
                    }

                    existeProducto.IdInventario = listaProductosDTOs.IdInventario;
                    existeProducto.IdCategoria = listaProductosDTOs.IdCategoria;
                    existeProducto.Nombre = listaProductosDTOs.Nombre;
                    existeProducto.Descripcion = listaProductosDTOs.Descripcion;
                    existeProducto.Informacion = listaProductosDTOs.Informacion;
                    existeProducto.Activo = listaProductosDTOs.Activo;
                    existeProducto.Descuento = listaProductosDTOs.Descuento;
                    existeProducto.FechaFinDescuento = listaProductosDTOs.FechaFinDescuento;

                    _context.ProductoEs.Update(existeProducto);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado el producto exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el producto!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ProductoCommands.actualizarProducto...");
                throw;
            }
        }
        #endregion

        #region AGREGAR FAVORITOS
        public async Task<RespuestaDTO> agregarFavoritos(FavoritosDTOs favoritosDTOs)
        {
            _logger.LogTrace("Iniciando metodo ProductoCommands.agregarFavoritos...");
            try
            {
                if (favoritosDTOs.IdUsuario != 0 )
                {
                    var newFavorito = new FavoritosDTOs
                    {
                        IdProducto = favoritosDTOs.IdProducto,
                        IdUsuario = favoritosDTOs.IdUsuario,
                        FechaAgregado = (DateTime.UtcNow).ToLocalTime(),
                    };

                    var favorito = FavoritosDTOs.CrearE(newFavorito);
                    await _context.FavoritosEs.AddAsync(favorito);
                    await _context.SaveChangesAsync();

                    if (favorito.IdDeseos != 0)
                    {
                        return new RespuestaDTO
                        {
                            resultado = true,
                            mensaje = "¡Se ha añadido el producto a la lista de deseos exitosamente!",
                        };
                    }
                    else
                    {
                        return new RespuestaDTO
                        {
                            resultado = false,
                            mensaje = "¡No se pudo agregar el producto a la lista de deseos ! Por favor, inténtalo de nuevo más tarde.",
                        };
                    }

                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el producto a la lista de deseos exitosamente!",
                    };

                }
               
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ProductoCommands.agregarFavoritos...");
                throw;
            }
        }

        #endregion

        #region ELIMINAR FAVORITOS
        public async Task<RespuestaDTO> eliminarFavoritos(int idProducto, int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo ProductoCommands.eliminarFavoritos...");
            try
            {
                var favorito = await _context.FavoritosEs.FirstOrDefaultAsync(f => f.IdProducto == idProducto && f.IdUsuario == idUsuario);

                if (favorito != null)
                {
                    _context.FavoritosEs.Remove(favorito);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡El producto ha sido eliminado de la lista de deseos exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡El producto no se encontró en la lista de deseos!",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ProductoCommands.eliminarFavoritos...");
                throw;
            }
        }
        #endregion
    }
}
