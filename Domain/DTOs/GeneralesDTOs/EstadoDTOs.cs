using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.GeneralesE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class EstadoDTOs
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPedido { get; set; }
        public bool EsEnvio { get; set; }

        public static EstadoDTOs CrearDTOs(EstadoE estadoE)
        {
            return new EstadoDTOs
            {
                IdEstado = estadoE.IdEstado,
                Nombre = estadoE.Nombre,
                Descripcion = estadoE.Descripcion,
                EsPedido = estadoE.EsPedido,
                EsEnvio = estadoE.EsEnvio,
            };
        }

        public static EstadoE CrearE(EstadoDTOs estadoDTOs)
        {
            return new EstadoE
            {
                IdEstado = estadoDTOs.IdEstado,
                Nombre = estadoDTOs.Nombre,
                Descripcion = estadoDTOs.Descripcion,
                EsPedido = estadoDTOs.EsPedido,
                EsEnvio = estadoDTOs.EsEnvio,
            };
        }
    }

    public class ObjetoEstados
    {
        public int IdPedido { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdEstadoEnvio { get; set; }
    }
}
