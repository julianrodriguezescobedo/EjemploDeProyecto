using System;

namespace EjemploDeProyecto.Interfaces
{
    public interface IBitacora : IEntity
    {
        DateTime Fecha {  get; set; }
        BitacoraTipo Tipo { get; set; }
        string Usuario { get; set; }
        string Mensaje { get; set; }
    }

    public enum BitacoraTipo
    {
        INFO = 1,
        ERROR = 2,
        ALL = 3
    }
}