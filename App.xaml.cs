using System.IO;
using System.Windows;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex = null;
        public App()
        {
            // Handle unhandled exceptions globally
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                DataManager.Instance.SaveData(); // Attempt to save data before exiting
            };
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "UniprixOperations";
            bool newSingleton;

            mutex = new Mutex(true, appName, out newSingleton);

            if (!newSingleton)
            {
                MessageBox.Show("L'application est déjà ouverte veuillez fermer celle-ci avant", "Déjà en cours d'utilisation",
                MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
                return;
            }


            base.OnStartup(e);


            string appData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "UniprixOperations"
            );

            string destFolder = Path.Combine(appData, "XmlData");
            Directory.CreateDirectory(destFolder); // safe to call always, no-op if exists

            string defaults = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XmlData");
            foreach (string file in Directory.GetFiles(defaults, "*.xml"))
            {
                string destination = Path.Combine(destFolder, Path.GetFileName(file));
                if (!File.Exists(destination))
                {
                    File.Copy(file, destination);
                }
            }

            // NOW always runs, every launch
            DataManager.Instance.LoadData();
            Console.WriteLine("StartUp done");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mutex?.ReleaseMutex();
            mutex?.Dispose();
            base.OnExit(e);
            DataManager.Instance.SaveData();
        }
    }


}
