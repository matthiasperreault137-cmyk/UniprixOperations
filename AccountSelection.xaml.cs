using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour AccountSelection.xaml
    /// </summary>
    public partial class AccountSelection : Page, IPageDef
    {
        public AccountSelection()
        {
            InitializeComponent();
        }
        private void OpenEmployeeLogin(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeLogin();
        }
        private void OpenManagerLogin(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerLogin();
        }

        public void Refresh()  
        {

        }



        private void ImportData(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.ImportData();
        }

        private void ExportData(object sender, RoutedEventArgs e)
        {
            DataManager.Instance.ExportData();
        }
    }
}
