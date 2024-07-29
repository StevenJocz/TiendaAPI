using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class CuponesDTOs
    {
        public int IdCupon { get; set; }
        public string TextoCupon { get; set; }
        public decimal ValorCupon { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreador { get; set; }
        public bool Activo { get; set; }

        public static CuponesDTOs CrearDTOs(CuponesE cuponesE)
        {
            return new CuponesDTOs
            {
                IdCupon = cuponesE.IdCupon,
                TextoCupon = cuponesE.TextoCupon,
                ValorCupon = cuponesE.ValorCupon,
                FechaLimite = cuponesE.FechaLimite,
                FechaCreacion = cuponesE.FechaCreacion,
                IdUsuarioCreador = cuponesE.IdUsuarioCreador,
                Activo = cuponesE.Activo
            };
        }

        public static CuponesE CrearE(CuponesDTOs cuponesDTOs)
        {
            return new CuponesE
            {
                IdCupon = cuponesDTOs.IdCupon,
                TextoCupon = cuponesDTOs.TextoCupon,
                ValorCupon = cuponesDTOs.ValorCupon,
                FechaLimite = cuponesDTOs.FechaLimite,
                FechaCreacion = cuponesDTOs.FechaCreacion,
                IdUsuarioCreador = cuponesDTOs.IdUsuarioCreador,
                Activo = cuponesDTOs.Activo
            };
        }
    }
}
