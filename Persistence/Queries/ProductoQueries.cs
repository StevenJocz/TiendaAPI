using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IProductoQueries
    {
        Task<List<InventarioSionDTOs>> inventariosSION();
        Task<List<InventarioSionDTOs>> inventariosSIONId(int idInventario);
        Task<List<VerProductoDtos>> listaProductos(int accion);
        Task<List<ListaProductosDTOs>> ProductosId(int idProducto);
        Task<List<FavoritosDTOs>> listaFavoritos(int idUsuario);
        Task<List<VerProductoDtos>> listaProductosFavoritos(List<int> IdProductos);
        Task<List<FavoritosInformacion>> listaFavoritosUsuarios(int idUsuario);
    }

    public class ProductoQueries: IProductoQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IProductoQueries> _logger;
        private readonly IConfiguration _configuration;

        public ProductoQueries(ILogger<ProductoQueries> logger, IConfiguration configuration)
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

        #region INVENTARIO SION
        public async Task<List<InventarioSionDTOs>> inventariosSION()
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.inventariosSION...");
            try
            {
                var inventario = await _context.InventarioSionEs.FromSqlInterpolated($"EXEC InventarioSION @Accion={1}, @IdInventario={0}").ToListAsync();

                var listInventario = new List<InventarioSionDTOs>();
                foreach (var item in inventario)
                {
                    var list = new InventarioSionDTOs
                    {
                        idInventario = item.idInventario,
                        codigo = item.codigo,
                        nombre = item.nombre,
                        precio = item.precio,
                        existencias = item.existencias,

                    };
                    listInventario.Add(list);
                }
                return listInventario;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CursoQueries.ConsultarMaterias...");
                throw;
            }
        }
        #endregion

        #region INVENTARIO SION POR ID
        public async Task<List<InventarioSionDTOs>> inventariosSIONId(int idInventario)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.inventariosSION...");
            try
            {
                var inventario = _context.InventarioSionEs
                       .FromSqlInterpolated($"EXEC InventarioSION @Accion={2}, @IdInventario={idInventario}").AsEnumerable()
                       .FirstOrDefault();

                var listInventario = new List<InventarioSionDTOs>();
                var list = new InventarioSionDTOs
                {
                    idInventario = inventario.idInventario,
                    codigo = inventario.codigo,
                    nombre = inventario.nombre,
                    precio = inventario.precio,
                    existencias = inventario.existencias,

                };
                listInventario.Add(list);
                return listInventario;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CursoQueries.ConsultarMaterias...");
                throw;
            }
        }
        #endregion

        #region LISTAR PRODUCTOS
        public async Task<List<VerProductoDtos>> listaProductos(int accion)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.listaProductos...");
            try
            {
                var expresion = (Expression<Func<ProductoE, bool>>)null;

                if (accion == 1)
                {
                    expresion = expresion = x => x.Activo == true || x.Activo == false;
                }

                else if (accion == 2)
                {
                    expresion = expresion = x => x.Activo == true;
                }

                var productos = await _context.ProductoEs.Where(expresion).ToListAsync();
                var ListProductos = new List<VerProductoDtos>();

                foreach (var item in productos)
                {
                    var imageneE = await _context.ImagenProductoEs.Where(x => x.IdProducto == item.IdProducto).Take(2).ToListAsync();

                    var ListImagenes = new List<ImagenDto>();

                    if (imageneE != null)
                    {
                        var imagenes = new ImagenDto
                        {
                            ImagenUno = imageneE.ElementAtOrDefault(0).Imagen,
                            ImagenDos = imageneE.ElementAtOrDefault(1).Imagen
                        };

                        ListImagenes.Add(imagenes);
                    }

                    var categoria = await _context.CategoriaEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdCategoria == item.IdCategoria);

                    var inventario =  _context.InventarioSionEs
                        .FromSqlInterpolated($"EXEC InventarioSION @Accion={2}, @IdInventario={item.IdInventario}").AsEnumerable()
                        .FirstOrDefault();

                    if (inventario != null && inventario.existencias <= 0 && accion == 2)
                    {
                        // No agregar el producto a la lista
                        continue;
                    }

                    var list = new VerProductoDtos
                    {
                        Id = item.IdProducto,
                        Nombre = accion == 1 ? inventario.codigo + " - " + item.Nombre : item.Nombre,
                        Categorias = categoria.Nombre,
                        Imagenes = ListImagenes,
                        AplicaDescuento = item.Descuento != 0 && item.FechaFinDescuento > DateTime.Now ? true : false,
                        Descuento = item.Descuento,
                        Nuevo = (DateTime.Now - item.FechaCreado).TotalDays < 15,
                        Activo = item.Activo,
                        existencias = inventario.existencias
                    };

                    ListProductos.Add(list);
                }

                return ListProductos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoQueries.listaProductos");
                throw;
            }
        }
        #endregion

        #region LISTAR PRODUCTOS POR ID
        public async Task<List<ListaProductosDTOs>> ProductosId(int idProducto)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.ProductosId...");
            try
            {
                var productos = await _context.ProductoEs.Where(x => x.IdProducto == idProducto).ToListAsync();

                var ListProductos = new List<ListaProductosDTOs>();

                foreach (var item in productos)
                {
                    var imageneE = await _context.ImagenProductoEs.Where(x => x.IdProducto == item.IdProducto).ToListAsync();
                    var ListImagenes = new List<ListaImagenesDTOs>();

                    foreach (var imagen in imageneE)
                    {
                        var imagenes = new ListaImagenesDTOs
                        {
                            id = imagen.IdImagenProducto,
                            imagen = imagen.Imagen,
                            nombreImagen = imagen.Nombre,
                            nombreColor = imagen.NombreColor,
                            color = imagen.Color,
                            porcentajeValor = imagen.PorcentajeValor.ToString(),
                            actualizar = true
                        };

                        ListImagenes.Add(imagenes);
                    }

                    var tallaE = await _context.TallaProductoEs.Where(x => x.IdProducto == item.IdProducto).ToListAsync();
                    var ListTallas = new List<ListaTallaDTOs>();

                    var inventario = _context.InventarioSionEs
                       .FromSqlInterpolated($"EXEC InventarioSION @Accion={2}, @IdInventario={item.IdInventario}").AsEnumerable()
                       .FirstOrDefault();

                    foreach (var talla in tallaE)
                    {
                        double valor = inventario.precio * talla.PorcentajeValor / 100;
                        var tallas = new ListaTallaDTOs
                        {
                            id = talla.IdTallaProducto,
                            nombre = talla.Talla,
                            porcentaje = talla.PorcentajeValor.ToString(),
                            valor = valor + inventario.precio
                        };

                        ListTallas.Add(tallas);
                    }

                    double precio = inventario.precio * inventario.iva / 100;


                    var list = new ListaProductosDTOs
                    {
                        Id = item.IdProducto,
                        IdInventario = item.IdInventario,
                        IdCategoria = item.IdCategoria,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        Informacion = item.Informacion,
                        Tags = item.Tags,
                        Descuento = item.Descuento,
                        FechaFinDescuento = item.FechaFinDescuento,
                        Activo = item.Activo,
                        IdTercero = item.IdTercero,
                        stock = inventario.existencias,
                        precioBase = inventario.precio + precio,
                        Imagenes = ListImagenes,
                        Tallas = ListTallas
                    };

                    ListProductos.Add(list);
                }

                return ListProductos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoQueries.ProductosId");
                throw;
            }
        }
        #endregion

        #region LISTAR  FAVORITOS
        public async Task<List<FavoritosDTOs>> listaFavoritos(int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.listaFavoritos...");
            try
            {
                var favoritos = await _context.FavoritosEs.Where(x => x.IdUsuario == idUsuario).ToListAsync();

                var ListFavoritos = new List<FavoritosDTOs>();

                foreach (var item in favoritos)
                {
                    var list = new FavoritosDTOs
                    {
                        IdDeseos = item.IdDeseos,
                        IdProducto = item.IdProducto,
                        IdUsuario = item.IdUsuario,
                        FechaAgregado = item.FechaAgregado,
                    };

                    ListFavoritos.Add(list);
                }

                return ListFavoritos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoQueries.listaFavoritos");
                throw;
            }
        }
        #endregion

        #region LISTAR PRODUCTOS FAVORITOS
        public async Task<List<VerProductoDtos>> listaProductosFavoritos(List<int> IdProductos)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.listaProductos...");
            try
            {
                var productos = await _context.ProductoEs.Where(x => IdProductos.Contains(x.IdProducto)).ToListAsync();

                var ListProductos = new List<VerProductoDtos>();

                foreach (var item in productos)
                {
                    var imageneE = await _context.ImagenProductoEs.Where(x => x.IdProducto == item.IdProducto).Take(2).ToListAsync();

                    var ListImagenes = new List<ImagenDto>();

                    if (imageneE != null)
                    {
                        var imagenes = new ImagenDto
                        {
                            ImagenUno = imageneE.ElementAtOrDefault(0).Imagen,
                            ImagenDos = imageneE.ElementAtOrDefault(1).Imagen
                        };

                        ListImagenes.Add(imagenes);
                    }

                    var categoria = await _context.CategoriaEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdCategoria == item.IdCategoria);

                    var inventario = _context.InventarioSionEs
                        .FromSqlInterpolated($"EXEC InventarioSION @Accion={2}, @IdInventario={item.IdInventario}").AsEnumerable()
                        .FirstOrDefault();

                    var list = new VerProductoDtos
                    {
                        Id = item.IdProducto,
                        Nombre = item.Nombre,
                        Categorias = categoria.Nombre,
                        Imagenes = ListImagenes,
                        AplicaDescuento = item.Descuento != 0 && item.FechaFinDescuento > DateTime.Now ? true : false,
                        Descuento = item.Descuento,
                        Nuevo = (DateTime.Now - item.FechaCreado).TotalDays < 15,
                        Activo = item.Activo,
                        existencias = inventario.existencias
                    };

                    ListProductos.Add(list);
                }

                return ListProductos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoQueries.listaProductos");
                throw;
            }
        }
        #endregion

        #region LISTAR  FAVORITOS USUARIO
        public async Task<List<FavoritosInformacion>> listaFavoritosUsuarios(int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo ProductoQueries.listaFavoritosUsuarios...");
            try
            {
                var favoritos = await _context.FavoritosEs.Where(x => x.IdUsuario == idUsuario).ToListAsync();

                var ListFavoritos = new List<FavoritosInformacion>();

                foreach (var item in favoritos)
                {
                    var producto = await _context.ProductoEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdProducto == item.IdProducto);
                    var categoria = await _context.CategoriaEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdCategoria == producto.IdCategoria);
                    var imagen = await _context.ImagenProductoEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdProducto == item.IdProducto);
                    var list = new FavoritosInformacion
                    {
                        
                        IdProducto = item.IdProducto,
                        Nombre = producto.Nombre,
                        Categoria = categoria.Nombre,
                        Imagen = imagen.Imagen
                    };

                    ListFavoritos.Add(list);
                }

                return ListFavoritos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ProductoQueries.listaFavoritosUsuarios...");
                throw;
            }
        }
        #endregion
    }
}
