               using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ProductoE;

namespace TiendaUNAC.Domain.DTOs.ProductoDTOs
{
    public class ImagenProductoDTOs
    {
        public int IdImagenProducto { get; set; }
        public int IdProducto { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string NombreColor { get; set; }
        public string Color { get; set; }
        public int PorcentajeValor { get; set; }

        public static ImagenProductoDTOs CrearDTOs(ImagenProductoE imagenProductoE)
        {
            return new ImagenProductoDTOs
            {
                IdImagenProducto = imagenProductoE.IdImagenProducto,
                IdProducto = imagenProductoE.IdProducto,
                Imagen = imagenProductoE.Imagen,
                Nombre = imagenProductoE.Nombre,
                NombreColor = imagenProductoE.NombreColor,
                Color = imagenProductoE.Color,
                PorcentajeValor = imagenProductoE.PorcentajeValor
            };
        }

        public static ImagenProductoE CrearE(ImagenProductoDTOs imagenProductoDTOs)
        {
            return new ImagenProductoE
            {
                IdImagenProducto = imagenProductoDTOs.IdImagenProducto,
                IdProducto = imagenProductoDTOs.IdProducto,
                Imagen = imagenProductoDTOs.Imagen,
                Nombre = imagenProductoDTOs.Nombre,
                NombreColor = imagenProductoDTOs.NombreColor,
                Color = imagenProductoDTOs.Color,
                PorcentajeValor = imagenProductoDTOs.PorcentajeValor
            };
        }
       
    }

    public class ListaImagenesDTOs
    {
        public int id { get; set; }
        public string imagen { get; set; }
        public string nombreImagen { get; set; }
        public string nombreColor { get; set; }
        public string color { get; set; }
        public string porcentajeValor { get; set; }
     
    }
}
