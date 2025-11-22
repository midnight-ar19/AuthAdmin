using AuthAdmin.Data;
using AuthAdmin.DTOs;
using AuthAdmin.Models;
using AuthAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAdmin.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ConfiguracionJwtServices _configuracionJwtServices;

        public UsuarioController(AppDbContext appDbContext, ConfiguracionJwtServices configuracionJwtServices)
        {
            _context = appDbContext;
            _configuracionJwtServices = configuracionJwtServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Get()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new  UsuarioResponse
                    {
                        Id = u.Id,
                        NombreApellido = u.Nombre + " "+ u.Apellido,
                        Correo = u.Correo
                    })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] UsuarioRequest request)
        {

            var existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo.Equals(request.Correo));
            if (existeCorreo)
            {
                return BadRequest("El correo ya ha sido registrado");
            }

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Correo = request.Correo
            };

            var hasher = new PasswordHasher<Usuario>();
            usuario.Contrasena = hasher.HashPassword(usuario, request.Contrasena);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var response = new UsuarioResponse
            {
                Id = usuario.Id,
                NombreApellido = usuario.Nombre + " " + usuario.Apellido,
                Correo = usuario.Correo
            };

            return CreatedAtAction(nameof(Get), new {id = usuario.Id}, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == request.Correo);
            if(usuario == null) return Unauthorized();

            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.Contrasena, request.Contrasena);


            var token = _configuracionJwtServices.GenerarJwtToken(usuario);

            if (resultado == PasswordVerificationResult.Success)
            {
                return Ok(new { mensaje = "Login correcto", token});
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
