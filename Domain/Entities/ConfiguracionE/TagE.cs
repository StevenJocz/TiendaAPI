using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ConfiguracionE
{
    [Table("TblCategorias")]
    public class TagE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTag { get; set; }
        public string Tag { get; set; }
        public bool Activo { get; set; }
    }
}
