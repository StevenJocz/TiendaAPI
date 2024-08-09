using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.PedidosE
{
    [Table("TblPedido_Registros")]
    public class PedidosRegistrosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido_Registro { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int IdInventario { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public decimal Valor { get; set; }
    }
}
