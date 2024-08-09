using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.GeneralesE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class CuponUsuarioDTOs
    {
        public int IdCuponesUsuario { get; set; }
        public int IdCupon { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }

        public static CuponUsuarioDTOs CrearDTOs(CuponUsuarioE cuponUsuarioE)
        {
            return new CuponUsuarioDTOs
            {
                IdCuponesUsuario = cuponUsuarioE.IdCuponesUsuario,
                IdCupon = cuponUsuarioE.IdCupon,
                IdUsuario = cuponUsuarioE.IdUsuario,
                FechaRegistro = cuponUsuarioE.FechaRegistro,

            };
        }

        public static CuponUsuarioE CrearE(CuponUsuarioDTOs cuponUsuarioDTOs)
        {
            return new CuponUsuarioE
            {
                IdCuponesUsuario = cuponUsuarioDTOs.IdCuponesUsuario,
                IdCupon = cuponUsuarioDTOs.IdCupon,
                IdUsuario = cuponUsuarioDTOs.IdUsuario,
                FechaRegistro = cuponUsuarioDTOs.FechaRegistro,

            };
        }
    }
}
