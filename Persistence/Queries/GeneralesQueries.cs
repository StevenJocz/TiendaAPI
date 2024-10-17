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
using TiendaUNAC.Domain.Entities.PedidosE;
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
        Task<List<EstadoDTOs>> listarEstados(int Accion);
        Task<List<Card>> listarItemDashboard();
        Task<Dictionary<string, object>> ObtenerVentasPorMesYAnio();
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
                    var cantidadCupones = await _context.CuponUsuarioEs.Where(x => x.IdCupon ==  item.IdCupon).ToListAsync();
                    var list = new CuponesDTOs
                    {
                        IdCupon = item.IdCupon,
                        TextoCupon = item.TextoCupon,
                        ValorCupon = item.ValorCupon,
                        FechaLimite = item.FechaLimite,
                        FechaCreacion = item.FechaCreacion,
                        IdUsuarioCreador = item.IdUsuarioCreador,
                        Activo = item.Activo,
                        VecesUtilizado = cantidadCupones.Count
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

        #region ESTADOS
        public async Task<List<EstadoDTOs>> listarEstados(int Accion)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.listarEstados...");
            try
            {
                var expresion = (Expression<Func<EstadoE, bool>>)null;
                if (Accion == 1)
                {
                    expresion = expresion = x => x.EsPedido == true && x.EsAdmin == false || x.IdEstado == 1;
                } 
                else
                {
                    expresion = expresion = x => x.EsEnvio == true;
                }

                var estados = await _context.EstadoEs.Where(expresion).ToListAsync();

                var ListaEstados = new List<EstadoDTOs>();

                foreach (var item in estados)
                {
                    var list = new EstadoDTOs
                    {
                        IdEstado = item.IdEstado,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        EsPedido = item.EsPedido,
                        EsEnvio = item.EsEnvio,
                    };

                    ListaEstados.Add(list);
                }


                return ListaEstados;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.listarEstados...");
                throw;
            }
        }
        #endregion

        #region CARD
        public async Task<List<Card>> listarItemDashboard()
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.listarItemDashboard...");
            try
            {
                var ListaCard = new List<Card>();

                // Productos
                var productos = await _context.ProductoEs.ToListAsync();
                ListaCard.Add(CrearCard("Productos", productos.Count, productos, p => p.FechaCreado, 1));

                // Usuarios
                var usuarios = await _context.UsuariosEs.ToListAsync();
                ListaCard.Add(CrearCard("Usuarios", usuarios.Count, usuarios, u => u.FechaRegistro, 2));

                // Pedidos
                var pedidos = await _context.PedidosEs.ToListAsync();
                ListaCard.Add(CrearCard("Pedidos", pedidos.Count, pedidos, p => p.FechaRegistro, 3));

                ListaCard.Add(CrearCardPedidos("Ventas", pedidos));


                return ListaCard;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo GeneralesQueries.listarItemDashboard...");
                throw;
            }
        }

        private Card CrearCard<T>(string titulo, int total, List<T> lista, Func<T, DateTime> selectorFecha, int tipo)
        {
            var mesActual = DateTime.Now.Month;
            var mesAnterior = DateTime.Now.AddMonths(-1).Month;

            var itemsMesActual = lista.Where(i => selectorFecha(i).Month == mesActual).ToList();
            var itemsMesAnterior = lista.Where(i => selectorFecha(i).Month == mesAnterior).ToList();

            var numeroNuevos = itemsMesActual.Count;
            var numeroAnteriores = itemsMesAnterior.Count;

            double porcentaje;

            if (numeroAnteriores == 0)
            {
                porcentaje = numeroNuevos > 0 ? 100 : 0;
            }
            else
            {
                porcentaje = ((double)(numeroNuevos - numeroAnteriores) / numeroAnteriores) * 100;
            }

            return new Card
            {
                Titulo = titulo,
                Porcentaje = porcentaje,
                Tipo = tipo,
                NumeroTotal = total,
                NumeroNuevos = numeroNuevos,
                Clase = 1
            };
        }

        private Card CrearCardPedidos(string titulo, List<PedidosE> pedidos)
        {
            var mesActual = DateTime.Now.Month;

            // Filtrar pedidos con estado 2
            var pedidosEstadoDos = pedidos.Where(p => p.IdEstado == 3).ToList();

            // Filtrar los pedidos con estado 2 del mes actual
            var pedidosMesActual = pedidosEstadoDos.Where(p => p.FechaRegistro.Month == mesActual).ToList();

            int numeroNuevos = pedidosMesActual.Count;
            decimal sumaValorTotalMesActual = pedidosMesActual.Sum(p => p.ValorTotal);
            decimal sumaValorTotalGeneral = pedidosEstadoDos.Sum(p => p.ValorTotal);

            return new Card
            {
                Titulo = titulo,
                Porcentaje = 0,  
                Tipo = 4, 
                NumeroTotal = sumaValorTotalGeneral,
                NumeroNuevos = sumaValorTotalMesActual,
                Clase = 1
            };
        }
        

        public async Task<Dictionary<string, object>> ObtenerVentasPorMesYAnio()
        {
            _logger.LogTrace("Iniciando método para obtener ventas por mes y año...");

            try
            {
                var ventasPorMesYAnio = new Dictionary<string, object>();

                // Obtener el año actual y el año anterior
                int añoActual = DateTime.Now.Year;
                int añoAnterior = añoActual - 1;

                var pData = new List<decimal>(); // Datos para el año anterior
                var uData = new List<decimal>(); // Datos para el año actual

                // Lista de meses
                var months = new List<string>
                {
                    "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                    "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
                };

                // Iterar por cada mes del año
                for (int i = 1; i <= 12; i++)
                {
                    // Filtrar los pedidos por mes y año
                    var ventasAñoAnterior = await _context.PedidosEs
                        .Where(p => p.FechaRegistro.Year == añoAnterior && p.FechaRegistro.Month == i)
                        .SumAsync(p => p.ValorTotal);

                    var ventasAñoActual = await _context.PedidosEs
                        .Where(p => p.FechaRegistro.Year == añoActual && p.FechaRegistro.Month == i)
                        .SumAsync(p => p.ValorTotal);

                    // Añadir los valores a las listas correspondientes
                    pData.Add(ventasAñoAnterior);
                    uData.Add(ventasAñoActual);
                }

                // Añadir las series y los meses al diccionario
                ventasPorMesYAnio["series"] = new List<object>
                {
                    new { data = pData, label = añoAnterior.ToString() },
                    new { data = uData, label = añoActual.ToString() }
                };

                ventasPorMesYAnio["months"] = months;

                return ventasPorMesYAnio;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener ventas por mes y año: ", ex);
                throw;
            }
        }
        #endregion

    }
}
