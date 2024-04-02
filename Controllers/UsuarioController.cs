using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newapi6.models;
using Npgsql;
using Dapper;

namespace newapi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public UsuarioController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetUsuarios")]
        public IEnumerable<Usuario> Get()
        {
            var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario");
            return usuarios;
        }

        [HttpPost(Name = "PostUsuario")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO usuario (nombres, apellidos, correo, telefono) " +
                        "VALUES (@Nombres, @Apellidos, @Correo, @Telefono)";

            _connection.Execute(query, usuario);

            // Devolver una respuesta exitosa
            return Ok("Usuario creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateUsuario")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingUsuario = _connection.QuerySingleOrDefault<Usuario>("SELECT * FROM usuario WHERE Id = @Id", new { Id = id });

            if (existingUsuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE usuario SET nombres = @Nombres, apellidos = @Apellidos," +
                        "correo = @Correo, telefono = @Telefono";

            _connection.Execute(query, new
            {
                Id = id,
                usuario.Nombres,
                usuario.Apellidos,
                usuario.Correo,
                usuario.Telefono
            });

            // Devolver una respuesta exitosa
            return Ok($"Usuario con ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteUsuaio")]
        public IActionResult Delete(int id)
        {
            // Verificar si el agricultor con el ID proporcionado existe
            var existingUsuario = _connection.QuerySingleOrDefault<Usuario>("SELECT * FROM usuario WHERE Id = @Id", new { Id = id });

            if (existingUsuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }

            // Eliminar el agricultor de la base de datos
            var query = "DELETE FROM usuario WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });

            // Devolver una respuesta exitosa
            return Ok($"Usuario con ID {id} eliminado exitosamente");
        }


    }
}