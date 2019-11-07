using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.Servicios;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ServiciosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Servicios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ServiciosViewModel>> Listar()
        {
            var servicio = await _context.Servicios.Include(s => s.estatus).ToListAsync();

            return servicio.Select(s => new ServiciosViewModel
            {
                idServicio = s.idServicio,
                nombre = s.nombre,
                idEstatus = s.idEstatus,
                estatus = s.estatus.estatus


            });
        }

        // GET: api/Servicios/BuscarPoridServicio/idServicio
        [HttpGet("[action]/{idServicioBuscar}")]
        public async Task<ActionResult> BuscarPoridServicio([FromRoute] int idServicioBuscar)
        {
            var servicio = await _context.Servicios.Include(c => c.estatus)
                .Where(c => c.idEstatus == 1).
                SingleOrDefaultAsync(c => c.idServicio == idServicioBuscar);

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(new ServiciosViewModel
            {
                idServicio = servicio.idServicio,
                nombre = servicio.nombre,
                idEstatus = servicio.idEstatus,
                estatus = servicio.estatus.estatus
            });
        }

        // POST: api/Servicios/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idServicio <= 0)
            {
                return BadRequest();
            }

            var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.idServicio == model.idServicio);

            if (servicio == null)
            {
                return NotFound();
            }
            servicio.nombre = model.nombre;

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

        // POST: api/Servicios/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;
            Servicio servicio= new Servicio
            {
                nombre = model.nombre,
                idEstatus = model.idEstatus,
                fechaRegistro = fechaHora
            };

            _context.Servicios.Add(servicio);
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
        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.idServicio == id);
        }
    }
}
