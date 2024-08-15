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
            service.AddTransient<IProductoCommands, ProductoCommands>();
            service.AddTransient<IUsuarioCommands, UsuarioCommands>();
            service.AddTransient<IGeneralesCommands, GeneralesCommands>();
            service.AddTransient<IPedidoCommands, PedidoCommands>();
            service.AddTransient<IComentarioCommands, ComentarioCommands>();

            // Queries Persistance Services
            service.AddTransient<ICategoriaQueries, CategoriaQueries>();
            service.AddTransient<ITagQuieries, TagQuieries>();
            service.AddTransient<IProductoQueries, ProductoQueries>();
            service.AddTransient<IUsuarioQueries, UsuarioQueries>();
            service.AddTransient<IGeneralesQueries, GeneralesQueries>();
            service.AddTransient<IPedidoQueries, PedidoQueries>();
            service.AddTransient<IComentarioQueries, ComentarioQueries>();

            // Utilidades
            service.AddScoped<IImagenes, Imagenes>();
            service.AddScoped<IPassword, Password>();
            service.AddScoped<IGenerarToken, GenerarToken>();

            return service;
        }
    }
}
