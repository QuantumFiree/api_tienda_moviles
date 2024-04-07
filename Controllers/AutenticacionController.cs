using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_tienda_moviles.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using api_tienda_moviles.Controllers;
using newapi6.Controllers;
using Npgsql;
using Dapper;
namespace api_tienda_moviles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string? SecretKey;
        private readonly NpgsqlConnection _connection;
        public AutenticacionController(IConfiguration config, NpgsqlConnection connection)
        {
            _connection = connection;
            SecretKey = config.GetSection("Settings").GetSection("SecretKey").ToString() ?? "vacio";
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario usuario)
        {
            var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario WHERE correo = @Correo", new { Correo = usuario.Correo });
            var usuarioRes = usuarios.FirstOrDefault();
            //var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario WHERE correo = @Correo", new { Correo = usuario.Correo });

            if (usuarioRes != null && usuario.Correo == usuarioRes.Correo && usuario.Clave == usuarioRes.Clave)
            {
                var keyBytes = Encoding.ASCII.GetBytes(SecretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            } else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }

        }
    }
}
