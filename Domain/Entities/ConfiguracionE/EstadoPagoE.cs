using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ConfiguracionE
{
    [Table("TblPlaceToPay_Estados")]
    public class EstadoPagoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int RequestId { get; set; }
        public int IdReferencia { get; set; }
        public DateTime Fecha { get; set; }
        public int Valor { get; set; }
        public string Moneda { get; set; }
        public string FormaPago { get; set; }
        public string Razon { get; set; }
        public string Mensaje { get; set; }
    }
}
