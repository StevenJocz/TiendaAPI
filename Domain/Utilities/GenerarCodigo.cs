using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Utilities
{
    public interface IGenerarCodigo
    {
        Task<string> generarCodigo();
    }

    public class GenerarCodigo: IGenerarCodigo
    {
        public async Task<string> generarCodigo()
        {
            const string caracteres = "0123456789";
            var random = new Random();

            var codigoBuilder = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                codigoBuilder.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return codigoBuilder.ToString();
        }
    }
}
