using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.Interfaces
{
    public interface IBitacoraFilters
    {
        string Usuario { get; set; }
        DateTime Desde { get; set; }
        DateTime Hasta { get; set; }
        BitacoraTipo Tipo { get; set; }
        string Like { get; set; }
    }
}
