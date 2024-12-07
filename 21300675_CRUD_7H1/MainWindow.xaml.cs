using _21300675_CRUD_7H1.Controladores;
using _21300675_CRUD_7H1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace _21300675_CRUD_7H1
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadData();
			LoadCalificaciones();
		}

		private void LoadData()
		{
			List<Estudiante> estudiantes = EstudianteDAO.GetItems();
			List<Calificacion> Calificaciones = CalificacionDAO.GetItems();

			var EstudiantesConCalificacion = from Estudiante in estudiantes
								  join Calificacion in Calificaciones on Estudiante.Calificacion equals Calificacion.IDCalificacion
								  select new
								  {
									  Estudiante.Registro,
									  Estudiante.Nombre,
									  Estudiante.Apellido,
									  CalificacionNombre = Calificacion.CalificacionNombre
								  };

			dataGridEstudiantes.ItemsSource = EstudiantesConCalificacion.ToList();
		}

		private void btnVolver_ClickAsignatura(object sender, RoutedEventArgs e)
		{
			ProfesorWindow AsignaturaWindow = new ProfesorWindow();
			AsignaturaWindow.Show();
			this.Close();
		}

		private void LoadCalificaciones()
		{
			List<Calificacion> Calificaciones = CalificacionDAO.GetItems();
			cmbCalificacion.ItemsSource = Calificaciones;
			cmbCalificacion.DisplayMemberPath = "CalificacionNombre";
			cmbCalificacion.SelectedValuePath = "IDCalificacion";
		}

		private void btnVolver_ClickCalificacion(object sender, RoutedEventArgs e)
		{
			GrupoWindow CalificacionWindow = new GrupoWindow();
			CalificacionWindow.Show();
			this.Close();
		}

		private void btnAgregar_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateFields())
			{
				if (IsRegistroExists(int.Parse(txtRegistro.Text)))
				{
					MessageBox.Show("El Registro ingresado ya existe. Por favor ingrese un registro único.");
					return;
				}

				Estudiante nuevoEstudiante = new Estudiante
				{
					Registro = int.Parse(txtRegistro.Text),
					Nombre = txtNombre.Text,
					Apellido = txtApellido.Text,
					Calificacion = (int)cmbCalificacion.SelectedValue
				};

				EstudianteDAO.AddItem(nuevoEstudiante);
				LoadData();
				ClearFields();
			}
			else
			{
				MessageBox.Show("Por favor, ingrese datos válidos.");
			}
		}

		private bool ValidateFields()
		{
			if (!Regex.IsMatch(txtRegistro.Text, @"^\d+$"))
			{
				MessageBox.Show("El campo Registro solo puede contener números.");
				return false;
			}

			if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\s]+$"))
			{
				MessageBox.Show("El campo Nombre solo puede contener letras y espacios.");
				return false;
			}

			if (!Regex.IsMatch(txtApellido.Text, @"^[a-zA-Z\s]+$"))
			{
				MessageBox.Show("El campo Apellido solo puede contener letras y espacios.");
				return false;
			}

			return true;
		}

		private bool IsRegistroExists(int registro)
		{
			List<Estudiante> estudiantes = EstudianteDAO.GetItems();
			return estudiantes.Any(a => a.Registro == registro);
		}

		private void ClearFields()
		{
			txtRegistro.Clear();
			txtNombre.Clear();
			txtApellido.Clear();
			cmbCalificacion.SelectedIndex = -1;
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
            if (dataGridEstudiantes.SelectedItem != null)
            {
                dynamic seleccionado = dataGridEstudiantes.SelectedItem;
                int registro = seleccionado.Registro; // Obtén el registro desde el objeto anónimo

                Estudiante estudianteOriginal = EstudianteDAO.GetItems().FirstOrDefault(e => e.Registro == registro);

                if (estudianteOriginal != null)
                {
                    // Actualiza los datos del estudiante
                    estudianteOriginal.Nombre = txtNombre.Text;
                    estudianteOriginal.Apellido = txtApellido.Text;
                    estudianteOriginal.Calificacion = (int)cmbCalificacion.SelectedValue;

                    EstudianteDAO.UpdateItem(estudianteOriginal);
                    LoadData();
                    ClearFields();
                    btnAgregar.Content = "Agregar";
                    btnAgregar.Click -= btnActualizar_Click;
                    btnAgregar.Click += btnAgregar_Click;
                }
                else
                {
                    MessageBox.Show("No se encontró el estudiante seleccionado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un Estudiante para modificar.");
            }
        }

		private void btnModificar_Click(object sender, RoutedEventArgs e)
		{
            if (dataGridEstudiantes.SelectedItem != null)
            {
                dynamic seleccionado = dataGridEstudiantes.SelectedItem;
                int registro = seleccionado.Registro;

                Estudiante EstudianteSeleccionado = EstudianteDAO.GetItems().FirstOrDefault(e => e.Registro == registro);

                if (EstudianteSeleccionado != null)
                {
                    txtRegistro.Text = EstudianteSeleccionado.Registro.ToString();
                    txtNombre.Text = EstudianteSeleccionado.Nombre;
                    txtApellido.Text = EstudianteSeleccionado.Apellido;
                    cmbCalificacion.SelectedValue = EstudianteSeleccionado.Calificacion;

                    btnAgregar.Content = "Actualizar";
                    btnAgregar.Click -= btnAgregar_Click;
                    btnAgregar.Click += (s, e) => btnActualizar_Click(s, e);
                }
                else
                {
                    MessageBox.Show("No se encontró el estudiante seleccionado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un estudiante para modificar.");
            }
        }

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (dataGridEstudiantes.SelectedItem != null)
    {
        dynamic seleccionado = dataGridEstudiantes.SelectedItem;
        int registro = seleccionado.Registro;

        Estudiante estudianteOriginal = EstudianteDAO.GetItems().FirstOrDefault(e => e.Registro == registro);

        if (estudianteOriginal != null)
        {
            var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar al Estudiante {estudianteOriginal.Nombre} {estudianteOriginal.Apellido}?",
                "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                EstudianteDAO.DeleteItem(estudianteOriginal);
                LoadData();
            }
        }
    }
    else
    {
        MessageBox.Show("Por favor, selecciona un Estudiante para eliminar.");
    }
		}
	}
}
