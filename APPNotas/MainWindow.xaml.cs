using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace APPNotas
{
    public partial class MainWindow : Window
    {
        private readonly CalificacionesRepository repository;
        private Calificaciones? DatoSeleccionado;

        public List<Calificaciones> Calificacion { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            repository = new CalificacionesRepository();
            Calificacion = repository.ObtenerTodos();
            DataGrid.ItemsSource = Calificacion;
            DataGrid.Items.Refresh();
        }

        private void ActualizarDataGrid()
        {
            var calificaciones = repository.ObtenerTodos();
            DataGrid.ItemsSource = calificaciones;
            DataGrid.Items.Refresh();
        }

        public void Crear_AgregarCalificacion()
        {
            // Logica para crear y agregar una nueva calificacion
            string Asignatura = AsignaturaTextBox.Text;
            string Maestro = MaestroTextBox.Text;
            string Estudiante = EstudianteTextBox.Text;
            string Matricula = MatriculaTextBox.Text;
            int Asistencia = int.Parse(AsistenciaTextBox.Text);
            int Parcial = int.Parse(ParcialTextBox.Text);
            int Practicas = int.Parse(PracticasTextBox.Text);
            int ExamenFinal = int.Parse(ExamenFinalTextBox.Text);

            // Validar que se hayan Convertido los campos de Asistencia, Parcial, Practicas y Examen Final a enteros


            if (!string.IsNullOrEmpty(Asignatura) && !string.IsNullOrEmpty(Maestro) && !string.IsNullOrEmpty(Estudiante) && !string.IsNullOrEmpty(Matricula))
            {
                try
                {



                    Calificaciones calificacion = new()
                    {
                        Asignatura = Asignatura,
                        Maestro = Maestro,
                        Estudiante = Estudiante,
                        Matricula = Matricula,
                        Asistencia = Asistencia,
                        Parcial = Parcial,
                        Practicas = Practicas,
                        ExamenFinal = ExamenFinal
                    };


                    MessageBox.Show("Calificacion agregada correctamente");
                    repository.Agregar(calificacion);
                    Clear();
                    ActualizarDataGrid();

                }

                catch (Exception)
                {
                    MessageBox.Show("Favor de ingresar solo numeros en los campos de Asistencia, Parcial, Practicas y Examen Final");
                }
            }

            else
            {
                MessageBox.Show("Favor de llenar todos los campos");
            }

        }

        // Funcion para actualizar los datos de la calificacion seleccionada
        public void Actualizar()
        {
            if (DatoSeleccionado != null)
            {
                DatoSeleccionado.Asignatura = AsignaturaTextBox.Text;
                DatoSeleccionado.Maestro = MaestroTextBox.Text;
                DatoSeleccionado.Estudiante = EstudianteTextBox.Text;
                DatoSeleccionado.Matricula = MatriculaTextBox.Text;
                DatoSeleccionado.Asistencia = int.Parse(AsistenciaTextBox.Text);
                DatoSeleccionado.Parcial = int.Parse(ParcialTextBox.Text);
                DatoSeleccionado.Practicas = int.Parse(PracticasTextBox.Text);
                DatoSeleccionado.ExamenFinal = int.Parse(ExamenFinalTextBox.Text);
                try
                {
                    _ = int.Parse(AsistenciaTextBox.Text);
                    _ = int.Parse(ParcialTextBox.Text);
                    _ = int.Parse(PracticasTextBox.Text);
                    _ = int.Parse(ExamenFinalTextBox.Text);

                    repository.Actualizar(DatoSeleccionado);
                    Clear();
                    ActualizarDataGrid();

                    MessageBox.Show("Calificacion actualizada correctamente");

                }
                catch (Exception)
                {
                    MessageBox.Show("Favor de ingresar solo numeros en los campos de Asistencia, Parcial, Practicas y Examen Final");
                }
            }

            else
            {
                MessageBox.Show("Favor de seleccionar un registro");
            }
        }

        // Funcion para eliminar una calificacion
        public void Eliminar()
        {
            if (DatoSeleccionado != null)
            {
                try
                {
                    MessageBoxResult Confirmacion = MessageBox.Show("¿Está seguro que desea eliminar la calificacion?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Confirmacion == MessageBoxResult.Yes)
                    {
                        // Eliminar la calificacion de la base de datos
                        repository.Eliminar(DatoSeleccionado);

                        MessageBox.Show("Calificacion eliminada de forma exitosa");
                        Clear();
                        ActualizarDataGrid();

                    }
                    if (Confirmacion == MessageBoxResult.No)
                    {
                        // Cancelar la eliminacion de la calificacion
                        Clear();
                        ActualizarDataGrid();

                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrio un error al intentar eliminar el registro");

                }
            }
        }

        // Funcion para limpiar los campos de texto
        public void Clear()
        {
            AsignaturaTextBox.Text = string.Empty;
            MaestroTextBox.Text = string.Empty;
            EstudianteTextBox.Text = string.Empty;
            MatriculaTextBox.Text = string.Empty;
            AsistenciaTextBox.Text = string.Empty;
            ParcialTextBox.Text = string.Empty;
            PracticasTextBox.Text = string.Empty;
            ExamenFinalTextBox.Text = string.Empty;
        }

        // Funciones de eventos
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Crear_AgregarCalificacion();

        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Actualizar();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }


        // Funciones del DataGrid
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatoSeleccionado = DataGrid.SelectedItem as Calificaciones;

            if (DatoSeleccionado != null)
            {
                AsignaturaTextBox.Text = DatoSeleccionado.Asignatura;
                MaestroTextBox.Text = DatoSeleccionado.Maestro;
                EstudianteTextBox.Text = DatoSeleccionado.Estudiante;
                MatriculaTextBox.Text = DatoSeleccionado.Matricula;
                AsistenciaTextBox.Text = DatoSeleccionado.Asistencia.ToString();
                ParcialTextBox.Text = DatoSeleccionado.Parcial.ToString();
                PracticasTextBox.Text = DatoSeleccionado.Practicas.ToString();
                ExamenFinalTextBox.Text = DatoSeleccionado.ExamenFinal.ToString();
            }
        }



        // Funciones extras (buscar y generar PDF de reporte de calificaciones)

        public void Buscar()
        {
            string Busqueda = BusquedaTextBox.Text;

            if (!string.IsNullOrEmpty(Busqueda))
            {
                var resultado = repository.BuscarPorMatricula(Busqueda);

                if (resultado.Count > 0)
                {
                    DataGrid.ItemsSource = resultado;
                }

                else
                {
                    MessageBox.Show("No se encontraron resultados");
                }
            }

            else
            {
                MessageBox.Show("Favor de ingresar un criterio de busqueda");
            }
        }


        // Crea PDF
        private async void BtnGeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            // Obtener datos
            var calificaciones = repository.ObtenerTodos();

            // Generar PDF 
            await PDF.GuardarPDFAsync(calificaciones);

            // Mensaje
            string rutaArchivo = Path.Combine(Environment.CurrentDirectory, "Reporte.pdf");

            // Mostrar cuadro de diálogo con opciones
            MessageBoxResult result = MessageBox.Show("¿Desea copiar la ruta del archivo?", "Opciónes", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Ejecutar acción según opción seleccionada
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // Copiar en porta_papeles
                    Clipboard.Clear();
                    Clipboard.SetText(rutaArchivo);
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("El archivo PDF ha sido guardado con éxito en el directorio actual. Puedes abrirlo en cualquier momento desde aquí: " + rutaArchivo, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }
    }
}