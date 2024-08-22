using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.NotificacionE;

namespace TiendaUNAC.Domain.DTOs.NotificacionDTOs
{
    public class NotificacionDTOs
    {
        public int IdNotificacion { get; set; }
        public int IdTipoNotificacion { get; set; }
        public int DeIdUsuario { get; set; }
        public int ParaIdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }

        public static NotificacionDTOs CrearDTO(NotificacionE notificacionE)
        {
            return new NotificacionDTOs
            {
                IdNotificacion = notificacionE.IdNotificacion,
                IdTipoNotificacion = notificacionE.IdTipoNotificacion,
                DeIdUsuario = notificacionE.DeIdUsuario,
                ParaIdUsuario = notificacionE.ParaIdUsuario,
                Fecha = notificacionE.Fecha,
                Leida = notificacionE.Leida
            };
        }

        public static NotificacionE CrearE(NotificacionDTOs notificacionDTOs)
        {
            return new NotificacionE
            {
                IdNotificacion = notificacionDTOs.IdNotificacion,
                IdTipoNotificacion = notificacionDTOs.IdTipoNotificacion,
                DeIdUsuario = notificacionDTOs.DeIdUsuario,
                ParaIdUsuario = notificacionDTOs.ParaIdUsuario,
                Fecha = notificacionDTOs.Fecha,
                Leida = notificacionDTOs.Leida
            };
        }

    }

    public class NotificacionCrear
    {
        public int IdTipoNotificacion { get; set; }
        public int DeIdUsuario { get; set; }
        public int ParaIdUsuario { get; set; }
        public int IdTipoRelacion { get; set; }
        public int IdRelacion { get; set; }
    }
}
