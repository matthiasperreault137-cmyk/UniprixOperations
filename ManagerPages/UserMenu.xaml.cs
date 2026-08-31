using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Page, IPageDef
    {
        private UserMenuData data;
        public UserMenu(UserMenuData data)
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
        public void OpenSections(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenSections();
        }
        public void OpenUserMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenUserMenu();
        }

        public void OpenAddUser(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerAddUser();
        }
        public void OpenUsers(object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerUserList();
        }



        public void Refresh()
        {
            //No dynamic data to update on this page
        }
    }
}
