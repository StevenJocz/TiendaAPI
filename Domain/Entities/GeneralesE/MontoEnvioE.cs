using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.GeneralesE
{
    [Table("TblMontoEnvioGratis")]
    public class MontoEnvioE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMonto { get; set; }
        public decimal ValorMonto { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int IdUsuarioActualizador { get; set; }

    }
}
