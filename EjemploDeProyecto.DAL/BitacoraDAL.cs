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
    public class BitacoraDAL
    {
        public void Add(Bitacora bitacora)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IS"].ConnectionString);
            try
            {
                connection.Open();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("Tipo", bitacora.Tipo.ToString());
                command.Parameters.AddWithValue("Usuario", bitacora.Usuario);
                command.Parameters.AddWithValue("Mensaje", bitacora.Mensaje);
                command.CommandText = "Bitacora_Add";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
