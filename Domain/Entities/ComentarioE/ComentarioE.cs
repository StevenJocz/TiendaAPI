using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ComentarioE
{
    [Table("TblProductoComentario")]
    public class ComentarioE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComentario { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public bool VistoAdmin { get; set; }
    }
}
