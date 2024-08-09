using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.PedidosE;

namespace TiendaUNAC.Domain.DTOs.PedidosDTOs
{
    public class PedidosDTOs
    {
        public int IdPedido { get; set; }
        public int Orden { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorEnvio { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoEntrega { get; set; }
        public DateTime FechaRegistro { get; set; }

        public static PedidosDTOs CrearDTOs(PedidosE pedidosE)
        {
            return new PedidosDTOs
            {
                IdPedido = pedidosE.IdPedido,
                Orden = pedidosE.Orden,
                IdUsuario = pedidosE.IdUsuario,
                IdEstado = pedidosE.IdEstado,
                SubTotal = pedidosE.SubTotal,
                ValorEnvio = pedidosE.ValorEnvio,
                ValorDescuento = pedidosE.ValorDescuento,
                ValorTotal = pedidosE.ValorTotal,
                TipoEntrega = pedidosE.TipoEntrega,
                FechaRegistro = pedidosE.FechaRegistro,

            };
        }

        public static PedidosE CrearE(PedidosDTOs pedidosDTOs)
        {
            return new PedidosE
            {
                IdPedido = pedidosDTOs.IdPedido,
                Orden = pedidosDTOs.Orden,
                IdUsuario = pedidosDTOs.IdUsuario,
                IdEstado = pedidosDTOs.IdEstado,
                SubTotal = pedidosDTOs.SubTotal,
                ValorEnvio = pedidosDTOs.ValorEnvio,
                ValorDescuento = pedidosDTOs.ValorDescuento,
                ValorTotal = pedidosDTOs.ValorTotal,
                TipoEntrega = pedidosDTOs.TipoEntrega,
                FechaRegistro = pedidosDTOs.FechaRegistro,

            };
        }
    }

    public class RegistrarPedido
    {
        public int IdUsuario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorEnvio { get; set; }
        public int IdCupon { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoEntrega { get; set; }
        public string Direccion { get; set; }
        public string Complemento { get; set; }
        public string Barrio { get; set; }
        public string Destinatario { get; set; }
        public string Responsable { get; set; }
        public List<PedidosRegistrosDTOs> Registros { get; set; } 
    }
}
