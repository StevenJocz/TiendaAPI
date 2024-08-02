using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.GeneralesE
{
    [Table("TblCupones")]
    public class CuponesE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCupon { get; set; }
        public string TextoCupon { get; set; }
        public decimal ValorCupon { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreador { get; set; }
        public bool Activo { get; set; }
    }
}
