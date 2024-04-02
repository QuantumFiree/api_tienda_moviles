using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Telefono
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Memoriaram { get; set; }
        public int Memoriainterna { get; set; }
        public string Color { get; set; }
        public string Pantalla { get; set; }
        public string Descripcionprocesador { get; set; }
        public string Descripcioncamarafrontal { get; set; }
        public string Descripcioncamaraprincipal { get; set; }
        public string Descripcionbateria { get; set; }
        public string Versionsistemaoperativo { get; set; }
        public string Descripcionadicional { get; set; }
        public int Precio { get; set; }

        public Telefono() // Constructor por defecto requerido para Dapper
        {
            Marca = "";
            Modelo = "";
            Memoriaram = 0;
            Memoriainterna = 0;
            Color = "";
            Pantalla = "";
            Descripcionprocesador = "";
            Descripcioncamarafrontal = ""; 
            Descripcioncamaraprincipal = "";
            Descripcionbateria = "";
            Versionsistemaoperativo = "";
            Descripcionadicional = "";
            Precio = 0;
        }

        public Telefono(string marca, string modelo, int memoriaRAM, int memoriaInterna, string color, string pantalla, string descripcionProcesador, string descripcionCamaraFrontal, string descripcionCamaraPrincipal, string descripcionBateria, string versionSistemaOperativo, string descripcionAdicional, int precio)
        {
            Marca = marca;
            Modelo = modelo;
            Memoriaram = memoriaRAM;
            Memoriainterna = memoriaInterna;
            Color = color;
            Pantalla = pantalla;
            Descripcionprocesador = descripcionProcesador;
            Descripcioncamarafrontal = descripcionCamaraFrontal;
            Descripcioncamaraprincipal = descripcionCamaraPrincipal;
            Descripcionbateria = descripcionBateria;
            Versionsistemaoperativo = versionSistemaOperativo;
            Descripcionadicional = descripcionAdicional;
            Precio = precio;
        }
    }
}
