using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamenBackendMEJIA.Contexto;
using ExamenBackendMEJIA.Modelo;
using ExamenBackendMEJIA.JWT;

namespace ExamenBackendMEJIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoController : ControllerBase
    {
        private readonly ExamenBackendContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AuthoController(ExamenBackendContext context, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost("Registrar")]
        public IActionResult Register([FromBody] Usuario user)
        {
            var usuarioExistente = _context.Usuarios.Any(u => u.NombreUsuario == user.NombreUsuario); ;

            if (usuarioExistente)
            {
                return BadRequest(new { Message = "El Nombre de Usuario ya está registrado" });
            }

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return Ok(new { Message = "Usuario registrado" });
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Usuarios.SingleOrDefault(u => u.NombreUsuario == request.Username && u.Contrasena == request.Password);
            if (user == null) return Unauthorized();

            var token = _jwtTokenService.GenerateToken(user.NombreUsuario);
            return Ok(new { Token = token });
        }
    }
}
