using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.Producto;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProductosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Productos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProductoViewModel>> Listar()
        {
            var producto = await _context.Productos.Include(p => p.estatus).Include(p => p.categoria).ToListAsync();

            return producto.Select(p => new ProductoViewModel
            {
                idProducto = p.idProducto,
                clave = p.clave,
                nombre = p.nombre,
                idCategoria = p.idCategoria,
                categoria = p.categoria.nombre,
                unidadMedida = p.unidadMedida,
                stock = p.stock,
                precioProducto = p.precioProducto,
                manoObra = p.manoObra,
                precio = p.precio,
                idEstatus = p.idEstatus,
                estatus = p.estatus.estatus,
                fechaRegistro = p.fechaRegistro,
                fechaActualizacion = p.fechaActualizacion


            });
        }

        // GET: api/Productos/BuscarPorCategoria/2
        [HttpGet("[action]/{idCategoriaBuscar}")]
        public async Task<IEnumerable<ProductoViewModel>> BuscarPorCategoria([FromRoute] int idCategoriaBuscar)
        {
            var producto = await _context.Productos
                .Include(p => p.estatus)
                .Include(p => p.categoria)
                .Where(p => p.idEstatus == 1)
                .Where(p => p.idCategoria == idCategoriaBuscar)
                .ToListAsync();

            return producto.Select(p => new ProductoViewModel
            {
                idProducto = p.idProducto,
                clave = p.clave,
                nombre = p.nombre,
                idCategoria = p.idCategoria,
                categoria = p.categoria.nombre,
                unidadMedida = p.unidadMedida,
                stock = p.stock,
                precioProducto = p.precioProducto,
                manoObra = p.manoObra,
                precio = p.precio,
                idEstatus = p.idEstatus,
                estatus = p.estatus.estatus,
                fechaRegistro = p.fechaRegistro,
                fechaActualizacion = p.fechaActualizacion


            });
        }

        // GET: api/Productos/BuscarClaveProducto/CLAVE
        [HttpGet("[action]/{clave}")]
        public async Task<ActionResult> BuscarClaveProducto([FromRoute] string clave)
        {
            var producto = await _context.Productos.Include(p => p.estatus).Include(p => p.categoria)
                .Where(p => p.idEstatus == 1).
                SingleOrDefaultAsync(p => p.clave == clave);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(new ProductoViewModel
            {
                idProducto = producto.idProducto,
                clave = producto.clave,
                nombre = producto.nombre,
                idCategoria = producto.idCategoria,
                categoria = producto.categoria.nombre,
                unidadMedida = producto.unidadMedida,
                stock = producto.stock,
                precioProducto = producto.precioProducto,
                manoObra = producto.manoObra,
                precio = producto.precio,
                idEstatus = producto.idEstatus,
                estatus = producto.estatus.estatus,
                fechaRegistro = producto.fechaRegistro,
                fechaActualizacion = producto.fechaActualizacion
            });
        }

        // GET: api/Productos/BuscarPoridProducto/CLAVE
        [HttpGet("[action]/{idProductoBuscar}")]
        public async Task<ActionResult> BuscarPoridProducto([FromRoute] int idProductoBuscar)
        {
            var producto = await _context.Productos.Include(p => p.estatus).Include(p => p.categoria)
                .Where(p => p.idEstatus == 1).
                SingleOrDefaultAsync(p => p.idProducto == idProductoBuscar);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(new ProductoViewModel
            {
                idProducto = producto.idProducto,
                clave = producto.clave,
                nombre = producto.nombre,
                idCategoria = producto.idCategoria,
                categoria = producto.categoria.nombre,
                unidadMedida = producto.unidadMedida,
                stock = producto.stock,
                precioProducto = producto.precioProducto,
                manoObra = producto.manoObra,
                precio = producto.precio,
                idEstatus = producto.idEstatus,
                estatus = producto.estatus.estatus,
                fechaRegistro = producto.fechaRegistro,
                fechaActualizacion = producto.fechaActualizacion
            });
        }

        // POST: api/Productos/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;
            Producto producto = new Producto
            {
                clave = model.clave,
                nombre = model.nombre,
                idCategoria = model.idCategoria,
                unidadMedida = model.unidadMedida,
                stock = model.stock,
                precioProducto = model.precioProducto,
                manoObra = model.manoObra,
                precio = model.precio,
                idEstatus = model.idEstatus,
                fechaRegistro = fechaHora,
                fechaActualizacion = fechaHora
                

            };

            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Productos/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idProducto <= 0)
            {
                return BadRequest();
            }
            var fechaHora = DateTime.Now;
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.idProducto == model.idProducto);

            if (producto == null)
            {
                return NotFound();
            }

            producto.clave = model.clave;
            producto.nombre = model.nombre;
            producto.idCategoria = model.idCategoria;
            producto.unidadMedida = model.unidadMedida;
            producto.stock = model.stock;
            producto.precioProducto = model.precioProducto;
            producto.manoObra = model.manoObra;
            producto.precio = model.precio;
            producto.idEstatus = model.idEstatus;
            producto.fechaActualizacion = fechaHora;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> IngresarSobrante([FromBody] SobranteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idProducto <= 0)
            {
                return BadRequest();
            }
            var fechaHora = DateTime.Now;
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.idProducto == model.idProducto);
            var stockNuevo = producto.stock + model.stock;

            if (producto == null)
            {
                return NotFound();
            }

            producto.stock = stockNuevo;
            producto.fechaActualizacion = fechaHora;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.idProducto == id);
        }
    }
}
