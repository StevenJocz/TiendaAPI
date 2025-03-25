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
        public int IdTipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdGenero { get; set; }
        public string Documento { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public int IdDepartamento { get; set; }
        public int IdMunicipio { get; set; }
        public string TipoVia { get; set; }
        public string Numero1 { get; set; }
        public string Numero2 { get; set; }
        public string Numero3 { get; set; }
        public string Correo { get; set; }
        public string? Password { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
