using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaUNAC.Domain.DTOs.PasarelaPagoDTOs
{
    public class PasarelaPagoDTOs
    {
        public string document { get; set; }
        public string documentType { get; set; } 
        public string name { get; set; } 
        public string surname { get; set; } 
        public string email { get; set; } 
        public string mobile { get; set; }
        public string reference { get; set; }
        public decimal total { get; set; }
    }

    public class ApiResponse
    {
        public bool Resultado { get; set; }  
        public ApiResponseData? Data { get; set; }  
        public string? Mensaje { get; set; }  
    }

    public class ApiResponseData
    {
        public string ProcessUrl { get; set; } = string.Empty;  
    }


    public class StatusResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }

    public class PaymentResponse
    {
        public string Franchise { get; set; }
        public string Reference { get; set; }
        public string IssuerName { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }

    }

    public class PaymentStatus
    {
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class EstadoSesionResponse
    {
        public int RequestId { get; set; }
        public StatusResponse Status { get; set; }
        public List<PaymentResponse> Payment { get; set; }
    }
}
