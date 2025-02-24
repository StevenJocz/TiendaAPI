using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.DTOs.ProductoDTOs
{
    public class InventarioSionDTOs
    {
        [Key]
        public int idInventario { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public double existencias { get; set; }
        public double iva { get; set; }
    }
}
