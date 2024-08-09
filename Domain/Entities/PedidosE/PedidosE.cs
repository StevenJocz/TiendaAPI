using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.PedidosE
{
    [Table("TblPedidos")]
    public class PedidosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }
        public int Orden { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorEnvio { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoEntrega { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
