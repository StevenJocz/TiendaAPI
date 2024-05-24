using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ProductoE;

namespace TiendaUNAC.Domain.DTOs.ProductoDTOs
{
    public class ProductoDTOs
    {
        public int IdProducto { get; set; }
        public int IdInventario { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Informacion { get; set; }
        public string Tags { get; set; }
        public int Descuento { get; set; }
        public DateTime FechaFinDescuento { get; set; }
        public bool Activo { get; set; }
        public int IdTercero { get; set; }
        public DateTime FechaCreado { get; set; }

        public static ProductoDTOs CrearDTOs(ProductoE productoE)
        {
            return new ProductoDTOs
            {
                IdProducto = productoE.IdProducto,
                IdInventario = productoE.IdInventario,
                IdCategoria = productoE.IdCategoria,
                Nombre = productoE.Nombre,
                Descripcion = productoE.Descripcion,
                Informacion = productoE.Informacion,
                Tags = productoE.Tags,
                Descuento = productoE.Descuento,
                FechaFinDescuento = productoE.FechaFinDescuento,
                Activo = productoE.Activo,
                IdTercero = productoE.IdTercero,
                FechaCreado = productoE.FechaCreado
            };
        }

        public static ProductoE CrearE(ProductoDTOs productoDTOs)
        {
            return new ProductoE
            {
                IdProducto = productoDTOs.IdProducto,
                IdInventario = productoDTOs.IdInventario,
                IdCategoria = productoDTOs.IdCategoria,
                Nombre = productoDTOs.Nombre,
                Descripcion = productoDTOs.Descripcion,
                Informacion = productoDTOs.Informacion,
                Tags = productoDTOs.Tags,
                Descuento = productoDTOs.Descuento,
                FechaFinDescuento = productoDTOs.FechaFinDescuento,
                Activo= productoDTOs.Activo,
                IdTercero = productoDTOs.IdTercero,
                FechaCreado = productoDTOs.FechaCreado
            };
        }
    }

    public class ListaProductosDTOs
    {
        public int Id { get; set; }
        public int IdInventario { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Informacion { get; set; }
        public string Tags { get; set; }
        public int Descuento { get; set; }
        public DateTime FechaFinDescuento { get; set; }
        public bool Activo { get; set; }
        public List<ListaImagenesDTOs> Imagenes { get; set; }
        public List<ListaTallaDTOs> Tallas { get; set; }
    }
}
