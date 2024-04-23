using EjemploDeProyecto.BE;
using EjemploDeProyecto.Interfaces;
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
                command.Parameters.AddWithValue("Tipo", bitacora.Tipo);
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

        public IBitacorasFiltered GetAllFiltered(int page, int perPage, IBitacoraFilters filters)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IS"].ConnectionString);
            try
            {
                var query = "Bitacora_GetAllFiltered";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("total", true);
                command.Parameters.AddWithValue("page", page);
                command.Parameters.AddWithValue("perPage", perPage);
                command.Parameters.AddWithValue("Usuario", filters.Usuario == null ? (object)DBNull.Value : filters.Usuario.Trim());
                command.Parameters.AddWithValue("Desde", filters.Desde.ToString() == "" ? (object)DBNull.Value : filters.Desde);
                command.Parameters.AddWithValue("Hasta", filters.Hasta.ToString() == "" ? (object)DBNull.Value : filters.Hasta);
                command.Parameters.AddWithValue("Tipo", filters.Tipo.ToString() == null ? (object)DBNull.Value : filters.Tipo);
                command.Parameters.AddWithValue("Like", filters.Like == null ? (object)DBNull.Value : filters.Like.Trim());
                command.CommandText = query;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                BitacorasFiltered bitacorasFiltered = new BitacorasFiltered();

                while (reader.Read())
                {
                    bitacorasFiltered.Page = Convert.ToInt32(reader["page"].ToString());
                    bitacorasFiltered.PerPage = Convert.ToInt32(reader["perPage"].ToString());
                    bitacorasFiltered.Total = Convert.ToInt32(reader["total"].ToString());
                    bitacorasFiltered.TotalPages = Convert.ToInt32(reader["totalPages"].ToString());
                }
                reader.Close();

                SqlCommand commandFilter = new SqlCommand();
                commandFilter.Connection = connection;
                commandFilter.Parameters.AddWithValue("total", false);
                commandFilter.Parameters.AddWithValue("page", page);
                commandFilter.Parameters.AddWithValue("perPage", perPage);
                commandFilter.Parameters.AddWithValue("Usuario", filters.Usuario == null ? (object)DBNull.Value : filters.Usuario.Trim());
                commandFilter.Parameters.AddWithValue("Desde", filters.Desde.ToString() == "" ? (object)DBNull.Value : filters.Desde);
                commandFilter.Parameters.AddWithValue("Hasta", filters.Hasta.ToString() == "" ? (object)DBNull.Value : filters.Hasta);
                commandFilter.Parameters.AddWithValue("Tipo", filters.Tipo.ToString() == null ? (object)DBNull.Value : filters.Tipo);
                commandFilter.Parameters.AddWithValue("Like", filters.Like == null ? (object)DBNull.Value : filters.Like.Trim());
                commandFilter.CommandText = query;
                commandFilter.CommandType = System.Data.CommandType.StoredProcedure;

                var readerBitacora = commandFilter.ExecuteReader();

                while (readerBitacora.Read())
                {
                    BitacoraFiltered bitacoraFiltered = new BitacoraFiltered();
                    bitacoraFiltered.Index = Convert.ToInt32(readerBitacora["Index"].ToString());
                    bitacoraFiltered.Id = Convert.ToInt32(readerBitacora["Id"].ToString());
                    bitacoraFiltered.Usuario = readerBitacora["Usuario"].ToString();
                    bitacoraFiltered.Fecha = Convert.ToDateTime(readerBitacora["Fecha"].ToString());
                    bitacoraFiltered.Tipo = (BitacoraTipo)Enum.ToObject(typeof(BitacoraTipo), Convert.ToInt32(readerBitacora["Tipo"].ToString()));
                    bitacoraFiltered.Mensaje = readerBitacora["Mensaje"].ToString();
                    bitacorasFiltered.Bitacoras.Add(bitacoraFiltered);
                }
                readerBitacora.Close();

                return bitacorasFiltered;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public IList<IBitacoraFilterd> GetAll(IBitacoraFilters filters)
        //{
        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IS"].ConnectionString);
        //    try
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = connection;
        //        command.Parameters.AddWithValue("Usuario", filters.Usuario == null ? (object)DBNull.Value : filters.Usuario);
        //        command.Parameters.AddWithValue("Desde", filters.Desde.ToString() == "" ? (object)DBNull.Value : filters.Desde);
        //        command.Parameters.AddWithValue("Hasta", filters.Hasta.ToString() == "" ? (object)DBNull.Value : filters.Hasta);
        //        command.Parameters.AddWithValue("Tipo", filters.Tipo.ToString() == null ? (object)DBNull.Value : filters.Tipo);
        //        command.Parameters.AddWithValue("Like", filters.Like == null ? (object)DBNull.Value : filters.Like);
        //        command.CommandText = "Bitacora_GetAll";
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        command.ExecuteReader();

        //        var reader = command.ExecuteReader();
        //        IList<IBitacora> listBitacora = new List<IBitacora>();

        //        while (reader.Read())
        //        {
        //            IBitacora bitacora = new Bitacora();
        //            bitacora.Id = Convert.ToInt32(reader["Id"].ToString());
        //            bitacora.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
        //            bitacora.Tipo = (BitacoraTipo)Enum.ToObject(typeof(BitacoraTipo), Convert.ToInt32(reader["Tipo"].ToString()));
        //            bitacora.Usuario = reader["Usuario"].ToString();
        //            bitacora.Mensaje = reader["Mensaje"].ToString();

        //            listBitacora.Add(bitacora);
        //        }
        //        reader.Close();

        //        return listBitacora;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}
    }
}
