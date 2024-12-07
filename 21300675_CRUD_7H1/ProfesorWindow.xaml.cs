using System.Collections.Generic;
using System.Windows;
using System.Linq;
using _21300675_CRUD_7H1.Controladores;
using _21300675_CRUD_7H1.Modelos;

namespace _21300675_CRUD_7H1
{
	public partial class ProfesorWindow : Window
	{
		public ProfesorWindow()
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			List<Asignatura> asignatura = AsignaturaDAO.GetItems();
			dataGridAsignaturas.ItemsSource = asignatura;
		}

		private void btnAgregarAsignatura_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateFields())
			{
				Asignatura nuevoAsignatura = new Asignatura
				{
					Nombre = txtNombreAsignatura.Text,
				};

				AsignaturaDAO.AddItem(nuevoAsignatura);
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
			if (string.IsNullOrWhiteSpace(txtNombreAsignatura.Text))
			{
				MessageBox.Show("Los campos Nombre son obligatorios.");
				return false;
			}

			return true;
		}

		private void ClearFields()
		{
			txtNombreAsignatura.Clear();
		}

		private void btnModificarAsignatura_Click(object sender, RoutedEventArgs e)
		{
			Asignatura AsignaturaSeleccionado = (Asignatura)dataGridAsignaturas.SelectedItem;
			if (AsignaturaSeleccionado != null)
			{
				txtNombreAsignatura.Text = AsignaturaSeleccionado.Nombre;

				btnAgregarAsignatura.Content = "Actualizar";
				btnAgregarAsignatura.Click -= btnAgregarAsignatura_Click;
				btnAgregarAsignatura.Click += (s, e) => btnActualizarAsignatura_Click(s, e, AsignaturaSeleccionado);
			}
			else
			{
				MessageBox.Show("Por favor, selecciona un Asignatura para modificar.");
			}
		}

		private void btnActualizarAsignatura_Click(object sender, RoutedEventArgs e, Asignatura AsignaturaSeleccionado)
		{
			if (ValidateFields())
			{
				AsignaturaSeleccionado.Nombre = txtNombreAsignatura.Text;

				AsignaturaDAO.UpdateItem(AsignaturaSeleccionado);
				LoadData();
				ClearFields();
				btnAgregarAsignatura.Content = "Agregar";
				btnAgregarAsignatura.Click -= (s, e) => btnActualizarAsignatura_Click(s, e, AsignaturaSeleccionado);
				btnAgregarAsignatura.Click += btnAgregarAsignatura_Click;
			}
			else
			{
				MessageBox.Show("Por favor, ingrese datos válidos.");
			}
		}

		private void btnEliminarAsignatura_Click(object sender, RoutedEventArgs e)
		{
			Asignatura AsignaturaSeleccionado = (Asignatura)dataGridAsignaturas.SelectedItem;
			if (AsignaturaSeleccionado != null)
			{
				MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este Asignatura?", "Confirmar eliminación", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					AsignaturaDAO.DeleteItem(AsignaturaSeleccionado.IDAsignatura);
					LoadData();
				}
			}
			else
			{
				MessageBox.Show("Por favor, selecciona un Asignatura para eliminar.");
			}
		}

		private void btnVolver_ClickEstudiante(object sender, RoutedEventArgs e)
		{
			MainWindow alumnoswindow = new MainWindow();
			alumnoswindow.Show();
			this.Close();
		}

		private void btnVolver_ClickCalificaciones(object sender, RoutedEventArgs e)
		{
			GrupoWindow grupoWindow = new GrupoWindow();
			grupoWindow.Show();
			this.Close();
		}

	}
}
