using EjemploDeProyecto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.BE
{
    public class BitacorasFiltered : IBitacorasFiltered
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public IList<IBitacoraFiltered> Bitacoras { get; set; } = new List<IBitacoraFiltered>();
    }
}
