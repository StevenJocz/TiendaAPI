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
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.PedidosE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IPedidoCommands
    {
        Task<RespuestaDTO> registrarPedido(RegistrarPedido registrarPedido);
    }
    public class PedidoCommands: IPedidoCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IPedidoCommands> _logger;
        private readonly IConfiguration _configuration;

        public PedidoCommands(ILogger<PedidoCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
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
                var pedido = new PedidosDTOs
                {
                    IdUsuario = registrarPedido.IdUsuario,
                    IdEstado = 1,
                    SubTotal = registrarPedido.SubTotal,
                    ValorEnvio = registrarPedido.ValorEnvio,
                    ValorDescuento = registrarPedido.ValorDescuento,
                    ValorTotal = registrarPedido.ValorTotal,
                    TipoEntrega = registrarPedido.TipoEntrega,
                    FechaRegistro = DateTime.UtcNow,
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
                            Valor = item.Valor
                        };

                        var registroE = PedidosRegistrosDTOs.CrearE(registros);
                        await _context.PedidosRegistrosEs.AddAsync(registroE);
                        await _context.SaveChangesAsync();
                    }

                    var envio = new EnvioDTOs
                    {
                        IdPedido = pedidoE.IdPedido,
                        IdEstado = 1,
                        Direccion = registrarPedido.Direccion,
                        Complemento = registrarPedido.Direccion,
                        Barrio = registrarPedido.Barrio,
                        Destinatario = registrarPedido.Destinatario,
                        Responsable = registrarPedido.Responsable
                    };

                    var envioE = EnvioDTOs.CrearE(envio);
                    await _context.EnvioEs.AddAsync(envioE);
                    await _context.SaveChangesAsync();

                    var cuponUsuario = new CuponUsuarioDTOs
                    {
                        IdCupon = registrarPedido.IdCupon,
                        IdUsuario = registrarPedido.IdUsuario,
                        FechaRegistro = DateTime.UtcNow,
                    };

                    var cuponUsuarioE = CuponUsuarioDTOs.CrearE(cuponUsuario);
                    await _context.CuponUsuarioEs.AddAsync(cuponUsuarioE);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el pedido exitosamente!",
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
    }
}
