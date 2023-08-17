using Microsoft.EntityFrameworkCore.Infrastructure;
using QuestPDF.Infrastructure;
using System.Windows;


namespace APPNotas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Metodo para crear la base de datos al iniciar la aplicación
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade facade = new NotasContext().Database;
            facade.EnsureCreated();
            // Licencia para la generación QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // Metodo para el MessageBoxResult al salir de la app
        protected override void OnExit(ExitEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Shutdown();
            }
            if (result == MessageBoxResult.No)
            {
                // Cancelar el cierre de la aplicación
                e.ApplicationExitCode = 1;

            }
        }

    }
}
