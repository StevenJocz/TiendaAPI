using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.UsuariosDTOs;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Domain.DTOs.GeneralesDTOs
{
    public class EmailDTOs
    {
        public int IdTemplateCorreo { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }

        public static EmailDTOs CrearDTOs(EmailE emailE)
        {
            return new EmailDTOs
            {
                IdTemplateCorreo = emailE.IdTemplateCorreo,
                Nombre = emailE.Nombre,
                Contenido = emailE.Contenido
            };
        }

        public static EmailE CrearE(EmailDTOs emailDTOs)
        {
            return new EmailE
            {
                IdTemplateCorreo = emailDTOs.IdTemplateCorreo,
                Nombre = emailDTOs.Nombre,
                Contenido = emailDTOs.Contenido
            };
        }
    }


    public class EmailEnviar
    {
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
}
