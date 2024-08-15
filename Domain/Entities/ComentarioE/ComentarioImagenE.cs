using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Entities.ComentarioE
{
    [Table("TblProductoComentarioImagen")]
    public class ComentarioImagenE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComentarioImagen { get; set; }
        public int IdComentario { get; set; }
        public string Imagen { get; set; }
    }
}
