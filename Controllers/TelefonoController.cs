using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using newapi6.models;
using Npgsql;
using Dapper;

namespace newapi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TelefonoController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public TelefonoController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetTelefonos")]
        public IEnumerable<Telefono> Get()
        {
            var telefonos = _connection.Query<Telefono>("SELECT * FROM telefono");
            return telefonos;
        }

        [HttpPost(Name = "PostTelefono")]
        public IActionResult Post([FromBody] Telefono telefono)
        {
            if (telefono == null)
            {
                return BadRequest("El objeto Agricultor no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO telefono (marca, modelo, memoriaram, memoriainterna, color, pantalla," +
            "descripcionprocesador, descripcioncamarafrontal, descripcioncamaraprincipal, descripcionbateria," +
            "versionsistemaoperativo, descripcionadicional, precio) " +
                        "VALUES (@Marca, @Modelo, @Memoriaram, @Memoriainterna, @Color, @Pantalla," +
                        "@Descripcionprocesador, @Descripcioncamarafrontal, @Descripcioncamaraprincipal, @Descripcionbateria," +
                        "@Versionsistemaoperativo, @Descripcionadicional, @Precio)";

            _connection.Execute(query, telefono);

            // Devolver una respuesta exitosa
            return Ok("Telefono creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateTelefono")]
        public IActionResult Put(int id, [FromBody] Telefono telefono)
        {
            if (telefono == null)
            {
                return BadRequest("El objeto Agricultor no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingTelefono = _connection.QuerySingleOrDefault<Telefono>("SELECT * FROM telefono WHERE Id = @Id", new { Id = id });

            if (existingTelefono == null)
            {
                return NotFound($"Telefono con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE telefono SET marca = @Marca, modelo = @Modelo, memoriaram = @Memoriaram, " +
                        "memoriainterna = @Memoriainterna, color = @Color, pantalla = @Pantalla, descripcionprocesador = @Descripcionprocesador," + 
                        "descripcioncamarafrontal = @Descripcioncamarafrontal, descripcioncamaraprincipal = @Descripcioncamaraprincipal," +
                        "descripcionbateria = @Descripcionbateria, versionsistemaoperativo = @Versionsistemaoperativo," +
                        "descripcionadicional = @Descripcionadicional, precio = @Precio WHERE Id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                telefono.Marca,
                telefono.Modelo,
                telefono.Memoriaram,
                telefono.Memoriainterna,
                telefono.Color,
                telefono.Pantalla,
                telefono.Descripcionprocesador,
                telefono.Descripcioncamarafrontal,
                telefono.Descripcioncamaraprincipal,
                telefono.Descripcionbateria,
                telefono.Versionsistemaoperativo,
                telefono.Descripcionadicional,
                telefono.Precio
            });

            // Devolver una respuesta exitosa
            return Ok($"Telefono con ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteTelefono")]
        public IActionResult Delete(int id)
        {
            // Verificar si el agricultor con el ID proporcionado existe
            var existingTelefono = _connection.QuerySingleOrDefault<Telefono>("SELECT * FROM telefono WHERE Id = @Id", new { Id = id });

            if (existingTelefono == null)
            {
                return NotFound($"Agricultor con ID {id} no encontrado");
            }

            // Eliminar el agricultor de la base de datos
            var query = "DELETE FROM telefono WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });

            // Devolver una respuesta exitosa
            return Ok($"Telefono con ID {id} eliminado exitosamente");
        }


    }
}