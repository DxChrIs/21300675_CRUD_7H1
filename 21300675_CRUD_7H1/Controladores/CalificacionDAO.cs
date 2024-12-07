using _21300675_CRUD_7H1.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21300675_CRUD_7H1.Controladores
{
    internal class CalificacionDAO
    {
        private static string ConnectionString = "" +
            "Encrypt=False;" +
            "Integrated Security=SSPI;" +
            "Persist Security Info=False;" +
            "Initial Catalog=CRUD;" +
            "Data Source=LAPTOP-CHRIS\\SQLEXPRESS";

        public static List<Calificacion> GetItems()
        {
            List<Calificacion> Calificacion = new List<Calificacion>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Calificacion", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Calificacion calificacion = new Calificacion
                    {
                        IDCalificacion = reader.GetInt32(0),
                        CalificacionNombre = reader.GetString(1),
                        Asignatura = reader.GetInt32(2)
                    };
                    Calificacion.Add(calificacion);
                }
            }

            return Calificacion;
        }

        // Método para agregar un nuevo Calificacion
        public static void AddItem(Calificacion Calificacion)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Calificacion (Calificacion, Asignatura) VALUES (@Calificacion, @Asignatura)", connection);
                command.Parameters.AddWithValue("@Calificacion", Calificacion.CalificacionNombre);
                command.Parameters.AddWithValue("@Asignatura", Calificacion.Asignatura);
                command.ExecuteNonQuery();
            }
        }

        // Método para modificar un Calificacion
        public static void UpdateItem(Calificacion Calificacion)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Calificacion SET Calificacion = @Calificacion, Asignatura = @Asignatura WHERE IDCalificacion = @IDCalificacion", connection);
                command.Parameters.AddWithValue("@IDCalificacion", Calificacion.IDCalificacion);
                command.Parameters.AddWithValue("@Calificacion", Calificacion.CalificacionNombre);
                command.Parameters.AddWithValue("@Asignatura", Calificacion.Asignatura);
                command.ExecuteNonQuery();
            }
        }

        // Método para eliminar un Calificacion
        public static void DeleteItem(int idCalificacion)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Calificacion WHERE IDCalificacion = @IDCalificacion", connection);
                command.Parameters.AddWithValue("@IDCalificacion", idCalificacion);
                command.ExecuteNonQuery();
            }
        }
        public static Calificacion GetItemById(int id)
        {
            return Calificacion.FirstOrDefault(c => c.IDCalificacion == id);
        }
    }
}
