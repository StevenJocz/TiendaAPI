﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IGeneralesQueries
    {
        Task<List<tiposDocumentosDTOs>> TiposDocumentos();
        Task<List<generosDTOs>> generos();
        Task<List<ubicacionDTOs>> ubicacion(int Accion, int Parametro);
    }

    public class GeneralesQueries: IGeneralesQueries, IDisposable
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<IGeneralesQueries> _logger;
        private readonly IConfiguration _configuration;

        public GeneralesQueries(ILogger<GeneralesQueries> logger, IConfiguration configuration)
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


        #region TIPO DOCUMENTOS
        public async Task<List<tiposDocumentosDTOs>> TiposDocumentos()
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.TiposDocumentos...");
            try
            {
                var documentos = await _context.tipoDocumentosEs.FromSqlInterpolated($"EXEC TipoDocumentos").ToListAsync();

                var listDocumentos = new List<tiposDocumentosDTOs>();
                foreach (var item in documentos)
                {
                    var list = new tiposDocumentosDTOs
                    {
                        IdDocumento = item.IdDocumento,
                        Documento = item.Documento
                    };
                    listDocumentos.Add(list);
                }
                return listDocumentos;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.TiposDocumentos...");
                throw;
            }
        }
        #endregion

        #region GENEROS
        public async Task<List<generosDTOs>> generos()
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.generos...");
            try
            {
                var generos = await _context.generosEs.FromSqlInterpolated($"EXEC Genero").ToListAsync();

                var listGeneros = new List<generosDTOs>();
                foreach (var item in generos)
                {
                    var list = new generosDTOs
                    {
                        IdGenero = item.IdGenero,
                        Genero = item.Genero
                    };
                    listGeneros.Add(list);
                }
                return listGeneros;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.generos...");
                throw;
            }
        }
        #endregion

        #region UBICACION "Paises,Departamentos, Municipios"
        public async Task<List<ubicacionDTOs>> ubicacion(int Accion, int Parametro)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.ubicacion...");
            try
            {
                var ubicacion = await _context.ubicacionEs.FromSqlInterpolated($"EXEC Ubicacion @Accion={Accion}, @Parametro={Parametro}").ToListAsync();

                var listUbicacion = new List<ubicacionDTOs>();
                foreach (var item in ubicacion)
                {
                    var list = new ubicacionDTOs
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                    };
                    listUbicacion.Add(list);
                }
                return listUbicacion;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.ubicacion...");
                throw;
            }
        }
        #endregion


    }
}
