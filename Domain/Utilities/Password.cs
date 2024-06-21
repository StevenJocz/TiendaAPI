using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Utilities
{
    public interface IPassword
    {
        Task<string> GenerarPassword(string password);
        Task<bool> VerificarPassword(string enteredPassword, string savedHashedPassword);
    }

    public class Password: IPassword
    {
        public async Task<string> GenerarPassword(string password)
        {
            // Generar una sal única para cada contraseña
            byte[] salt = GenerateSalt();

            using (var sha256Hash = SHA256.Create())
            {
                // Concatenar la contraseña con la sal
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));

                // Generar el hash
                byte[] hashBytes = sha256Hash.ComputeHash(combinedBytes);

                // Convertir el hash y la sal a cadenas base64 para almacenarlos
                string hashedPassword = Convert.ToBase64String(hashBytes);
                string saltString = Convert.ToBase64String(salt);

                // Concatenar el hash y la sal para poder verificarlos juntos más adelante
                return hashedPassword + ":" + saltString;
            }
        }

        public async Task<bool> VerificarPassword(string enteredPassword, string savedHashedPassword)
        {
            // Separar el hash y la sal almacenados
            string[] parts = savedHashedPassword.Split(':');
            string hashedPassword = parts[0];
            string saltString = parts[1];

            // Convertir la sal de cadena base64 a bytes
            byte[] salt = Convert.FromBase64String(saltString);

            using (var sha256Hash = SHA256.Create())
            {
                // Concatenar la contraseña ingresada con la sal almacenada
                byte[] combinedBytes = Encoding.UTF8.GetBytes(enteredPassword + Convert.ToBase64String(salt));

                // Generar el hash de la contraseña ingresada con la misma sal
                byte[] enteredPasswordHashBytes = sha256Hash.ComputeHash(combinedBytes);

                // Convertir el hash a cadena base64
                string enteredPasswordHash = Convert.ToBase64String(enteredPasswordHashBytes);

                // Comparar el hash generado con el hash almacenado
                return string.Equals(enteredPasswordHash, hashedPassword);
            }
        }

        private byte[] GenerateSalt()
        {
            // Generar una sal aleatoria utilizando RNGCryptoServiceProvider
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
