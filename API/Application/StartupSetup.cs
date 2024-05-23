using TiendaUNAC.Domain.Utilities;
using TiendaUNAC.Persistence.Commands;
using TiendaUNAC.Persistence.Queries;

namespace TiendaUNAC.API.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStartupSetup(this IServiceCollection service, IConfiguration configuration)
        {
            // Commands Persistance Services
            service.AddTransient<ICategoriaCommands, CategoriaCommands>();
            service.AddTransient<ITagCommands, TagCommands>();


            // Queries Persistance Services
            service.AddTransient<ICategoriaQueries, CategoriaQueries>();
            service.AddTransient<ITagQuieries, TagQuieries>();


            // Utilidades
            service.AddScoped<IImagenes, Imagenes>();

            return service;
        }
    }
}
