﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Commands
{
    public interface IGeneralesCommands
    {
        Task<RespuestaDTO> crearCupon(CuponesDTOs cuponesDTOs);
    }
    public class GeneralesCommands: IGeneralesCommands, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IGeneralesCommands> _logger;
        private readonly IConfiguration _configuration;

        public GeneralesCommands(ILogger<GeneralesCommands> logger, IConfiguration configuration)
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

        #region CREAR CUPON
        public async Task<RespuestaDTO> crearCupon(CuponesDTOs cuponesDTOs)
        {
            _logger.LogTrace("Iniciando metodo GeneralesCommands.crearCupon...");
            try
            {
                var newCupon = new CuponesDTOs
                {
                    TextoCupon = cuponesDTOs.TextoCupon,
                    ValorCupon = cuponesDTOs.ValorCupon,
                    FechaLimite = cuponesDTOs.FechaLimite,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = cuponesDTOs.Activo,
                    IdUsuarioCreador = cuponesDTOs.IdUsuarioCreador
                };

                var cupon = CuponesDTOs.CrearE(newCupon);
                await _context.CuponesEs.AddAsync(cupon);
                await _context.SaveChangesAsync();

                if (cupon.IdCupon != 0)
                {
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el cupon exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar el cupon! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesCommands.crearCupon...");
                throw;
            }
        }

        #endregion
    }
}