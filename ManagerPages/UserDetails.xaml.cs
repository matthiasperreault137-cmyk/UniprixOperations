using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UniprixOperations.ManagerPages;
using UniprixOperations.XmlDataManager;
using UniprixOperations.UserManagement;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Page
    {
        private bool isEditing = false;
        private ManagerUserList parent;
        private User user;
        public UserDetails(User user, ManagerUserList parent)
        {
            InitializeComponent();
            this.user = user; 
            this.parent = parent;
            Refresh();
        }


        //Refresh

        public void Refresh()
        {
            Load();
            
        }

        //Loading
        private void Load()
        {
            UDName.Text = user.Name;
            if(user is Employee)
            {
                UDEmployeeCheck.IsChecked = true;
            }
            else
            {
                if (user is Manager)
                {
                    Manager m = (Manager)user;
                    UDManagerCheck.IsChecked = true;
                    UDPassword.Text = m.PassWord;
                }
                else
                {
                    Supervisor s = (Supervisor)user;
                    UDSupervisorCheck.IsChecked = true;
                    UDPassword.Text = s.PassWord;
                }
                UDPassword.IsReadOnly = true;                                   //??????????????????? pourquoi il veut pas rester en readonly
            }
        }



        //Check Event handlers


        private void UDEmployeeCheck_Checked(object sender, RoutedEventArgs e)
        {
            UDManagerCheck.IsChecked = false;
            UDSupervisorCheck.IsChecked = false;
            UDPassword.Text = "";
            UDPassword.IsReadOnly = true;
        }

        private void UDSupervisorCheck_Checked(object sender, RoutedEventArgs e)
        {
            UDEmployeeCheck.IsChecked = false;
            UDPassword.IsReadOnly = false;
            UDManagerCheck.IsChecked = false;
        }
        private void UDManagerCheck_Checked(object sender, RoutedEventArgs e)
        {
            UDEmployeeCheck.IsChecked = false;
            UDPassword.IsReadOnly = false;
            UDSupervisorCheck.IsChecked = false;
        }

        //Modify

        private void ModifyUser(Object sender, RoutedEventArgs e)
        {
            if(UserStore.CurrentUser == user)
            {
                MessageBox.Show("Impossible de modifier votre propre utilisateur \n Cannot modify your own user");
                return;
            }
            else
            {
                if (isEditing)
                {
                    isEditing = false;
                    UDModify.Content = "Modifier / Modify";
                    LockFields();
                }
                else
                {
                    isEditing = true;
                    UDModify.Content = "Enregistrer/Save";
                    UnlockFields();
                }
            }
        }


        private void LockFields()
        {
            UDName.IsReadOnly = true;
            UDEmployeeCheck.IsEnabled = false;
            UDManagerCheck.IsEnabled = false;
            UDSupervisorCheck.IsEnabled = false;
            UDPassword.IsReadOnly = true;


            string name;
            if (UDName.Text == "" || UDName.Text == null)
            {
                MessageBox.Show("Veuillez enter un nom SVP \n Please enter a valid name");
                return;
            }
            else
            {
                name = UDName.Text;
            }
            if (UDEmployeeCheck.IsChecked == false && UDManagerCheck.IsChecked == false && UDSupervisorCheck.IsChecked == false)
            {
                MessageBox.Show("Veuillez choisir un type d'utilisateur SVP \n Please choose a user type");
                return;
            }
            else if (UDManagerCheck.IsChecked == false && UDSupervisorCheck.IsChecked == false)
            {
                Employee newemployee = new Employee(name);
                DataManager.Instance.Users.UserList.Add(newemployee);
                DataManager.Instance.Users.UserList.Remove(user);
                parent.Refresh();
                return;
            }
            else
            {
                string password;
                if (UDPassword.Text == "" || UDPassword.Text == null)
                {
                    MessageBox.Show("Veuillez entrer un mot de passe SVP \n Please enter a password");
                    return;
                }
                else
                {
                    password = UDPassword.Text;
                }
                if (UDManagerCheck.IsChecked == false)
                {
                    Supervisor newsupervisor = new Supervisor(name, password);
                    DataManager.Instance.Users.UserList.Add(newsupervisor);
                    DataManager.Instance.Users.UserList.Remove(user);
                    parent.Refresh();
                    return;
                }
                else
                {
                    Manager newmanager = new Manager(name, password);
                    DataManager.Instance.Users.UserList.Add(newmanager);
                    DataManager.Instance.Users.UserList.Remove(user);
                    parent.Refresh();
                    return;
                }
            }
        }


        private void UnlockFields()
        {
            UDName.IsReadOnly = false;
            UDPassword.IsReadOnly = false;
            UDEmployeeCheck.IsEnabled = true;
            UDManagerCheck.IsEnabled = true;
            UDSupervisorCheck.IsEnabled = true;
        }


        //Delete

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if(user == UserStore.CurrentUser)
            {
                MessageBox.Show("Impossible de supprimer votre propre utilisateur \n Cannot delete your own user");
                return;
            }
            else
            {
                MessageBox.Show("Utilisateur supprimé");
                DataManager.Instance.Users.UserList.Remove(user);
                parent.Refresh();
            }
        }
    }
}
