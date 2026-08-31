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
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerAddUser.xaml
    /// </summary>
    public partial class ManagerAddUser : Page, IPageDef
    {
        private ManagerAddUserData data;
        public ManagerAddUser(ManagerAddUserData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }


        //Navigation

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
            MAUName.Text = "";
            MAUEmployeeCheck.IsChecked = false;
            MAUManagerCheck.IsChecked = false;
            MAUPassword.Text = "";
            MAUPassword.IsReadOnly = true;
        }


        //Check Event handlers


        private void MAUEmployeeCheck_Checked(object sender, RoutedEventArgs e)
        {
            MAUManagerCheck.IsChecked = false;
            MAUSupervisorCheck.IsChecked = false;
            MAUPassword.Text = "";
            MAUPassword.IsReadOnly = true;
        }

        private void MAUSupervisorCheck_Checked(object sender, RoutedEventArgs e)
        {
            MAUEmployeeCheck.IsChecked = false;
            MAUPassword.IsReadOnly = false;
            MAUManagerCheck.IsChecked = false;
        }
        private void MAUManagerCheck_Checked(object sender, RoutedEventArgs e)
        {
            MAUEmployeeCheck.IsChecked = false;
            MAUPassword.IsReadOnly = false;
            MAUSupervisorCheck.IsChecked = false;
        }


        //Adding new user

        private void AddUser(object sender, RoutedEventArgs e)
        {
            string name;
            if(MAUName.Text == "" || MAUName.Text == null)
            {
                MessageBox.Show("Veuillez enter un nom SVP \n Please enter a valid name");
                return;
            }
            else
            {
                name = MAUName.Text;
            }
            if(MAUEmployeeCheck.IsChecked == false && MAUManagerCheck.IsChecked == false && MAUSupervisorCheck.IsChecked == false)
            {
                MessageBox.Show("Veuillez choisir un type d'utilisateur SVP \n Please choose a user type");
                return;
            }
            else if(MAUManagerCheck.IsChecked == false && MAUSupervisorCheck.IsChecked == false)
            {
                Employee newemployee = new Employee(name);
                DataManager.Instance.Users.UserList.Add(newemployee);
                MessageBox.Show("Nouveau compte employé ajouté avec succès \n New employee account added successfully");
                Refresh();
                return;
            }
            else
            {
                string password;
                if(MAUPassword.Text == "" || MAUPassword.Text == null)
                {
                    MessageBox.Show("Veuillez entrer un mot de passe SVP \n Please enter a password");
                    return;
                }
                else
                {
                    password = MAUPassword.Text;
                }
                if(MAUManagerCheck.IsChecked == false)
                {
                    Supervisor newsupervisor = new Supervisor(name, password);
                    DataManager.Instance.Users.UserList.Add(newsupervisor);
                    MessageBox.Show("Nouveau compte Responsable ajouté avec succès \nNew Supervisor account added successfully");
                    Refresh();
                    return;
                }
                else
                {
                    Manager newmanager = new Manager(name, password);
                    DataManager.Instance.Users.UserList.Add(newmanager);
                    MessageBox.Show("Nouveau compte Gérant ajouté avec succès \nNew Manager account added successfully");
                    Refresh();
                    return;
                }
            }
        }



    }
}
