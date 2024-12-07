using _21300675_CRUD_7H1.Controladores;
using _21300675_CRUD_7H1.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace _21300675_CRUD_7H1
{
	public partial class GrupoWindow : Window
	{
		public GrupoWindow()
		{
			InitializeComponent();
			LoadData();
			LoadTutors();
		}

		private void LoadData()
		{
			List<Calificacion> Calificacions = CalificacionDAO.GetItems();
			List<Asignatura> Asignaturaes = AsignaturaDAO.GetItems();

			var CalificacionsConNombres = from Calificacion in Calificacions
								   join Asignatura in Asignaturaes on Calificacion.Asignatura equals Asignatura.IDAsignatura
								   select new
								   {
									   Calificacion.IDCalificacion,
									   Calificacion.CalificacionNombre,
									   TutorNombre = Asignatura.Nombre
								   };

			dataGridCalificaciones.ItemsSource = CalificacionsConNombres.ToList();
		}

		private void LoadTutors()
		{
			List<Asignatura> Asignatura = AsignaturaDAO.GetItems();
			cmbTutor.ItemsSource = Asignatura;
			cmbTutor.DisplayMemberPath = "Nombre";
			cmbTutor.SelectedValuePath = "IDAsignatura";
		}

		private void btnAgregarCalificacion_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateFields())
			{
				Calificacion nuevoCalificacion = new Calificacion
				{
					CalificacionNombre = txtCalificacion.Text,
					Asignatura = (int)cmbTutor.SelectedValue
				};

				CalificacionDAO.AddItem(nuevoCalificacion);
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
			if (cmbTutor.SelectedValue == null)
			{
				MessageBox.Show("Por favor, seleccione un tutor.");
				return false;
			}

			return true;
		}

		private void ClearFields()
		{
			txtCalificacion.Clear();
			cmbTutor.SelectedIndex = -1;
		}

		private void btnModificarCalificacion_Click(object sender, RoutedEventArgs e)
		{
            var itemSeleccionado = dataGridCalificaciones.SelectedItem;
            if (itemSeleccionado != null)
            {
                // Recuperar el ID de la calificación seleccionada
                int idCalificacion = (int)itemSeleccionado.GetType().GetProperty("IDCalificacion").GetValue(itemSeleccionado);

                // Buscar el objeto original en la base de datos
                Calificacion CalificacionSeleccionado = CalificacionDAO.GetItemById(idCalificacion);

                if (CalificacionSeleccionado != null && ValidateFields())
                {
                    CalificacionSeleccionado.CalificacionNombre = txtCalificacion.Text;
                    CalificacionSeleccionado.Asignatura = (int)cmbTutor.SelectedValue;

                    CalificacionDAO.UpdateItem(CalificacionSeleccionado);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese datos válidos.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una calificación para modificar.");
            }
        }

		private void btnEliminarCalificacion_Click(object sender, RoutedEventArgs e)
		{
			Calificacion CalificacionSeleccionado = (Calificacion)dataGridCalificaciones.SelectedItem;
			if (CalificacionSeleccionado != null)
			{
				MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este Calificacion?", "Confirmar eliminación", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					CalificacionDAO.DeleteItem(CalificacionSeleccionado.IDCalificacion);
					LoadData();
				}
			}
			else
			{
				MessageBox.Show("Seleccione un Calificacion para eliminar.");
			}
		}

		private void btnVolver_ClickEstudiantes(object sender, RoutedEventArgs e)
		{
			MainWindow Estudianteswindow = new MainWindow();
			Estudianteswindow.Show();
			this.Close();
		}

		private void btnVolver_ClickAsignaturas(object sender, RoutedEventArgs e)
		{
			ProfesorWindow AsignaturaWindow = new ProfesorWindow();
			AsignaturaWindow.Show();
			this.Close();
		}
	}
}
