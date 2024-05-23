using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.Utilities
{
    public interface IImagenes
    {
        Task<string> guardarImage(string base64Image, string ruta);
        Task<bool> eliminarImage(string imageUrl);
    }


    public class Imagenes : IImagenes
    {
        private string rutaUrl = "http://localhost:5072/";

        public async Task<string> guardarImage(string base64Image, string ruta)
        {
            try
            {
                string[] base64Parts = base64Image.Split(',');

                if (base64Parts.Length != 2)
                {
                    throw new ArgumentException("La cadena Base64 no tiene el formato esperado.");
                }

                // La segunda parte contiene la representación Base64 de la imagen
                string base64Data = base64Parts[1];
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                string fileName = $"{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(ruta, fileName);
                await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64Data));
                string[] rutaDos = ruta.Split('/');
                string rutaImagen = rutaUrl + rutaDos[1] + "/" + fileName;

                return rutaDos[1] + "/" + fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> eliminarImage(string imageUrl)
        {
            try
            {
                var fullPath = Path.Combine("wwwroot", imageUrl);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
