using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ConfiguracionE;

namespace TiendaUNAC.Domain.DTOs.ConfiguracionDTOs
{
    public class TagDTOs
    {
        public int IdTag { get; set; }
        public string Tag { get; set; }
        public bool Activo { get; set; }

        public static TagDTOs CrearDTOs(TagE tagE)
        {
            return new TagDTOs
            {
                IdTag = tagE.IdTag,
                Tag = tagE.Tag,
                Activo = tagE.Activo,
            };
        }

        public static TagE CrearE(TagDTOs tagDTOs)
        {
            return new TagE
            {
                IdTag = tagDTOs.IdTag,
                Tag = tagDTOs.Tag,
                Activo = tagDTOs.Activo,
            };
        }
    }
}
