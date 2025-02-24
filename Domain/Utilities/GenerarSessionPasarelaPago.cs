using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.PasarelaPagoDTOs;

namespace TiendaUNAC.Domain.Utilities
{
    public interface IGenerarSessionPasarelaPago
    {
        Task<ApiResponse> CrearSesionPago(PasarelaPagoDTOs request);
        Task<bool> EstadoSesion(int TipoReferencia, string Reference);
    }
    public class GenerarSessionPasarelaPago : IGenerarSessionPasarelaPago
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiPasarelaUrl;

        public GenerarSessionPasarelaPago(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiPasarelaUrl=  configuration.GetValue<string>("_apiPasarelaUrl");
        }

        public async Task<ApiResponse> CrearSesionPago(PasarelaPagoDTOs request)
        {
            
            var body = new
            {
                buyer = new
                {
                    document = request.document,
                    documentType = request.documentType,
                    name = request.name,
                    surname = request.surname,
                    email = request.email,
                    mobile = request.mobile,
                    
                },
                payment = new
                {
                    reference = request.reference,
                    description = "Compra realizada desde la Tienda UNAC",
                    amount = new
                    {
                        currency = "COP",
                        total = request.total,
                    }
                },
                returnUrl = $"https://tienda.unac.edu.co/PedidoExitoso/{request.reference}",
                cancelUrl = $"https://tienda.unac.edu.co/PedidoCancelado/{request.reference}",
                TipoReferencia = 1
            };

            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            var response = await _httpClient.PostAsync(_apiPasarelaUrl + "/sesion/post_sesion", content);
;
            // Verifica si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Lee la respuesta como texto
                var jsonResponse = await response.Content.ReadAsStringAsync();

               
                var newSesion = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });


                return newSesion;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<bool> EstadoSesion(int TipoReferencia, string Reference)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiPasarelaUrl}/states/get_states_sesion?TipoReferencia={TipoReferencia}&Reference={Reference}");

                if (!response.IsSuccessStatusCode)
                {
                    return false; 
                }else
                {
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
