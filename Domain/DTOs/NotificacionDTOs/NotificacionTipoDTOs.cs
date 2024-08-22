using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.NotificacionE;

namespace TiendaUNAC.Domain.DTOs.NotificacionDTOs
{
    public class NotificacionTipoDTOs
    {
        public int IdTipoNotificacion { get; set; }
        public string Texto { get; set; }
        public string Icono { get; set; }

        public static NotificacionTipoDTOs CrearDTO(NotificacionTipoE notificacionTipoE)
        {
            return new NotificacionTipoDTOs
            {
                IdTipoNotificacion = notificacionTipoE.IdTipoNotificacion,
                Texto = notificacionTipoE.Texto,
                Icono = notificacionTipoE.Icono
            };
        }

        public static NotificacionTipoE CrearE(NotificacionTipoDTOs notificacionTipoDTOs)
        {
            return new NotificacionTipoE
            {
                IdTipoNotificacion = notificacionTipoDTOs.IdTipoNotificacion,
                Texto = notificacionTipoDTOs.Texto,
                Icono = notificacionTipoDTOs.Icono
            };
        }
    }
}
