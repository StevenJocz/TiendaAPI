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
        public int IdTipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public int IdDepartamento { get; set; }
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
                IdTipoUsuario = usuariosE.IdTipoUsuario,
                Nombre = usuariosE.Nombre,
                Apellido = usuariosE.Apellido,
                IdTipoDocumento = usuariosE.IdTipoDocumento,
                Documento = usuariosE.Documento,
                FechaNacimiento = usuariosE.FechaNacimiento,
                Celular = usuariosE.Celular,
                IdPais = usuariosE.IdPais,
                IdDepartamento = usuariosE.IdDepartamento,
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
                IdTipoUsuario = usuariosDTOs.IdTipoUsuario,
                Nombre = usuariosDTOs.Nombre,
                Apellido = usuariosDTOs.Apellido,
                IdTipoDocumento = usuariosDTOs.IdTipoDocumento,
                Documento = usuariosDTOs.Documento,
                FechaNacimiento = usuariosDTOs.FechaNacimiento,
                Celular = usuariosDTOs.Celular,
                IdPais = usuariosDTOs.IdPais,
                IdDepartamento = usuariosDTOs.IdDepartamento,
                IdMunicipio = usuariosDTOs.IdMunicipio,
                Direccion = usuariosDTOs.Direccion,
                Correo = usuariosDTOs.Correo,
                Password = usuariosDTOs.Password,
                FechaRegistro = usuariosDTOs.FechaRegistro
            };
        }

        public class InicioSesionDTOs
        {
            public string correo { get; set; }
            public string password { get; set; }
        }

        public class passwordDTOs
        {
            public int accion { get; set; }
            public int idUsuario { get; set; }
            public string? correo { get; set; }
            public string password { get; set; }
        }

        public class DatosUsuarioDTOs
        {
            public int idUsuario { get; set; }
            public string nombre { get; set; }
            public string correo { get; set; }
            public string telefono { get; set; }
            public string direccion { get; set; }
            public int tipoUsuario { get; set; }
        }

        public class RespuestaInicioSesion
        {
            public bool resultado { get; set; }
            public string mensaje { get; set; }
            public string token { get; set; }
        }


        public class InformacionUsuariosDTOS
        {
            public int IdUsuario { get; set; }
            public string TipoUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string TipoDocumento { get; set; }
            public string Documento { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Celular { get; set; }
            public string Ubicacion { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
            public DateTime FechaRegistro { get; set; }
        }
    }

    
}
