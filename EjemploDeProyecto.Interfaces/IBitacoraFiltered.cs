using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.Interfaces
{
    public interface IBitacoraFiltered
    {
        int Index { get; set; }
        int Id { get; set; }
        DateTime Fecha { get; set; }
        BitacoraTipo Tipo { get; set; }
        string Usuario { get; set; }
        string Mensaje { get; set; }
    }
}
