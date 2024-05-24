using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ProductoE
{
    [Table("TblTallaProducto")]
    public class TallaProductoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTallaProducto { get; set; }
        public int IdProducto { get; set; }
        public string Talla { get; set; }
        public int PorcentajeValor { get; set; }
    }
}
