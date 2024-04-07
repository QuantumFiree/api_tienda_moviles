using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newapi6.models;
using Npgsql;
using Dapper;
using api_tienda_moviles.Models;

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

        public IEnumerable<Usuario> GetUsuarioPorCorreo(string correo)
        {
            var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario WHERE correo = @Correo", new {Correo = correo});
            return usuarios;
        }

        [HttpPost(Name = "PostUsuario")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario  == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO usuario (correo, clave, nombres, apellidos, telefono, rol) " +
                        "VALUES (@Correo, @Clave, @Nombres, @Apellidos, @Telefono, @Rol)";

            _connection.Execute(query, usuario);

            // Devolver una respuesta exitosa
            return Ok("Usuario creado exitosamente");
        }

        [HttpPut(Name = "UpdateUsuario")]
        public IActionResult Put([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingUsuario= _connection.QuerySingleOrDefault<Usuario>("SELECT * FROM usuario WHERE id = @Id", new { Id = usuario.Id });

            if (existingUsuario == null)
            {
                return NotFound($"Usuario con ID {usuario.Id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE usuario SET correo = @Correo, clave = @Clave, nombres = @Nombres, " +
                "apellidos = @Apellidos, telefono = @Telefono, rol = @Rol" +
                        " WHERE id = @Id";

            _connection.Execute(query, new
            {
                usuario.Correo,
                usuario.Clave,
                usuario.Nombres,
                usuario.Apellidos,
                usuario.Telefono,
                usuario.Rol,
                usuario.Id,
            });

            // Devolver una respuesta exitosa
            return Ok($"Usuario con ID {usuario.Id} actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteUsuario")]
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