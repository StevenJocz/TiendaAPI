using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ConfiguracionE;

namespace TiendaUNAC.Domain.DTOs.ConfiguracionDTOs
{
    public class EstadoPagoDTOs
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int RequestId { get; set; }
        public int IdReferencia { get; set; }
        public DateTime Fecha { get; set; }
        public int Valor { get; set; }
        public string Moneda { get; set; }
        public string FormaPago { get; set; }
        public string Razon { get; set; }
        public string Mensaje { get; set; }
        public string Correo { get; set; }

        public static EstadoPagoDTOs CrearDTOs(EstadoPagoE estadoPagoE)
        {
            return new EstadoPagoDTOs
            {
                IdEstado = estadoPagoE.IdEstado,
                Estado = estadoPagoE.Estado,
                RequestId = estadoPagoE.RequestId,
                IdReferencia = estadoPagoE.IdReferencia,
                Fecha = estadoPagoE.Fecha,
                Valor = estadoPagoE.Valor,
                Moneda = estadoPagoE.Moneda,
                FormaPago = estadoPagoE.FormaPago,
                Razon = estadoPagoE.Razon,
                Mensaje = estadoPagoE.Mensaje,
            };
        }
    }
}
