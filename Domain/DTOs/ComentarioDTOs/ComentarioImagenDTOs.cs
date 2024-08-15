using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ComentarioE;

namespace TiendaUNAC.Domain.DTOs.ComentarioDTOs
{
    public class ComentarioImagenDTOs
    {
        public int IdComentarioImagen { get; set; }
        public int IdComentario { get; set; }
        public string Imagen { get; set; }

        public static ComentarioImagenDTOs CrearDTOs(ComentarioImagenE comentarioImagenE)
        {
            return new ComentarioImagenDTOs
            {
                IdComentarioImagen = comentarioImagenE.IdComentarioImagen,
                IdComentario = comentarioImagenE.IdComentario,
                Imagen = comentarioImagenE.Imagen,
            };
        }

        public static ComentarioImagenE CrearE(ComentarioImagenDTOs comentarioImagenDTOs)
        {
            return new ComentarioImagenE
            {
                IdComentarioImagen = comentarioImagenDTOs.IdComentarioImagen,
                IdComentario = comentarioImagenDTOs.IdComentario,
                Imagen = comentarioImagenDTOs.Imagen,
            };
        }
    }
}
