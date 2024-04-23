using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.Interfaces
{
    public interface IBitacorasFiltered
    {
        int Page { get; set; }
        int PerPage { get; set; }
        int Total { get; set; }
        int TotalPages { get; set; }
        IList<IBitacoraFiltered> Bitacoras { get; set; }
    }
}
