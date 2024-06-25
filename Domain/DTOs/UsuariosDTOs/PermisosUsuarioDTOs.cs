using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.ConfiguracionDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Domain.DTOs.UsuariosDTOs
{
    public class PermisosUsuarioDTOs
    {
        public int IdTipoUsuariosPermiso { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Path { get; set; }
        public string Icono { get; set; }
        public string Texto { get; set; }

        public static PermisosUsuarioDTOs CrearDTOs(PermisosUsuarioE permisosUsuarioE)
        {
            return new PermisosUsuarioDTOs
            {
                IdTipoUsuariosPermiso = permisosUsuarioE.IdTipoUsuariosPermiso,
                IdTipoUsuario = permisosUsuarioE.IdTipoUsuario,
                Path = permisosUsuarioE.Path,
                Icono = permisosUsuarioE.Icono,
                Texto = permisosUsuarioE.Texto
            };
        }

        public static PermisosUsuarioE CrearE(PermisosUsuarioDTOs permisosUsuarioDTOs)
        {
            return new PermisosUsuarioE
            {
                IdTipoUsuariosPermiso = permisosUsuarioDTOs.IdTipoUsuariosPermiso,
                IdTipoUsuario = permisosUsuarioDTOs.IdTipoUsuario,
                Path = permisosUsuarioDTOs.Path,
                Icono = permisosUsuarioDTOs.Icono,
                Texto = permisosUsuarioDTOs.Texto
            };
        }
    }
}
