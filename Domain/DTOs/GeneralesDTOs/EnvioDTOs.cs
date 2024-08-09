using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.GeneralesE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class EnvioDTOs
    {
        public int IdEnvio { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado { get; set; }
        public string Direccion { get; set; }
        public string Complemento { get; set; }
        public string Barrio { get; set; }
        public string Destinatario { get; set; }
        public string Responsable { get; set; }

        public static EnvioDTOs CrearDTOs(EnvioE envioE)
        {
            return new EnvioDTOs
            {
                IdEnvio = envioE.IdEnvio,
                IdPedido = envioE.IdPedido,
                IdEstado = envioE.IdEstado,
                Direccion  = envioE.Direccion,
                Complemento = envioE.Complemento,
                Barrio = envioE.Barrio,
                Destinatario = envioE.Destinatario,
                Responsable = envioE.Responsable

            };
        }

        public static EnvioE CrearE(EnvioDTOs envioDTOs)
        {
            return new EnvioE
            {
                IdEnvio = envioDTOs.IdEnvio,
                IdPedido = envioDTOs.IdPedido,
                IdEstado = envioDTOs.IdEstado,
                Direccion = envioDTOs.Direccion,
                Complemento = envioDTOs.Complemento,
                Barrio = envioDTOs.Barrio,
                Destinatario = envioDTOs.Destinatario,
                Responsable = envioDTOs.Responsable

            };
        }
    }
}
