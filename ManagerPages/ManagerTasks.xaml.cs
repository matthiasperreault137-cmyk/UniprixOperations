using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerTasks.xaml
    /// </summary>
    public partial class ManagerTasks : Page, IPageDef
    {
        private ManagerTasksData data;
        public ManagerTasks(ManagerTasksData data)
        {
            InitializeComponent();
            this.data = data;
        }
        //Side Bar Navigation Functions
        public void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerMainMenu();
        }
        public void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerTasks();
        }
        public void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerExpireMenu();
        }
        public void OpenTaskHistory(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenTaskHistory();
        }
        public void OpenUserMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenUserMenu();
        }
        public void OpenDailyTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerDailyTasks();
        }
        public void OpenAddDailyTask(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenAddDailyTask();
        }
        public void OpenRecurringTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerRecurringTasks();
        }
        public void OpenAddRecurringTask(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenAddRecurringTask();
        }


        public void Refresh()
        {

        }
    }
}
