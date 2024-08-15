using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ProductoE;

namespace TiendaUNAC.Domain.DTOs.ProductoDTOs
{
    public  class FavoritosDTOs
    {
        public int IdDeseos { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaAgregado { get; set; }

        public static FavoritosDTOs CrearDTOs(FavoritosE favoritosE)
        {
            return new FavoritosDTOs
            {
                IdDeseos = favoritosE.IdDeseos,
                IdProducto = favoritosE.IdProducto,
                IdUsuario = favoritosE.IdUsuario,
                FechaAgregado = favoritosE.FechaAgregado,
            };
        }

        public static FavoritosE CrearE(FavoritosDTOs favoritosDTOs)
        {
            return new FavoritosE
            {
                IdDeseos = favoritosDTOs.IdDeseos,
                IdProducto = favoritosDTOs.IdProducto,
                IdUsuario = favoritosDTOs.IdUsuario,
                FechaAgregado = favoritosDTOs.FechaAgregado,
            };
        }
    }

    public class FavoritosInformacion
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
    }
}
