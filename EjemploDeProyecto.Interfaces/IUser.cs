using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.Interfaces
{
    public interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}
