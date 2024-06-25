using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.UsuariosE
{
    [Table("TblTipoUsuarioPermiso")]
    public class PermisosUsuarioE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoUsuariosPermiso { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Path { get; set; }
        public string Icono { get; set; }
        public string Texto { get; set; }

    }
}
