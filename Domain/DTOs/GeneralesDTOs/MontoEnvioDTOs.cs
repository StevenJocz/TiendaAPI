using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.GeneralesE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class MontoEnvioDTOs
    {
        public int IdMonto { get; set; }
        public decimal ValorMonto { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int IdUsuarioActualizador { get; set; }

        public static MontoEnvioDTOs CrearDTOs(MontoEnvioE montoEnvioE)
        {
            return new MontoEnvioDTOs
            {
                IdMonto = montoEnvioE.IdMonto,
                ValorMonto = montoEnvioE.ValorMonto,
                FechaActualizacion = montoEnvioE.FechaActualizacion,
                IdUsuarioActualizador = montoEnvioE.IdUsuarioActualizador,
            };
        }

        public static MontoEnvioE CrearE(MontoEnvioDTOs montoEnvioDTOs)
        {
            return new MontoEnvioE
            {
                IdMonto = montoEnvioDTOs.IdMonto,
                ValorMonto = montoEnvioDTOs.ValorMonto,
                FechaActualizacion = montoEnvioDTOs.FechaActualizacion,
                IdUsuarioActualizador = montoEnvioDTOs.IdUsuarioActualizador,
            };
        }
    }
}
