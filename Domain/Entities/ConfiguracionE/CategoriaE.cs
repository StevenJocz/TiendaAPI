using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ConfiguracionE
{
    [Table("TblCategorias")]
    public class CategoriaE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public int IdTercero { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
