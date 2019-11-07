using BackEndSistema.Datos.Mapping;
using BackEndSistema.Datos.Mapping.Cotizacion;
using BackEndSistema.Datos.Mapping.Usuarios;
using BackEndSistema.Entidades;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace BackEndSistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estatus> Estatus { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CotizacionE> Cotizaciones { get; set; }
        public DbSet<Detalles_Cotizacion> DetallesCotizacion { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<OC> OCs { get; set; }
        public DbSet<Detalles_OC> Detalles_OCs { get; set; }
        public DbSet<Plantilla> Plantillas { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TipoClienteMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EstatusMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProductoMap());
            modelBuilder.ApplyConfiguration(new ServicioMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CotizacionMap());
            modelBuilder.ApplyConfiguration(new DetalleCotizacionMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
            modelBuilder.ApplyConfiguration(new ProveedorMap());
            modelBuilder.ApplyConfiguration(new OCMap());
            modelBuilder.ApplyConfiguration(new PlantillaMap());
            modelBuilder.ApplyConfiguration(new Detallles_OCMap());
        }
    }
}
