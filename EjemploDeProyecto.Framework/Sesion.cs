using EjemploDeProyecto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.Framework
{
    public class Sesion
    {
        private static Sesion _sesion;

        private IUser User { get; set; }
        private DateTime FechaInicio { get; set; }

        private Sesion() { }

        public static Sesion GetInstance
        {
            get
            {
                if (_sesion == null) throw new Exception("Sesión no iniciada.");
                return _sesion;
            }
        }

        public static void Login(IUser user)
        {
            try
            {
                if (_sesion == null)
                {
                    _sesion = new Sesion();
                    _sesion.User = user;
                    _sesion.FechaInicio = DateTime.Now;
                }
                else
                {
                    throw new Exception("Sesión ya iniciada.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Logout()
        {
            try
            {
                _sesion = null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ObtenerUsername()
        {
            try
            {
                return _sesion.User.Username;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
