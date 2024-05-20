using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.DTOs
{
    public class LoginDTOs
    {
    }


    public class DateUserDTOs
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string foto { get; set; }
        public int tipoUsuario { get; set; }
    }

    public class Responselogin
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string Token { get; set; }
    }
}
