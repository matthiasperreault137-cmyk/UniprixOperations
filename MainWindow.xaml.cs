using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UniprixOperations.HelperClasses;
using UniprixOperations.UserManagement;
using System.IO;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Threading.Timer autoSaveTimer;

        public MainWindow()
        {
            InitializeComponent();
            StartSaveTimer();
            MainFrame.Navigated += (s, e) => MainFrame.NavigationService.RemoveBackEntry();
            MainFrame.Navigate(new AccountSelection());
            Navigator.Initialize(MainFrame);
        }
        public void openAccountSelection(Object sender, RoutedEventArgs e)
        {
            UserStore.CurrentUser = null;
            MainFrame.Navigate(new AccountSelection());
        }

        private void RefreshMouseEnter(object sender, MouseEventArgs e)
        {
            ((Image) sender).Opacity = 0.7;
        }

        private void RefreshMouseLeave(object sender, MouseEventArgs e)
        {
            ((Image) sender).Opacity = 1.0;
        }

        private void RefreshPage(Object sender, MouseButtonEventArgs e)
        {
            ((IPageDef)MainFrame.Content).Refresh();
        }



        private void StartSaveTimer()
        {
            TimeSpan interval = TimeSpan.FromHours(1);
            autoSaveTimer = new System.Threading.Timer(_ => AutoSave(), null, TimeSpan.FromMinutes(1), interval);
        }

        private void AutoSave()
        {
            DataManager.Instance.SaveData();
            string appData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "UniprixOperations"
            );

            string xmlData = Path.Combine(appData, "XmlData");
            Directory.CreateDirectory(xmlData);

            string backupFolder = Path.Combine(appData, "Backups"); 
            Directory.CreateDirectory(backupFolder);


            CleanBackups(backupFolder);


            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
            string destFolder = Path.Combine(backupFolder, timeStamp);
            Directory.CreateDirectory(destFolder);

            foreach (string file in Directory.GetFiles(xmlData, "*.xml"))
            {
                string dest = Path.Combine(destFolder, Path.GetFileName(file));
                File.Copy(file, dest, overwrite: true);
            }
        }

        private void CleanBackups(string Backups, int keepDays = 7)
        {
            foreach(string dir in Directory.GetDirectories(Backups))
            {
                if(Directory.GetCreationTime(dir) < DateTime.Now.AddDays(-keepDays))
                {
                    Directory.Delete(dir, recursive: true);
                }
            }

        }

        private void OpenEmployeeMessages(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeMessages();
        }
    }
}