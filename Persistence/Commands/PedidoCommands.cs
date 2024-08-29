using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.PedidosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;
using static System.Net.Mime.MediaTypeNames;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IPedidoCommands
    {
        Task<RespuestaDTO> registrarPedido(RegistrarPedido registrarPedido);
        Task<RespuestaDTO> actualizarEstado(ObjetoEstados objetoEstados);
    }
    public class PedidoCommands: IPedidoCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IPedidoCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly INotificacionCommads _notificacion;

        public PedidoCommands(ILogger<PedidoCommands> logger, IConfiguration configuration, INotificacionCommads notificacion)
        {
            _logger = logger;
            _notificacion = notificacion;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("ConnectionTienda");
            _context = new TiendaUNACContext(connectionString);
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

        #region REGISTRO PEDIDOS
        public async Task<RespuestaDTO> registrarPedido(RegistrarPedido registrarPedido)
        {
            _logger.LogTrace("Iniciando metodo PedidoCommands.registrarPedido...");
            try
            {
                var ultimaOrden = await _context.PedidosEs.OrderByDescending(x => x.IdPedido).FirstOrDefaultAsync();

                var pedido = new PedidosDTOs
                {
                    IdUsuario = registrarPedido.IdUsuario,
                    Orden = ultimaOrden == null ? 1: ultimaOrden.Orden + 1,
                    IdEstado = 1,
                    SubTotal = registrarPedido.SubTotal,
                    ValorEnvio = registrarPedido.ValorEnvio,
                    ValorDescuento = registrarPedido.ValorDescuento,
                    ValorTotal = registrarPedido.ValorTotal,
                    TipoEntrega = registrarPedido.TipoEntrega,
                    FechaRegistro = (DateTime.UtcNow).ToLocalTime(),
                };

                var pedidoE = PedidosDTOs.CrearE(pedido);
                await _context.PedidosEs.AddAsync(pedidoE);
                await _context.SaveChangesAsync();

                if (pedidoE.IdPedido != 0)
                {
                    foreach (var item in registrarPedido.Registros)
                    {
                        var registros = new PedidosRegistrosDTOs
                        {
                            IdPedido = pedidoE.IdPedido,
                            IdProducto = item.IdProducto,
                            IdInventario = item.IdInventario,
                            Cantidad = item.Cantidad,
                            Nombre = item.Nombre,
                            Color = item.Color,
                            Talla = item.Talla,
                            ValorUnidad = item.ValorUnidad,
                            ValorTotal = item.ValorUnidad * item.Cantidad,
                            imagen = item.imagen
                        };

                        var registroE = PedidosRegistrosDTOs.CrearE(registros);
                        await _context.PedidosRegistrosEs.AddAsync(registroE);
                        await _context.SaveChangesAsync();
                    }

                    var envio = new EnvioDTOs
                    {
                        IdPedido = pedidoE.IdPedido,
                        IdEstado = 2,
                        Direccion = registrarPedido.Direccion,
                        Complemento = registrarPedido.Complemento,
                        Barrio = registrarPedido.Barrio,
                        Destinatario = registrarPedido.Destinatario,
                        Responsable = registrarPedido.Responsable
                    };

                    var envioE = EnvioDTOs.CrearE(envio);
                    await _context.EnvioEs.AddAsync(envioE);
                    await _context.SaveChangesAsync();

                    if (registrarPedido.IdCupon != 0 )
                    {
                        var cuponUsuario = new CuponUsuarioDTOs
                        {
                            IdCupon = registrarPedido.IdCupon,
                            IdUsuario = registrarPedido.IdUsuario,
                            FechaRegistro = DateTime.UtcNow,
                        };

                        var cuponUsuarioE = CuponUsuarioDTOs.CrearE(cuponUsuario);
                        await _context.CuponUsuarioEs.AddAsync(cuponUsuarioE);
                        await _context.SaveChangesAsync();
                    }

                    var notificacion = new NotificacionCrear
                    {
                        IdTipoNotificacion = 1,
                        DeIdUsuario = registrarPedido.IdUsuario,
                        ParaIdUsuario = 2,
                        IdTipoRelacion = 1,
                        IdRelacion = pedidoE.IdPedido
                    };

                    await _notificacion.agregarNotificacion(notificacion);

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el pedido exitosamente!",
                        orden = ultimaOrden == null ? 1 : ultimaOrden.Orden + 1,
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar el pedido! Por favor, inténtalo de nuevo más tarde.",
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo PedidoCommands.registrarPedido...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR ESTADO
        public async Task<RespuestaDTO> actualizarEstado(ObjetoEstados objetoEstados)
        {
            _logger.LogTrace("Iniciando metodo PedidoCommands.actualizarEstado...");
            try
            {
                var pedido = await _context.PedidosEs.FirstOrDefaultAsync(x => x.IdPedido == objetoEstados.IdPedido);
                var envio = await _context.EnvioEs.FirstOrDefaultAsync(x => x.IdPedido == objetoEstados.IdPedido);

                if (pedido != null || envio != null)
                {
                    if (objetoEstados.IdEstadoPedido != 0)
                    {
                        pedido.IdEstado = objetoEstados.IdEstadoPedido;
                        _context.PedidosEs.Update(pedido);

                        var notificacionPedido = new NotificacionCrear
                        {
                            IdTipoNotificacion = objetoEstados.IdEstadoPedido,
                            DeIdUsuario = 2,
                            ParaIdUsuario = pedido.IdUsuario,
                            IdTipoRelacion = 2,
                            IdRelacion = pedido.IdPedido
                        };

                        await _notificacion.agregarNotificacion(notificacionPedido);
                    }

                    if (objetoEstados.IdEstadoEnvio != 0)
                    {
                        envio.IdEstado = objetoEstados.IdEstadoEnvio;
                        _context.EnvioEs.Update(envio);

                        var notificacionEnvio = new NotificacionCrear
                        {
                            IdTipoNotificacion = objetoEstados.IdEstadoEnvio,
                            DeIdUsuario = 2,
                            ParaIdUsuario = pedido.IdUsuario,
                            IdTipoRelacion = 2,
                            IdRelacion = envio.IdEnvio
                        };

                        await _notificacion.agregarNotificacion(notificacionEnvio);
                    }

                   
                    await _context.SaveChangesAsync();
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar pedido!. Por favor, verifica los datos.",

                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo PedidoCommands.actualizarEstado...");
                throw;
            }
        }
        #endregion

    }
}
