using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.Entities.ComentarioE;
using TiendaUNAC.Domain.Entities.ProductoE;

namespace TiendaUNAC.Domain.DTOs.ComentarioDTOs
{
    public class ComentarioDTOs
    {
        public int IdComentario { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public bool VistoAdmin { get; set; }
        public List<ComentarioImagenDTOs>? imagenes { get; set; }

        public static ComentarioDTOs CrearDTOs(ComentarioE comentarioE)
        {
            return new ComentarioDTOs
            {
                IdComentario = comentarioE.IdComentario,
                IdProducto = comentarioE.IdProducto,
                IdUsuario = comentarioE.IdUsuario,
                Comentario = comentarioE.Comentario,
                Fecha = comentarioE.Fecha,
                Calificacion = comentarioE.Calificacion,
                VistoAdmin = comentarioE.VistoAdmin,
            };
        }

        public static ComentarioE CrearE(ComentarioDTOs comentarioDTOs)
        {
            return new ComentarioE
            {
                IdComentario = comentarioDTOs.IdComentario,
                IdProducto = comentarioDTOs.IdProducto,
                IdUsuario = comentarioDTOs.IdUsuario,
                Comentario = comentarioDTOs.Comentario,
                Fecha = comentarioDTOs.Fecha,
                Calificacion = comentarioDTOs.Calificacion,
                VistoAdmin = comentarioDTOs.VistoAdmin,
            };
        }
    }

    public class Comentarios
    {
        public int IdComentario { get; set; }
        public string Imagen{ get; set; }
        public string Producto { get; set; }
        public string Cliente { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public bool VistoAdmin { get; set; }
        public List<ComentarioImagenDTOs>? imagenes { get; set; }
    }
}
