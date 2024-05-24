using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ProductoE
{
    [Table("TblImagenProducto")]
    public class ImagenProductoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdImagenProducto { get; set; }
        public int IdProducto { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string NombreColor { get; set; }
        public string Color { get; set; }
        public int PorcentajeValor { get; set; }

    }
}
