using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TiendaUNAC.Domain.Entities.ConfiguracionE;

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

        // Configuración
        public virtual DbSet<CategoriaE> CategoriaEs { get; set; }
        public virtual DbSet<TagE> TagEs { get; set; }

    }
}
