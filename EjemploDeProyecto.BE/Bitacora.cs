using EjemploDeProyecto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.BE
{
    public class Bitacora : Entity, IBitacora
    {
        public DateTime Fecha { get; set; }
        public BitacoraTipo Tipo { get; set; }
        public string Usuario { get; set; }
        public string Mensaje { get; set; }
    }
}