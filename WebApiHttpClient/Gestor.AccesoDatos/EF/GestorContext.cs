using Gestor.AccesoDatos.Configuraciones;
using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using SistemaAutenticacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.AccesoDatos.EF
{
    public class GestorContext : DbContext
    {

        public DbSet<Articulo> Articulos { get; set; } 
        
        public DbSet<MovimientoStock> MovimientosStock {  get; set; }

        public DbSet<TipoMovimiento> TiposMovimientos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Administrador> Administradores { get; set; }

        public DbSet<Encargado> Miembros { get; set; }

        public DbSet<Parametro> Parametros { get; set; }

        public GestorContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UsuarioConfiguracion().Configure(modelBuilder.Entity<Usuario>());
        }
    }
}
