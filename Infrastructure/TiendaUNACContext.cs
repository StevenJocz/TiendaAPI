﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.NotificacionDTOs;
using TiendaUNAC.Domain.DTOs.PedidosDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ComentarioE;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.GeneralesE;
using TiendaUNAC.Domain.Entities.NotificacionE;
using TiendaUNAC.Domain.Entities.PedidosE;
using TiendaUNAC.Domain.Entities.ProductoE;
using TiendaUNAC.Domain.Entities.UsuariosE;

namespace TiendaUNAC.Infrastructure
{
    public class TiendaUNACContext : DbContext
    {
        private readonly string _connection;
        private readonly short _commandTimeOut;

        public TiendaUNACContext(string connection)
        {
            _connection = connection;
            _commandTimeOut = 60;
        }

        public TiendaUNACContext(string connection, short commandTimeOut)
        {
            _connection = connection;
            _commandTimeOut = commandTimeOut;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection, sqlserverOptions => sqlserverOptions.CommandTimeout(_commandTimeOut));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Generales
        public virtual DbSet<tiposDocumentosDTOs> tipoDocumentosEs { get; set; }
        public virtual DbSet<generosDTOs> generosEs { get; set; }
        public virtual DbSet<ubicacionDTOs> ubicacionEs { get; set; }
        public virtual DbSet<MontoEnvioE> MontoEnvioEs { get; set; }
        public virtual DbSet<CuponesE> CuponesEs { get; set; }
        public virtual DbSet<CuponUsuarioE> CuponUsuarioEs { get; set; }
        public virtual DbSet<EstadoE> EstadoEs { get; set; }
        public virtual DbSet<EnvioE> EnvioEs { get; set; }
        public virtual DbSet<EmailE> EmailEs { get; set; }

        // Configuración
        public virtual DbSet<CategoriaE> CategoriaEs { get; set; }
        public virtual DbSet<TagE> TagEs { get; set; }
        public virtual DbSet<EstadoPagoE> EstadoPagoEs { get; set; }

        // Productos
        public virtual DbSet<ProductoE> ProductoEs { get; set; }
        public virtual DbSet<ImagenProductoE> ImagenProductoEs { get; set; }
        public virtual DbSet<TallaProductoE> TallaProductoEs { get; set; }
        public virtual DbSet<InventarioSionDTOs> InventarioSionEs { get; set; }
        public virtual DbSet<FavoritosE> FavoritosEs { get; set; }

        //Comentarios
        public virtual DbSet<ComentarioE> ComentarioEs { get; set; }
        public virtual DbSet<ComentarioImagenE> ComentarioImagenEs { get; set; }

        // Usuarios 
        public virtual DbSet<UsuariosE> UsuariosEs { get; set; }
        public virtual DbSet<PermisosUsuarioE> PermisosUsuarioEs { get; set; }
        public virtual DbSet<CodigoE> CodigoEs { get; set; }
       
        // Pedidos
        public virtual DbSet<PedidosE> PedidosEs { get; set; }
        public virtual DbSet<PedidosRegistrosE> PedidosRegistrosEs { get; set; }
        public virtual DbSet<ListaPedidoDTOs> ListaPedidos { get; set; }

        //Notificaciones
        public virtual DbSet<NotificacionE> NotificacionEs { get; set; }
        public virtual DbSet<NotificacionRelacionE> NotificacionRelacionEs { get; set; }
        public virtual DbSet<ListarNotificacionesDTOs> ListarNotificacionEs { get; set; }
        public virtual DbSet<CountNotificaciones> CountNotificacionEs { get; set; }
        public virtual DbSet<PagoPendiente> PagoPendienteEs { get; set; }

    }
}
