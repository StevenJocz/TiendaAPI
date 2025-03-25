using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.DTOs.NotificacionDTOs
{
    public class PagoPendiente
    {
        [Key]
        public string IdReferencia { get; set; }
        public Int32 Valor { get; set; }
    }
}
