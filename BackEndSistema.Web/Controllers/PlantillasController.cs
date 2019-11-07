using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.Plantilla;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PlantillasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Plantillas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PlantillaViewModel>> Listar()
        {
            var plantilla = await _context.Plantillas.Include(c => c.estatus).ToListAsync();

            return plantilla.Select(c => new PlantillaViewModel
            {
                idPlantilla = c.idPlantilla,
                nombre = c.nombre,
                correo = c.correo,
                idEstatus = c.idEstatus,
                estatus= c.estatus.estatus,
                fechaRegistro = c.fechaRegistro
            });
        }

        // GET: api/Plantillas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var plantilla = await _context.Plantillas.Include(c => c.estatus).
                SingleOrDefaultAsync(c => c.idPlantilla == id);

            if (plantilla == null)
            {
                return NotFound();
            }

            return Ok(new PlantillaViewModel
            {
                idPlantilla = plantilla.idPlantilla,
                nombre = plantilla.nombre,
                correo = plantilla.correo,
                idEstatus = plantilla.idEstatus,
                estatus = plantilla.estatus.estatus,
                fechaRegistro = plantilla.fechaRegistro
            });
        }

        // POST: api/Plantillas/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idPlantilla <= 0)
            {
                return BadRequest();
            }

            var plantilla = await _context.Plantillas.FirstOrDefaultAsync(c => c.idPlantilla == model.idPlantilla);

            if (plantilla == null)
            {
                return NotFound();
            }

            plantilla.nombre = model.nombre;
            plantilla.correo = model.correo;
            plantilla.idEstatus = model.idEstatus;


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

        // POST: api/Plantillas/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;
            Plantilla plantilla = new Plantilla
            {
                nombre = model.nombre,
                correo = model.correo,
                idEstatus = 1,
                fechaRegistro = fechaHora

            };

            _context.Plantillas.Add(plantilla);
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

        private bool PlantillaExists(int id)
        {
            return _context.Plantillas.Any(e => e.idPlantilla == id);
        }
    }
}
