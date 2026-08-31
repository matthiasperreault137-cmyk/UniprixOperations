using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.EmployeePageData;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour EmployeeTasks.xaml
    /// </summary>
    public partial class EmployeeTasks : Page, IPageDef
    {
        private EmployeeTasksData data;
        public EmployeeTasks(EmployeeTasksData data)
        {
            InitializeComponent();
            this.data = data;
        }
        private void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeMainMenu();
        }

        private void OpenRecurringTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeRecurringTasks();
        }
        private void OpenDailyTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeDailyTasks();
        }
        private void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeAddExpiredProduct();
        }

        private void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeTasks();
        }
        public void Refresh() { }
    }
}
