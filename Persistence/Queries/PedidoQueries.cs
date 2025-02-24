using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.UsuariosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IPedidoQueries {
        Task<List<ListaPedidoDTOs>> ListarPedidos(int accion, int idUsuario);
        Task<List<ListarPedidoIdDTOs>> ListarPedidosId(int idPedido);
        Task<List<EstadoPagoDTOs>> ListarPedidoOrden(int referencia);
    }

    public class PedidoQueries: IPedidoQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly TiendaUNACContext _contextSion = null;
        private readonly ILogger<IPedidoQueries> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGenerarSessionPasarelaPago _generarSessionPasarelaPago;


        public PedidoQueries(ILogger<PedidoQueries> logger, IConfiguration configuration, IGenerarSessionPasarelaPago generarSessionPasarelaPago)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);

            string? connectionStringDos = _configuration.GetConnectionString("ConnectionSionWeb");
            _contextSion = new TiendaUNACContext(connectionStringDos);

            _generarSessionPasarelaPago = generarSessionPasarelaPago;
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion


        #region LISTA TODOS LOS PEDIDOS
        public async Task<List<ListaPedidoDTOs>> ListarPedidos(int accion, int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo PedidoQueries.Pedidos...");
            try
            {
                var pedidos = await _context.ListaPedidos.FromSqlInterpolated($"EXEC Listar_Pedidos @Accion={accion}, @IdUsuario={idUsuario}").ToListAsync();

                var listPedidos = new List<ListaPedidoDTOs>();
                foreach (var item in pedidos)
                {
                    var list = new ListaPedidoDTOs
                    {
                        Id = item.Id,
                        Orden = item.Orden,
                        Total = item.Total,
                        Cliente = item.Cliente,
                        EstadoPedido = item.EstadoPedido,
                        EstadoEnvio = item.EstadoEnvio,
                        TipoEntrega = item.TipoEntrega,
                        Fecha = item.Fecha,

                    };
                    listPedidos.Add(list);
                }
                return listPedidos;

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo PedidoQueries.Pedidos...");
                throw;
            }
        }
        #endregion

        #region LISTA  PEDIDOS POR ID
        public async Task<List<ListarPedidoIdDTOs>> ListarPedidosId(int idPedido)
        {
            _logger.LogTrace("Iniciando metodo PedidoQueries.ListarPedidosId...");
            try
            {
                var pedido = await _context.PedidosEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdPedido == idPedido);

                var listaPedido = new List<ListarPedidoIdDTOs>();

                if (pedido.IdPedido > 0)
                {
                    var registroPedidos = await _context.PedidosRegistrosEs.Where(x => x.IdPedido == idPedido).ToListAsync();
                    var usuario = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdUsuario == pedido.IdUsuario);
                    var envio = await  _context.EnvioEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdPedido == idPedido);

                    var listaRegistros = new List<listaRegistrosPedidos>();

                    foreach (var item in registroPedidos)
                    {
                        var resgistros = new listaRegistrosPedidos
                        {
                            idProducto = item.IdProducto,
                            foto = item.imagen,
                            nombre = item.Nombre,
                            color = item.Color,
                            talla = item.Talla,
                            cantidad = item.Cantidad,
                            precio = item.ValorUnidad,
                            total = item.ValorTotal,
                        };

                        listaRegistros.Add(resgistros);
                    }

                    var usuarioPedido = new usuarioPedido
                    {
                        idUsuario = usuario.IdUsuario,
                        nombre = usuario.Nombre + " " + usuario.Apellido,
                        documento = usuario.Documento,
                        correo = usuario.Correo,
                        telefono = usuario.Celular,

                    };

                    var envioPedido = new envioPedido
                    {
                        idEnvio = envio.IdEnvio,
                        tipoEntrega = pedido.TipoEntrega,
                        direccion = envio.Direccion,
                        barrio = envio.Barrio,
                        complemento = envio.Complemento,
                        destinatario = envio.Destinatario,
                        responsable = envio.Responsable,

                    };


                    var lista = new ListarPedidoIdDTOs
                    {
                        idPedido = pedido.IdPedido,
                        orden = pedido.Orden,
                        idEstadoPedido = pedido.IdEstado,
                        idEstadoEnvio = envio.IdEstado,
                        fechaRegistro = pedido.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss"),
                        subTotal = pedido.SubTotal,
                        valorEnvio = pedido.ValorEnvio,
                        descuento = pedido.ValorDescuento,
                        total = pedido.ValorTotal,
                        registros = listaRegistros,
                        usuarios = usuarioPedido,
                        envio = envioPedido

                    };

                    listaPedido.Add(lista);
                }

                return listaPedido;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo PedidoQueries.ListarPedidosId...");
                throw;
            }

        }
        #endregion


        #region LISTA  PEDIDOS POR ORDEN
        public async Task<List<EstadoPagoDTOs>> ListarPedidoOrden(int referencia)
        {
            _logger.LogTrace("Iniciando metodo PedidoQueries.ListarPedidosId...");
            try
            {
                var pedido = await _context.PedidosEs.AsNoTracking().FirstOrDefaultAsync(x => x.Referencia == referencia);

                var pago = new List<EstadoPagoDTOs>();

                if (pedido.IdPedido > 0)
                {
                    var usuario = await _context.UsuariosEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdUsuario == pedido.IdUsuario);

                    bool estadoPasarelaPago = await _generarSessionPasarelaPago.EstadoSesion(1, pedido.Referencia.ToString());

                    var EstadoSion = await _contextSion.EstadoPagoEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdReferencia == referencia);

                    if (estadoPasarelaPago)
                    {
                        pedido.IdEstado = EstadoSion != null ? EstadoSion.Estado switch
                        {
                            "APPROVED" => 3,
                            "PENDING" => 1,
                            _ => 8
                        } : 8;

                        pedido.FormaPago = EstadoSion.FormaPago;

                        _context.PedidosEs.Update(pedido);
                        await _context.SaveChangesAsync();
                    }

                    var estadoPagoTexto = EstadoSion != null ? EstadoSion.Estado switch
                    {
                        "APPROVED" => "Aprobada",
                        "PENDING" => "Pendiente",
                        "REJECTED" => "Cancelada",
                        "FAILED" => "Fallida",
                        "PARTIAL_APPROVED" => "Aprobada Parcialmente",
                        "EXPIRED" => "Expirada",
                        "CANCELLED" => "Cancelada",
                        "REVERSED" => "Reversada",
                        _ => "Desconocida"
                    } : "Desconocida";


                    var lista = new EstadoPagoDTOs
                    {
                        IdEstado = EstadoSion.IdEstado,
                        Estado = estadoPagoTexto,
                        RequestId = EstadoSion.RequestId,
                        IdReferencia = EstadoSion.IdReferencia,
                        Fecha = EstadoSion.Fecha,
                        Valor = EstadoSion.Valor,
                        Moneda = EstadoSion.Moneda,
                        FormaPago = EstadoSion.FormaPago,
                        Razon = EstadoSion.Razon,
                        Mensaje = EstadoSion.Mensaje,
                        Correo = usuario.Correo,
                    };

                    pago.Add(lista);
                }

                return pago;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo PedidoQueries.ListarPedidosId...");
                throw;
            }

        }
        #endregion

    }
}
