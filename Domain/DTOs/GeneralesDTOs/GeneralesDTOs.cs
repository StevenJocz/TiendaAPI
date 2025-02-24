using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.PasarelaPagoDTOs;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class RespuestaDTO
    {
        public bool resultado { get; set; }
        public string mensaje { get; set; }
        public int? referencia { get; set; }
        public ApiResponse? apiResponse { get; set; }
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

    public class Card
    {
        public string Titulo { get; set; }
        public double Porcentaje { get; set; }
        public int Tipo { get; set; }
        public decimal NumeroTotal { get; set; }
        public decimal NumeroNuevos { get; set; }
        public int Clase { get; set; }
    }

    

}
