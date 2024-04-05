using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Rol
    {
        public int Id { get; set; }
        public int Idusuario { get; set; }
        public int Idtelefono { get; set; }
        public int Cantidad { get; set; }
        public int Preciototal { get; set; }
        public DateTime Fechaorden { get; set; }


        public Rol() // Constructor por defecto requerido para Dapper
        {
            Idusuario = 0;
            Idtelefono = 0;
            Cantidad = 0;
            Preciototal = 0;
            Fechaorden = DateTime.Now;
        }
        public Rol(int idusuario, int idtelefono, int cantidad, int preciototal, DateTime fechaorden)
        {
            Idusuario = idusuario;
            Idtelefono = idtelefono;
            Cantidad = cantidad;
            Preciototal = preciototal;
            Fechaorden = fechaorden;
        }
    }
}
