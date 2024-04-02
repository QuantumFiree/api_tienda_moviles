using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }

        public Usuario() // Constructor por defecto requerido para Dapper
        {
            Nombres = "";
            Apellidos = "";
            Telefono = 0;
            Correo = "";
        }

        public Usuario(string nombres, string apellidos, string correo, int telefono)
        {
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Telefono = telefono;
        }
    }
}
