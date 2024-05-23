using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Infrastructure;

namespace TiendaUNAC.Persistence.Commands
{
    public interface ITagCommands
    {
        Task<RespuestaDTO> crearTag(TagDTOs tagDTOs);
        Task<RespuestaDTO> actualizarTag(TagDTOs tagDTOs);
    }
    public class TagCommands: ITagCommands , IDisposable 
    {
        private readonly TiendaUNACContext _context = null;
        private readonly ILogger<ITagCommands> _logger;
        private readonly IConfiguration _configuration;

        public TagCommands(ILogger<TagCommands> logger, IConfiguration configuration)
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

        #region CREAR TAG
        public async Task<RespuestaDTO> crearTag(TagDTOs tagDTOs)
        {
            _logger.LogTrace("Iniciando metodo TagCommands.crearTag...");
            try
            {
                var newTag = new TagDTOs
                {
                    IdTag = tagDTOs.IdTag,
                    Tag = tagDTOs.Tag,
                    Activo = tagDTOs.Activo,
                };

                var tag = TagDTOs.CrearE(newTag);
                await _context.TagEs.AddAsync(tag);
                await _context.SaveChangesAsync();

                if (tag.IdTag != 0)
                {
                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha añadido el tag exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo agregar el tag! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo TagCommands.crearTag...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR TAG
        public async Task<RespuestaDTO> actualizarTag(TagDTOs tagDTOs)
        {
            _logger.LogTrace("Iniciando metodo TagCommands.actualizarTag...");
            try
            {
                var existeTag = await _context.TagEs.FirstOrDefaultAsync(x => x.IdTag == tagDTOs.IdTag);
                if (existeTag != null)
                {

                    existeTag.IdTag = tagDTOs.IdTag;
                    existeTag.Tag = tagDTOs.Tag;
                    existeTag.Activo = tagDTOs.Activo;

                    _context.TagEs.Update(existeTag);
                    await _context.SaveChangesAsync();

                    return new RespuestaDTO
                    {
                        resultado = true,
                        mensaje = "¡Se ha actualizado el tag exitosamente!",
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        resultado = false,
                        mensaje = "¡No se pudo encontrar el tag!. Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodoTagCommands.actualizarTag...");
                throw;
            }
        }
        #endregion


    }
}
