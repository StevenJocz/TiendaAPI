using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Domain.DTOs.UsuariosDTOs
{
    public class CodigoDTOs
    {
        public int IdCodigo { get; set; }
        public int Codigo { get; set; }
        public string Correo { get; set; }

        public static CodigoDTOs CrearDTOs(CodigoE codigoE)
        {
            return new CodigoDTOs
            {
                IdCodigo = codigoE.IdCodigo,
                Codigo = codigoE.Codigo,
                Correo = codigoE.Correo
            };
        }

        public static CodigoE CrearE(CodigoDTOs codigoDTOs)
        {
            return new CodigoE
            {
                IdCodigo = codigoDTOs.IdCodigo,
                Codigo = codigoDTOs.Codigo,
                Correo = codigoDTOs.Correo
            };
        }
    }
}
