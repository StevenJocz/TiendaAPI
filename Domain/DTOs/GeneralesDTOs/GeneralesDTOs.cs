using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class RespuestaDTO
    {
        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }

    public class tiposDocumentosDTOs
    {
        [Key]
        public short IdDocumento { get; set; }
        public string Documento { get; set; }
    }


    public class generosDTOs
    {
        [Key]
        public short IdGenero { get; set; }
        public string Genero { get; set; }
    }

    public class ubicacionDTOs
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
