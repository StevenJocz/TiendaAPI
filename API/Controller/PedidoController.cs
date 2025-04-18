﻿using Microsoft.AspNetCore.Mvc;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaUNAC.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
       
        private readonly IPedidoCommands _pedidoCommands;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(IPedidoCommands pedidoCommands, IPedidoQueries pedidoQueries, ILogger<PedidoController> logger)
        {
            _pedidoCommands = pedidoCommands;
            _pedidoQueries = pedidoQueries;
            _logger = logger;
        }

        #region POST
        [HttpPost("Post_Registrar_Pedido")]
        public async Task<IActionResult> registrarPedido([FromBody] RegistrarPedido registrarPedido)
        {
            try
            {
                _logger.LogInformation("Iniciando PedidoController.registrarPedido...");
                var respuesta = await _pedidoCommands.registrarPedido(registrarPedido);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.registrarPedido...");
                throw;
            }
        }
        #endregion

        #region GET
        [HttpGet("Get_Pedidos")]
        public async Task<IActionResult> ListarPedidos(int accion, int idUsuario)
        {
            _logger.LogInformation("Iniciando PedidoController.ListarPedidos...");
            try
            {
                var respuesta = await _pedidoQueries.ListarPedidos(accion, idUsuario);

                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.ListarPedidos");
                throw;
            }
        }

        [HttpGet("Get_Id_Pedidos")]
        public async Task<IActionResult> ListarPedidosId(int idPedido)
        {
            _logger.LogInformation("Iniciando PedidoController.ListarPedidosId...");
            try
            {
                var respuesta = await _pedidoQueries.ListarPedidosId(idPedido);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.ListarPedidosId");
                throw;
            }
        }

        [HttpGet("Get_Orden_Pedidos")]
        public async Task<IActionResult> ListarPedidoOrden(int referencia)
        {
            _logger.LogInformation("Iniciando PedidoController.ListarPedidoOrden...");
            try
            {
                var respuesta = await _pedidoQueries.ListarPedidoOrden(referencia);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.ListarPedidoOrden");
                throw;
            }
        }
        #endregion

        #region PUT
        [HttpPut("Put_Actualizar_Estado")]
        public async Task<IActionResult> actualizarEstado([FromBody] ObjetoEstados objetoEstados)
        {
            try
            {
                _logger.LogInformation("Iniciando PedidoController.actualizarEstado...");
                var respuesta = await _pedidoCommands.actualizarEstado(objetoEstados);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar PedidoController.actualizarEstado...");
                throw;
            }
        }
        #endregion
    }
}
