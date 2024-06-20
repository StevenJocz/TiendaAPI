using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.UsuariosE
{
    [Table("TblUsuarios")]
    public class UsuariosE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Celular { get; set; }
        public int IdMunicipio { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
