using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.NotificacionE
{
    [Table("TblNotificacionesTipo")]
    public class NotificacionTipoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoNotificacion { get; set; }
        public string Texto { get; set; }
        public string Icono { get; set; }
    }
}
