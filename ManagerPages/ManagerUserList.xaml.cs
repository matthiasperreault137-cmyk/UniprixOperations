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
using UniprixOperations.UserManagement;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerUserList.xaml
    /// </summary>
    public partial class ManagerUserList : Page, IPageDef
    {
        private string searchText = "";
        private User? SelectedUser;
        private ManagerUserListData data;
        public ManagerUserList(ManagerUserListData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
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


        //Refresh
        public void Refresh()
        {
            load();
            MULUserDetails.Content = null;
        }



        //Loading List
        private void load()
        {
            MULUsers.Children.Clear();
            List<User> users = UserSearch.SortByName(MULSearch.Text);

            foreach (User user in users)
            {
                Button b = CreateUserButton(user);
                MULUsers.Children.Add(b);
            }
        }
        private Button CreateUserButton(User user)
        {
            Button b = new Button();
            b.Content = user.Name;
            b.Click += (s, e) => Select(user);
            b.Height = 90;
            b.Style = (Style) FindResource("ManagerUser");
            return b;

        }
        private void Select(User user)
        {
            if(SelectedUser == user)
            {
                SelectedUser = null;
                MULUserDetails.Content = null;
            }
            else
            {
                SelectedUser = user;
                MULUserDetails.Content = new UserDetails(user, this);
            }


        }


        //Searching

        private void SearchTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;


            searchText = MULSearch.Text;

            if (this.IsLoaded)
            {
                Refresh();
            }
        }
    }
}
