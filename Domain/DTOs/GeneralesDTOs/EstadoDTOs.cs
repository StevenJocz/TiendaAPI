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
        public bool EsEstadoPedido { get; set; }

        public static EstadoDTOs CrearDTOs(EstadoE estadoE)
        {
            return new EstadoDTOs
            {
                IdEstado = estadoE.IdEstado,
                Nombre = estadoE.Nombre,
                EsEstadoPedido = estadoE.EsEstadoPedido
            };
        }

        public static EstadoE CrearE(EstadoDTOs estadoDTOs)
        {
            return new EstadoE
            {
                IdEstado = estadoDTOs.IdEstado,
                Nombre = estadoDTOs.Nombre,
                EsEstadoPedido = estadoDTOs.EsEstadoPedido
            };
        }
    }
}
