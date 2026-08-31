using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.EmployeePageData;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour EmployeeMainMenu.xaml
    /// </summary>
    public partial class EmployeeMainMenu : Page,IPageDef
    {
        private EmployeeMainMenuData data;
        public EmployeeMainMenu(EmployeeMainMenuData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeTasks();
        }
        private void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeMainMenu();
        }
        private void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeAddExpiredProduct();
        }
        public void Refresh() { }
    }
}
