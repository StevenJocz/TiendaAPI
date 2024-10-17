using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.UsuariosE
{
    [Table("TblCodigo")]
    public class CodigoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCodigo { get; set; }
        public int Codigo { get; set; }
        public string Correo { get; set; }
    }
}
