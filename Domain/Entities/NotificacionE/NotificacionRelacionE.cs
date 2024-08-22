using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.NotificacionE
{
    [Table("TblNotificacionesRelacion")]
    public class NotificacionRelacionE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNotificacionesRelacion { get; set; }
        public int IdNotificaciones { get; set; }
        public int IdTipoRelacionNotificaciones { get; set; }
        public int IdRelacion { get; set; }
    }
}
