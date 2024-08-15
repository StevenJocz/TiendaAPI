using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ProductoE
{
    [Table("TblListaDeseos")]
    public class FavoritosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDeseos { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaAgregado { get; set; }
    }
}
