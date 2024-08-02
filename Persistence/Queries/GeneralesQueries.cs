using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Queries
{
    public interface IGeneralesQueries
    {
        Task<List<tiposDocumentosDTOs>> TiposDocumentos();
        Task<List<generosDTOs>> generos();
        Task<List<ubicacionDTOs>> ubicacion(int Accion, int Parametro);
        Task<List<CuponesDTOs>> cupones();
        Task<List<CuponesDTOs>> consultarCupon(string cupon, int idUsuario);
        Task<List<CuponesDTOs>> cuponesId(int IdCupon);
        Task<List<MontoEnvioDTOs>> listarMonto(int IdMonto);
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

        #region CUPONES
        public async Task<List<CuponesDTOs>> cupones()
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.cupones...");
            try
            {
                var cupones = await _context.CuponesEs.Where(x => x.Activo == true || x.Activo == false).ToListAsync();

                var ListCupones = new List<CuponesDTOs>();

                foreach (var item in cupones)
                {
                    var list = new CuponesDTOs
                    {
                        IdCupon = item.IdCupon,
                        TextoCupon = item.TextoCupon,
                        ValorCupon = item.ValorCupon,
                        FechaLimite = item.FechaLimite,
                        FechaCreacion = item.FechaCreacion,
                        IdUsuarioCreador = item.IdUsuarioCreador,
                        Activo = item.Activo
                    };

                    ListCupones.Add(list);
                }

                return ListCupones;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.cupones...");
                throw;
            }
        }
        #endregion 


        #region CONSULTAR CUPONE
        public async Task<List<CuponesDTOs>> consultarCupon(string cupon, int idUsuario)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.consultarCupon...");
            try
            {
                var cupones = await _context.CuponesEs.FromSqlInterpolated($"EXEC Verificar_Cupo_Usuario @IdUsuario={idUsuario}, @Cupon={cupon}").ToListAsync();

                var ListCupones = new List<CuponesDTOs>();

                foreach (var item in cupones)
                {
                    var list = new CuponesDTOs
                    {
                        IdCupon = item.IdCupon,
                        TextoCupon = item.TextoCupon,
                        ValorCupon = item.ValorCupon,
                        FechaLimite = item.FechaLimite,
                        FechaCreacion = item.FechaCreacion,
                        IdUsuarioCreador = item.IdUsuarioCreador,
                        Activo = item.Activo
                    };

                    ListCupones.Add(list);
                }

                return ListCupones;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.consultarCupon...");
                throw;
            }
        }
        #endregion

        #region CUPONES POR ID
        public async Task<List<CuponesDTOs>> cuponesId(int IdCupon)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.cuponesId...");
            try
            {
                var cupones = await _context.CuponesEs.AsNoTracking().FirstOrDefaultAsync(x => x.IdCupon == IdCupon);

                var ListCupones = new List<CuponesDTOs>();

                var list = new CuponesDTOs
                {
                    IdCupon = cupones.IdCupon,
                    TextoCupon = cupones.TextoCupon,
                    ValorCupon = cupones.ValorCupon,
                    FechaLimite = cupones.FechaLimite,
                    FechaCreacion = cupones.FechaCreacion,
                    IdUsuarioCreador = cupones.IdUsuarioCreador,
                    Activo = cupones.Activo
                };

                ListCupones.Add(list);

                return ListCupones;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.cuponesId...");
                throw;
            }
        }
        #endregion


        #region MONTO
        public async Task<List<MontoEnvioDTOs>> listarMonto(int IdMonto)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.listoMonto...");
            try
            {
                var monto = await _context.MontoEnvioEs.Where(x => x.IdMonto == IdMonto).ToListAsync();

                var ListMonto = new List<MontoEnvioDTOs>();

                foreach (var item in monto)
                {
                    var list = new MontoEnvioDTOs
                    {
                        IdMonto = item.IdMonto,
                        ValorMonto = item.ValorMonto,
                        FechaActualizacion = item.FechaActualizacion,
                        IdUsuarioActualizador = item.IdUsuarioActualizador,
                    };

                    ListMonto.Add(list);
                }
                

                return ListMonto;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.listoMonto...");
                throw;
            }
        }
        #endregion

    }
}
