using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.NotificacionE
{
    [Table("TblNotificaciones")]
    public class NotificacionE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNotificacion { get; set; }
        public int IdTipoNotificacion { get; set; }
        public int DeIdUsuario { get; set; }
        public int ParaIdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }
    }
}
