using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.GeneralesE
{
    [Table("TblEnvio")]
    public class EnvioE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnvio { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado { get; set; }
        public string Direccion { get; set; }
        public string Complemento { get; set; }
        public string Barrio { get; set; }
        public string Destinatario { get; set; }
        public string Responsable { get; set; }
    }
}
