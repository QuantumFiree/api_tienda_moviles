using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class SesionesChat
    {
        public int Id { get; set; }
        public int Idusuario { get; set; }
        public DateTime Fechasesion { get; set; }
        public string Mensajesesion { get; set; }
        public string Respuestachatbot { get; set; }


        public SesionesChat() // Constructor por defecto requerido para Dapper
        {
            Idusuario = 0;
            Fechasesion = DateTime.Now;
            Mensajesesion = "";
            Respuestachatbot = "";
        }
        public SesionesChat(int idusuario, DateTime fechasesion, string mensajesesion, string respuestachatbot)
        {
            Idusuario = idusuario;
            Fechasesion = fechasesion;
            Mensajesesion = mensajesesion;
            Respuestachatbot = respuestachatbot;
        }
    }
}
