using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.OC;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public OCsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/OCs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<OCViewModel>> Listar()
        {
            var oc = await _context.OCs
                .Include(i => i.proveedores)
                .ToListAsync();

            return oc.Select(i => new OCViewModel
            {
                idOc = i.idOc,
                idProveedor = i.idProveedor,
                proveedor = i.proveedores.nombreVendedor,
                precio = i.precio,
                idCotizacion = i.idCotizacion,
                fechaRegistro = i.fechaRegistro


            });
        }

       
        // GET: api/OCs/ListarDetalles/125
        [HttpGet("[action]/{idOc}")]
        public async Task<IEnumerable<MostrarDetalleViewModel>> ListarDetalles([FromRoute] int idOc)
        {
            var detalle = await _context.Detalles_OCs
                .Include(a => a.productos)
                .Where(d => d.idOc == idOc)
                .ToListAsync();

            return detalle.Select(d => new MostrarDetalleViewModel
            {
                idProducto = d.idProducto,
                producto = d.productos.nombre,
                cantidad = d.cantidad
            }); ;
        }

        // POST: api/OCs/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;

            OC oc = new OC
            {
                idProveedor = model.idProveedor,
                precio = model.precio,
                idCotizacion = model.idCotizacion,
                fechaRegistro = fechaHora
            };


            try
            {
                _context.OCs.Add(oc);
                await _context.SaveChangesAsync();
                var id = oc.idOc;
                foreach (var det in model.detalles)
                {
                    Detalles_OC detalle = new Detalles_OC
                    {
                        idOc = id,
                        idProducto = det.idProducto,
                        cantidad = det.cantidad
                    };
                    _context.Detalles_OCs.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok();
        }

        private bool OCExists(int id)
        {
            return _context.OCs.Any(e => e.idOc == id);
        }
    }
}
