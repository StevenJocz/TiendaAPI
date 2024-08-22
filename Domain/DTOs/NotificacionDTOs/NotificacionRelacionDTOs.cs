using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.NotificacionE;

namespace TiendaUNAC.Domain.DTOs.NotificacionDTOs
{
    public class NotificacionRelacionDTOs
    {
        public int IdNotificacionesRelacion { get; set; }
        public int IdNotificaciones { get; set; }
        public int IdTipoRelacionNotificaciones { get; set; }
        public int IdRelacion { get; set; }

        public static NotificacionRelacionDTOs CrearDTO(NotificacionRelacionE notificacionRelacionE)
        {
            return new NotificacionRelacionDTOs
            {
                IdNotificacionesRelacion = notificacionRelacionE.IdNotificacionesRelacion,
                IdNotificaciones = notificacionRelacionE.IdNotificaciones,
                IdTipoRelacionNotificaciones = notificacionRelacionE.IdTipoRelacionNotificaciones,
                IdRelacion = notificacionRelacionE.IdRelacion
            };
        }

        public static NotificacionRelacionE CrearE(NotificacionRelacionDTOs notificacionRelacionDTO)
        {
            return new NotificacionRelacionE
            {
                IdNotificacionesRelacion = notificacionRelacionDTO.IdNotificacionesRelacion,
                IdNotificaciones = notificacionRelacionDTO.IdNotificaciones,
                IdTipoRelacionNotificaciones = notificacionRelacionDTO.IdTipoRelacionNotificaciones,
                IdRelacion = notificacionRelacionDTO.IdRelacion
            };
        }
    }
}
