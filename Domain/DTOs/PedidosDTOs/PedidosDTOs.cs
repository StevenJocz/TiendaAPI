using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    #region REGISTRAR PEDIDO
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
    #endregion

    #region LISTAR PEDIDOS
    public class ListaPedidoDTOs
    {
        [Key]
        public int Id { get; set; }
        public int Orden { get; set; }
        public decimal Total { get; set; }
        public string Cliente { get; set; }
        public string EstadoPedido { get; set; }
        public string EstadoEnvio { get; set; }
        public string TipoEntrega { get; set; }
        public string Fecha { get; set; }
    }
    #endregion

    # region LISTAR PEDIDO POR ID
    public class ListarPedidoIdDTOs
    {
        public int idPedido { get; set; }
        public int orden { get; set; }
        public int idEstadoPedido { get; set; }
        public int idEstadoEnvio { get; set; }
        public string fechaRegistro { get; set; }
        public decimal subTotal { get; set; }
        public decimal valorEnvio { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
        public List<listaRegistrosPedidos> registros { get; set; }
        public usuarioPedido usuarios { get; set; }
        public envioPedido envio { get; set; }
    }

    public class listaRegistrosPedidos
    {
        public int idProducto { get; set; }
        public string foto { get; set; }
        public string nombre { get; set; }
        public string color { get; set; }
        public string talla { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal total { get; set; }
    }

    public class usuarioPedido
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }

    public class envioPedido
    {
        public int idEnvio { get; set; }
        public string tipoEntrega { get; set; }
        public string direccion { get; set; }
        public string barrio { get; set; }
        public string complemento { get; set; }
        public string destinatario { get; set; }
        public string responsable { get; set; }

    }

    #endregion
}
