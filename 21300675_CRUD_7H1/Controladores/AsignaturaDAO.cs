using _21300675_CRUD_7H1.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21300675_CRUD_7H1.Controladores
{
    internal class AsignaturaDAO
    {
        private static string ConnectionString = "" +
            "Encrypt=False;" +
            "Integrated Security=SSPI;" +
            "Persist Security Info=False;" +
            "Initial Catalog=CRUD;" +
            "Data Source=LAPTOP-CHRIS\\SQLEXPRESS";

        public static List<Asignatura> GetItems()
        {
            List<Asignatura> Asignaturaes = new List<Asignatura>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Asignatura", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Asignatura Asignatura = new Asignatura
                    {
                        IDAsignatura = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                    };
                    Asignaturaes.Add(Asignatura);
                }
            }

            return Asignaturaes;
        }

        // Agregar un nuevo Asignatura
        public static void AddItem(Asignatura Asignatura)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Asignatura (Nombre) VALUES (@Nombre)", connection);
                command.Parameters.AddWithValue("@Nombre", Asignatura.Nombre);
                command.ExecuteNonQuery();
            }
        }

        // Modificar un Asignatura existente
        public static void UpdateItem(Asignatura Asignatura)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Asignatura SET Nombre = @Nombre WHERE IDAsignatura = @IDAsignatura", connection);
                command.Parameters.AddWithValue("@IDAsignatura", Asignatura.IDAsignatura);
                command.Parameters.AddWithValue("@Nombre", Asignatura.Nombre);
                command.ExecuteNonQuery();
            }
        }

        // Eliminar un Asignatura
        public static void DeleteItem(int idAsignatura)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Asignatura WHERE IDAsignatura = @IDAsignatura", connection);
                command.Parameters.AddWithValue("@IDAsignatura", idAsignatura);
                command.ExecuteNonQuery();
            }
        }
    }
}
