using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Cotizacion;
using BackEndSistema.Web.Models.Cotizacion.CotizacionE;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CotizacionesController(DbContextSistema context)
        {
            _context = context;
        }



        // GET: api/Cotizacion/Mostrar/idCotizacion
        [HttpGet("[action]/{idCotizacion}")]
        public async Task<IEnumerable<CotizacionViewModel>> Mostrar([FromRoute] int idCotizacion)
        {
            var idcliente = 0;
            

            var SelectCotizacion = await _context.Cotizaciones.FindAsync(idCotizacion);
            idcliente = SelectCotizacion.idCliente;
            
            //Listar Clientes
             var cliente = await _context.Clientes
                .Include(c => c.tipocliente)
                .Where(c => c.idCliente == idcliente)
                .ToListAsync();
            var Clientes = cliente.Select(c => new objCliente
            {
                idCliente = idcliente,
                nombre = c.nombre,
                rfc = c.RFC,
                razonSocial = c.razonSocial,
                idTipoCliente = c.idTipoCliente,
                tipoCliente = c.tipocliente.tipoCliente,
                correo = c.correo
            });
            

            //listar los servicios
            var detalleCotizacion = await _context.DetallesCotizacion
                .Where(d => d.idCotizacion == idCotizacion)
                .OrderByDescending(d => d.idServicio)
                .ToListAsync();
            var serv = detalleCotizacion
                .Select(m => m.idServicio).Distinct();
            var cats = detalleCotizacion
               .Select(m => m.idCategoria).Distinct();
            var prods = detalleCotizacion
               .Select(m => m.idProducto).Distinct();

            
            InfoViewModel jsonObject = new InfoViewModel();
            jsonObject.servicios = new List<objServicios>();
            foreach ( var i in serv)
            {
                var servicio = await _context.Servicios.FindAsync(i);
                jsonObject.categorias = new List<objCategorias>();
                foreach (var ii in cats)
                {
                    var categoria = await _context.Categorias
                    .Where(c => c.idServicio == i)
                    //.Where(s => cats.Contains(s.idCategoria))
                    .Where(c => c.idCategoria == ii)
                    .SingleOrDefaultAsync();
                    jsonObject.productos = new List<objProductos>();
                    foreach (var iii in prods)
                    {
                        var producto = await _context.Productos
                        .Where(p => p.idCategoria == ii)
                        .Where(p => p.idProducto == iii)
                        .SingleOrDefaultAsync();
                        var dtcotizacion = await _context.DetallesCotizacion
                            .Where(d => d.idCotizacion == idCotizacion)
                            .Where(d => d.idProducto == iii)
                            .SingleOrDefaultAsync();
                        if (producto != null && dtcotizacion != null)
                        {
                            jsonObject.productos.Add(new objProductos()
                            {
                                idProducto = producto.idProducto,
                                clave = producto.clave,
                                unidadMedida = producto.unidadMedida,
                                stock = producto.stock,
                                precioProducto = producto.precioProducto,
                                manoObra = producto.manoObra,
                                precio = producto.precio,
                                cantidad = Convert.ToString(dtcotizacion.cantidad),
                                monto = Convert.ToString(dtcotizacion.monto)
                            });
                        }
                    }
                    if (categoria != null)
                    {
                        jsonObject.categorias.Add(new objCategorias()
                        {
                            idcategoria = categoria.idCategoria,
                            nombreCategoria = categoria.nombre,
                            objproductos = jsonObject.productos
                        });
                    }

                }
                jsonObject.servicios.Add(new objServicios()
                {
                    idServicio = servicio.idServicio,
                    nombreServicio = servicio.nombre,
                    objcategorias = jsonObject.categorias
                });
            }
            jsonObject.categorias = new List<objCategorias>();
            jsonObject.productos = new List<objProductos>();

            var cotizacion = await _context.Cotizaciones
                .Include(c => c.plantillas)
                .Include(c => c.cliente)
                .Include(c => c.estatus)
                .Where(c => c.idCotizacion == idCotizacion)
                .ToListAsync();
            return cotizacion.Select(c => new CotizacionViewModel
            {
                idCotizacion = idCotizacion,
                fechaRegistro = c.fechaRegistro,
                idCliente = c.idCliente,
                nombre = c.cliente.nombre,
                idEstatus = c.idEstatus,
                estatus = c.estatus.estatus,
                clientes = Clientes,
                total = c.total,
                moneda = c.moneda,
                tipoCambio = c.tipoCambio,
                descuento = c.descuento,
                aumento = c.aumento,
                idPlantilla = c.idPlantilla,
                horasPlaneadas = c.horasPlaneadas,
                horasFinales = c.horasFinales,
                nombreP = c.plantillas.nombre,
                DatosCotizacion = jsonObject

            });
        }

        // GET: api/Cotizacion/Listado
        [HttpGet("[action]")]
        public async Task<IEnumerable<CotizacionViewModel>> Listado()
        {
            var cotizacion = await _context.Cotizaciones
                .Include(c => c.estatus)
                .Include(c => c.usuarios)
                .Include(c => c.cliente)
                .Include(c => c.plantillas)
                .ToListAsync();
            return cotizacion.Select(c => new CotizacionViewModel
            {
                idCotizacion = c.idCotizacion,
                fechaRegistro = c.fechaRegistro,
                idCliente = c.idCliente,
                nombre = c.cliente.nombre,
                total = c.total,
                moneda = c.moneda,
                tipoCambio = c.tipoCambio,
                descuento = c.descuento,
                aumento = c.aumento,
                idEstatus = c.idEstatus,
                estatus = c.estatus.estatus,
                horasPlaneadas = c.horasPlaneadas,
                horasFinales = c.horasFinales,
                idPlantilla = c.idPlantilla,
                nombreP = c.plantillas.nombre
         

            });
        }

        // POST: api/Cotizaciones/Crear no funciona!
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;

            CotizacionE cotizacion = new CotizacionE
            {
                idUsuario = model.idUsuario,
                fechaRegistro = fechaHora,
                fechaActualizacion = fechaHora,
                idCliente = model.idCliente,
                idEstatus = model.idEstatus,
                //subtotal = model.subtotal,
                //iva = model.iva,
                total = model.total,
                descuento  =  model.descuento,
                aumento  = model.aumento
            };


            try
            {
                _context.Cotizaciones.Add(cotizacion);
                await _context.SaveChangesAsync();
                var id = cotizacion.idCotizacion;
                foreach (var det in model.detalles)
                {
                    Detalles_Cotizacion detalle = new Detalles_Cotizacion
                    {
                        idCotizacion = id,
                        cantidad = det.cantidad,
                        //unidadMedida = det.unidadMedida,
                        //precioUnitario = det.precioUnitario,
                       //descripcion  = det.descripcion,
                        idProducto = det.idProducto
                    };
                    _context.DetallesCotizacion.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok();
        }

        // POST: api/Cotizaciones/Guardar
        [HttpPost("[action]")]
        public async Task<IActionResult> Guardar([FromBody] objProyecto model)
        {
            var fechaHora = DateTime.Now;
            var holi = model.objcliente.idCliente;
            var aumento = 0;
            switch (model.objcliente.tipoCliente)
            {
                case "A":
                    aumento = 10;
                        break;
                case "B":
                    aumento = 20;
                    break;
                case "C":
                    aumento = 30;
                    break;
                case "D":
                    aumento = 40;
                    break;

            }
            CotizacionE cotizacion = new CotizacionE
            {
                idCliente = model.objcliente.idCliente,
                idUsuario = 3,
                idEstatus = 1,
                total = model.totalProyecto,
                moneda = model.objmoneda.moneda,
                tipoCambio = Convert.ToDecimal(model.objmoneda.tipoCambio),
                descuento = model.descuento,
                aumento = aumento,
                idPlantilla = 0,
                fechaRegistro = fechaHora,
                fechaActualizacion = fechaHora



            };
           
            try
            {
                _context.Cotizaciones.Add(cotizacion);
                await _context.SaveChangesAsync();
                var id = cotizacion.idCotizacion;
                foreach (var serv in model.objservicios)
                {
                    var idser = serv.idServicio;
                    foreach (var cat in serv.objcategorias)
                    {
                        var idcat = cat.idcategoria;
                        foreach(var pro in cat.objproductos)
                        {
                            Detalles_Cotizacion detalle = new Detalles_Cotizacion
                            {
                                idCotizacion = id,
                                idProducto = pro.idProducto,
                                cantidad = Convert.ToInt32(pro.cantidad),
                                monto = Convert.ToDecimal(pro.monto),
                                comentarios = "",
                                fechaRegistro = fechaHora,
                                idServicio = idser,
                                idCategoria = idcat
                            };
                            _context.DetallesCotizacion.Add(detalle);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        // POST: api/Cotizaciones/Actualizar
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] objProyectoActualizar model)
        {
            var fechaHora = DateTime.Now;
            var holi = model.objcliente.idCliente;
            var aumento = 0;
            switch (model.objcliente.tipoCliente)
            {
                case "A":
                    aumento = 10;
                    break;
                case "B":
                    aumento = 20;
                    break;
                case "C":
                    aumento = 30;
                    break;
                case "D":
                    aumento = 40;
                    break;

            }

            var cotizacion = await _context.Cotizaciones.FirstOrDefaultAsync(c => c.idCotizacion == model.idCotizacion);
            if (cotizacion == null)
            {
                return NotFound();
            }
            cotizacion.idCliente = model.objcliente.idCliente;
            cotizacion.idUsuario = 3;
            cotizacion.idEstatus = 1;
            cotizacion.total = model.totalProyecto;
            cotizacion.moneda = model.objmoneda.moneda;
            cotizacion.tipoCambio = Convert.ToDecimal(model.objmoneda.tipoCambio);
            cotizacion.descuento = model.descuento;
            cotizacion.aumento = aumento;
            cotizacion.idPlantilla = 0;
            cotizacion.fechaActualizacion = fechaHora;
            try
            {
                await _context.SaveChangesAsync();
                var id = model.idCotizacion;
                foreach (var serv in model.objservicios)
                {
                    var idser = serv.idServicio;
                    foreach (var cat in serv.objcategorias)
                    {
                        var idcat = cat.idcategoria;
                        foreach (var pro in cat.objproductos)
                        {
                            var det = await _context.DetallesCotizacion.FirstOrDefaultAsync(a => a.idProducto == pro.idProducto);
                            if (det == null)
                            {
                                det.idCotizacion = id;
                                det.idProducto = pro.idProducto;
                                det.cantidad = Convert.ToInt32(pro.cantidad);
                                det.monto = Convert.ToDecimal(pro.monto);
                                det.comentarios = "";
                                det.fechaRegistro = fechaHora;
                                det.idServicio = idser;
                                det.idCategoria = idcat;

                                try
                                {
                                    await _context.SaveChangesAsync();
                                }
                                catch (DbUpdateConcurrencyException)
                                {
                                    //Guardar Excepcion
                                    return BadRequest();
                                }
                            }
                            else
                            {
                                Detalles_Cotizacion detalle = new Detalles_Cotizacion
                                {
                                    idCotizacion = id,
                                    idProducto = pro.idProducto,
                                    cantidad = Convert.ToInt32(pro.cantidad),
                                    monto = Convert.ToDecimal(pro.monto),
                                    comentarios = "",
                                    fechaRegistro = fechaHora,
                                    idServicio = idser,
                                    idCategoria = idcat
                                };
                                _context.DetallesCotizacion.Add(detalle);
                            }    
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        // Post: api/Cotizaciones/AsignarCotizacion
        [HttpPost("[action]")]
        public async Task<IActionResult> AsignarCotizacion([FromBody] AsignarCotizacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idCotizacion <= 0)
            {
                return BadRequest();
            }

            var cotizacion = await _context.Cotizaciones.FirstOrDefaultAsync(c => c.idCotizacion == model.idCotizacion);

            if (cotizacion == null)
            {
                return NotFound();
            }
            var fecha = DateTime.Now;

            cotizacion.fechaActualizacion = fecha;
            cotizacion.horasPlaneadas = model.horasPlaneadas;
            cotizacion.idPlantilla = model.idPlantilla;
            cotizacion.idEstatus = 1;

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

        // Post: api/Cotizaciones/FinalizarCotizacion
        [HttpPost("[action]")]
        public async Task<IActionResult> FinalizarCotizacion([FromBody] FinalizarCotizacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idCotizacion <= 0)
            {
                return BadRequest();
            }

            var cotizacion = await _context.Cotizaciones.FirstOrDefaultAsync(c => c.idCotizacion == model.idCotizacion);

            if (cotizacion == null)
            {
                return NotFound();
            }
            var fecha = DateTime.Now;

            cotizacion.fechaActualizacion = fecha;
            cotizacion.horasFinales = model.horasFinales;
            cotizacion.idEstatus = 1;

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

        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> CotizacionMes12()
        {
            var consulta = await _context.Cotizaciones
                .GroupBy(c => c.fechaActualizacion.Month)
                .Select(x => new { etiqueta = x.Key, valor = x.Sum(c => c.total) })
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor
            });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EliminarDetalle([FromBody] EliminarViewModel model)
        {
            var detalle = await _context.DetallesCotizacion.FindAsync(model.idDetalle);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.DetallesCotizacion.Remove(detalle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //faltan de hacer la consulta bien
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> IngresosvsEngresos()
        {
            var consulta = await _context.Cotizaciones
                .GroupBy(c => c.fechaActualizacion.Month)
                .Select(x => new { etiqueta = x.Key, valor = x.Sum(c => c.total) })
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor
            });

        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> RealvsPlaneado()
        {
            var consulta = await _context.Cotizaciones
                .Where(c => c.fechaActualizacion.Year == 2019)
                .GroupBy(c => c.fechaActualizacion.Month)
                .Select(x => new { etiqueta = x.Key, valor = x.Sum(c => c.horasPlaneadas), valor2 = x.Sum(c => c.horasFinales) })
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor,
                valor2 = v.valor2
            });

        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> NumCotizaciones()
        {
            var consulta = await _context.Cotizaciones
                .GroupBy(c => c.fechaActualizacion.Month)
                .Select(x => new { etiqueta = x.Key, valor = x.Count(c => c.idEstatus == 6), valor2 =  x.Count(c => c.idEstatus == 7), valor3 = x.Count(c => c.idEstatus == 3)})
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor,
                valor2 = v.valor2,
                valor3 = v.valor3
            });

        }

        private bool CotizacionEExists(int id)
        {
            return _context.Cotizaciones.Any(e => e.idCotizacion == id);
        }
    }
}
