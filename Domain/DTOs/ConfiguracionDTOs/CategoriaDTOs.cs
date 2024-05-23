using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ConfiguracionE;

namespace TiendaUNAC.Domain.DTOs.ConfiguracionDTOs
{
    public class CategoriaDTOs
    {
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public int IdTercero { get; set; }
        public DateTime FechaCreacion { get; set; }

        public static CategoriaDTOs CrearDTOs(CategoriaE categoriaE)
        {
            return new CategoriaDTOs
            {
                IdCategoria = categoriaE.IdCategoria,
                Titulo = categoriaE.Titulo,
                Descripcion = categoriaE.Descripcion,
                Nombre = categoriaE.Nombre,
                Imagen = categoriaE.Imagen,
                Activo = categoriaE.Activo,
                IdTercero = categoriaE.IdTercero,
                FechaCreacion = categoriaE.FechaCreacion
            };
        }

        public static CategoriaE CrearE(CategoriaDTOs categoriaDTOs)
        {
            return new CategoriaE
            {
                IdCategoria = categoriaDTOs.IdCategoria,
                Titulo = categoriaDTOs.Titulo,
                Descripcion = categoriaDTOs.Descripcion,
                Nombre = categoriaDTOs.Nombre,
                Imagen = categoriaDTOs.Imagen,
                Activo = categoriaDTOs.Activo,
                IdTercero = categoriaDTOs.IdTercero,
                FechaCreacion = categoriaDTOs.FechaCreacion
            };
        }
    }
}
