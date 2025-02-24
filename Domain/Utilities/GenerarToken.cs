using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static TiendaUNAC.Domain.DTOs.UsuariosDTOs.UsuariosDTOs;

namespace TiendaUNAC.Domain.Utilities
{
    public interface IGenerarToken
    {
        Task<string> Token(DatosUsuarioDTOs datosUsuarioDTOs);
    }

    public class GenerarToken: IGenerarToken
    {
        public async Task<string> Token(DatosUsuarioDTOs datosUsuarioDTOs)
        {
            try
            {
                var key = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                var keyBytes = Encoding.ASCII.GetBytes(key);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(datosUsuarioDTOs.idUsuario)));
                claims.AddClaim(new Claim("idUsuario", Convert.ToString(datosUsuarioDTOs.idUsuario)));
                claims.AddClaim(new Claim("nombre", datosUsuarioDTOs.nombre));
                claims.AddClaim(new Claim("apellido", datosUsuarioDTOs.apellido));
                claims.AddClaim(new Claim("tipoDocumento", Convert.ToString(datosUsuarioDTOs.tipoDocumento)));
                claims.AddClaim(new Claim("documento", datosUsuarioDTOs.documento));
                claims.AddClaim(new Claim("correo", datosUsuarioDTOs.correo));
                claims.AddClaim(new Claim("telefono", datosUsuarioDTOs.telefono));
                claims.AddClaim(new Claim("genero", Convert.ToString(datosUsuarioDTOs.genero)));
                claims.AddClaim(new Claim("tipoUsuario", Convert.ToString(datosUsuarioDTOs.tipoUsuario)));
                claims.AddClaim(new Claim("pais", Convert.ToString(datosUsuarioDTOs.pais)));
                claims.AddClaim(new Claim("departamento", Convert.ToString(datosUsuarioDTOs.departamento)));
                claims.AddClaim(new Claim("ciudad", Convert.ToString(datosUsuarioDTOs.ciudad)));
                claims.AddClaim(new Claim("tipoVia", Convert.ToString(datosUsuarioDTOs.tipoVia)));
                claims.AddClaim(new Claim("numero1", datosUsuarioDTOs.Numero1));
                claims.AddClaim(new Claim("numero2", datosUsuarioDTOs.Numero2));
                claims.AddClaim(new Claim("numero3", datosUsuarioDTOs.Numero3));


                var credencialesToken = new SigningCredentials
                (
                   new SymmetricSecurityKey(keyBytes),
                   SecurityAlgorithms.HmacSha256Signature
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials = credencialesToken
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return tokenCreado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
