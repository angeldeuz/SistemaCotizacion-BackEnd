using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndSistema.Datos;
using BackEndSistema.Entidades.Usuarios;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BackEndSistema.Web.Models.Usuarios.Usuario;

namespace BackEndSistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private readonly IConfiguration _config;

        public UsuariosController(DbContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(u => u.estatus).ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                idUsuario = u.idUsuario,
                usuario = u.usuario,
                fechaRegistro = u.fechaRegistro,
                idEstatus = u.idEstatus,
                estatus = u.estatus.estatus,
                password_hash = u.password_hash,
                Rol = u.Rol

            });
        }

        // POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = model.usuario.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.usuario == usuario))
            {
                return BadRequest("El usuario ya existe");
            }


            var fecha = DateTime.Now;
            //CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario uss = new Usuario
            {
                usuario = model.usuario,
                fechaRegistro = fecha,
                idEstatus = 1,
                password_hash = model.password,
                password_salt = model.password,
                Rol = "Administrador"
            };

            _context.Usuarios.Add(uss);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Usuarios/Actualizar
        //[Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idUsuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.idUsuario == model.idUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usuario = model.usuario;
            usuario.idEstatus = model.idEstatus;
            usuario.Rol = model.Rol;

            if (model.act_password == true)
            {
                //CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = model.password;
                usuario.password_salt = model.password;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

       

         // POST: api/Usuarios/Login   
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var uss = model.usuario.ToLower();

            //var uss = await _context.Usuarios.Where(u => u.condicion == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.email == email);
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.usuario == uss);

            if (usuario == null)
            {
                return NotFound();
            }

            //if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
            //{
            //    return NotFound();
            //}

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
                new Claim(ClaimTypes.Name, uss),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("idUsuario", usuario.idUsuario.ToString()),
                new Claim("Rol", usuario.Rol),
                new Claim("nombre", usuario.usuario)
            };

            return Ok(
                    new { token = GenerarToken(claims) }
                );

        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idUsuario == id);
        }
    }
}
