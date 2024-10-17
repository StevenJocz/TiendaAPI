using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.GeneralesE
{
    [Table("TblTemplateCorreo")]
    public class EmailE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTemplateCorreo { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
    }
}
