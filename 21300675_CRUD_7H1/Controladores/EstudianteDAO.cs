using _21300675_CRUD_7H1.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21300675_CRUD_7H1.Controladores
{
    internal class EstudianteDAO
    {
        private static string ConnectionString = "" +
            "Encrypt=False;" +
            "Integrated Security=SSPI;" +
            "Persist Security Info=False;" +
            "Initial Catalog=CRUD;" +
            "Data Source=LAPTOP-CHRIS\\SQLEXPRESS";

        public static void AddItem(Estudiante Estudiante)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand sqlCommand = new SqlCommand(
                    "INSERT INTO Estudiante (Registro, Nombre, Apellido, Calificacion) " +
                    "VALUES (@Registro, @Nombre, @Apellido, @Calificacion)", con);

                sqlCommand.Parameters.AddWithValue("@Registro", Estudiante.Registro);
                sqlCommand.Parameters.AddWithValue("@Nombre", Estudiante.Nombre);
                sqlCommand.Parameters.AddWithValue("@Apellido", Estudiante.Apellido);
                sqlCommand.Parameters.AddWithValue("@Calificacion", Estudiante.Calificacion);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public static List<Estudiante> GetItems()
        {
            List<Estudiante> list = new List<Estudiante>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand select = con.CreateCommand();
                select.CommandText = "SELECT * FROM Estudiante";
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    Estudiante Estudiante = new Estudiante
                    {
                        Registro = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Calificacion = reader.GetInt32(3)
                    };
                    list.Add(Estudiante);
                }
            }

            return list;
        }
        public static void UpdateItem(Estudiante Estudiante)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand sqlCommand = new SqlCommand(
                    "UPDATE Estudiante SET " +
                    "Nombre = @Nombre, " +
                    "Apellido = @Apellido, " +
                    "Calificacion = @Calificacion " +
                    "WHERE Registro = @Registro", con);

                sqlCommand.Parameters.AddWithValue("@Nombre", Estudiante.Nombre);
                sqlCommand.Parameters.AddWithValue("@Apellido", Estudiante.Apellido);
                sqlCommand.Parameters.AddWithValue("@Calificacion", Estudiante.Calificacion);
                sqlCommand.Parameters.AddWithValue("@Registro", Estudiante.Registro);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteItem(Estudiante Estudiante)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand sqlCommand = new SqlCommand(
                    "DELETE FROM Estudiante WHERE Registro = @Registro", con);

                sqlCommand.Parameters.AddWithValue("@Registro", Estudiante.Registro);

                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
