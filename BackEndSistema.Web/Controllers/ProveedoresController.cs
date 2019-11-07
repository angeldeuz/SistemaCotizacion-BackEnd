using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.Proveedor;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProveedoresController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Proveedores/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorViewModel>> Listar()
        {
            var proveedor = await _context.Proveedores.ToListAsync();

            return proveedor.Select(c => new ProveedorViewModel
            {
                idProveedor = c.idProveedor,
                razonSocial = c.razonSocial,
                diasCredito = c.diasCredito,
                RFC = c.RFC,
                correo = c.correo,
                nombreVendedor = c.nombreVendedor
            });
        }

        // GET: api/Proveedores/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute] int id)
        {
            var c = await _context.Proveedores.
                SingleOrDefaultAsync(cl => cl.idProveedor == id);

            if (c == null)
            {
                return NotFound();
            }

            return Ok(new ProveedorViewModel
            {
                razonSocial = c.razonSocial,
                diasCredito = c.diasCredito,
                RFC = c.RFC,
                correo = c.correo,
                nombreVendedor = c.nombreVendedor
            });
        }

        // POST: api/Proveedores/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProveedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idProveedor <= 0)
            {
                return BadRequest();
            }

            var proveedor = await _context.Proveedores.FirstOrDefaultAsync(c => c.idProveedor == model.idProveedor);

            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor.razonSocial = model.razonSocial;
            proveedor.diasCredito = model.diasCredito;
            proveedor.RFC = model.RFC;
            proveedor.nombreVendedor = model.nombreVendedor;
            //cliente.fechaRegistro = model.fechaRegistro;
            proveedor.correo = model.correo;


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

        // POST: api/Proveedores/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;
            Proveedor proveedor = new Proveedor
            {
                razonSocial = model.razonSocial,
                diasCredito = model.diasCredito,
                RFC = model.RFC,
                correo = model.correo,
                nombreVendedor = model.nombreVendedor

            };

            _context.Proveedores.Add(proveedor);
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
        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.idProveedor == id);
        }
    }
}
