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
    public class OrdenesController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public OrdenesController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetOrdenes")]
        public IEnumerable<Ordenes> Get()
        {
            var ordenes = _connection.Query<Ordenes>("SELECT * FROM ordenes");
            return ordenes;
        }

        [HttpPost(Name = "PostOrdenes")]
        public IActionResult Post([FromBody] Ordenes orden)
        {
            try
            {
                if (orden == null)
                {
                    return BadRequest("El objeto orden no puede ser nulo");
                }

                // Insertar el agricultor en la base de datos
                var query = "INSERT INTO ordenes (idusuario, idtelefono, cantidad, preciototal, fechaorden) " +
                            "VALUES (@Idusuario, @Idtelefono, @Cantidad, @Preciototal, @Fechaorden)";

                _connection.Execute(query, orden);

                // Devolver una respuesta exitosa
                return Ok("Orden creada exitosamente");
            }
            catch (Exception ex)
            {
                // Manejar el error y devolver una respuesta adecuada
                Console.WriteLine($"Se produjo un error al procesar la solicitud: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Se produjo un error interno en el servidor");
            }
        }


        [HttpPut("{id}", Name = "UpdateOrdenes")]
        public IActionResult Put(int id, [FromBody] Ordenes orden)
        {
            if (orden == null)
            {
                return BadRequest("El objeto Orden no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingOrden = _connection.QuerySingleOrDefault<Ordenes>("SELECT * FROM ordenes WHERE Id = @Id", new { Id = id });

            if (existingOrden == null)
            {
                return NotFound($"Orden con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE ordenes SET idusuario = @Idusuario, idtelefono = @Idtelefono, " +
                        "cantidad = @Cantidad, preciototal = @Preciototal, fechaorden = @Fechaorden";

            _connection.Execute(query, new
            {
                Id = id,
                orden.Idusuario,
                orden.Idtelefono,
                orden.Cantidad,
                orden.Preciototal,
                orden.Fechaorden,
            });

            // Devolver una respuesta exitosa
            return Ok($"Orden con ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteOrdenes")]
        public IActionResult Delete(int id)
        {
            // Verificar si el agricultor con el ID proporcionado existe
            var existingOrden = _connection.QuerySingleOrDefault<Ordenes>("SELECT * FROM ordenes WHERE Id = @Id", new { Id = id });

            if (existingOrden == null)
            {
                return NotFound($"Orden con ID {id} no encontrado");
            }

            // Eliminar el agricultor de la base de datos
            var query = "DELETE FROM ordenes WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });

            // Devolver una respuesta exitosa
            return Ok($"Orden con ID {id} eliminado exitosamente");
        }


    }
}