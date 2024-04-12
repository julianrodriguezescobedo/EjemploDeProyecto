using EjemploDeProyecto.BE;
using EjemploDeProyecto.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.BLL
{
    public class PersonaBLL
    {
        private PersonaDAL personaDAL;

        public PersonaBLL()
        {
            personaDAL = new PersonaDAL();
        }

        public void Add(Persona persona)
        {
            try
            {
                personaDAL.Add(persona);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
