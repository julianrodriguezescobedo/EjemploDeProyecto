using EjemploDeProyecto.BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploDeProyecto.DAL
{
    public class PersonaDAL
    {
        public void Add(Persona persona)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IS"].ConnectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("Nombre", persona.Nombre);
                command.CommandText = "INSERT into Persona (Nombre) values (@Nombre)";
                command.CommandType = System.Data.CommandType.Text;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
