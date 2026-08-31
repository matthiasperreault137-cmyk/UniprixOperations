using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.UserManagement;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerMainMenu.xaml
    /// </summary>
    public partial class ManagerMainMenu : Page, IPageDef
    {
        private ManagerMainMenuData data;
        public ManagerMainMenu(ManagerMainMenuData data)
        {
            InitializeComponent();
            this.data = data;
        }

        //Side Navigation Bar Click Functions
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
            if(UserStore.CurrentUser is Manager)
            {
                Navigator.OpenUserMenu();
            }
            else
            {
                MessageBox.Show("Seul un gérant peut accéder à ceci\nOnly Managers can access this");
                return;
            }
        }
        public void OpenMessages(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerMessages();
        }

        public void Refresh()
        {

        }
    }
}
