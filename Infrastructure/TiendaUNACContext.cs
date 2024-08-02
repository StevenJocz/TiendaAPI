using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.DTOs.GeneralesDTOs;
using TiendaUNAC.Domain.DTOs.ProductoDTOs;
using TiendaUNAC.Domain.Entities.ConfiguracionE;
using TiendaUNAC.Domain.Entities.GeneralesE;
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

        // Configuración
        public virtual DbSet<CategoriaE> CategoriaEs { get; set; }
        public virtual DbSet<TagE> TagEs { get; set; }

        //Productos
        public virtual DbSet<ProductoE> ProductoEs { get; set; }
        public virtual DbSet<ImagenProductoE> ImagenProductoEs { get; set; }
        public virtual DbSet<TallaProductoE> TallaProductoEs { get; set; }
        public virtual DbSet<InventarioSionDTOs> InventarioSionEs { get; set; }

        //Usuarios 
        public virtual DbSet<UsuariosE> UsuariosEs { get; set; }
        public virtual DbSet<PermisosUsuarioE> PermisosUsuarioEs { get; set; }
       


    }
}
