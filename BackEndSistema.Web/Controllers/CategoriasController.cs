using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.Categoria;
namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CategoriasController(DbContextSistema context)
        {
            _context = context;
        }

    

        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaViewModel>> Listar()
        {
            var categoria = await _context.Categorias
                .Include(c => c.estatus)
                .Include(c => c.servicio)
                .Where(c => c.idEstatus == 1 )
                .ToListAsync();
           
            return categoria.Select(c => new CategoriaViewModel
            {
                idCategoria = c.idCategoria,
                nombre = c.nombre,
                idServicio = c.idServicio,
                servicio = c.servicio.nombre,
                idEstatus = c.idEstatus,
                estatus = c.estatus.estatus,
                fechaRegistro = c.fechaRegistro


            });
        }


        // GET: api/Categorias/BuscarPorServicio/idServicio
        [HttpGet("[action]/{idServicioBuscar}")]
        public async Task<IEnumerable<CategoriaViewModel>> BuscarPorServicio([FromRoute] int idServicioBuscar)
        {
            var categoria = await _context.Categorias
                .Include(c => c.estatus)
                .Include(c => c.servicio)
                .Where(c => c.idEstatus == 1)
                .Where(c => c.idServicio == idServicioBuscar)
                .ToListAsync();

            return categoria.Select(c => new CategoriaViewModel
            {
                idCategoria = c.idCategoria,
                nombre = c.nombre,
                idServicio = c.idServicio,
                servicio = c.servicio.nombre,
                idEstatus = c.idEstatus,
                estatus = c.estatus.estatus,
                fechaRegistro = c.fechaRegistro


            });
        }

        // GET: api/Categorias/BuscarPoridCategoria/idCategoria
        [HttpGet("[action]/{idCategoriaBuscar}")]
        public async Task<ActionResult> BuscarPoridCategoria([FromRoute] int idCategoriaBuscar)
        {
            var categoria = await _context.Categorias.Include(c => c.estatus).Include(c => c.servicio)
                .Where(c => c.idEstatus == 1).
                SingleOrDefaultAsync(c => c.idCategoria == idCategoriaBuscar);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaViewModel
            {
                idCategoria = categoria.idCategoria,
                nombre = categoria.nombre,
                idServicio = categoria.idServicio,
                servicio = categoria.servicio.nombre,
                idEstatus = categoria.idEstatus,
                estatus = categoria.estatus.estatus,
                fechaRegistro = categoria.fechaRegistro
            });
        }

        // POST: api/Categorias/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idCategoria <= 0)
            {
                return BadRequest();
            }

            var cliente = await _context.Categorias.FirstOrDefaultAsync(c => c.idCategoria == model.idCategoria);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.nombre = model.nombre;
            cliente.idServicio = model.idServicio;


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

        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;
            Categoria categoria = new Categoria
            {
                nombre = model.nombre,
                idServicio = model.idServicio,
                idEstatus = model.idEstatus,
                fechaRegistro = fechaHora

            };

            _context.Categorias.Add(categoria);
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

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.idCategoria == id);
        }
    }
}
