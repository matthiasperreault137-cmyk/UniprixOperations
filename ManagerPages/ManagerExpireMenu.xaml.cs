using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerExpireMenu.xaml
    /// </summary>
    public partial class ManagerExpireMenu : Page, IPageDef
    {
        private ManagerExpireMenuData data;
        public ManagerExpireMenu(ManagerExpireMenuData data)
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
        public void OpenSections(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenSections();
        }
        public void OpenAddExpiredProduct(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerAddExpiredProduct();
        }
        public void OpenAllProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerAllExpiredProducts();
        }
        public void OpenAddSection(object sender, RoutedEventArgs e)
        {
            Navigator.OpenAddSection();
        }
        public void OpenSectionList(object sender, RoutedEventArgs e)
        {
            Navigator.OpenSectionList();
        }


        public void Refresh()
        {

        }
    }
}
