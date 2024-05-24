using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ProductoE
{
    [Table("TblProductos")]
    public class ProductoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        public int IdInventario { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Informacion { get; set; }
        public string Tags { get; set; }
        public int Descuento { get; set; }
        public DateTime FechaFinDescuento { get; set; }
        public bool Activo { get; set; }
        public int IdTercero { get; set; }
        public DateTime FechaCreado { get; set; }

    }
}
