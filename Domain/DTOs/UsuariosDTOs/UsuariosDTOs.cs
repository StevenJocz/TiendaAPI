using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Domain.DTOs.UsuariosDTOs
{
    public class UsuariosDTOs
    {
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

        public static UsuariosDTOs CrearDTOs(UsuariosE usuariosE)
        {
            return new UsuariosDTOs
            {
                IdUsuario = usuariosE.IdUsuario,
                Nombre = usuariosE.Nombre,
                Apellido = usuariosE.Apellido,
                IdTipoDocumento = usuariosE.IdTipoDocumento,
                Documento = usuariosE.Documento,
                FechaNacimiento = usuariosE.FechaNacimiento,
                Celular = usuariosE.Celular,
                IdMunicipio = usuariosE.IdMunicipio,
                Direccion = usuariosE.Direccion,
                Correo = usuariosE.Correo,
                Password = usuariosE.Password, 
                FechaRegistro= usuariosE.FechaRegistro
            };
        }

        public static UsuariosE CrearE(UsuariosDTOs usuariosDTOs)
        {
            return new UsuariosE
            {
                IdUsuario = usuariosDTOs.IdUsuario,
                Nombre = usuariosDTOs.Nombre,
                Apellido = usuariosDTOs.Apellido,
                IdTipoDocumento = usuariosDTOs.IdTipoDocumento,
                Documento = usuariosDTOs.Documento,
                FechaNacimiento = usuariosDTOs.FechaNacimiento,
                Celular = usuariosDTOs.Celular,
                IdMunicipio = usuariosDTOs.IdMunicipio,
                Direccion = usuariosDTOs.Direccion,
                Correo = usuariosDTOs.Correo,
                Password = usuariosDTOs.Password,
                FechaRegistro = usuariosDTOs.FechaRegistro
            };
        }
    }

    
}
