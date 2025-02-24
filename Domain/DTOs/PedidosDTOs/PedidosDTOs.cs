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
        public int Referencia { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorEnvio { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPago { get; set; }
        public string TipoEntrega { get; set; }
        public DateTime FechaRegistro { get; set; }

        public static PedidosDTOs CrearDTOs(PedidosE pedidosE)
        {
            return new PedidosDTOs
            {
                IdPedido = pedidosE.IdPedido,
                Orden = pedidosE.Orden,
                Referencia = pedidosE.Referencia,
                IdUsuario = pedidosE.IdUsuario,
                IdEstado = pedidosE.IdEstado,
                SubTotal = pedidosE.SubTotal,
                ValorEnvio = pedidosE.ValorEnvio,
                ValorDescuento = pedidosE.ValorDescuento,
                ValorTotal = pedidosE.ValorTotal,
                FormaPago = pedidosE.FormaPago,
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
                Referencia = pedidosDTOs.Referencia,
                IdUsuario = pedidosDTOs.IdUsuario,
                IdEstado = pedidosDTOs.IdEstado,
                SubTotal = pedidosDTOs.SubTotal,
                ValorEnvio = pedidosDTOs.ValorEnvio,
                ValorDescuento = pedidosDTOs.ValorDescuento,
                ValorTotal = pedidosDTOs.ValorTotal,
                FormaPago = pedidosDTOs.FormaPago,
                TipoEntrega = pedidosDTOs.TipoEntrega,
                FechaRegistro = pedidosDTOs.FechaRegistro,

            };
        }
    }

    #region REGISTRAR PEDIDO
    public class RegistrarPedido
    {
        public DatosCliente Cliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorEnvio { get; set; }
        public int IdCupon { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoEntrega { get; set; }
        
        public List<PedidosRegistrosDTOs> Registros { get; set; } 
    }


    public class DatosCliente
    {
        public string Correo { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Celular { get; set; } 
        public int Genero { get; set; }
        public int Pais { get; set; }
        public int Departamento { get; set; }
        public int Ciudad { get; set; }
        public string TipoVia { get; set; }
        public string Numero1 { get; set; } 
        public string Numero2 { get; set; } 
        public string Numero3 { get; set; }
        public string Complementario { get; set; }
        public string Barrio { get; set; }
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
        public string? estadoPago { get; set; }
        public string? formaPago { get; set; }
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
