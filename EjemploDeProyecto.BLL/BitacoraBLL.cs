using EjemploDeProyecto.BE;
using EjemploDeProyecto.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.BLL
{
    public class BitacoraBLL
    {
        private BitacoraDAL bitacoraDAL;

        public BitacoraBLL()
        {
            bitacoraDAL = new BitacoraDAL();
        }

        public void Add(Bitacora bitacora)
        {
            try
            {
                bitacoraDAL.Add(bitacora);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
